using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductionSystem.Domain.IRepositories;
using System.Threading.Tasks;

namespace ProductionSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            
            if (HttpContext.Session.GetString("UserId") != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null) return RedirectToAction("Login");

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "رمز جدید و تکرار آن یکسان نیستند";
                return RedirectToAction("Profile");
            }

            var user = await _unitOfWork.Users.GetByIdAsync(int.Parse(userId));
            if (user == null || !BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
            {
                TempData["Error"] = "رمز عبور فعلی اشتباه است";
                return RedirectToAction("Profile");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "رمز عبور با موفقیت تغییر یافت";
            return RedirectToAction("Profile");
        }
        public IActionResult GetHash(string pass)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(pass);
            return Content(hash);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است";
                return View();
            }

            if (!user.IsActive)
            {
                ViewBag.Error = "حساب کاربری شما غیرفعال است. با مدیر سیستم تماس بگیرید";
                return View();
            }

            if (user != null && user.IsActive && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("FullName", user.FullName);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("RoleId", user.RoleId);

                // ذخیره Permission ها
                var permissions = "";
              

                if (user.Id==1)
                {
                    permissions += "User" + ",";
                    permissions += "Role" + ",";
                    permissions += "Unit" + ",";
                    permissions += "Personnel" + ",";
                    permissions += "Customer" + ",";
                    permissions += "Parameter" + ",";
                    permissions += "Product" + ",";
                    permissions += "Order" + ",";
                    permissions += "ProductionReceipt" + ",";
                    permissions += "WasteReceipt" + ",";
                    permissions += "Report" + ",";
                }
                else
                {
                    if (user.Role != null && user.Role.RolePermissions != null)
                    {
                        foreach (var rp in user.Role.RolePermissions)
                        {
                            permissions += rp.PermissionKey + ",";
                        }
                    }
                }


                    HttpContext.Session.SetString("Permissions", permissions);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}