using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Services;
using AllYourChatsAreBelongToUs.Services.Integrations;
namespace AllYourChatsAreBelongToUs
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
            services.AddMvc();
            services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase(databaseName: "UserContext"));
            services.AddHttpClient<SlackIntegrationClient>();
            services.AddTransient<UserService>(opt => new UserService(opt.GetService<UserContext>(), opt.GetService<SlackIntegrationClient>()));
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
