using Identity.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.CustomValidator
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {

            List<IdentityError> errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError() { 
                
                    Code = "PasswordContainsUserName",
                    Description = "Parola kullanıcı adını içeremez."
                });

            }

            if(errors.Count > 0 )
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));

            }
            return Task.FromResult(IdentityResult.Success);

            
        }
    }
}
