using ARS.Models;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Controllers
{
    public class UserController : Controller
    {
		private readonly MainDbContext db;

        public UserController(MainDbContext db)
        {
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

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}
	}
}
