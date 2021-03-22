using System;
using System.Collections.Generic;
using Entities.Models;
using Entities;
using Contracts;
using Microsoft.Extensions.Options;
using System.Linq;
using AuthService.Interfaces;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AuthService
{ 
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JsonSerializerOptions _serializerSettings;

        public AuthService(UserManager<AppUser> userManager, IOptions<AppSettings> appSettings, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _serializerSettings = new JsonSerializerOptions {
                WriteIndented = true
            };
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest credentials, string ipAddress)
        {
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null) {
                return null;
            }

            // Serialize and return the response
            var refreshToken = _jwtFactory.GenerateRefreshToken(ipAddress).Result;
            var user = _userManager.Users.SingleOrDefault(x => x.UserName == credentials.UserName);
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            var response = new {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                authToken = await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity),
                expiresIn = (int)_jwtOptions.ValidFor.TotalSeconds,
                refreshToken = refreshToken.Token
            };
            var json = JsonSerializer.Serialize(response, _serializerSettings);

            return new AuthenticateResponse(user, await _jwtFactory.GenerateEncodedToken(credentials.UserName, identity), refreshToken.Token);
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = _jwtFactory.GenerateRefreshToken(ipAddress).Result;
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            // generate new jwt
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = claims.Select(x => x.Value).ToList();
            var identity = _jwtFactory.GenerateClaimsIdentity(user.UserName, user.Id, userRoles);
            var jwtToken = _jwtFactory.GenerateEncodedToken(user.UserName, identity).Result;

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _userManager.UpdateAsync(user);
            return true;
        }

        /*public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }*/

        // helper methods

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password)) {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null) {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password)) {
                        var claims = await _userManager.GetClaimsAsync(userToVerify);
                        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id, claims.Select(x => x.Value).ToList()));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
