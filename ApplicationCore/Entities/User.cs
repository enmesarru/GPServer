using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{
    public class User: IdentityUser, IBaseEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        List<Game> Games { get; set; }
    }
}