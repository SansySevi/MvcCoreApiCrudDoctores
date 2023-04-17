using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDoctores.Services;

namespace MvcCoreApiCrudDoctores.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceApiDoctores service;

        public DoctoresController(ServiceApiDoctores service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
