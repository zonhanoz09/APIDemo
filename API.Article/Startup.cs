using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using API.Identity.IdentityServer;

namespace API.Identity
{
    public class Startup
    {
        public Startup()
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer()
                                 .AddInMemoryApiScopes(Config.ApiScopes)
                                 .AddInMemoryClients(Config.Clients);
            builder.AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            // uncomment if you want to add MVC
            //app.UseStaticFiles();
            //app.UseRouting();

            app.UseIdentityServer();
        }
    }
}
