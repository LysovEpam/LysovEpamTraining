using System;
using System.IO;
using System.Reflection;
using BL.OnlineStore;
using BL.OnlineStore.Services.MainServices;
using BL.OnlineStore.Services.SystemServices;
using BLContracts;
using BLContracts.MainService;
using BLContracts.SystemService;
using DAL.OnlineStore;
using DALContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
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

		private const string CorsPolicy = "CorsPolicy";

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{


			services.AddCors();


			// Add framework services.
			services.AddMvc();

			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
			});


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
			#region DI

			services.AddTransient<IPasswordHash, PasswordHash>();
			services.AddTransient<IDbContext>(s=> new DbContextCache(ServerConfig.GetStringConnection()));

			services.AddTransient<IRegistrationService, RegistrationService>();
			services.AddTransient<IAuthorizationService, AuthorizationService>();
			services.AddTransient<IProductCategoryService, ProductCategoryService>();
			services.AddTransient<IProductInformationService, ProductInformationService>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<IUserOrderService, UserOrderService>();

			
			#endregion

			services.AddMvcCore();


			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			#region CORS

			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory(CorsPolicy));
			});
			var corsBuilder = new CorsPolicyBuilder();
			corsBuilder.AllowAnyHeader();
			corsBuilder.AllowAnyMethod();
			corsBuilder.WithOrigins("http://localhost:4200");
			corsBuilder.AllowCredentials();

			services.AddCors(options =>
			{
				options.AddPolicy(CorsPolicy, corsBuilder.Build());
			});

			#endregion



		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				c.RoutePrefix = string.Empty;
			});

			app.UseCors(CorsPolicy);

			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseMvc();




		}





	}
}
