using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using lab1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using lab1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration[
        "ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    option => option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = AuthOptions.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    });
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.Map("/login", async (Person user, DataContext db) =>
{
    Person? userDB = await db.Users!.FirstOrDefaultAsync(p => p.Login == user.Login && p.Password == user.Password);
    if (userDB == null)
    {
        return Results.Unauthorized();
    }
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };
    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.Issuer,
        audience: AuthOptions.Audience,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(
            AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
    );
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = user.Login
    };
    return Results.Json(response);
});
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);
app.Run();


public class AuthOptions
{
    public const string Issuer = "MyAuthServer";
    public const string Audience = "MyAuthClient";
    const string Key = "k@2in2ov7nov!nld$nl@2in2ov7nd$nl";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
