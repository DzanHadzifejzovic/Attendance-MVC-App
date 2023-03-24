using FIsrtMVCapp.Models;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FIsrtMVCapp.Data;
using Microsoft.AspNetCore.Routing.Constraints;
using FIsrtMVCapp;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();

builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

builder.Services.AddDbContext<PeopleContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PrimaryConnectionString")));

//zbog identity-ja da bi user imao property-je i identity usera- i custom usera(klasa nasljedjuje IdentityUser klasu)
builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
       .AddEntityFrameworkStores<FIsrtMVCappContext>();

builder.Services.AddDbContext<FIsrtMVCappContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PrimaryConnectionString")));

// Add services to the container.

// builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate(); // za dodavanje sertifikata httpS

builder.Services.AddControllersWithViews(options => {
    options.CacheProfiles.Add("CacheProfile1", new CacheProfile()
    {
        Location = ResponseCacheLocation.Client,
        Duration = 20
    });
})
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
    });

var app = builder.Build();

app.UseResponseCaching();

//
var supportedCultures = new[] { "en", "de-DE", "sr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);
//

/*Configuration = app.Configuration;*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


//zbog identity-ja
app.MapRazorPages();
//

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "attendant",
    pattern: "attendant/{firstName}-{lastName}",
    defaults: new
    {
        controller = "Home",
        action = "AttendantDetails"
    },
    constraints: new { firstName = "[a-z]{3,7}" }); //, httpMethod = new HttpMethodRouteConstraint("POST")


/*
 //klasicni nacin za definisanje rute za area
 app.MapControllerRoute(
           name: "preview default",
           pattern: "Preview/{controller=Home}/{action=Index}/{id?}"

       );
 
*/

/*
// bolji nacin za definisanje rute za area /vazeci samo za jednu areu
app.MapAreaControllerRoute
    (
        name: "preview default",
        areaName: "Preview",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
*/


// jos bolji nacin za definisanje rute za area /vazeci  za sve area-e(ako ih ima puno)
app.MapControllerRoute
    (
        name: "preview default",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );



app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}/{*slug}");


app.Run();