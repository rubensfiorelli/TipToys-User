using Aws_Login.Application.Adapters;
using Aws_Login.Application.Ports;
using Aws_Login.Core.Ports;
using Aws_Login.Core.ServicesCore;
using Aws_Login.Infrastruture.Adapters;
using Aws_Login.Infrastruture.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO.Compression;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(jwt =>
{
    jwt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    jwt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(ConfigurationJwt.JwtPrivateKey);
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true
    };

});

builder.Services.AddResponseCompression(opts =>
{
    opts.Providers.Add<GzipCompressionProvider>();
    opts.EnableForHttps = true;

});

builder.Services.Configure<GzipCompressionProviderOptions>(opts =>
{
    opts.Level = CompressionLevel.Optimal;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<EmailService>();

builder.Services
   .AddDbContextPool<ApplicationDbContext>(opts => opts
   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
   .UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"), b => b
   .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddScoped(typeof(IUSerRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
ConfigurationJwt.JwtPrivateKey = app.Configuration.GetValue<string>("JwtKey");
ConfigurationJwt.JwtPrivateKey = app.Configuration.GetValue<string>("ApiKeyName");
ConfigurationJwt.JwtPrivateKey = app.Configuration.GetValue<string>("ApiKey");

var smtp = new ConfigurationJwt.SmtpConfiguration();
app.Configuration.GetSection("Smtp").Bind(smtp);
ConfigurationJwt.Smtp = smtp;


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();

app.MapControllers();

app.Run();
