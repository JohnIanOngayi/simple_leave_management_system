using simple_leave_management_system.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMySqlContext(builder.Configuration);
/// IF MSSQL add connection string `DefaultConnection` in appsettings.json and comment mysql above
//builder.Services.ConfigureMsSqlContext(builder.Configuration);

// Add Context
builder.Services.ConfigureRepositoryWrapper();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
