
using Blog.Api.Policies.Comments;
using Blog.Api.Policies.Comments.Requirements;
using Blog.Api.Policies.Posts;
using Blog.Api.Policies.Posts.Requirements;
using Blog.Domain.Account.Services;
using Blog.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
        .AddControllers()
        .AddNewtonsoftJson(c =>
        {
            c.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            c.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditPost", policy => policy.Requirements.Add(new PostOwnerRequirement()));
    options.AddPolicy("DeletePost", policy => policy.Requirements.Add(new PostOwnerRequirement()));
    options.AddPolicy("EditComment", policy => policy.Requirements.Add(new CommentOwnerRequirement()));
    options.AddPolicy("DeleteComment", policy => policy.Requirements.Add(new CommentOwnerRequirement()));
});



DependencyInjection.AddInfrastructure(builder.Services, builder.Configuration);
builder.Services.AddSingleton<IAuthorizationHandler, PostOwnerAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, CommentOwnerAuthorizationHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        //definir configuracoes
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
        "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                      {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var seedUserRoleInitial = services.GetRequiredService<ISeedAccountService>();

    seedUserRoleInitial.SeedRoles();
    seedUserRoleInitial.SeedUsers();
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
