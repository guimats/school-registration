using SchoolRegistration.Domain.Interfaces;
using SchoolRegistration.Infrastructure.Factories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Configuration;
using System.Web.Http;

namespace SchoolRegistration.API.App_Start
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize(HttpConfiguration config)
        {
            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(config);
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Infraestrutura / Factory
            container.Register<IDbConnectionFactory>(() => new SqlConnectionFactory(connectionString), Lifestyle.Singleton);

            // Repositories

            // Services
        }
    }
}