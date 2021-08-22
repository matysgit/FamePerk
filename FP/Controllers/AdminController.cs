using FP.DAL;
using FP.DAL.Classes;
using FP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FP.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        #region Product Category


        public ActionResult ProductCategory()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to save product category
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveProductCategory(ProductCategoryModal objData)
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
            var result = objCategory.SaveProductCategory(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of product category
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProductCategoryList()
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
            var result = objCategory.GetProductCategoryList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular product category detail
        /// </summary>
        /// <param name="ProductCategoryId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProductCategoryById(int productCategoryId)
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
            var result = objCategory.GetProductCategoryById(productCategoryId);
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
        public JsonResult RemoveProductCategory(int productCategoryId)
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
            var result = objCategory.RemoveProductCategory(productCategoryId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }

        #endregion

        #region Campaign Duration

        public ActionResult CampaignDuration()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }
		
			/// <summary>
        /// Used to save Campaign Duration
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCampaignDuration(CampaignDurationModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignDuration obj = new CampaignDuration();
            var result = obj.SaveCampaignDuration(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of CampaignDuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCampaignDurationList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignDuration obj = new CampaignDuration();
            var result = obj.GetCampaignDurationList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular Campaign Duration detail
        /// </summary>
        /// <param name="CampaignDurationId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCampaignDurationById(int CampaignDurationId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignDuration obj = new CampaignDuration();
            var result = obj.GetCampaignDurationById(CampaignDurationId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove Campaign Duration
        /// </summary>
        /// <param name="CampaignDurationId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveCampaignDuration(int CampaignDurationId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignDuration obj = new CampaignDuration();
            var result = obj.RemoveCampaignDuration(CampaignDurationId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }


        #endregion

        #region Campaign Type

        public ActionResult CampaignType()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }
		
			/// <summary>
        /// Used to save CampaignType
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveCampaignType(CampaignTypeModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignType obj = new CampaignType();
            var result = obj.SaveCampaignType(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of CampaignType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCampaignTypeList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignType obj = new CampaignType();
            var result = obj.GetCampaignTypeList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular CampaignType detail
        /// </summary>
        /// <param name="CampaignTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCampaignTypeById(int CampaignTypeId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignType obj = new CampaignType();
            var result = obj.GetCampaignTypeById(CampaignTypeId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove CampaignType
        /// </summary>
        /// <param name="CampaignTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveCampaignType(int CampaignTypeId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            CampaignType obj = new CampaignType();
            var result = obj.RemoveCampaignType(CampaignTypeId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }

		
        #endregion

        #region Audience Age

        public ActionResult AudienceAge()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to save Budget
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveAudienceAge(AudienceAgeModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }

            AudienceAge obj = new AudienceAge();
            var result = obj.SaveAudienceAge(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of AudienceAge
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAudienceAgeList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            AudienceAge obj = new AudienceAge();
            var result = obj.GetAudienceAgeList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular AudienceAge detail
        /// </summary>
        /// <param name="AudienceAgeId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAudienceAgeById(int AudienceAgeId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            AudienceAge obj = new AudienceAge();
            var result = obj.GetAudienceAgeById(AudienceAgeId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove AudienceAge
        /// </summary>
        /// <param name="AudienceAgeId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveAudienceAge(int AudienceAgeId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            AudienceAge obj = new AudienceAge();
            var result = obj.RemoveAudienceAge(AudienceAgeId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }

        #endregion

        #region Budget

        public ActionResult Budget()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }


 		/// <summary>
        /// Used to save Budget
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveBudget(BudgetModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Budget obj = new Budget();
            var result = obj.SaveBudget(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of Budget
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBudgetList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Budget obj = new Budget();
            var result = obj.GetBudgetList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular Budget detail
        /// </summary>
        /// <param name="BudgetId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetBudgetById(int BudgetId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Budget obj = new Budget();
            var result = obj.GetBudgetById(BudgetId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove Budget
        /// </summary>
        /// <param name="BudgetId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveBudget(int BudgetId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Budget obj = new Budget();
            var result = obj.RemoveBudget(BudgetId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }


        #endregion        

        #region Channel


        public ActionResult Channel()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to save Channel
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveChannel(ChannelModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Channel objCategory = new Channel();
            var result = objCategory.SaveChannel(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of Channel
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetChannelList()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Channel objCategory = new Channel();
            var result = objCategory.GetChannelList();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular product category detail
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetChannelById(int ChannelId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Channel objCategory = new Channel();
            var result = objCategory.GetChannelById(ChannelId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove Channel
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveChannel(int ChannelId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Channel objCategory = new Channel();
            var result = objCategory.RemoveChannel(ChannelId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }

        #endregion

        #region Level
        public ActionResult Level()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to save level
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveLevel(LevelModal objData)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Level objLevel = new Level();
            var result = objLevel.SaveLevel(objData);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        /// <summary>
        /// Used to get list of level
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLevel()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Level objLevel = new Level();
            var result = objLevel.GetLevel();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get particular level detail
        /// </summary>
        /// <param name="LevelId"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetLevelById(int LevelId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Level objLevel = new Level();
            var result = objLevel.GetLevelById(LevelId);
            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to remove level
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveLevel(int LevelId)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            Level objLevel = new Level();
            var result = objLevel.RemoveLevel(LevelId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent
            });
        }


        #endregion

        #region Campaign
        public ActionResult Campaign()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        /// <summary>
        /// Used to Get all campaign
        /// </summary>
        /// <param name="objModal"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCampaignList(string Type)
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
            var result = obj.GetCampaignListForAdmin(userId, Type);

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCampaignById(int CampaignId, string Type)
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
            CreateCampaign obj = new CreateCampaign();
            var result = obj.UpdateCampaignById(CampaignId, Type, userId);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }
        #endregion Campaign

        #region Creatot
        public ActionResult Creator()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

          #endregion

        public ActionResult Client()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", controllerName: "Account");
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetAllClient()
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            AudienceAge obj = new AudienceAge();
            var result = obj.GetAllClient();

            return Json(new
            {
                data = result,
                statusCode = result != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditClient(string Id, string Type)
        {
            if (Session["UserId"] == null)
            {
                return Json(new
                {
                    data = "logOut",
                    statusCode = "logOut" != null ? HttpStatusCode.OK : HttpStatusCode.NoContent
                }, JsonRequestBehavior.AllowGet);
            }
            AudienceAge obj = new AudienceAge();
            var result = obj.EditClient(Id, Type);
            return Json(new
            {
                statusCode = result > 0 ? HttpStatusCode.OK : result == 0 ? HttpStatusCode.Conflict : HttpStatusCode.NoContent
            });
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
       
    }
}