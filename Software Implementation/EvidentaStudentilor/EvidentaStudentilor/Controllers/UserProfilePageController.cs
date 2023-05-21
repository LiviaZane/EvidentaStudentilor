using EvidentaStudentilor.LogicServices.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EvidentaStudentilor.Controllers
{
    public class UserProfilePageController : Controller
    {

        public UserProfilePageController()
        {
        }

        // GET: UserProfilePageController
        [HttpGet]
        public ActionResult Index()
        {
            string userId = HttpContext.Session.GetString("UserId");                                          // get userId from session variable UserId
            ViewBag.UserPicture = userId + ".jpg";   // temp data to send to view
            ViewBag.UserEmail = HttpContext.Session.GetString("UserName");
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            ViewBag.UserEntireName = HttpContext.Session.GetString("UserEntireName");
            ViewBag.UserProfile = HttpContext.Session.GetString("UserProfile");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)                                                 // after submit in view
        {
            await UploadFile(file);                                                                           // wait for UploadFile method implemented bellow
            ViewBag.Message = "File Uploaded successfully.";
            return RedirectToAction("Index");
        }


        
        private async Task<bool> UploadFile(IFormFile file)                                                   // upload file on server
        {
            try
            {
                if (file.Length > 0)
                {
                    string userId = HttpContext.Session.GetString("UserId");                                 // get userId from session variable UserId
                    string filename = userId.ToString() + ".jpg";
                    string path = @"wwwroot/userPictures";
                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))        // open a filestream and 
                    {
                        await file.CopyToAsync(filestream);                        // wait until file is copied as userId.jpg into the folder wwwroot/userPictures
                    }
                }
            }
            catch (Exception ex)                           // if something goes wrong with file saving on hard-disk
            {
                throw(ex);                                 // throw an exception 
            }
            return true;                                   // I tryed with void but doesn't work Task<void> and I used Task<bool> but need to return a bool value
        }
    }
}
