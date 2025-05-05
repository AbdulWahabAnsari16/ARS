using ARS.Models;
using ARS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ARS.Controllers
{
    public class UserController : Controller
    {
		private readonly MainDbContext db;
        private readonly EmailService email;

        public UserController(MainDbContext db, EmailService e)
        {
            email = e;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Packages()
		{
			return View();
		}
		public IActionResult Hotels()
		{
			return View();
		}
		public IActionResult Insurance()
		{
			return View();
		}
		public IActionResult BlogHome()
		{
			return View();
		}
		public IActionResult BlogSingle()
		{
			return View();
		}
		public IActionResult Elements()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Contact(Contact contact)
		{
			db.Contacts.Add(contact);
			db.SaveChanges();
			ViewBag.msg = "Form submited successfully";
			return View();
		}

		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
        public IActionResult SignUp(User u)
        {
            var user = new User
            {
                Username = u.Username,
                Email = u.Email,
                Password = u.Password,    // ← now populated
                IsGuest = false,
                CreatedAt = DateTime.UtcNow
            };
            db.Users.Add(user);
			db.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {          
            return View();
        }
        [HttpPost]
        public IActionResult Login(User userlogin)
        {
			var user = db.Users.Where(db => db.Email == userlogin.Email && db.Password == userlogin.Password).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetInt32("id", user.UserId);
                HttpContext.Session.SetString("name", user.Username);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.err = "Login Failed";
            }
            return View();
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }
		[HttpPost]
        public IActionResult VerifyEmail(User u)
        {
            var user = db.Users.Where(db => db.Email == u.Email).FirstOrDefault();
            if (user != null)
            {
                string randomString = GenerateRandomString();
                email.SendEmail(u.Email, "Code", $"Your Verification Code is {randomString}");
                verificationCode newCode = new verificationCode
                {
                    vCode = randomString
                };
                TempData["userEmail"] = u.Email;
                db.verificationCodes.Add(newCode);
                db.SaveChanges();
                return RedirectToAction("VerifyCode");
            }
            ViewBag.err = "Email does not exist in our database";
            return View();
        }
        public static string GenerateRandomString(int length = 6)
        {
            const string characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] randomString = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomString[i] = characters[random.Next(characters.Length)];
            }

            return new string(randomString);
        }

        public IActionResult VerifyCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerifyCode(verificationCode code)
        {
            var check = db.verificationCodes.Where(db => db.vCode == code.vCode).FirstOrDefault();
            if (check != null)
            {
                return RedirectToAction("ForgotPass");
            }
            ViewBag.err = "VerificationCode does not exist in our database";
            return View();
        }

        public IActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPass(User pass)
        {
            string newPassword = Request.Form["NewPassword"];
            string confirmPassword = Request.Form["ConfirmPassword"];
            string userEmail = TempData["userEmail"] as string;

            if (string.IsNullOrEmpty(userEmail))
            {
                ViewBag.err = "Email not found. Please request a password reset.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.err = "Passwords do not match.";
                TempData["userEmail"] = userEmail;
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                ViewBag.err = $"User not found with the provided email: {userEmail}";
                TempData["userEmail"] = userEmail;
                return View();
            }

            user.Password = newPassword;
            db.SaveChanges();

            return RedirectToAction("Login");
        }



        public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}
	}
}
