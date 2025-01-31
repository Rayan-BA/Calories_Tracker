using api.Interfaces;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.Text;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    builder.Services.AddControllers();

    builder.Services.AddScoped<IFoodRepository, FoodRepository>();

    builder.Services.AddSingleton<MongoDBService>();

    builder.Services.AddIdentity<User, Role>()
      .AddMongoDbStores<User, Role, ObjectId>(
        $"{builder.Configuration["MongoDB:URI"]}{builder.Configuration["MongoDB:Database"]}",
        builder.Configuration["MongoDB:Database"]
      );

    builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme =
      options.DefaultChallengeScheme =
      options.DefaultSignInScheme =
      options.DefaultSignOutScheme =
      options.DefaultForbidScheme =
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SingingKey"]))
      };
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}