using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constructor.Storage;
using Constructor.Storage.Managers.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Constructor.Storage.Managers;
using Constructor.Storage.Managers.Drives;
using Constructor.Storage.Managers.CPUs;
using Constructor.Storage.Managers.FANs;
using Constructor.Storage.Managers.FSPs;
using Constructor.Storage.Managers.GPUs;
using Constructor.Storage.Managers.Motherboards;
using Constructor.Storage.Managers.RAMs;
using Constructor.Storage.Managers.Devices;
using Constructor.Storage.Managers.Pairs;
using Constructor.Storage.Managers.Assemblies;
using Constructor.Storage.Managers.CpuFans;

namespace Constructor
{
    public class Startup
    {

        private IConfigurationRoot _confString;

        public Startup(IWebHostEnvironment tmp2)
        {
            
            _confString = new ConfigurationBuilder().SetBasePath(tmp2.ContentRootPath).AddJsonFile("DBSettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddTransient<ICasesManager, CasesManager>();
            services.AddTransient<ICPUsManager, CPUsManager>();
            services.AddTransient<ICpuFansManager, CpuFansManager>();
            services.AddTransient<IDrivesManager, DrivesManager>();
            services.AddTransient<IFANsManager, FANsManager>();
            services.AddTransient<IFSPsManager, FSPsManager>();
            services.AddTransient<IGPUsManager, GPUsManager>();
            services.AddTransient<IMotherboardsManager, MotherboardsManager>();
            services.AddTransient<IRAMsManager,RAMsManager>();
            services.AddTransient<IDevicesManager, DevicesManager>();
            services.AddTransient<IFAPManager, FAPManager>();
            services.AddTransient<IDAPManager, DAPManager>();
            services.AddTransient<IRAPManager, RAPManager>();
            services.AddTransient<IAssembliesManager, AssembliesManager>();
            services.AddTransient<IAssemblyContainerManager, AssemblyContainerManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Main}/{action=MainPage}/{id?}");
            });

        }
    }
}
