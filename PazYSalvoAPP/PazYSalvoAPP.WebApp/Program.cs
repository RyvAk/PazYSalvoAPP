using Microsoft.EntityFrameworkCore; // Referencia para configurar cadena de conexi�n
using PazYSalvoAPP.Business.Services;
// using PazYSalvoAPP.Data.Repositories;
using PazYSalvoAPP.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cadena de conexi�n
builder.Services.AddDbContext<PazSalvoContext>( c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
});

// Inyectar dependencias necesarias
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<IMediosDePagoService, MediosDePagoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
