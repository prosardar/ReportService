using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Report.BL;
using Report.Data;

namespace ReportService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var createContext = new Func<IDbContext>(() => new PgContext(Configuration.GetConnectionString("DbInfo:EmployeeDb")));
            services.AddMvcCore();
            services.AddTransient<IEmpCodeResolver, EmpCodeResolver>();
            services.AddTransient(s => createContext());
            services.AddTransient<ISalaryReport>(s => new SalaryReportService(createContext())
            {
                EmpCodeResolver = new EmpCodeResolver(),
                EmpSalaryService = new EmpSalaryService()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
