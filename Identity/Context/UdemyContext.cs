using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Context
{
    public class UdemyContext : IdentityDbContext<AppUser , AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server = (localdb)\mssqllocaldb; database= UdemyIdentity; Integrated Security = true;");
            base.OnConfiguring(optionsBuilder);
        }

       
    }
}
