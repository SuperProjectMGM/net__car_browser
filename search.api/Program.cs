using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using search.api.Data;
using search.api.Interfaces;
using search.api.Repositories;
using search.api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using search.api.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using search.api.AppExtensions;
using search.api.Messages;
using search.api.Providers;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer eyJhbGci...'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddScoped<IEmailInterface, EmailService>();
builder.Services.AddScoped<IRentalInterface, RentalRepository>();
builder.Services.AddScoped<IAuthorizationHandler, DrivingLicenseRequirementHandler>();
builder.Services.AddScoped<IMessageHandlerInterface, MessageHandler>();
builder.Services.AddScoped<IAuthInterface, AuthRepository>();
builder.Services.AddScoped<IUserInfoInterface, UserRepository>();
builder.Services.AddSingleton<RabbitMessageService>();
builder.Services.AddHttpClient<IRentalInterface, RentalRepository>();
builder.Services.AddScoped<IKEJHelper, KEJHelper>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Devconnection")));

builder.Services.AddIdentity<UserDetails, IdentityRole<int>>().AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT_AUDIENCE"],
        ValidIssuer = builder.Configuration["JWT_ISSUER"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_KEY"]))
    };
}).AddCookie()
      //.AddGoogle(googleOptions =>
      //{
       //  googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    //  googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
      //})
    ;

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DrivingLicenseRequired", policy =>
    policy.Requirements.Add(new DrivingLicenseRequirement()));
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
});


builder.Services.AddScoped<ISearchInterface, SearchMainService>();

builder.Services.AddHttpClient<IVehicleGetter, OurVehicleGetter>();
builder.Services.AddHttpClient<IVehicleGetter, KEJVehicleGetter>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:4199", "https://nice-tree-06b2b2403.5.azurestaticapps.net",
         "http://localhost:4199/dashboard", "https://localhost:4199", "https://nice-tree-06b2b2403.5.azurestaticapps.net/view-deals")
               .AllowAnyMethod()
               .AllowAnyHeader()
                .AllowCredentials();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseRabbitMessageService();
app.Run();

