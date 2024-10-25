using ConsumeAPI.Models;
using ConsumeAPI.Services;
using Microsoft.AspNetCore.Mvc;
namespace ConsumeAPI.Controllers
{
   
        public class AccountController : Controller
        {
            private readonly IAuthService _authService;
            public AccountController(IAuthService authService)
            {
                _authService = authService;
            }
            // GET: /Account/Login
            public IActionResult Login()
            {
                return View(); // Make sure this points to Views/Account/Login.cshtml
            }

            [HttpPost]
            public async Task<IActionResult> Login(UserLogin loginModel)
            {
                if (ModelState.IsValid)
                {
                    // Authenticate and retrieve the token
                    string token = await _authService.AuthenticateAsync(loginModel);

                    if (!string.IsNullOrEmpty(token))
                    {
                        // Store the token in session
                        HttpContext.Session.SetString("JWToken", token);

                        // Redirect to the dashboard on successful login
                        return RedirectToAction("GetAllProduct", "Consume");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }

                return View(loginModel);
            }

        }

}
