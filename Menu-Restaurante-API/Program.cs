using Menu_Restaurante_API.Data;
using Menu_Restaurante_API.Repositories.Implementations;
using Menu_Restaurante_API.Repositories.Interfaces;
using Menu_Restaurante_API.Servicies.Implementations;
using Menu_Restaurante_API.Servicies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

// =====================  DB CONTEXT  =====================
builder.Services.AddDbContext<MenuRestauranteContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// =====================  REPOSITORIES  =====================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// =====================  SERVICES  =====================
builder.Services.AddScoped<IUserService, UserService>(); //scoped: una instancia por request.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// =====================  CONTROLLERS & SWAGGER  =====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Menu-Restaurante-API",
        Version = "v1"
    });

    // 🔐 Definición del esquema de seguridad tipo Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT con el formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // 🔐 Indicar que todas las operaciones pueden usar ese esquema
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
            Array.Empty<string>()
        }
    });
});


// =====================  JWT AUTH  =====================
// (usa las claves que pusimos en appsettings.json: Jwt:Key, Jwt:Issuer, Jwt:Audience)
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

if (!string.IsNullOrEmpty(jwtKey))
{
    var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });
}

var app = builder.Build();

// =====================  PIPELINE HTTP  =====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

