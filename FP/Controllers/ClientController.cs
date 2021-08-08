using FP.DAL;
using FP.DAL.Classes;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace FP.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        public ActionResult Welcome()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            Creator obj = new Creator();
            string userId = Session["UserId"].ToString();
            var result = obj.GetClientDetail(userId);
            if (result == null)
            {
                result = "Client";
            }
            HttpContext.Session["ClientIsApproved"] = result;
            return View();
        }

        public ActionResult Creator()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }


        #region Mailbox
        public ActionResult Mailbox()
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
            Mailbox obj = new Mailbox();
            string userId = Session["UserId"].ToString();
            var result = obj.GetMailboxList(MailTypeId, MailBoxFilterBy, userId);
            HttpContext.Session["MailTypeId"]= MailTypeId;
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
            Mailbox obj = new Mailbox();
            string userId = Session["UserId"].ToString();
            string currencyType = "USD";
            if (Session["CurrencyType"] != null)
            {
                currencyType = Session["CurrencyType"].ToString();
            }
            var result = obj.GetMailboxById(MailboxId, userId, currencyType);

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
            Mailbox obj = new Mailbox();
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
            Mailbox obj = new Mailbox();
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
            Mailbox objMailbox = new Mailbox();
            string userId = Session["UserId"].ToString();
            var result = objMailbox.ReplyMailbox(objData, userId);
            if(result != -1 || result != 0)
            {
                
                try
                {
                    string[] fileArray = Directory.GetFiles(Server.MapPath("\\Upload\\Messages\\" + userId)); 
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
                            string sourcePath = Server.MapPath("\\Upload\\Messages\\" + userId);
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

                            result = objMailbox.SaveMailBoxFile(id, orgFileName, filePath);
                        }
                    }

                }
                catch
                {

                }
            }
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }


        /// <summary>
        /// by: Gaurav
        /// Desc: Get brand mail document
        /// </summary>
        /// <param name="MailId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDocument(int MailboxId)
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
            var result = obj.GetDocument(MailboxId);
          
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult dwnfile(byte[] fileData)
        {
            MemoryStream ms = new MemoryStream(fileData);
            return new FileStreamResult(ms, "application/vnd.ms-excel");
        }
        [HttpGet]
        public ActionResult DownloadDocument(int BrandMailFileId)
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
           var result = obj.GetDocumentForDownload(BrandMailFileId);

            foreach (var item in result)
            {
                var a = item;
                byte[] bytes = a.FileData;
                var d=  dwnfile(bytes);
                try {
                                       
                    string fileName = a.FileName;
                    
                    Response.Clear();
                    MemoryStream ms = new MemoryStream(bytes);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment;filename=labtest.xls");
                    Response.Buffer = true;
                    ms.WriteTo(Response.OutputStream);
                    Response.End();
                                    }
                catch (Exception ex)
                {

                }
                }
            return null;

        }

        [HttpPost]
        public JsonResult ApproveProposal(int MailboxId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Mailbox objMailbox = new Mailbox();
            string userId = Session["UserId"].ToString();

            var result = objMailbox.ApproveProposal(MailboxId, userId);
            return Json(new
            {
                statusCode = result > -1 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        [HttpPost]
        public JsonResult SendProposalUpdate(int MailboxId, int UpdateValue)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Mailbox objMailbox = new Mailbox();
            string userId = Session["UserId"].ToString();

            var result = objMailbox.SendProposalUpdate(MailboxId, UpdateValue, userId);
            return Json(new
            {
                statusCode = result > -1 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        #endregion

        #region Proposals

        public ActionResult Projects()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to get list of Project list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProjectList()
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
            string convertToCurrency = "USD";
            if (Session["CurrencyType"] != null)
            {
                convertToCurrency = Session["CurrencyType"].ToString();
            }
            var result = obj.GetProjectList(userId, convertToCurrency);
           
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular Project detail
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProjectById(int ProjectId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            ProductCategory objCategory = new ProductCategory();
            var resultProductCateegoryList = objCategory.GetProductCategoryList();


            Projects obj = new Projects();
            var resultYouTubeList = obj.GetYouTubeTypeList();

            Session["ProjectId"] = ProjectId;
            string userId = Session["UserId"].ToString();
            string convertToCurrency = "USD";
            if (Session["CurrencyType"] != null)
            {
                convertToCurrency = Session["CurrencyType"].ToString();
            }
            var result = obj.GetProjectById(ProjectId, userId, convertToCurrency);
            return Json(new
            {
                data = result,
                dataProductList= resultProductCateegoryList,
                dataYouTubeList= resultYouTubeList,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to save/update proposal
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveProposal(ProjectsProposalModal objData)
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
            string ProjectId = Session["ProjectId"].ToString();
            string userId = Session["UserId"].ToString();
            string currencyType = "USD";
            if (Session["CurrencyType"] != null)
            {
                currencyType = Session["CurrencyType"].ToString();
            }
            var result = obj.SaveProposal(objData, ProjectId, userId, currencyType);//todo
            if (result != -1)
            {
                try {
                    string[] fileArray = Directory.GetFiles(Server.MapPath("\\Upload\\ProjectProposal\\" + userId)); //Directory.GetFiles(@"D:\Gaurav_Project_2020\FP_OLD\FP\FP\Upload\ProjectProposal\" + userId);
                                                                                                                     //string[] fileArray1 = Directory.GetFiles(Server.MapPath("\\Upload\\ProjectProposal\\" + userId));
                    if (fileArray.Length > 0)
                    {
                        int id = result;
                        for (int i = 0; i < fileArray.Length; i++)
                        {

                            string item = fileArray[i];
                            //string path = @"D:\Gaurav_Project_2020\FP_OLD\FP\FP\Upload\TempDir\Messages\";  Path.GetExtension(fileName);
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

                            result = obj.SaveProposalFile(id, orgFileName, filePath);


                        }
                    }

                }
                catch
                {

                }
            }
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// used to get proposal details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProposal(int ProjectId)
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
            string convertToCurrency = "USD";
            if (Session["CurrencyType"] != null)
            {
                convertToCurrency = Session["CurrencyType"].ToString();
            }
            var result = obj.GetProposalByProjectId(ProjectId, userId, convertToCurrency);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region mail documents
        public JsonResult UploadDocuments()
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
                string TempFilePath = Server.MapPath("\\Upload\\TempDir");
                if (!Directory.Exists(TempFilePath))
                {
                    Directory.CreateDirectory(TempFilePath);
                }

                string OriginalFilePath = Server.MapPath("\\Upload\\Messages\\" + userId);
                if (!Directory.Exists(OriginalFilePath))
                {
                    Directory.CreateDirectory(OriginalFilePath);
                }
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    actualFileName[j] = file.FileName;
                    //add file with guid
                    fileName[j] = Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", "").Replace("'", "").Trim()) + "~" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    try
                    {
                        string filePathName = Path.Combine(TempFilePath, fileName[i]);
                        file.SaveAs(filePathName);

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
                ActualFileName = actualFileName
            });
        }


        [HttpGet]
        public JsonResult RemoveDocument(string fileName)
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
            bool IsValidRemoved = false;
            if (fileName != null)
            {
                string TempFilePath = Server.MapPath("\\Upload\\TempDir\\");
                string OriginalFilePath = Server.MapPath("\\Upload\\Messages\\" + userId+"\\"); 

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

        #endregion

        #region mail documents
        public JsonResult UploadProposalDocuments()
        {
            string[] fileName = new string[Request.Files.Count];
            string[] actualFileName = new string[Request.Files.Count];
            int j = 0;
            string userId = Session["UserId"].ToString();
            bool ISValidUpload = true;
            if (Request.Files != null)
            {
                string TempFilePath = Server.MapPath("\\Upload\\TempDir");
                if (!Directory.Exists(TempFilePath))
                {
                    Directory.CreateDirectory(TempFilePath);
                }

                string OriginalFilePath = Server.MapPath("\\Upload\\ProjectProposal\\" + userId);
                if (!Directory.Exists(OriginalFilePath))
                {
                    Directory.CreateDirectory(OriginalFilePath);
                }

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    actualFileName[j] = file.FileName;
                    //add file with guid
                    fileName[j] = Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", "").Replace("'", "").Trim()) + "~" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                   // OriginalFileName[j]= Path.GetFileNameWithoutExtension(file.FileName.Replace(" ", "").Replace("'", "").Trim()) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    try
                    {
                        string filePathName = Path.Combine(TempFilePath, fileName[i]);
                        file.SaveAs(filePathName);

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
                ActualFileName = actualFileName
            });
        }


        [HttpGet]
        public JsonResult RemoveProposalDocument(string fileName)
        {
            string userId = Session["UserId"].ToString();
            bool IsValidRemoved = false;
            if (fileName != null)
            {
                string TempFilePath = Server.MapPath("\\Upload\\TempDir\\" );
               // string OriginalFilePath = Server.MapPath("\\Upload\\Documents");
                string OriginalFilePath = Server.MapPath("\\Upload\\ProjectProposal\\" + userId+"\\");
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

       

        #endregion

        #region Wallet
        public ActionResult Wallet()
        {
            //ViewBag.StripePublishKey = ConfigurationManager.AppSettings["stripePublishableKey"];
            //if (Session["UserId"] == null)
            //{
            //    return RedirectToAction("Login", controllerName: "Account");
            //}
            return View();
        }

        public ActionResult CheckUserSession()
        {
            return RedirectToAction("Login", controllerName: "Account");
            //return Json(new
            //{
            //    data = "logOut",
            //    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            //}, JsonRequestBehavior.AllowGet);
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

            string currencyType = "USD";
            if (Session["CurrencyType"] != null)
            {
                currencyType = Session["CurrencyType"].ToString();
            }

            string userId = Session["UserId"].ToString();
            WalletDetails obj = new WalletDetails();
            var result = obj.GetWalletAmountList(userId, currencyType);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Charge(string token, string email, string amount)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string stripePublishableKey = ConfigurationManager.AppSettings["stripePublishableKey"];
            Stripe.StripeConfiguration.SetApiKey(stripePublishableKey);
            Stripe.StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["stripeSecreteKey"]; //"sk_test_51ISlIhLdK4kakFbtU1foyV6V5OTH5USbvWNqKZ5zIFkqUbPygXVZcDWxIgwRh9ElRPysz2So63tHFHtM28HP5qDR00IcY6Cr8D";

            //Create Customer Object and Register it on Stripe  
            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions();
            myCustomer.Email = email;
            myCustomer.Name = "test";
            myCustomer.Source = token;
            myCustomer.Address = new AddressOptions
            {
                Line1 = "510 Townsend St",
                PostalCode = "98140",
                City = "San Francisco",
                State = "CA",
                Country = "US",
            };
            //myCustomer.SourceToken = newToken.Id;
            var customerService = new Stripe.CustomerService();
            Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

            //Create Charge Object with details of Charge  
            var options = new Stripe.ChargeCreateOptions
            {
                Amount = Convert.ToInt32(amount),
                Currency = "INR",
                ReceiptEmail = email,
                Customer = stripeCustomer.Id,
                //Source= stripeToken,
                Description = "Add amount in FP wallet", //Optional  
            };
            //and Create Method of this object is doing the payment execution.  
            var service = new Stripe.ChargeService();
            Stripe.Charge charge = service.Create(options); // This will do the Payment

            //if (charge.Status == "sucessed")
            //{
            WalletAmountModal objData = new WalletAmountModal();
            objData.Amount = Convert.ToString(charge.Amount);
            objData.TransactionID = charge.StripeResponse.RequestId;
            objData.Status = charge.Status;
            string userId = Session["UserId"].ToString();
            WalletDetails objCategory = new WalletDetails();
            var result = objCategory.SaveWalletDetails(objData, userId);
            //}

            return View("Wallet");
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
            // if(Session["UserId"] != null) { 
            string userId = Session["UserId"].ToString();
                WalletDetails objCategory = new WalletDetails();
                var result = objCategory.SaveWalletDetails(objData, userId);
                return Json(new
                {
                    statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
                });                                                 
            
        }

        /// <summary>
        /// Used to get particular product category detail
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetWalletAmountById(int Id)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            WalletDetails objCategory = new WalletDetails();
            string userId = Session["UserId"].ToString();
            var result = objCategory.GetWalletAmountById(Id, userId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove product category
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveWalletAmount(int Id)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            WalletDetails objCategory = new WalletDetails();
            string userId = Session["UserId"].ToString();
            var result = objCategory.RemoveWalletAmount(Id, userId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }

        #endregion

        #region BankDetails

        public ActionResult BankDetails()
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
        public JsonResult GetBankList()
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
            BankDetail obj = new BankDetail();
            var result = obj.GetBankList(userId);

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
        public JsonResult SaveBankDetails(BankDetailModal objData)
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
            BankDetail obj = new BankDetail();
            var result = obj.SaveBankDetails(objData, userId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get particular product category detail
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBankDetailById(int Id)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            BankDetail obj = new BankDetail();
            string userId = Session["UserId"].ToString();
            var result = obj.GetBankDetailById(Id, userId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove product category
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveBank(int Id)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            BankDetail obj = new BankDetail();
            string userId = Session["UserId"].ToString();
            var result = obj.RemoveBank(Id, userId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }



        #endregion

        public ActionResult CreateCampaign()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        [HttpPost]
        public JsonResult SaveCampaign(CampaignModal objData)
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
            string currencyType = "USD";
            if (Session["CurrencyType"] != null)
            {
                currencyType = Session["CurrencyType"].ToString();
            }
            CreateCampaign obj = new CreateCampaign();
            var result = obj.SaveCampaign(objData, userId, currencyType);//, SupplementalChannels, YouTubeVideoType);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
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

                string OriginalFilePath = Server.MapPath("\\Upload\\ProductPhoto\\" + userId);
                
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
                        //string filePathName = Path.Combine(TempFilePath, fileName[i]);
                        //file.SaveAs(filePathName);

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
                UserId = userId
            });
        }


        [HttpGet]
        public JsonResult GetAllProposal()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            string convertToCurrency = "USD";
            if (Session["CurrencyType"] != null)
            {
                convertToCurrency = Session["CurrencyType"].ToString();
            }

            Projects obj = new Projects();
            string userId = Session["UserId"].ToString();
            var result = obj.GetAllProposal(userId, convertToCurrency);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult RemoveProposal(int CapaignId)
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
            var result = obj.RemoveProposal(CapaignId, userId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }


        [HttpGet]
        public JsonResult GetCreatorInfoMail(string Email)
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
            
            var result = objCreator.GetCreatorInfoMail(Email);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
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
    }
}