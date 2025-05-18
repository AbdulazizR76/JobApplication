using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using JobApplication.Models;
using Microsoft.Owin.Security;


public class ApplicationUserManager : UserManager<ApplicationUser>
{
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
    {
    }

    public static ApplicationUserManager Create(
        IdentityFactoryOptions<ApplicationUserManager> options,
        IOwinContext context)
    {
        var manager = new ApplicationUserManager(
            new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

        // Username validation
        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
        };

        // Password validation
        manager.PasswordValidator = new PasswordValidator
        {
            RequiredLength = 6,
            RequireDigit = true,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonLetterOrDigit = true
        };

        return manager;
    }

    
}
public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
{
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
    {
    }

    public static ApplicationSignInManager Create(
        IdentityFactoryOptions<ApplicationSignInManager> options,
        IOwinContext context)
    {
        return new ApplicationSignInManager(
            context.GetUserManager<ApplicationUserManager>(),
            context.Authentication);
    }
}