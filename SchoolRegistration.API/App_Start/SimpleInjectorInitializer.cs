using SchoolRegistration.Domain.Interfaces.Factories;
using SchoolRegistration.Domain.Interfaces.Repositories;
using SchoolRegistration.Infrastructure.Factories;
using SchoolRegistration.Infrastructure.Repositories;
using SchoolRegistration.Service.Interfaces;
using SchoolRegistration.Service.Services;
using SchoolRegistration.Service.Validators.Filters;
using SchoolRegistration.Service.Validators.Requests;
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
            container.Register<IStudentRepository, StudentRepository>(Lifestyle.Scoped);
            container.Register<IRegistrationRepository, RegistrationRepository>(Lifestyle.Scoped);
            container.Register<ISchoolClassRepository, SchoolClassRepository>(Lifestyle.Scoped);
            container.Register<IReportRepository, ReportRepository>(Lifestyle.Scoped);

            // Services
            container.Register<IStudentService, StudentService>(Lifestyle.Scoped);
            container.Register<IRegistrationService, RegistrationService>(Lifestyle.Scoped);
            container.Register<ISchoolClassService, SchoolClassService>(Lifestyle.Scoped);
            container.Register<IReportService, ReportService>(Lifestyle.Scoped);

            // Validators
            container.Register<StudentRequestValidator>(Lifestyle.Scoped);
            container.Register<StudentFilterValidator>(Lifestyle.Scoped);
            container.Register<RegistrationRequestValidator>(Lifestyle.Scoped);
        }
    }
}