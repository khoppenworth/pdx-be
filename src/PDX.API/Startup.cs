using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PDX.API.Helpers;
using PDX.API.Middlewares.Token;
using PDX.API.Services;
using PDX.API.Services.Interfaces;
using PDX.BLL.Helpers.ObjectDiffer;
using PDX.BLL.Repositories;
using PDX.BLL.Services;
using PDX.BLL.Services.Email;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Interfaces.Email;
using PDX.BLL.Services.Notification;
using PDX.DAL;
using PDX.DAL.Reporting.Engine;
using PDX.DAL.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PDX.API {
    public partial class Startup {
        private readonly IHostingEnvironment _hostingEnv;
        public Startup (IHostingEnvironment env) {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (env.ContentRootPath)
                .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : false, reloadOnChange : true)
                .AddEnvironmentVariables ();
            Configuration = builder.Build ();
            _hostingEnv = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            var connectionString = Configuration.GetConnectionString ("DefaultConnection");
            // Add framework services.
            services.AddDbContext<PDXContext> (options => {
                options.UseNpgsql (connectionString);
                options.EnableSensitiveDataLogging ();
                options.UseQueryTrackingBehavior (QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Scoped);

            services.AddMvc (options => {
                options.OutputFormatters.Insert (0, new CustomDecimalFormatter ());
            }).AddJsonOptions (options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });
            //FormOptions
            services.Configure<FormOptions> (options => {
                options.ValueCountLimit = int.MaxValue;
            });

            //Policies
            ConfigurePolicies (services);

            //Compression
            services.Configure<GzipCompressionProviderOptions> (options => options.Level = CompressionLevel.Optimal);
            services.Configure<FormOptions> (options => options.ValueCountLimit = int.MaxValue);
            services.AddResponseCompression (options => {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider> ();
            });

            //App Settings
            services.Configure<PDX.BLL.Model.Config.ConnectionConfig> (Configuration.GetSection ("ConnectionStrings"));
            services.Configure<PDX.BLL.Model.Config.AttachmentConfig> (Configuration.GetSection ("AttachmentOptions"));
            services.Configure<PDX.API.Middlewares.Token.TokenProviderOptions> (Configuration.GetSection ("JwtOptions"));
            services.Configure<PDX.BLL.Model.Config.EmailConfig> (Configuration.GetSection ("EmailSettings"));
            services.Configure<PDX.Logging.Models.LoggingConfig> (Configuration.GetSection ("LoggingOptions"));
            services.Configure<PDX.BLL.Model.Config.NotificationConfig> (Configuration.GetSection ("NotificationConfig"));
            services.Configure<PDX.BLL.Model.Config.SystemUserConfig> (Configuration.GetSection ("SystemUser"));

            //JavascriptServices
            services.AddNodeServices ();

            //Repository and EF Core
            services.AddScoped (typeof (IGenericRepository<>), typeof (GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddScoped<IUserRepository, UserRepository> ();
            services.AddScoped<IProductRepository, ProductRepository> ();
            services.AddScoped<IMARepository, MARepository> ();
            services.AddScoped<IImportPermitRepository, ImportPermitRepository> ();

            //Other Services
            services.AddTransient<IEmailSender, EmailSender> ();
            services.AddTransient<IProducer, Producer> ();
            services.AddTransient<IDataProvider, DataProvider> ();
            services.AddTransient<PDX.Logging.ILogger, PDX.Logging.Logger> ();
            DataTables.AspNet.AspNetCore.Configuration.RegisterDataTables (services);
            //Type Binding for ObjectDifference
            services.AddTransient<ITypeDiffer, PrimativeDiffer> ();
            services.AddTransient<ITypeDiffer, ObjectEqualityEnumerableDiffer> ();
            services.AddTransient<ITypeDiffer, ObjectTypeDiffer> ();
            services.AddTransient (typeof (IEqualityComparer<object>), typeof (DefaultEqualityComparer));
            services.AddTransient<IDiffer, Differ> ();

            //Services
            services.AddTransient (typeof (IService<>), typeof (Service<>));
            services.AddTransient<IAccountService, AccountService> ();
            services.AddTransient<IUserService, UserService> ();
            services.AddTransient<IMenuService, MenuService> ();
            services.AddTransient<IPermissionService, PermissionService> ();
            services.AddTransient<IDocumentService, DocumentService> ();
            services.AddTransient<IAgentService, AgentService> ();
            services.AddTransient<IImportPermitService, ImportPermitService> ();
            services.AddTransient<IImportPermitDeliveryService, ImportPermitDeliveryService> ();
            services.AddTransient<IProductService, ProductService> ();
            services.AddTransient<IManufacturerService, ManufacturerService> ();
            services.AddTransient<IPortOfEntryService, PortOfEntrySerivce> ();
            services.AddTransient<IRoleService, RoleService> ();
            services.AddTransient<IIpermitLogStatusService, IpermitLogStatusService> ();
            services.AddTransient<IModuleDocumentService, ModuleDocumentService> ();
            services.AddTransient<ISupplierService, SupplierService> ();
            services.AddTransient<IGenerateDocuments, GenerateDocument> ();
            services.AddTransient<IReportService, ReportService> ();
            services.AddTransient<IWIPService, WIPService> ();
            services.AddTransient<IChangeLogService, ChangeLogService> ();
            services.AddTransient<IStatusLogService, StatusLogService> ();
            services.AddTransient<IChecklistService, ChecklistService> ();
            services.AddTransient<ISubmoduleChecklistService, SubmoduleChecklistService> ();
            services.AddTransient<IMAService, MAService> ();
            services.AddTransient<IMALogStatusService, MALogStatusService> ();
            services.AddTransient<IMAAssignmentService, MAAssignmentService> ();
            services.AddTransient<IMAReviewService, MAReviewService> ();
            services.AddTransient<IFieldService, FieldService> ();
            services.AddTransient<IUtilityService, UtilityService> ();
            services.AddTransient<IShipmentService, ShipmentService> ();
            services.AddTransient<INotificationService, NotificationService> ();
            services.AddTransient<IProductTypeService, ProductTypeService> ();
            services.AddTransient<IMedicalDeviceStagingService, MedicalDeviceStagingService> ();
            services.AddTransient (typeof (NotificationFactory));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info { Title = "iImport API" });
            });

            services.ConfigureSwaggerGen (c => {
                //Set the comments path for the swagger json and ui.                    
                c.IncludeXmlComments (Path.Combine (System.AppContext.BaseDirectory, "PDX.API.xml"));
                c.CustomSchemaIds (x => x.FullName);
            });

            //JWT    
            ConfigureAuth (services);

            //DateTables
            DataTables.AspNet.AspNetCore.Configuration.RegisterDataTables (services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole (Configuration.GetSection ("Logging"));
            loggerFactory.AddDebug ();

            // Configure the HTTP request pipeline.
            app.UseFileServer (new FileServerOptions () {
                FileProvider = new PhysicalFileProvider (
                        Path.Combine (Directory.GetCurrentDirectory (), @"node_modules")),
                    RequestPath = new PathString ("/node_modules"),
                    EnableDirectoryBrowsing = true
            });
            app.UseStaticFiles ();

            //Cors
            app.UseCors ("CorsPolicy");

            app.UseDeveloperExceptionPage ();
            app.UseResponseCompression ();
            //Use JWT for this case           

            var jwtOptions = GetTokenProviderOptions ();
            app.UseSimpleTokenProvider (jwtOptions);
            app.UseMiddleware<TokenProviderMiddleware> (Options.Create (jwtOptions));
            app.UseAuthentication ();
            app.UseMvc ();

            //Swagger
            app.UseSwagger (c => {
                c.PreSerializeFilters.Add ((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "V1 Docs");
                c.EnableValidator ();
                c.DocExpansion (DocExpansion.None);
                c.SupportedSubmitMethods (SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete, SubmitMethod.Patch);
                c.InjectStylesheet ("/swagger-ui-themes/themes/3.x/theme-flattop.css");
            });
        }

    }
}