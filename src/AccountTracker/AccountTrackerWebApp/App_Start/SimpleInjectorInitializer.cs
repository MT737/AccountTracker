[assembly: WebActivator.PostApplicationStartMethod(typeof(AccountTrackerWebApp.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace AccountTrackerWebApp.App_Start
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using AccountTrackerLibrary.Data;
    using AccountTrackerLibrary.Models;
    using AccountTrackerLibrary.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<Context>(Lifestyle.Scoped);
            container.Register<AccountRepository>(Lifestyle.Scoped);
            container.Register<CategoryRepository>(Lifestyle.Scoped);
            container.Register<TransactionRepository>(Lifestyle.Scoped);
            container.Register<TransactionTypeRepository>(Lifestyle.Scoped);
            container.Register<VendorRepository>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            //If DI container is verrifying, supply our own authentication. This prevents an exception.
            container.Register(() => container.IsVerifying
                ? new OwinContext().Authentication
                : HttpContext.Current.GetOwinContext().Authentication,
            Lifestyle.Scoped);

            //Need to specify the constructor for UserStore. Use the Context instance generated above for the constructor.
            container.Register<IUserStore<User>>(() =>
                new UserStore<User>(container.GetInstance<Context>()),
            Lifestyle.Scoped);
        }
    }
}