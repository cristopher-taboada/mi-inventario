using Microsoft.AspNetCore.Mvc;
using LOGIN.Models;
using Supabase;

namespace LOGIN.Controllers
{
    public class AccountController : Controller
    {
        private readonly Supabase.Client _supabase;

        public AccountController(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _supabase
                    .From<Usuario>()
                    .Where(u => u.Email == model.Email && u.Password == model.Password)
                    .Get();

                var usuario = response.Models.FirstOrDefault();

                if (usuario != null)
                {
                    HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                    HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre ?? "");
                    HttpContext.Session.SetString("UsuarioEmail", usuario.Email ?? "");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email o contraseña incorrectos");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var existResponse = await _supabase
                    .From<Usuario>()
                    .Where(u => u.Email == usuario.Email)
                    .Get();

                if (existResponse.Models.Any())
                {
                    ModelState.AddModelError("Email", "Este email ya está registrado");
                    return View(usuario);
                }

                usuario.FechaRegistro = DateTime.Now;
                await _supabase.From<Usuario>().Insert(usuario);

                TempData["Mensaje"] = "Registro exitoso. ¡Inicia sesión!";
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}