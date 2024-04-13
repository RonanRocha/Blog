using Blog.Application.Core.Services;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Mappings;
using Blog.Domain.Account.Services;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using Blog.Infrastructure.Data.Account.Services;
using Blog.Infrastructure.Data.Context;
using Blog.Infrastructure.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Blog.Infrastructure.Data.Uploads;
using Blog.Domain.Core.Repositories;
using Blog.Domain.Core.Uploads;
using Blog.Application.Utils;
using Microsoft.AspNetCore.Http;
using Blog.Application.Core.Posts.Validators;
using MediatR;
using Blog.Application.Pipelines;
using Hangfire;


namespace Blog.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(configuration
                          .GetConnectionString("DefaultConnection"),
                              b => b.MigrationsAssembly(
                                      typeof(BlogDbContext).Assembly.FullName))
            );

            services.AddIdentity<User, IdentityRole>( x =>
            {
                x.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<BlogDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ISeedAccountService, SeedAccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFileHandler, FileHandler>();
            services.AddScoped<IEmailService,EmailService>();


            services.AddHangfire(options =>
            {
                options.UseSqlServerStorage(configuration
                          .GetConnectionString("HangFireConnection"));
               
            });

            services.AddHangfireServer(options =>
            {
                options.Queues = new string[] { "resetpasswordqueue", "confirmemailqueue", "mfaqueue" };
            });

            //services.AddValidatorsFromAssemblyContaining<PostCreateCommandValidator>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            var assembly = AppDomain.CurrentDomain.Load("Blog.Application");

            AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));


        



            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(ValidationRequestBehavior<,>));
            });


            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });


            return services;
        }
    }
}
