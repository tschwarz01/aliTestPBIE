using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.PowerBI.Security;
using aliTestPBIE.ViewModels;

namespace aliTestPBIE.Controllers
{
    public class HomeController : Controller
    {
        private IConfigurationRoot _config;
        private string workspaceCollection;
        private string workspaceId;
        private string accessKey;
        private string apiUrl;
        private string reportId;

        public HomeController(IConfigurationRoot config)
        {
            _config = config;

            workspaceCollection = _config["PowerBI:WorkspaceCollection"];
            workspaceId = _config["PowerBI:WorkspaceId"];
            accessKey = _config["PowerBI:AccessKey"];
            apiUrl = _config["PowerBI:ApiUrl"];
            reportId = _config["PowerBI:ReportId"];
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewBag.AccessKey = accessKey;

            var embedToken = PowerBIToken.CreateReportEmbedToken(workspaceCollection, workspaceId, reportId);
            var jwt = embedToken.Generate(accessKey);

            var viewModel = new ReportKeyViewModel
            {
                AccessToken = jwt
            };


            return View(viewModel);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
