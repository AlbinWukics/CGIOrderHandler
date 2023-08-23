using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using OrderHandler.BusinessLogic.Services;
using OrderHandler.DataAccess.Contexts;
using OrderHandler.DomainCommons.DataModels;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Exstensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException(
    "Connection string 'IdentityConnection' not found.");
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(identityConnectionString));


var orderHandlerConnectionString = builder.Configuration.GetConnectionString("OrderHandlerConnection") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'OrderHandlerConnection' not found.");
builder.Services.AddDbContext<OrderHandlerContext>(options =>
    options.UseSqlServer(orderHandlerConnectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUserModel>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUserModel, IdentityContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");


// Gives an instance/adds services to the endpoint url.
app.MapEndpoints();


app.Run();