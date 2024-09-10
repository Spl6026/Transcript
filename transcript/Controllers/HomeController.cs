using FastReport;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Net;
using System.Xml.Linq;
using transcript.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace transcript.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            configuration = config;
            _webHostEnvironment = webHostEnvironment;
        }

        DataBase dataBase = new DataBase();

        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            ViewBag.DT = dataBase.showStu(dt, configuration.GetConnectionString("DefaultConnection"));
            return View();
        }

        public IActionResult generate(string stuno)
        {
            List<Courses> courses = dataBase.getCourse(stuno, configuration.GetConnectionString("DefaultConnection"));
            List<Student> stu = dataBase.getStudent(stuno, configuration.GetConnectionString("DefaultConnection"));
            byte[] pdf = GeneratePDF(stu, courses, stuno);
            return File(pdf, "application/pdf", DateTime.Now.ToString("yyyyMMdd_") + stuno + ".pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        byte[] GeneratePDF(List<Student> stu, List<Courses> courses, string stuno)
        {
            string ContentRootPath = _webHostEnvironment.ContentRootPath;

            List<Sign> sign = new List<Sign>();

            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();

            sign.Add(new Sign(Path.Combine(ContentRootPath, "img")));

            rep.Load(Path.Combine(ContentRootPath, "testc.frx"));
            rep.RegisterData(stu, "StudentRef");
            rep.RegisterData(courses, "CoursesRef");
            rep.RegisterData(sign, "SignRef");
            if (rep.Report.Prepare())
            {
                FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                pdfExport.ShowProgress = true;
                pdfExport.Subject = "Transcript";
                pdfExport.Title = stuno;
                pdfExport.Author = "NCYU";
                MemoryStream ms = new MemoryStream();
                rep.Report.Export(pdfExport, ms);
                rep.Dispose();
                pdfExport.Dispose();
                return ms.ToArray();
            }
            else
            {
                Debug.WriteLine("MyAction 已被執行");
                return null;
            }
        }
    }
}