using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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


        public JsonResult UploadDocuments()
        {
            string[] fileName = new string[Request.Files.Count];
            string[] actualFileName = new string[Request.Files.Count];
            int j = 0;
            bool ISValidUpload = true;
            if (Request.Files != null)
            {
                string TempFilePath = Server.MapPath("\\Upload\\TempDir");
                if (!Directory.Exists(TempFilePath))
                {
                    Directory.CreateDirectory(TempFilePath);
                }
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    actualFileName[j] = file.FileName;
                    //add file with guid
                    fileName[j] = Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", "").Replace("'", "").Trim()) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    try
                    {
                        string filePathName = Path.Combine(TempFilePath, fileName[i]);
                        file.SaveAs(filePathName);
                        j++;
                    }
                    catch (Exception)
                    {
                        ISValidUpload = false;
                    }
                }
            }
            //return the status and file path 
            return Json(new
            {
                ISValidUpload,
                UplodedFileName = fileName,
                ActualFileName = actualFileName
            });
        }

        [HttpGet]
        public JsonResult RemoveDocument(string fileName)
        {
            bool IsValidRemoved = false;
            if (fileName != null)
            {
                string TempFilePath = Server.MapPath("\\Upload\\TempDir");
                string OriginalFilePath = Server.MapPath("\\Upload\\Documents");

                string tempFilePathName = Path.Combine(TempFilePath, fileName);
                string orgFilePathName = Path.Combine(OriginalFilePath, fileName);

                //Delete from temp
                if (System.IO.File.Exists(tempFilePathName))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(tempFilePathName);
                }
                //Delete from originial
                if (System.IO.File.Exists(orgFilePathName))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(orgFilePathName);
                    IsValidRemoved = true;
                }
            }
            //return the status and file path 
            return Json(new
            {
                data = IsValidRemoved
            }, JsonRequestBehavior.AllowGet);
        }

    
    }
}