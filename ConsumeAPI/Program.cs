using ConsumeAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Register HttpClient
builder.Services.AddHttpClient();
// Add session services to the container
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

// Register IHttpContextAccessor to access HttpContext in controllers
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add controllers with views (for ModelState support)
builder.Services.AddControllersWithViews();


// Configure JWT Authentication
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
        ValidIssuer = "https://myapi.example.com", // Set your valid issuer
        ValidAudience = "https://myapp.example.com", // Set your valid audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("B2E2D9A18A4E54B17FC9AEF65B3B490715B8D36E7F1F11E1238D4A012D0E35D6")) // Set your secret key
    };
});
// Add services to the container
builder.Services.AddRazorPages();

var app = builder.Build();



// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // For HTTPS in production
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Enable session middleware before routing and authorization
app.UseSession();
app.UseEndpoints(endpoints =>
{
    // Set the default route to the Account/Login page
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");


});
//services.AddHttpContextAccessor();

// Map controller routes
app.MapDefaultControllerRoute();

// Map Razor Pages
app.MapRazorPages();

app.Run();
