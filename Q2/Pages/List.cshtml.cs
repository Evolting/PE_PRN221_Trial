using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Entity;
using System.Data.SqlTypes;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Q2.Pages
{
    public class ListModel : PageModel
    {
        PRN221_TrialContext context = new PRN221_TrialContext();

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ListModel(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
            List<Service> services = context.Services.Include(s => s.EmployeeNavigation).Where(s => s.Month == 3).ToList();

            ViewData["services"] = services;
        }

        public void OnPostSearch(String title)
        {
            List<Service> services = context.Services.Include(s => s.EmployeeNavigation).Where(s => s.RoomTitle.Contains(title)).ToList();

            ViewData["services"] = services;
        }

        public async Task OnPostImport(IFormFile xmlFile)
        {
            List<Service> services = new List<Service>();

            if (xmlFile != null)
            {
                var file = Path.Combine(_environment.ContentRootPath, "uploads", xmlFile.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await xmlFile.CopyToAsync(fileStream);
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(ServiceList));
            using (FileStream fileStream = new FileStream(Path.Combine(_environment.ContentRootPath, "uploads", xmlFile.FileName), FileMode.Open))
            {
                ServiceList result = (ServiceList)serializer.Deserialize(fileStream);
                services = result.Services;
            }

            foreach (var s in services)
            {
                s.Id = 0;

                context.Services.Add(s);
                context.SaveChanges();
            }

            List<Service> allServices = context.Services.Include(s => s.EmployeeNavigation).Where(s => s.Month == 3).ToList();

            ViewData["services"] = allServices;
        }
    }
}
