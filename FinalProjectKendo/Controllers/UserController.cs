using FinalProjectKendo.DatabaseRepo;
using FinalProjectKendo.DataContext;
using FinalProjectKendo.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FinalProjectKendo.Controllers
{
    public class UserController : Controller
    {
       
        UserDataRepo repo;

        public UserController()
        {
            repo = new UserDataRepo();
        }
        // GET: User
        public ActionResult Index()
        {

            return View();
        }
       
        //code for register
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterPanel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPanel(UserData model)
        {
            int i = repo.RegisterUser(model);
            if (i >= 1)
            {
                int id = repo.GetUserId(model.u_email);
                return RedirectToAction("LoginPanel");
            }
            else
            {
                return View();
            }
        }
        //code for login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginPanel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPanel(string u_email, string u_password)
        {
            bool check = repo.signIn(u_email, u_password);
            if (check == true)
            {
                    ViewBag.message1 = "SucessFully Login";
                    return RedirectToAction("Index", "User");
                 
            }
            else
            {
                ViewBag.message2 = "Invalid Credential";
                return View();
            }
        }
        //code for state and city dropdown
         public JsonResult GetstateData()
        {
            var state = repo.GetAllstate();
            return Json(state, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityData(int id)
        {
            var city = repo.GetAllcity(id);

            var jsonresult = Json(city, JsonRequestBehavior.AllowGet);
            return jsonresult;
        }
        [Authorize]
        [HttpGet]
        public ActionResult SignOut(UserData model)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginPanel", "User");

        }
        
        
    }
}
