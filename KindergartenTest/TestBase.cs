using Microsoft.Extensions.DependencyInjection;
using ShopTARge22.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARge22.KindergartenTest.Macros;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.ApplicationServices.Services;
using Microsoft.Extensions.Hosting;
using ShopTARge22.KindergartenTest.Mock;

namespace ShopTARge22.KindergartenTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; }
        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        protected T Macro<T>() where T : IMacros
        {
            return serviceProvider.GetService<T>();
        }

        //andmebaasiga ühendus
        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IKindergartensServices, KindergartensServices>();
            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();

            services.AddDbContext<ShopTARge22Context>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            RegisterMacros(services);
        }

        //registreerib macrod
        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);
            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface
                && !x.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }


    }
}
