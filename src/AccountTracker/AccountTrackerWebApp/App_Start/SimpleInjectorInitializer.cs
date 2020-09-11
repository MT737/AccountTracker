[assembly: WebActivator.PostApplicationStartMethod(typeof(AccountTrackerWebApp.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace AccountTrackerWebApp.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using AccountTrackerLibrary.Data;
    using AccountTrackerLibrary.Models;
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
            // For instance:
            container.Register<Context>(Lifestyle.Scoped);
            container.Register<AccountRepository>(Lifestyle.Scoped);
            container.Register<CategoryRepository>(Lifestyle.Scoped);
            container.Register<TransactionRepository>(Lifestyle.Scoped);
            container.Register<TransactionTypeRepository>(Lifestyle.Scoped);
            container.Register<VendorRepository>(Lifestyle.Scoped);
        }
    }
}