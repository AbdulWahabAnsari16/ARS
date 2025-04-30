using ARS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ARS.Controllers
{
    public class AdminController : Controller
    {
        private readonly MainDbContext db;

        public AdminController(MainDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
			if (HttpContext.Session.GetInt32("id") == null)
			{
				return RedirectToAction("Login");
			}
            var list = db.Users.ToList();
            return View(list);
        }

        public IActionResult DeleteUser(int? id)
        {
            var data = db.Users.Find(id);
            db.Users.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Authentication Section

        public IActionResult Login()
        {
			if (HttpContext.Session.GetInt32("id") != null)
			{
				return RedirectToAction("Index");
			}
			return View();
        }
        [HttpPost]
        public IActionResult Login(AdminLogin adminLogin)
        {
            var admin = db.AdminLogin.Where(db => db.Email == adminLogin.Email && db.Password == adminLogin.Password).FirstOrDefault();
            if (admin != null)
            {
                HttpContext.Session.SetInt32("id", admin.Id);
                HttpContext.Session.SetString("name", admin.Name);
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
			return RedirectToAction("Login");
		}

        // City Section
        public IActionResult AddCity()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddCity(City cities)
        {
            db.Cities.Add(cities);
            db.SaveChanges();
            //ViewBag.msg = "city inserted";
            return RedirectToAction("ViewCities");
        }
        public IActionResult ViewCities()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.Cities.ToList();
            return View(list);
        }
        public IActionResult EditCity(int? id)
        {
            var data = db.Cities.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditCity(City city)
        {
            db.Cities.Update(city);
            db.SaveChanges();
            //ViewBag.msg = "updated";
            return RedirectToAction("ViewCities");
        }
        public IActionResult DeleteCity(int? id)
        {
            var data = db.Cities.Find(id);
            db.Cities.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewCities");
        }


        
        // Airport Section
        public IActionResult AddAirport()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var cities = db.Cities.ToList();
            ViewBag.cities = new SelectList(cities, "CityId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddAirport(Airport airport)
        {
            db.Airports.Add(airport);
            db.SaveChanges();
            //ViewBag.msg = "Inserted";
            return RedirectToAction("ViewAirports");
        }
        public IActionResult ViewAirports()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.Airports.Include(cities => cities.City);
            return View(list);
        }
        public IActionResult EditAirport(int? id)
        {
            var data = db.Airports.Find(id);
            var cities = db.Cities.ToList();
            ViewBag.cities = new SelectList(cities, "CityId", "Name");
            return View(data);
        }
        [HttpPost]
        public IActionResult EditAirport(Airport airport)
        {           
            db.Airports.Update(airport);
            db.SaveChanges();
            //ViewBag.msg = "updated";
            return RedirectToAction("ViewAirports");
        }
        public IActionResult DeleteAirport(int? id)
        {
            var data = db.Airports.Find(id);
            db.Airports.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewAirports");
        }


        // FlightRoutes Section
        public IActionResult AddFltRoute()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var airports = db.Airports.ToList();
            ViewBag.airport = new SelectList(airports, "AirportId", "IATACode");
            return View();
        }
        [HttpPost]
        public IActionResult AddFltRoute(FlightRoutes routes)
        {
            db.FlightRoutes.Add(routes);
            db.SaveChanges();
            //ViewBag.msg = "Inserted";
            var airports = db.Airports.ToList();
            ViewBag.airport = new SelectList(airports, "AirportId", "IATACode");
            return RedirectToAction("ViewFltRoutes");
        }

        public IActionResult ViewFltRoutes()
        {
            if (HttpContext.Session.GetInt32("id") == null)
			{
				return RedirectToAction("Login");
			}
            var list = db.FlightRoutes
                         .Include(fr => fr.OriginAirport)
                         .Include(fr => fr.DestinationAirport)
                         .ToList();
            return View(list);
        }
        public IActionResult EditFltRoute(int? id)
        {         
            var data = db.FlightRoutes.Find(id);
            var airports = db.Airports.ToList();
            ViewBag.airport = new SelectList(airports, "AirportId", "IATACode");
            return View(data);
        }
        [HttpPost]
        public IActionResult EditFltRoute(FlightRoutes flightRoute)
        {
            db.FlightRoutes.Update(flightRoute);
            db.SaveChanges();
            return RedirectToAction("ViewFltRoutes");
        }
        public IActionResult DeleteFltRoute(int? id)
        {
            var data = db.FlightRoutes.Find(id);
            db.FlightRoutes.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewFltRoutes");
        }

        // Flight Section
        public IActionResult AddFlight()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddFlight(Flight flight)
        {
            db.Flights.Add(flight);
            db.SaveChanges();
            return RedirectToAction("VewFlights");
        }
        public IActionResult VewFlights()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.Flights.ToList();
            return View(list);
        }

        public IActionResult EditFlight(int? id)
        {
            var data = db.Flights.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditFlight(Flight flight)
        {
            db.Flights.Update(flight);
            db.SaveChanges();
            return RedirectToAction("VewFlights");
        }

        public IActionResult DeleteFlight(int? id)
        {
            var data = db.Flights.Find(id);
            db.Flights.Remove(data);
            db.SaveChanges();
            return RedirectToAction("VewFlights");
        }

        // Class Section
        public IActionResult AddClass()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddClass(Class classes)
        {
            db.Classes.Add(classes);
            db.SaveChanges();
            return RedirectToAction("ViewClasses");
        }

        public IActionResult ViewClasses()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.Classes.ToList();
            return View(list);
        }
        public IActionResult EditClass(int? id)
        {
            var data = db.Classes.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditClass(Class classes)
        {
            db.Classes.Update(classes);
            db.SaveChanges();
            return RedirectToAction("ViewClasses");
        }
        public IActionResult DeleteClass(int? id)
        {
            var data = db.Classes.Find(id);
            db.Classes.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewClasses");
        }


        // FlightSchedule Section
        public IActionResult AddFltSchedule()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var Flight = db.Flights.ToList();
            ViewBag.flights = new SelectList(Flight, "FlightId", "FlightNumber");
            var airports = db.Airports.ToList();
            ViewBag.airports = new SelectList(airports, "AirportId", "IATACode");
            var classes = db.Classes.ToList();
            ViewBag.classes = new SelectList(classes, "ClassId", "ClassName");
            return View();
        }
        [HttpPost]
        public IActionResult AddFltSchedule(FlightSchedule flightSchedule)
        {
            db.FlightSchedules.Add(flightSchedule);
            db.SaveChanges();
            ViewBag.msg = "Inserted";
            return RedirectToAction("ViewFltSchedules");
        }
        public IActionResult ViewFltSchedules()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.FlightSchedules
                         .Include(fr => fr.DepartureAirport)
                         .Include(fr => fr.ArrivalAirport)
                         .Include(fr => fr.Flight)
                         .Include(fr => fr.Class)
                         .ToList();
            return View(list);
        }
        public IActionResult EditFltSchedule(int? id)
        {
            var data = db.FlightSchedules.Find(id);
            var Flight = db.Flights.ToList();
            ViewBag.flights = new SelectList(Flight, "FlightId", "FlightNumber");
            var airports = db.Airports.ToList();
            ViewBag.airports = new SelectList(airports, "AirportId", "IATACode");
            var classes = db.Classes.ToList();
            ViewBag.classes = new SelectList(classes, "ClassId", "ClassName");
            return View(data);
        }
        [HttpPost]
        public IActionResult EditFltSchedule(FlightSchedule flightSchedule)
        {
            db.FlightSchedules.Update(flightSchedule);
            db.SaveChanges();
            return RedirectToAction("ViewFltSchedules");
        }
        public IActionResult DeleteFltSchedule(int? id)
        {
            var data = db.FlightSchedules.Find(id);
            db.FlightSchedules.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewFltSchedules");
        }

        // PricingRules Section
        public IActionResult AddPricingRule()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var classes = db.Classes.ToList();
            ViewBag.classes = new SelectList(classes, "ClassId", "ClassName");
            return View();
        }
        [HttpPost]
        public IActionResult AddPricingRule(PricingRule pricingRule)
        {
            db.PricingRules.Add(pricingRule);
            db.SaveChanges();
            return RedirectToAction("ViewPricingRules");
        }
        public IActionResult ViewPricingRules()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.PricingRules.Include(classes => classes.Class);
            return View(list);
        }
        public IActionResult EditPricingRule(int? id)
        {
            var data = db.PricingRules.Find(id);
            var classes = db.Classes.ToList();
            ViewBag.classes = new SelectList(classes, "ClassId", "ClassName");
            return View(data);
        }
        [HttpPost]
        public IActionResult EditPricingRule(PricingRule pricingRule)
        {
            db.PricingRules.Update(pricingRule);
            db.SaveChanges();
            return RedirectToAction("ViewPricingRules");
        }
        public IActionResult DeletePricingRule(int? id)
        {
            var data = db.PricingRules.Find(id);
            db.PricingRules.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewPricingRules");
        }

        // CancellationPolicies Section
        public IActionResult AddCnclPolicy()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddCnclPolicy(CancellationPolicy cnclpolicy)
        {
            db.CancellationPolicies.Add(cnclpolicy);
            db.SaveChanges();
            return RedirectToAction("ViewCnclPolicies");
        }
        public IActionResult ViewCnclPolicies()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            var list = db.CancellationPolicies.ToList();
            return View(list);
        }
        public IActionResult EditCnclPolicy(int? id)
        {
            var data = db.CancellationPolicies.Find(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult EditCnclPolicy(CancellationPolicy cnclpolicy)
        {
            db.CancellationPolicies.Update(cnclpolicy);
            db.SaveChanges();
            return RedirectToAction("ViewCnclPolicies");
        }

        public IActionResult DeleteCnclPolicy(int? id)
        {
            var data = db.CancellationPolicies.Find(id);
            db.CancellationPolicies.Remove(data);
            db.SaveChanges();
            return RedirectToAction("ViewCnclPolicies");
        }
    }
}
