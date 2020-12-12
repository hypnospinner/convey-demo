using System;
using Convey.Types;

namespace UnicornParty.Models
{
    public class User : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}