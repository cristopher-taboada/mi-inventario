using Microsoft.EntityFrameworkCore;
using LOGIN.Data;

var builder = WebApplication.CreateBuilder(args);

// --- Servicios para la web ---
builder.Services.AddControllersWithViews();

// --- Configurar Supabase (URL y Anon Key) ---
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseAnonKey = builder.Configuration["Supabase:AnonKey"];

// Verificar que las variables no sean nulas
if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseAnonKey))
{
    throw new Exception("Faltan las variables de configuración de Supabase: Url o AnonKey");
}

// Inicializar el cliente de Supabase
builder.Services.AddSingleton<Supabase.Client>(sp =>
    new Supabase.Client(supabaseUrl, supabaseAnonKey));

// --- Configuración de sesiones ---
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();