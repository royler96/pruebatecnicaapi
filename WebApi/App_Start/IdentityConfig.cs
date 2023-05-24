﻿using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Providers;
using WebApi.DataAccess.Models;

namespace WebApi.App_Start
{
    public class ApplicationUserManager : UserManager<usuario>
    {
        public ApplicationUserManager(IUserStore<usuario> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStoreApp());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<usuario>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                //RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<usuario>(dataProtectionProvider.Create("ASP.NET Identity 2"));
            }
            return manager;
        }
    }
}