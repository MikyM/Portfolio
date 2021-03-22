using Entities.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class AppUserDto
    {
        // Extended Properties
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
