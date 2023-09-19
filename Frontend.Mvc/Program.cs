using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddHealthChecks().AddCheck("Health", () => HealthCheckResult.Healthy("Ok"));

var app = builder.Build();

string basePath = app.Configuration["API_PATH_BASE"];
/*
app.Use((context, next) =>
{
    string _pathBase = basePath;
    PathString matchedPath;
    PathString remainingPath;

    if (context.Request.Path.StartsWithSegments(_pathBase, out matchedPath, out remainingPath))
    {
        var originalPath = context.Request.Path;
        var originalPathBase = context.Request.PathBase;
        context.Request.Path = remainingPath;
        context.Request.PathBase = originalPathBase.Add(matchedPath);
        var re = context.Request.PathBase;
        return next.Invoke();

    }
    else
    {
        return next.Invoke();
    }
});
*/
/*
if (!string.IsNullOrWhiteSpace(basePath))
{
    app.UsePathBase($"/{basePath.TrimStart('/')}");
}
else
{
    app.UsePathBase("/frontend2");
}
*/
app.UsePathBase("/frontend2");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(e => {
    e.MapHealthChecks("/health");
});
app.Run();

