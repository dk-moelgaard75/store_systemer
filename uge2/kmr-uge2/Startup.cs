using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using kmr_uge2.Services;
using System.Configuration;
using Microsoft.Azure.Cosmos;

namespace kmr_uge2
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
            services.AddControllersWithViews();
            services.AddSingleton<IPersonService>(InitializeCosmosClientForPersonsInstanceAsync(Configuration.GetSection("CosmosDbPerson")).GetAwaiter().GetResult());
            services.AddSingleton<ICovidTestService>(InitializeCosmosClientForCovidTestsInstanceAsync(Configuration.GetSection("CosmosDbCovidTest")).GetAwaiter().GetResult());
        }
        private static async Task<PersonService> InitializeCosmosClientForPersonsInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            //Microsoft.Azure.Cosmos.CosmosClient
            CosmosClient client = new CosmosClient(account, key);
            PersonService personService = new PersonService(client, databaseName, containerName);
            //Microsoft.Azure.Cosmos.DatabaseResponse
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            return personService;

        }
        private static async Task<CovidTestService> InitializeCosmosClientForCovidTestsInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            //Microsoft.Azure.Cosmos.CosmosClient
            CosmosClient client = new CosmosClient(account, key);
            CovidTestService covidTestService = new CovidTestService(client, databaseName, containerName);
            //Microsoft.Azure.Cosmos.DatabaseResponse
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            return covidTestService;

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
