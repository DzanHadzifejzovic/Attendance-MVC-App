using FIsrtMVCapp.Filters;
using FIsrtMVCapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace FIsrtMVCapp.Controllers
{
    public class   HomeController : Controller
    {

        IStringLocalizer<SharedResource> localizer;

        IMemoryCache memoryCache;
        public HomeController(IStringLocalizer<SharedResource> localizer, IMemoryCache memoryCache)
        {
            this.localizer = localizer;
            this.memoryCache = memoryCache;
        }

        [Authorize]
        [HandleError]
        [MyAction]
        public IActionResult Index()
        {
            memoryCache.Set("message", "Hello from cache");

            //Console.WriteLine("Index get action method activated by redirect method from another action method");

            //throw new Exception(); //za isprobavanje exception filtera

            string firstNameText = this.localizer["First Name"].Value;

            Console.WriteLine(firstNameText);

            return View();
        }

        
        [HttpPost]
        [Authorize]
        public IActionResult Index(Person person)
        {
            /*
            Person person = new Person();
            person.FirstName = firstName;
            person.LastName = lastName;
            person.DateOfBirth = dateOfBirth; //ZAMENJENO JEDNIM PARAMETROM PERSON, ALI MORAJU SVOJSTVA DA IMAJU ISTA IMENA KAO I NAME ATRIBUT (NIJE BITNO JESU LIS LOVA MALA ILI VELIKA)

            ViewBag.AttendantAdded = firstName; //ukoliko vrsimo preusmeravanje ovim podacima se gubi pristup, pa cemo ih sacuyvati pomocu TempData ili cookie-ja ...
            //return View();

            // return View((object) firstName);

            // string fn = HttpContext.Request.Form["firstName"]; // u slucaju da nismo koristili automatsko mapiranje 
            


            if (string.IsNullOrEmpty(person.FirstName) || person.FirstName.Length <5)
            {
                ModelState.AddModelError("FirstName", "Please enter your name");
            }

            if (string.IsNullOrEmpty(person.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter your last name.");
            }

            if (person.DateOfBirth == null)
            {
                ModelState.AddModelError("DateOfBirth", "Please enter your date of birth.");
            }

            if ((person.DateOfBirth > DateTime.Now || person.DateOfBirth < DateTime.Now.AddYears(-150)))
            {
                ModelState.AddModelError("DateOfBirth", "Date of birth is out of bounds.");
            }
             */


            if (ModelState.IsValid)
            {

                Attendance.AddAttendant(person);
                TempData["FirstName"] = person.FirstName + " " + person.LastName;
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }

        public IActionResult AttendantDetails(string firstName,string lastName)
        {
            PeopleContext context = new PeopleContext();
            Person person = context.People.Where(x => x.FirstName.ToLower().Equals(firstName.ToLower()) && x.LastName.ToLower().Equals(lastName.ToLower())).FirstOrDefault();
       
            if(person == null)
            {
                return NotFound();
            }
            return View("Attendant",person);
                
        }

        public IActionResult SetCulture(string culture, string sourceUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return Redirect(sourceUrl);
        
        }

        // [ResponseCache(Duration =20,Location = ResponseCacheLocation.Client)]
        // [ResponseCache(CacheProfileName ="CacheProfile1")]
        // [ResponseCache(Duration =30,VaryByHeader ="user-agent")]
        [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "country" })]
        public IActionResult About()
        {
            var message = memoryCache.Get("message");

            Console.WriteLine(message);
            return View();
        }

    }
}
