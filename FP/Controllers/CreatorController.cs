using FP.DAL;
using FP.DAL.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FP.Controllers
{
    public class CreatorController : Controller
    {
        // GET: Creator

        #region CreatorBasic Profile
        public ActionResult Index()
        {
            // var res = GetUnReadMsg("");

            // ViewBag.MessageCount = JsonConvert.SerializeObject(res.Data);
            //var a = JObject.Parse(ViewBag.MessageCount);
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        [HttpPost]
        public JsonResult SaveCreator(CreatorModal objData)
        {
            Creator objCreator = new Creator();

            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string userId = Session["UserId"].ToString();
            string fileName = "";
            try
            {
                string[] fileArray = Directory.GetFiles(Server.MapPath("\\Upload\\Profile\\" + userId));
                if (fileArray.Length > 0)
                {
                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        string item = fileArray[i];
                        fileName = Path.GetFileName(item);
                    }
                }
            }
            catch
            {
               
            }
            if(fileName!="")
            fileName= "~/Upload/Profile/" + userId + "/" + fileName;

            var result = objCreator.SaveCreator(objData, userId, fileName);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        [HttpGet]
        public JsonResult GetUnReadMsg(string Email)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator objCreator = new Creator();
            string userId = Session["UserId"].ToString();
            var result = objCreator.GetUnReadMsg(userId);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCreatorInfo(string UserId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator objCreator = new Creator();
                string userId = "";
                if (UserId == "" || UserId == null)
                {
                    userId = Session["UserId"].ToString();
                }
                else
                {
                    userId = UserId;
                }
                var result = objCreator.GetCreatorInfo(userId);

                return Json(new
                {
                    data = result,
                    statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
           
        }

        [HttpGet]
        public JsonResult GetCountry()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator objCreator = new Creator();
            var result = objCreator.GetCountry();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MailBox
        public ActionResult CreatorMailbox()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to get list of Mailbox
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMailboxList(int MailTypeId, string MailBoxFilterBy)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator obj = new Creator();
            string userId = Session["UserId"].ToString();
            var result = obj.GetMailboxList(MailTypeId, MailBoxFilterBy, userId);
            HttpContext.Session["MailTypeId"] = MailTypeId;
            HttpContext.Session["MailBoxFilterBy"] = MailBoxFilterBy;
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular Mail detail
        /// </summary>
        /// <param name="MailboxId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMailboxById(int MailboxId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator obj = new Creator();
            string userId = Session["UserId"].ToString();
            var result = obj.GetMailboxById(MailboxId, userId);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to seach mail form any text
        /// </summary>
        /// <param name="MailBoxSearchByText"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMailBoxByText(string MailBoxSearchByText)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator obj = new Creator();

            var result = obj.GetMailBoxByText(MailBoxSearchByText, HttpContext.Session["MailTypeId"].ToString(), HttpContext.Session["MailBoxFilterBy"].ToString());

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// used to remove mailbox
        /// </summary>
        /// <param name="MailboxId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveMailboxs(int MailboxId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator obj = new Creator();

            var result = obj.RemoveMailboxs(MailboxId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }

        [HttpPost]
        public JsonResult SaveMailbox(MailboxModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator obj = new Creator();
            var result = obj.ReplyMailbox(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }


        #endregion

        public ActionResult Projects()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        #region Wallet
        public ActionResult Wallet()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to get list of Mailbox
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetWalletAmountList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string userId = Session["UserId"].ToString();
            WalletDetails obj = new WalletDetails();
            var result = obj.GetWalletAmountList(userId);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to save product category
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveWalletDetails(WalletAmountModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string userId = Session["UserId"].ToString();
            WalletDetails objCategory = new WalletDetails();
            var result = objCategory.SaveWalletDetails(objData, userId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

       
        #endregion

        /// <summary>
        /// Used to get profile Image
        /// </summary>
         /// <returns></returns>
        //[HttpGet]
        public JsonResult GetProfileImg(string UserId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string fileName = "";
            string userId = "";
            if (UserId == null)
            {
                userId = Session["UserId"].ToString();
            }
            else
                userId = UserId;// Session["UserId"].ToString();
            try
            {
                //string[] fileArray = Directory.GetFiles(@"D:\Gaurav_Project_2020\FP_OLD\FP\FP\Upload\Profile\" + userId);
                string[] fileArray = Directory.GetFiles(Server.MapPath("\\Upload\\Profile\\" + userId));
                //  string[] fileArray = Directory.GetFiles("\\Upload\\TempDir\\Messages\\" + userId);
                if (fileArray.Length > 0)
                {

                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        string item = fileArray[i];
                        fileName = Path.GetFileName(item);
                    }
                }

                return Json(new
                {
                    // data = fileName,
                    UserId = userId,
                    FileName = fileName
                });
            }
            catch
            {
                return Json(new
                {
                    // data = fileName,
                    UserId = "",
                    FileName = ""
                });
            }
        }

        public JsonResult UploadImage()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string[] fileName = new string[Request.Files.Count];
            string[] actualFileName = new string[Request.Files.Count];
            int j = 0;
            string userId = Session["UserId"].ToString();
            bool ISValidUpload = true;
            if (Request.Files != null)
            {
                
                string OriginalFilePath = Server.MapPath("\\Upload\\Profile\\" + userId);
                if (Directory.Exists(OriginalFilePath))
                {


                    Directory.Delete(OriginalFilePath, true);
                }
                if (!Directory.Exists(OriginalFilePath))
                {
                    Directory.CreateDirectory(OriginalFilePath);
                }
                //else

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    actualFileName[j] = file.FileName;
                    //add file with guid
                    fileName[j] = Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", "").Replace("'", "").Trim()) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    try
                    {
                      
                        string originalfilePathName = Path.Combine(OriginalFilePath, fileName[i]);
                        file.SaveAs(originalfilePathName);

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
                ActualFileName = actualFileName, 
                UserId= userId
            });
        }


        [HttpGet]
        public JsonResult GetCampaignListForCreator()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string userId = Session["UserId"].ToString();
            Projects obj = new Projects();
            var result = obj.GetCampaignListForCreator();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCampaignByFillter(CampaignFillterModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }

            string userId = Session["UserId"].ToString();
            Projects obj = new Projects();
            var result = obj.GetCampaignByFillter(objData);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSupplementalChannelList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }

            Projects obj = new Projects();
            var result = obj.GetSupplementalChannelList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetYouTubeTypeList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Projects obj = new Projects();
            var result = obj.GetYouTubeTypeList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjectProposalById(int ProjectId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Projects obj = new Projects();
            Session["ProjectId"] = ProjectId;
            string userId = Session["UserId"].ToString();
            var result = obj.GetProjectProposalById(ProjectId, userId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProposalDocumentById(int ProjectId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Projects obj = new Projects();
            string userId = Session["UserId"].ToString();
            var result = obj.GetProposalDocumentById(ProjectId, userId);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteDocument(int BrandMailFileId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Mailbox obj = new Mailbox();
            string userId = Session["UserId"].ToString();
            var result = obj.DeleteDocument(BrandMailFileId, userId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);

        }

        ///Send SendProposalUpdate to client
        ///By: Gaurav
        ///Date: 6/21/2020
        [HttpPost]
        public JsonResult SendProposalUpdate(ProjectProposalUpdateModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string userId = Session["UserId"].ToString();
            Projects obj = new Projects();

            var result = obj.SendProposalUpdate(objData, userId);
            if (result != -1)
            {

                string[] fileArray = Directory.GetFiles(Server.MapPath("\\Upload\\ProjectProposal\\" + userId)); //Directory.GetFiles(@"D:\Gaurav_Project_2020\FP_OLD\FP\FP\Upload\ProjectProposal\" + userId);
                                                                                                                 //string[] fileArray1 = Directory.GetFiles(Server.MapPath("\\Upload\\ProjectProposal\\" + userId));
                if (fileArray.Length > 0)
                {
                    int id = result;
                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        string item = fileArray[i];
                        string fileName = "";

                        fileName = Path.GetFileName(item);
                        string[] orgFileNameArr = fileName.Split('~');
                        string orgFileName = orgFileNameArr[0] + Path.GetExtension(fileName);
                        //for move file temp to org path
                        string sourcePath = Server.MapPath("\\Upload\\ProjectProposal\\" + userId);
                        string targetPath = Server.MapPath("\\Upload\\Files\\" + userId);
                        if (!Directory.Exists(targetPath))
                        {
                            Directory.CreateDirectory(targetPath);
                        }

                        string sourceFile = Path.Combine(sourcePath, fileName);
                        string destFile = Path.Combine(targetPath, fileName);
                        System.IO.File.Move(sourceFile, destFile);
                        //End for move temp to org path
                        string filePath = "../Upload/Files/" + userId + "/" + fileName;

                        result = obj.ProjectProposalUpdatesFile(id, orgFileName, filePath);


                    }
                }

                //if (Directory.Exists(@"D:\Gaurav_Project_2020\FP_OLD\FP\FP\Upload\ProjectProposal\" + userId))
                //{
                //    Directory.Delete(@"D:\Gaurav_Project_2020\FP_OLD\FP\FP\Upload\ProjectProposal\" + userId );
                //}
            }
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        [HttpGet]
        public JsonResult GetCreatorList( )
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            // if(Session["ClientIsApproved"].ToString() != "Client") { 
            Projects obj = new Projects();
            var result = obj.GetCreatorList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
                   
        }

        [HttpGet]
        public JsonResult GetCreatorInfoForClient(string UserId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator objCreator = new Creator();
            string userId = "";
            if (UserId == "" || UserId == null)
            {
                userId = Session["UserId"].ToString();
            }
            else
            {
                userId = UserId;
            }
            var result = objCreator.GetCreatorInfo(userId);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCreatorFeedBack(string UserId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Creator objCreator = new Creator();
            string userId = "";
            if (UserId == "" || UserId == null)
            {
                userId = Session["UserId"].ToString();
            }
            else
            {
                userId = UserId;
            }
            var result = objCreator.GetCreatorFeedBack(userId);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCreatorByFillter(CreatorFillterModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string userId = Session["UserId"].ToString();
            Creator objCreator = new Creator();
            var result = objCreator.GetCreatorByFillter(objData);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }
    }
}