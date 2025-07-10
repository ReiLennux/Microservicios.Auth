using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingCart.API;
using ShoppingCart.API.Contract;
using ShoppingCart.API.Data;
using ShoppingCart.API.Extensions;
using ShoppingCart.API.Services;
using ShoppingCart.API.Utility;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// AutoMapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddScoped<BackendApiAutenticationHttpClientHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Product",

    u => u.BaseAddress = new
    Uri(builder.Configuration["ServiceUrl:ProductUrl"]))
    .AddHttpMessageHandler<BackendApiAutenticationHttpClientHandler>();
builder.Services.AddHttpClient("Coupon",

    u => u.BaseAddress = new
    Uri(builder.Configuration["ServiceUrl:CouponUrl"]))
    .AddHttpMessageHandler<BackendApiAutenticationHttpClientHandler>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
// Swagger + JWT
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Cart MicroService", Version = "v1" });

    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter 'Bearer {token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
});

//builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenLocalhost(5004);
// });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
    });
}
//app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

//app.UseAuthorization();


app.MapControllers();

app.Run();