using BL.OnlineStore;
using BL.OnlineStore.SystemBlModel;
using BLContracts;
using BLContracts.SystemBl;
using CommonEntities;
using DAL.OnlineStore;
using DALContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StoreWebApi
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

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			IPasswordHash passwordHash = new PasswordHash(UserAccess.PasswordHashLength);
			IDbContext dbContext = new DbContext("");


			//IDbCacheAdapter dbCacheAdapter = new DbCacheAdapter(dbContext);


			
			services.AddTransient<IRegistrationBlModel>(s => new RegistrationBlModel(passwordHash, dbContext));
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
