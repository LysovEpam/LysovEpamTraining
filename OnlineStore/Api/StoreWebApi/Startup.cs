using System;
using System.IO;
using System.Reflection;
using BL.OnlineStore;
using BL.OnlineStore.BlModels.MainBlModels;
using BL.OnlineStore.BlModels.SystemBlModels;
using BL.OnlineStore.Tests.Mocks;
using BLContracts;
using BLContracts.MainBl;
using BLContracts.SystemBl;
using DAL.OnlineStore;
using DALContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StoreWebApi.AuthorizationModel;
using Swashbuckle.AspNetCore.Swagger;

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
			#region JWT Authentication

			const string signingSecurityKey = AuthorizationDataModel.SigningSecurityKey;

			var signingKey = new SigningSymmetricKey(signingSecurityKey);
			services.AddSingleton<IJwtSigningEncodingKey>(signingKey);

			

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			const string jwtSchemeName = AuthorizationDataModel.JwtSchemeName;

			var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;



			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingDecodingKey.GetKey(),

				ValidateIssuer = true,
				ValidIssuer = AuthorizationDataModel.ValidIssuer,

				ValidateAudience = true,
				ValidAudience = AuthorizationDataModel.ValidAudience,

				ValidateLifetime = true,

				ClockSkew = TimeSpan.FromSeconds(5)
			};
			
			services
				.AddAuthentication(options => 
				{
					options.DefaultAuthenticateScheme = jwtSchemeName;
					options.DefaultChallengeScheme = jwtSchemeName;
				})
				.AddJwtBearer(jwtSchemeName, jwtBearerOptions =>
					{
						jwtBearerOptions.TokenValidationParameters = tokenValidationParameters;
					});

			#endregion

			//================================================================================================
			//DI

			string stringConnection = @"Data Source=(local)\SQLEXPRESS; Initial Catalog=OnlineStore; Integrated Security=True";

			IPasswordHash passwordHash = new PasswordHashMock();
			IDbContext dbContext = new DbContext(stringConnection);
			IProgramLogRegister logRegister = new ProgramLogRegister();
			 

			var registrationBlModel = new RegistrationBlModel(passwordHash, dbContext, logRegister);
			var authorizationBlModel = new AuthorizationBlModel(dbContext, passwordHash, logRegister);
			var productCategoryBlModel = new ProductCategoryBlModel(dbContext, logRegister);
			var imageProductBl = new ImageProductBlModel(logRegister, "localhost", "controllerPath");
			var productInformationBlModel = new ProductInformationDlModel(dbContext, logRegister);
			var productBlModel = new ProductDlModel(dbContext, logRegister);
			var userOrderBlModel = new UserOrderBlModel(dbContext, logRegister);

			//services.AddTransient<IProgramLogRegister>(s => logRegister);
			services.AddTransient<IRegistrationBlModel>(s => registrationBlModel);
			services.AddTransient<IAuthorizationBlModel>(s => authorizationBlModel);
			services.AddTransient<IProductCategoryBlModel>(s => productCategoryBlModel);
			services.AddTransient<IImageProductBlModel>(s => imageProductBl);
			services.AddTransient<IProductInformationBlModel>(s => productInformationBlModel);
			services.AddTransient<IProductBlModel>(s => productBlModel);
			services.AddTransient<IUserOrderBlModel>(s => userOrderBlModel);

			services.AddMvc();


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseMvc();

			app.UseSwagger();
			
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				c.RoutePrefix = string.Empty;
			});



		}





	}
}
