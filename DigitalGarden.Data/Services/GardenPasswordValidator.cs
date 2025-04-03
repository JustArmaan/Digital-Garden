using Microsoft.AspNetCore.Identity;
using DigitalGarden.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DigitalGarden.Services
{
    public class GardenPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            var errors = new List<IdentityError>();

            // Check if password contains username
            if (password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsUsername",
                    Description = "Your password cannot contain your username."
                });
            }

            // Check if password contains email
            var emailParts = user.Email?.Split('@');
            if (emailParts?.Length > 0 && !string.IsNullOrEmpty(emailParts[0]) &&
                password.Contains(emailParts[0], StringComparison.OrdinalIgnoreCase))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainsEmail",
                    Description = "Your password cannot contain parts of your email address."
                });
            }

            string[] gardenWords = { "plant", "garden", "flower", "seed", "soil", "water", "green" };
            foreach (var word in gardenWords)
            {
                if (password.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    errors.Add(new IdentityError
                    {
                        Code = "PasswordContainsCommonWord",
                        Description = "Your password cannot contain common garden-related words."
                    });
                    break;
                }
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]"))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordMissingSpecialChar",
                    Description = "Your password must contain at least one special character."
                });
            }


            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success :
                IdentityResult.Failed(errors.ToArray()));
        }
    }
}
