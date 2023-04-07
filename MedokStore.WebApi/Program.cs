using MedokStore.Application;
using MedokStore.Application.Common.Mappings;
using MedokStore.Application.Interfaces;
using MedokStore.Domain.Entity;
using MedokStore.Identity.Middleware;
using MedokStore.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


///=============1=================

builder.Services.AddControllers();

///=============2=================

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMedokStoreDbContext).Assembly));
});

///=============3=================

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<MedokStoreDbContext>()
               .AddDefaultTokenProviders();

///=============4=================

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});

///=============5=================

builder.Services.AddApplication();

///=============6=================

builder.Services.AddPersistence(builder.Configuration);

///=============7=================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

///=============8=================

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    })
    .AddFacebook(config =>
    {
        config.AppId = builder.Configuration["Facebook:AppId"];
        config.AppSecret = builder.Configuration["Facebook:AppSecret"];
        config.SaveTokens = true;
    });

///=============9=================

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(builder.Configuration["Roles:Client"], policyBuilder =>
    {
        policyBuilder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, builder.Configuration["Roles:Client"])
                                    || x.User.HasClaim(ClaimTypes.Role, builder.Configuration["Roles:Admin"])
                                    || x.User.HasClaim(ClaimTypes.Role, builder.Configuration["Roles:SuperAdmin"]));
    });
    options.AddPolicy(builder.Configuration["Roles:Admin"], policyBuilder =>
    {
        policyBuilder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, builder.Configuration["Roles:Admin"])
                                    || x.User.HasClaim(ClaimTypes.Role, builder.Configuration["Roles:SuperAdmin"]));
    });
    options.AddPolicy(builder.Configuration["Roles:SuperAdmin"], policyBuilder =>
    {
        policyBuilder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, builder.Configuration["Roles:SuperAdmin"]));
    });
});

///============10=================

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MedokStore",
        Version = "1.0.3",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Baerer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Here enter JWT token format: Bearer[token]"
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
            new string[]{}
        }
    });
});

///============11=================

builder.Services.AddEndpointsApiExplorer();

///============End================

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<MedokStoreDbContext>();
        DbInitializer.Initializer(context);
    }
    catch (Exception) { }
}
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowAnyOrigin");
app.UseCorsMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UserCustomExceptionHandler();
app.MapControllers();
app.Run();