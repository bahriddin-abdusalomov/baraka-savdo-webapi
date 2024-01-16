using Microsoft.OpenApi.Models;

namespace Baraka_Savdo.WebApi.Configurations;

public static class SwaggerConfiguration
{
    public static void SwaggerConfigure(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Baraka Savdo Api",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }});
        });
    }
}
