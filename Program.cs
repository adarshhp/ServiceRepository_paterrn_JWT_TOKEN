using Microsoft.EntityFrameworkCore;
using TVM.DbContexts;
using TVM.Repository;
using TVM.Repository.impl;
using TVM.Services;
using TVM.Services.impl;
//jwt
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddHttpClient<IDataService, DataService>();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//    .RequireAuthenticatedUser()
//    .Build();
//});
//builder.Services.AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme =
//        JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme =
//        JwtBearerDefaults.AuthenticationScheme;
//    }).AddJwtBearer(options =>
//    {
//    options.TokenValidationParameters = new TokenValidationParameters
//{
//        ValidateIssuer = false,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        //ValidIssuer = "yourIssuer",
//        //ValidAudience = "yourAudience",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecgfdfghjhgfhjghghghghjhjkretKey"))

//    };
//    });

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
      .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
      .RequireAuthenticatedUser()
      .Build();
});
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
