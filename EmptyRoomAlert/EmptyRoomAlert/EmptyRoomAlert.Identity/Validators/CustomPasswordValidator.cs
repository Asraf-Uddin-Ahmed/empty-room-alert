﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmptyRoomAlert.Identity.Validators
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public CustomPasswordValidator()
        {
            base.RequiredLength = 6;
            //base.RequireNonLetterOrDigit = true;
            //base.RequireDigit = true;
            //base.RequireLowercase = true;
            //base.RequireUppercase = true;
        }
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);

            if (password.Contains("abcdef") || password.Contains("123456") || password.Contains("password"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Password can not contain sequence of characters");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}