using Crawler.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crawler.Demo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new CrawlerModel();
            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(CrawlerModel model)
        {
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                var uri = new Uri(model.Uri);
                var crawler = new Api.Crawler(uri);
                var result = await crawler.StartCrawlerAsync(false);
                model.Links = result.Select(x => x.AbsoluteUri).ToList();
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}