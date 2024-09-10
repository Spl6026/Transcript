using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using transcript.Models;

namespace transcript.Controllers
{
    public class InsertController : Controller
    {
        DataBase dataBase = new DataBase();

        private readonly IConfiguration configuration;

        public InsertController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            configuration = config;
        }

        public IActionResult student(stu Student)
        {
            dataBase.setStu(Student, configuration.GetConnectionString("DefaultConnection"));
            return RedirectToAction("Addstudent");
        }

        public IActionResult course(stu_crs Crs)
        {
            dataBase.setCrs(Crs, configuration.GetConnectionString("DefaultConnection"));
            return RedirectToAction("Addcourse");
        }

        public IActionResult Addstudent()
        {
            return View("Addstudent");
        }

        public IActionResult Addcourse() 
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            ViewBag.DT1 = dataBase.showStu(dt1, configuration.GetConnectionString("DefaultConnection"));
            ViewBag.DT2 = dataBase.showCrs(dt2, configuration.GetConnectionString("DefaultConnection"));
            return View("Addcourse");
        }

        public JsonResult GetStuCrs([FromBody]int stuno)
        {
            List<stu_crs> crs = dataBase.GetStuCrs(stuno, configuration.GetConnectionString("DefaultConnection"));
            Debug.WriteLine(crs);
            return Json(crs);
        }

        public IActionResult DelStuCrs([FromBody] stu_crs crs)
        {
            Debug.WriteLine(crs.stu_course_no);
            dataBase.DelStuCrs(crs, configuration.GetConnectionString("DefaultConnection"));
            return Ok();
        }
    }
}
