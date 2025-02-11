using backend.DAL.Data;
using backend.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using backend.Firebase;
using backend.Mapping;

var builder = WebApplication.CreateBuilder(args);

FirebaseApp.Create(new AppOptions
{
    // TODO: via .ENV file, but for testing this is fine
    Credential = GoogleCredential.FromFile("./firebase-config.json")
});

builder.Services.AddAuthentication("Firebase")
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>("Firebase", null);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/frank-de-pratende-bank";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/frank-de-pratende-bank",
            ValidateAudience = true,
            ValidAudience = "frank-de-pratende-bank",
            ValidateLifetime = true
        };
    });
 

//builder.Services.AddCors(); 

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins(
                "https://frankthebank.netlify.app",  // Production
                "http://localhost:3000",            // Local development
                "http://127.0.0.1:3000"             // Alternative localhost
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Only if using cookies/auth headers
    });
});

// ...

var app = builder.Build();

// MIDDLEWARE ORDER MATTERS! This should come:
// - After UseRouting
// - Before UseAuthorization
// - Before MapControllers
app.UseCors("AllowFrontend");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowNetlify");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
