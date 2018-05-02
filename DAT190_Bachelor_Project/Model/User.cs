using System;

namespace DAT190_Bachelor_Project.Model
{
    public class User
    {
        // Properties
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Vehicle Vehicle { get; set; }
        public CarbonFootprint CarbonFootprint { get; set; }

        // Necessary to use SBanken open API
        public string SocialSecurityNr { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        // Constructor
        public User()
        {
        }
    }
}
