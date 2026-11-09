using Microsoft.AspNetCore.Mvc;
public class AccountController(RaceDayApi api):Controller{
 [HttpGet]public IActionResult Login()=>View(new LoginVm());
 [HttpPost][ValidateAntiForgeryToken]public async Task<IActionResult> Login(LoginVm m){if(!ModelState.IsValid)return View(m);var r=await api.Login(m.Email,m.Password);if(!r.ok){ModelState.AddModelError("","Login failed.");return View(m);}return RedirectToAction("Index","Dashboard");}
 [HttpGet]public IActionResult Register()=>View(new RegisterVm());
 [HttpPost][ValidateAntiForgeryToken]public async Task<IActionResult> Register(RegisterVm m){if(!ModelState.IsValid)return View(m);var r=await api.Send(HttpMethod.Post,"api/auth/register",m);if(!r.ok){ModelState.AddModelError("","Registration failed: "+r.body);return View(m);}TempData["Message"]="Registration successful. Please log in.";return RedirectToAction(nameof(Login));}
 public async Task<IActionResult> Logout(){await api.Send<object>(HttpMethod.Post,"api/auth/logout");HttpContext.Session.Clear();return RedirectToAction(nameof(Login));}
}