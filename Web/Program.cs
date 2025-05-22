using Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración
builder.Services.Configure<MicroserviceSettings>(
    builder.Configuration.GetSection("MicroserviceSettings"));

// Agregar servicios necesarios
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Recomendado
builder.Services.AddSession();    // Para guardar sesión

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
app.UseSession(); // Activar el middleware de sesión
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
