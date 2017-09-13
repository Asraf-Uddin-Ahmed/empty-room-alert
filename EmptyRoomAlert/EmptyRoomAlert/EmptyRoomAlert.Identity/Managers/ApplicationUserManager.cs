using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Services.Email;
using EmptyRoomAlert.Foundation.Persistence;
using EmptyRoomAlert.Foundation.Persistence.Services.Email;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.Identity.Message;
using EmptyRoomAlert.Identity.Providers;
using EmptyRoomAlert.Identity.Validators;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace EmptyRoomAlert.Identity.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationDbContext appDbContext = context.Get<ApplicationDbContext>();
            ApplicationUserManager appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser, CustomRole, Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>(appDbContext));

            appUserManager.UserValidator = new CustomUserValidator(appUserManager);
            appUserManager.PasswordValidator = new CustomPasswordValidator();

            ConfigureEmailServiceProvider(appDbContext, appUserManager, options);

            return appUserManager;
        }


        private static void ConfigureEmailServiceProvider(ApplicationDbContext appDbContext, ApplicationUserManager appUserManager, IdentityFactoryOptions<ApplicationUserManager> options)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(appDbContext);
            IIdentityMessageBuilder identityMessageBuilder = new IdentityMessageBuilder(unitOfWork);
            IEmailService emailService = new EmailService(unitOfWork);
            appUserManager.EmailService = new EmailServiceProvider(emailService, appUserManager, identityMessageBuilder);

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }
        }
    }
}