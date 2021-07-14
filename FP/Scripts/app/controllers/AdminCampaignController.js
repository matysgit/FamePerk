'use strict';

fpApp.controller("ProjectController", function ($scope, fpService, $http) {

    $scope.fn_DefaultProjectSettings = function () {
        $("#divYouTubeVideoType").hide();
        $scope.fn_GetProductCategory();
        $scope.fn_GetCampaignDuration();
        $scope.fn_GetBudget();
        $scope.fn_GetAge();
        $scope.fn_GetAllProjects('Pending');
        $scope.fn_GetYouTubeType();
        $scope.fn_GetSupplementalChannel();
        $scope.ProjectModal = {};
        $scope.ProjectProposalModal = {};
        $scope.lstDocuments = [];
        $scope.docLoader = false;
        var url;
        url = window.location.href;
        var campaignData = url.split("?");
        var isCamapaignId = campaignData[1].split("=");
        if ("CampaignId" == isCamapaignId[0]) {
            $scope.fn_ViewProject(isCamapaignId[1]);
        }
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

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

        $scope.YouTubeVideoTypeModal = [];
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

        $scope.ProductCategory = [];
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

        $scope.CampaignDuration = [];
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

        $scope.AudienceAgeModal = [];
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

        $scope.Budget = [];

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

    $scope.fn_GetAllProjects = function (Type) {
        $("#divViewProject").hide();

        $("#divProposal").hide();

        $("#divCampaignList").show();
        $scope.lstProjects = [];
        $scope.gridLoader = true;
        var param = { Type: Type };
        fpService.getData($_AdminCampaign.Get, param, function (response) {
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

        
        if (Type == "Pending") {
            $("#divButton").show();
            $("#divButtonApproved").hide();
            $("#divButtonRejected").hide();
        }
        else if (Type == "Approved") {
            $("#divButton").hide();
            $("#divButtonApproved").show();
            $("#divButtonRejected").hide();
        }
        else {
            $("#divButton").hide();
            $("#divButtonApproved").hide();
            $("#divButtonRejected").show();
        }
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
        // /Creator/Projects
        // location.href = '/Creator/Index';//@Url.Action("Creator","Index")';

    }

    $scope.fn_UpdateCampaign = function (CampaignId, Type, ListType) {
        var param = { CampaignId: CampaignId, Type: Type };
        fpService.postData($_AdminCampaign.Update, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Campaign updated successfully.');
                $scope.fn_GetAllProjects(ListType);
            }
           
            if (responseJson.statusCode === 204) {
                toastr.error('Error.');
                $scope.fn_GetAllProjects(ListType);
            }
        });
    }
   

    $scope.fn_ViewProject = function (ProjectId) {
        $scope.ShowSendUpdateDiv = false;
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
                $("#divCampaignList").hide();
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

                audienceArryNew.forEach(function (itemArr) {
                    angular.forEach($scope.AudienceAgeModal, function (item) {
                        if (parseInt(itemArr) === item.AudienceAgeId) {
                            item.isChecked = true;
                        }
                    });
                });

                var supplementalArry = responseJson.data[0].SupplementalChannels
                var supplementalArryNew = supplementalArry.split(',')

                supplementalArryNew.forEach(function (itemArr) {
                    angular.forEach($scope.SupplementalChannelModal, function (item) {
                        if (parseInt(itemArr) === item.SupplementalId) {
                            item.isChecked = true;
                        }
                    });
                });

                var youTubeTypeArry = responseJson.data[0].YouTubeVideoType
                var youTubeTypeArryNew = youTubeTypeArry.split(',')

                youTubeTypeArryNew.forEach(function (itemArr) {
                    angular.forEach($scope.YouTubeVideoTypeModal, function (item) {
                        if (parseInt(itemArr) === item.YouTubeVideoTypeId) {
                            item.isChecked = true;
                            $("#YouTubeCheck1").prop("checked", true);
                            $("#divYouTubeVideoType").show();
                        }
                    });
                });


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
                            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });



    };





    $scope.fn_SubmitProposal = function (ProjectId) {
        $("#divCampaignList").hide();
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
                    }
                    else if (Payment == "1") {
                        $("#divMilestone").hide();
                        $("#divFixedCost").show();
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
        }
        else if (Payment == "1") {
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
                toastr.success('Proposal save successfully.');
                $scope.fn_DefaultProjectSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Proposal already exists.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in replying.');
            }
        });
    };




    $scope.fn_GetProjectAmount = function (FixedAmt) {
        
    };

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
                toastr.success('Documant Downloaded.');
                // $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.error('Error in dowloading.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in dowloading.');
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
                toastr.success('File delete successfully.');
                $scope.lstDocument = responseJson.data;
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Error in deleteing.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in deleteing.');
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
                    fpService.postData($_Project.RemoveCampaign, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Campaign Deleted successfully.');
                            $scope.fn_GetAllProposal();
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.success('Error in removing Campaign.');
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

    $scope.fn_ShowUpdateDiv = function (ProjectProposalId, CampaignId) {
        $scope.ProposalUpdateModal = {};
        $scope.ProposalUpdateModal.ProjectProposalId = ProjectProposalId;
        $scope.ProposalUpdateModal.CampaignId = CampaignId;
        $("#divCampaignList").hide();
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
                toastr.success('Project Proposal Update send successfully.');
                $scope.fn_DefaultProjectSettings();
            }

            if (responseJson.statusCode === 204) {
                toastr.error('Error in replying.');
            }
        });
    };

});
