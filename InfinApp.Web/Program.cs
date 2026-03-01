using InfinApp.Web.Data;
using InfinApp.Web.Repositories;
using InfinApp.Web.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registrar servicios
builder.Services.AddScoped<IControlActividadRepository, ControlActividadRepository>();
builder.Services.AddScoped<IControlActividadService, ControlActividadService>();
builder.Services.AddScoped<IActividadColaboradorRepository, ActividadColaboradorRepository>();
builder.Services.AddScoped<IActividadColaboradorService, ActividadColaboradorService>();

// SQL Connection Factory
builder.Services.AddSingleton<SqlConnectionFactory>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=ControlActividad}/{action=Index}/{id?}");

app.Run();
