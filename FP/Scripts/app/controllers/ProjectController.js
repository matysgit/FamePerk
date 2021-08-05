'use strict';

fpApp.controller("ProjectController", function ($scope, fpService, $http) {
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);
    $scope.fn_DefaultProjectSettings = function () {
        $scope.fn_GetCurrencyType();
        $scope.fn_GetUnReadMsg();
        // var param1 = $location.search().param1;

        $scope.fn_GetAllProposal();
        //$("#v-pills-1").hide();
        //$("#divViewProject").show();

        //$("#divProposal").hide();
        $("#divYouTubeVideoType").hide();

        $scope.fn_GetProductCategory();
        $scope.fn_GetCampaignDuration();
        $scope.fn_GetBudget();
        $scope.fn_GetAge();

        $scope.fn_GetAllProjects();
        $scope.fn_GetYouTubeType();
        $scope.fn_GetSupplementalChannel();

        $scope.ProjectModal = {};
        $scope.ProjectProposalModal = {};

        $scope.lstDocuments = [];
        //$scope.imgProfile = '../Images/profile.png';
        //$scope.imgLoader = false;
        $scope.docLoader = false;

        var url;
        url = window.location.href;
        var campaignData = url.split("?");
        var isCamapaignId = campaignData[1].split("=");
        if ("CampaignId" == isCamapaignId[0]) {
            $scope.fn_ViewProject(isCamapaignId[1]);
        }

    };

    $scope.CurrencyTypeModal = {};
    $scope.fn_ChangeCurrencyType = function (CurrencyTypeModal) {
        //  $scope.CampaignFillterModal = {};
        //$scope.CampaignModal = {};
        fpService.getData($_Creator.SetCurrencyType, CurrencyTypeModal, function (response) {
            var responseJson = response.data;
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Currency = {};
                $scope.CurrencyTypeModal = {};
                $scope.Currency = responseJson.data;
                $scope.CurrencyTypeModal.CurrencyId = response.data.currentCureency;

                $scope.fn_GetAllProjects();
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    }

    $scope.fn_GetCurrencyType = function () {

        $scope.Currency = {};
        $scope.CurrencyTypeModal = {};
        fpService.getData($_Creator.GetCurrencyType, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Currency = responseJson.data;
                $scope.CurrencyTypeModal.CurrencyId = response.data.currentCureency;

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });

    };


    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetUnReadMsg = function () {
        $scope.UnReadMsgModal = {};
        fpService.getData($_Mailbox.GetUnReadMsg, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.UnReadMsgModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_GetSupplementalChannel = function () {

        $scope.SupplementalChannelModal = [];
        fpService.getData($_Creator.GetSupplementalChannel, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.SupplementalChannelModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetYouTubeType = function () {

        $scope.YouTubeVideoTypeModal = {};
        fpService.getData($_Creator.GetYouTubeType, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.YouTubeVideoTypeModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetProductCategory = function () {

        $scope.ProductCategory = {};
        fpService.getData($_ProductCategory.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.ProductCategory = responseJson.data;
                $scope.CreateCampaignModal.ProductCategoryId = $scope.ProductCategory[0].ProductCategoryId;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetCampaignDuration = function () {

        $scope.CampaignDuration = {};
        fpService.getData($_CampaignDuration.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CampaignDuration = responseJson.data;
                $scope.CreateCampaignModal.CampaignDurationId = $scope.CampaignDuration[0].CampaignDurationId;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetAge = function () {

        $scope.AudienceAgeModal = {};
        fpService.getData($_AudienceAge.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.AudienceAgeModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetBudget = function () {

        $scope.Budget = {};

        fpService.getData($_Budget.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Budget = responseJson.data;
                $scope.CreateCampaignModal.BudgetId = $scope.Budget[0].BudgetId;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetAllProjects = function () {
        $("#divViewProject").hide();

        $("#divProposal").hide();

        $("#v-pills-1").show();
        $scope.lstProjects = {};
        $scope.gridLoader = true;
        fpService.getData($_Project.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstProjects = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_GetHomePage = function () {

        var url;
        url = window.location.href;
        var campaignData = url.split("?");
        var isCamapaignId = campaignData[1].split("=");
        if ("CampaignId" == isCamapaignId[0]) {
            location.href = '/Creator/Index';//@Url.Action("Creator","Index")';
        }
        else
            $scope.fn_DefaultProjectSettings();
        
    }

    $scope.fn_GetAllProposal = function () {
        $scope.lstProposal = {};
        $scope.gridLoader = true;
        //$scope.ProductCategory = [];

        fpService.getData($_Project.GetAllProposal, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstProposal = responseJson.data;
            }
        });
    }

    $scope.fn_ViewProject = function (ProjectId) {
        //  $scope.fn_ViewProject(ProjectId);
        $("#divViewProject").show();
        $("#divProposalDetail").show();
        $("#divProposal").hide();
        $scope.SubmitProposal = "Submit Proposal";
        $scope.SaveProposal = "Save Proposal";

        $scope.CampaignModal = {};
        //$scope.ProductCategory = [];
        var param = { ProjectId: ProjectId };
        fpService.getData($_Project.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CampaignModal = responseJson.data[0];


                $scope.ProductCategory.ProductCategoryId = "3";// $scope.CampaignModal.ProductCategoryId.toString();// responseJson.data[0].ProductCategoryId;
                $("#v-pills-1").hide();
                $("#divViewProject").show();

                $("#divProposal").hide();

                if (responseJson.data[0].Status == 'Draft') {
                    $scope.SubmitProposal = "Update Proposal";
                    $scope.SaveProposal = "Update Proposal";
                }
                else if (responseJson.data[0].Status == 'Publish') {
                    $scope.SubmitProposal = "View Proposal";
                }

                var audienceArry = responseJson.data[0].AudienceAgeId
                var audienceArryNew = audienceArry.split(',')

                $scope.AudienceAgeModal = {};
                fpService.getData($_AudienceAge.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.AudienceAgeModal = responseJson.data;

                        audienceArryNew.forEach(function (itemArr) {
                            angular.forEach($scope.AudienceAgeModal, function (item) {
                                if (parseInt(itemArr) === item.AudienceAgeId) {
                                    item.isChecked = true;
                                }
                            });
                        });
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

                //audienceArryNew.forEach(function (itemArr) {
                //    angular.forEach($scope.AudienceAgeModal, function (item) {
                //        if (parseInt(itemArr) === item.AudienceAgeId) {
                //            item.isChecked = true;
                //        }
                //    });
                //});
                var supplementalArry = responseJson.data[0].SupplementalChannels
                var supplementalArryNew = supplementalArry.split(',')
             
                $scope.SupplementalChannelModal = {};
                fpService.getData($_Creator.GetSupplementalChannel, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.SupplementalChannelModal = responseJson.data;
                        supplementalArryNew.forEach(function (itemArr) {
                            angular.forEach($scope.SupplementalChannelModal, function (item) {
                                if (parseInt(itemArr) === item.SupplementalId) {
                                    item.isChecked = true;
                                }
                            });
                        });
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

                

                //supplementalArryNew.forEach(function (itemArr) {
                //    angular.forEach($scope.SupplementalChannelModal, function (item) {
                //        if (parseInt(itemArr) === item.SupplementalId) {
                //            item.isChecked = true;
                //        }
                //    });
                //});

                //$scope.fn_GetYouTubeType();

                var youTubeTypeArry = responseJson.data[0].YouTubeVideoType
                var youTubeTypeArryNew = youTubeTypeArry.split(',')
                $scope.YouTubeVideoTypeModal = {};
                fpService.getData($_Creator.GetYouTubeType, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.YouTubeVideoTypeModal = responseJson.data;
                        youTubeTypeArryNew.forEach(function (itemArr) {
                            angular.forEach($scope.YouTubeVideoTypeModal, function (item) {
                                if (parseInt(itemArr) === item.YouTubeVideoTypeId) {
                                    item.isChecked = true;
                                    $("#YouTubeCheck1").prop("checked", true);
                                    $("#divYouTubeVideoType").show();
                                }
                            });
                        });
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });
                //youTubeTypeArryNew.forEach(function (itemArr) {
                //    angular.forEach($scope.YouTubeVideoTypeModal, function (item) {
                //        if (parseInt(itemArr) === item.YouTubeVideoTypeId) {
                //            item.isChecked = true;
                //            $("#YouTubeCheck1").prop("checked", true);
                //            $("#divYouTubeVideoType").show();
                //        }
                //    });
                //});


                if (responseJson.data[0].PrivateCampaign == "True") {
                    $("#chkPrivateCampaign").prop("checked", true);
                    //element(by.id('chkPrivateCampaign')).getAttribute('checked')
                }
                if (responseJson.data[0].AudienceGender == "All") {
                    $("#rdbAudienceGenderAll").prop("checked", true);
                }

                else if (responseJson.data[0].AudienceGender == "Female") {
                    $("#rdbAudienceGenderFemale").prop("checked", true);
                }

                else {
                    $("#rdbAudienceGenderMale").prop("checked", true);
                }

                if (responseJson.data[0].ShippingProduct == "True") {
                    $("#radioShippingProductsYes").prop("checked", true);
                }
                // $("#divViewProject").show();

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });

        fpService.getData($_Project.GetProposal, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.SubmitProposal = "Submit Proposal";
                $scope.SaveProposal = "Save Proposal";

                if (responseJson.data[0].Status == 'Draft') {
                    $scope.SubmitProposal = "Update Proposal";
                    $scope.SaveProposal = "Update Proposal";
                }
                else if (responseJson.data[0].Status == 'Publish') {
                    $scope.SubmitProposal = "View Proposal";
                }
                //    else {
                //    $scope.SubmitProposal = "Submit Proposal";
                //    $scope.SaveProposal = "Save Proposal";
                //    }

                //if (responseJson.data[0].length == 0) {
                //    $scope.SubmitProposal = "Submit Proposal";
                //    $scope.SaveProposal = "Save Proposal";
                //}
                //else {


                //    $("#v-pills-1").hide();
                //    $("#divViewProject").show();

                //    $("#divProposal").hide();

                //    if (responseJson.data[0].Status == 'Draft') {
                //        $scope.SubmitProposal = "Update Proposal";
                //        $scope.SaveProposal = "Update Proposal";
                //    }
                //    else if (responseJson.data[0].Status == 'Publish') {
                //        $scope.SubmitProposal = "View Proposal";
                //    }
                //    }


                // $scope.ProjectProposalModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });



    };

    $scope.fn_SubmitProposal = function (ProjectId) {
        $("#v-pills-1").hide();
        $("#divProposalDetail").hide();
        $("#divViewProject").show();

        $("#divProposal").show();
        $("#divMilestone").hide();
        $("#divFixedCost").hide();
        $("#divMilestone1").hide();
        $("#divMilestone2").hide();
        $("#divMilestone3").hide();
        $("#divFileUpload").show();
        $scope.ShowSave = true;
        // ProjectId = 1;

        $("#aDelete").hide();

        if ($scope.SubmitProposal != "Submit Proposal") {
            $scope.ProjectProposalModal = {};
            var param = { ProjectId: ProjectId };
            fpService.getData($_Project.GetProposal, param, function (response) {
                var responseJson = response.data;
                if (responseJson.statusCode === 200) {
                    if (responseJson.data == "logOut") {
                        RedirectToLogin();
                    }
                    $scope.ProjectProposalModal = responseJson.data[0];

                    $("#divMilestone1").hide();
                    $("#divMilestone2").hide();
                    $("#divMilestone3").hide();
                    var Payment = $scope.ProjectProposalModal.PaymentType;
                    if (Payment == "2") {
                        $("#divMilestone").show();
                        $("#divFixedCost").hide();
                        var Milestone = $scope.ProjectProposalModal.NoOfMilestone;

                        if (Milestone == "3") {
                            $("#divMilestone1").show();
                            $("#divMilestone2").show();
                            $("#divMilestone3").show();
                        }
                        if (Milestone == "2") {
                            $("#divMilestone1").show();
                            $("#divMilestone2").show();
                        }

                        else if (Milestone == "1") {
                            $("#divMilestone1").show();
                        }
                        $scope.fn_GetProjectMilestoneAmount(5);


                    }
                    else if (Payment == "1") {
                        $("#divMilestone").hide();
                        $("#divFixedCost").show();
                        $scope.fn_GetProjectAmount(1);
                    }
                    else {
                        $("#divMilestone").hide();
                        $("#divFixedCost").hide();
                    }
                    if ($scope.ProjectProposalModal.Status == "Publish") {
                        $("#divFileUpload").hide();
                        $scope.ShowSave = false;
                    }
                    // $scope.ProjectProposalModal = responseJson.data;
                }
                if (responseJson.statusCode === 204) {
                    toastr.error('Error in getting data.');
                }
            });

            fpService.getData($_Project.GetProposalDocument, param, function (response) {
                var responseJson = response.data;
                if (responseJson.statusCode === 200) {
                    if (responseJson.data == "logOut") {
                        RedirectToLogin();
                    }
                    $scope.lstDocument = responseJson.data;

                }
                if (responseJson.statusCode === 204) {
                    toastr.error('Error in getting data.');
                }
            });
        }


    };


    $scope.fn_GetMilestone = function () {
        $("#divMilestone1").hide();
        $("#divMilestone2").hide();
        $("#divMilestone3").hide();
       
        $scope.FpAmount = '0';
        $scope.ReceiveAmount = '0';
        var Payment = $scope.ProjectProposalModal.PaymentType;
        if (Payment == "2") {
            $("#divMilestone").show();
            $("#divFixedCost").hide();
            var Milestone = $scope.ProjectProposalModal.NoOfMilestone;

            if (Milestone == "3") {
                $("#divMilestone1").show();
                $("#divMilestone2").show();
                $("#divMilestone3").show();
            }
            if (Milestone == "2") {
                $("#divMilestone1").show();
                $("#divMilestone2").show();
            }

            else if (Milestone == "1") {
                $("#divMilestone1").show();
            }

            $scop.fn_GetProjectMilestoneAmount(5);
        }
        else if (Payment == "1") {
            var value = $("#txtFixedCostAmount").val();
            $scope.fn_GetProjectAmount(value);
            $("#divMilestone").hide();
            $("#divFixedCost").show();
        }
        else {
            $("#divMilestone").hide();
            $("#divFixedCost").hide();
        }
    };

    $scope.fn_SaveProposal = function (ProjectProposalModal) {
        $scope.ProjectProposalModal = {};
        fpService.postData($_Project.Save, ProjectProposalModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Proposal save successfully.', "Success");
                $scope.fn_DefaultProjectSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Proposal already exists.',"Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in replying.',"Error");
            }
        });
    };




    //$scope.fn_GetProjectAmount = function () {
    //    //var FixedAmt = FixedCostAmount;
    //    var FpFee = 25;
    //    //$("#txtFixedCostAmount1").val = (FixedAmt * FpFee) / 100;
    //    //$("#txtFixedCostAmount2").val = FixedCostAmount-( (FixedAmt * FpFee) / 100);

    //};

    $scope.fn_DownloadDocument = function (BrandMailFileId) {
        $scope.MailboxModal = {};
        var param = { BrandMailFileId: BrandMailFileId };
        fpService.getData($_Project.DownloadDocument, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $window.open(responseJson)
                toastr.success('Documant Downloaded.', "Success");
                // $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.error('Error in dowloading.',"Error");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in dowloading.',"Error");
            }
        });
    };


    $scope.fn_DocumentUpload = function (uploadedFile) {
        $scope.docLoader = true;
        var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".mp4", ".mpeg", ".mkv", ".xls", ".xlsx", ".pdf", ".doc", ".docx", ".csv", ".mp3", "wav", ".txt", ".avi", ".asf", ".mov", ".AVCHD", ".flv", ".mpg", ".WMV", ".DivX", ".H.264"];
        if (uploadedFile.length > 0) {
            if (uploadedFile[0]) {
                if (uploadedFile[0].size > 0) {
                    var blnValid = false;
                    var sFileName = uploadedFile[0].name;
                    var sFileSize = uploadedFile[0].size;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            break;
                        }
                    }
                    if (!blnValid) {
                        $scope.IsUploading = false;
                        toastr["warning"]("Please upload a valid document.", "Warning");
                        return true;
                    }
                    if (blnValid && sFileSize > (1024 * 1024 * 200)) {
                        $scope.IsUploading = false;
                        toastr["warning"]("Upload document size exceeded allowed limits(Maximum 200MB).", "Warning");
                        return true;
                    }
                    var formData = new FormData();
                    for (var i in uploadedFile) {
                        formData.append('file', uploadedFile[i]);
                    }

                    $http.post($_Project.UploadDocument, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (result) {
                        var response = result.data;
                        angular.forEach(response.ActualFileName, function (value, index) {
                            $scope.lstDocuments.push({
                                FileName: value,
                                UplodedFileName: response.UplodedFileName[index]
                            });
                        });
                        $scope.docLoader = false;
                    });
                }
            }
            // angular.element("input[type='file']").val(null);
        }
    };


    $scope.fn_RemoveDocument = function (lst) {
        fpService.getData($_Project.RemoveDocument, { fileName: lst.UplodedFileName }, function () {
            $scope.lstDocuments.splice($scope.lstDocuments.indexOf(lst), 1);
        });
    };

    $scope.fn_ViewDocument = function (lst) {
        window.open("../Upload/TempDir/" + lst.UplodedFileName);
    };

    //$scope.fn_ViewProductPhoto = function (PhotoPath) {
    //    window.open(PhotoPath);
    //};

    $scope.fn_DownloadDocument = function (FilePath) {
        window.open(FilePath);
    };

    $scope.fn_DeleteDocument = function (BrandMailFileId) {
        var param = { BrandMailFileId: BrandMailFileId };
        fpService.getData($_Project.DeleteDocument, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('File delete successfully.', "Success");
                $scope.lstDocument = responseJson.data;
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Error in deleteing.',"Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in deleteing.',"Error");
            }
        });
    };


    $scope.fn_RemoveCampaign = function (CampaignId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this Campaign?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { CampaignId: CampaignId };
                    fpService.postData($_Project.RemoveProposal, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Campaign Deleted successfully.', "Success");
                            $scope.fn_GetAllProposal();
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in removing Campaign.',"Error");
                        }
                    });
                }
            }, {
                label: 'No',
                cssClass: 'roundedbtn_outline btndialog',
                action: function (dialog) {
                    dialog.close();
                }
            }]
        });
    };

    $scope.fn_GetProjectAmount = function (FixedAmt) {
        //var FixedAmt = $scope.ProjectProposalModal.ProposalAmount;
        var FpFee = 15;
        var value = $scope.ProjectProposalModal.ProposalAmount;
        $scope.FpAmount = (parseFloat(value) * parseFloat(FpFee) / 100);
        //$("#txtFixedCostAmount1").val = (FixedAmt * FpFee) / 100;
        //$("#txtFixedCostAmount2").val = FixedCostAmount-( (FixedAmt * FpFee) / 100);
        $scope.ReceiveAmount = parseFloat(value) - parseFloat($scope.FpAmount);
    };


    $scope.fn_ShowUpdateDiv = function (ProjectProposalId, CampaignId) {
        $scope.ProposalUpdateModal = {};
        $scope.ProposalUpdateModal.ProjectProposalId = ProjectProposalId;
        $scope.ProposalUpdateModal.CampaignId = CampaignId;
        $("#v-pills-1").hide();
        $("#divProposalDetail").hide();
        $("#divViewProject").show();

        $("#divProposal").hide();
        $("#divMilestone").hide();
        $("#divFixedCost").hide();
        $("#divMilestone1").hide();
        $("#divMilestone2").hide();
        $("#divMilestone3").hide();
        $("#divFileUpload").show();
        $scope.ShowSave = false;
        $scope.ShowSendUpdateDiv = true;

    };

    $scope.fn_SendProposalUpdate = function (ProposalUpdateModal) {
        $scope.ProposalUpdateModal = {};
        fpService.postData($_Project.SendProposalUpdate, ProposalUpdateModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Project Proposal Update send successfully.', "Success");
                $scope.fn_DefaultProjectSettings();
            }

            if (responseJson.statusCode === 204) {
                toastr.error('Error in replying.',"Error");
            }
        });
    };


    $scope.fn_GetProjectMilestoneAmount = function (FixedAmt) {
        //var FixedAmt = $scope.ProjectProposalModal.ProposalAmount;
        var FpFee = 15;
        $scope.ProjectProposalModal.Milestone1Amount;
        $scope.ProjectProposalModal.Milestone2Amount;
        $scope.ProjectProposalModal.Milestone3Amount;

        var milestone1 = $scope.ProjectProposalModal.Milestone1Amount;//$("#txtMilestone1Amt").val();
        var milestone2 = $scope.ProjectProposalModal.Milestone2Amount;//$("#txtMilestone2Amt").val();
        var milestone3 = $scope.ProjectProposalModal.Milestone3Amount;//$("#txtMilestone3Amt").val();

      
        if (milestone3 != "") {
            $scope.FpAmount = ((parseFloat(milestone1) + parseFloat(milestone2) + parseFloat(milestone3)) * parseFloat(FpFee) / 100);
            $scope.ReceiveAmount = (parseFloat(milestone1) + parseFloat(milestone2) + parseFloat(milestone3)) - parseFloat($scope.FpAmount);
        }
        if (milestone2 != "") {
            $scope.FpAmount = ((parseFloat(milestone1) + parseFloat(milestone2)) * parseFloat(FpFee) / 100);
            $scope.ReceiveAmount = (parseFloat(milestone1) + parseFloat(milestone2)) - parseFloat($scope.FpAmount);
        }
        else {
            $scope.FpAmount = (parseFloat(milestone1) * parseFloat(FpFee) / 100);
            $scope.ReceiveAmount = parseFloat(milestone1) - parseFloat($scope.FpAmount);
        }
        

    };


});
