using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Models
{
    public class UserRoleEntity :IdentityRole<Guid>
    {
        public UserRoleEntity() : base()
        {

        }

        public UserRoleEntity(string roleName):base(roleName)
        { }
    }
}
