'use strict';

fpApp.controller("CreatorController", function ($scope, fpService, $http) {
    
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);

    $scope.example = {
        value: new Date(2013, 8, 22)
    };
    $scope.fn_GetCurrencyRates = function () {

        $scope.Currency = {};

        fpService.getData("https://api.exchangeratesapi.io/latest?base=INR", "", function (response) {
            $scope.Currency = response.data;
           

        });
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_DefaultCreatorSettings = function () {
        $scope.fn_GetCurrencyRates();
        $scope.fn_GetUnReadMsg();
        $("#divProfile").hide();
        $("#divAllCreator").show();
        $("#divCreatorProfile").hide();

        $("#divDashboard").show();
        $scope.fn_GetCampaignList();
       
        $scope.fn_GetCountry();
        $scope.fn_GetAge();
        $scope.fn_GetCreatorInfo("");
        $scope.fn_GetProfileImg();
        $scope.fn_GetProductCategory();
        $scope.fn_GetCampaignDuration();
        $scope.fn_GetBudget();
        $scope.fn_GetCreatorList();
    };

    $scope.fn_GetUnReadMsg = function () {
        $scope.UnReadMsgModal = {};
        //var param = { MailboxId: MailboxId };
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

    $scope.fn_GetProductCategory = function () {

        $scope.ProductCategory = {};
        fpService.getData($_ProductCategory.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.ProductCategory = responseJson.data;
               // $scope.ProductCategory[0].ProductCategoryId; 
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_EditProfile = function () {
      //  $scope.fn_GetCountry();
        //$scope.divProfile.show();
        //$scope.divDashboard.hide();
        $scope.fn_GetCreatorInfo("");
        $scope.fn_GetProfileImg();
        $("#divProfile").show();

        $("#divDashboard").hide();
    }

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
    
    $scope.fn_GetCampaignList = function () {
        $scope.CampaignModal = {};
        fpService.getData($_Creator.GetCampaignList, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CampaignModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_GetCreatorList = function () {
        $scope.CreatorModal = {};
        fpService.getData($_Creator.GetCreatorList, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CreatorModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };
    $scope.fn_GetProfileImg = function () {
        fpService.postData($_Creator.GetImg, "", function (response) {
            var responseJson = response.data;

            if (responseJson.data == "logOut") {
                RedirectToLogin();
            }
            if (responseJson.FileName == "") {
                    $scope.imgProfile = '../Images/profile.png';
                    //$scope.imgLoader = true;
                }
                else {

                $scope.imgProfile = '../Upload/Profile/' + responseJson.UserId + '/' + responseJson.FileName;
                    $scope.imgLoader = false;
                }
            
            //if (responseJson.statusCode === 204) {
            //    toastr.error('Error in getting data.');
            //}
        });
    };

    $scope.fn_ImageUpload = function (uploadedFile) {
        $scope.imgLoader = true;
        var _validFileExtensions = [".jpg", ".jpeg", ".gif", ".png"];
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

                    $http.post($_UploadImage, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (result) {
                        $scope.imgProfile = '../Upload/Profile/' +  result.data.UserId +'/'+ result.data.UplodedFileName[0];
                        $scope.imgLoader = false;
                    });
                }
            }
            // angular.element("input[type='file']").val(null);
        }
    }

    //$scope.fn_GetCountry() = function () {
    //    $scope.Country = {};
    //    fpService.getData($_Creator.GetCountry, "", function (response) {
    //        var responseJson = response.data;
    //        if (responseJson.statusCode === 200) {
    //            $scope.Country = responseJson.data;
    //        }
    //        if (responseJson.statusCode === 204) {
    //            toastr.error('Error in getting data.');
    //        }
    //    });
    //}

    $scope.fn_GetCountry = function () {

        $scope.Country = {};
        fpService.getData($_Creator.GetCountry, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Country = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetCreatorInfo = function (value) {
        $scope.CreatorModal = {};
        var param = { value: value };
        fpService.getData($_Creator.Get, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CreatorModal = responseJson.data;

                $scope.CreatorDOB = {
                    DOB: new Date(responseJson.data.DOB)
                };

                angular.forEach($scope.Country, function (item) {
                    if (parseInt($scope.CreatorModal.CountryId) === item.CountryId) {
                        $scope.CreatorModal.CountryId = item.CountryId;

                    }
                });

                var audienceArry = responseJson.data.TargetAudience
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

                var categoriesArry = responseJson.data.Categories
                var categoriesArryNew = categoriesArry.split(', ')

                $scope.ProductCategory = {};
                fpService.getData($_ProductCategory.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.ProductCategory = responseJson.data;

                        categoriesArryNew.forEach(function (itemArr) {
                            angular.forEach($scope.ProductCategory, function (item) {
                                if ((itemArr) === item.Name) {
                                    item.isChecked = true;
                                }
                            });
                        });

                        // $scope.ProductCategory[0].ProductCategoryId; 
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_SaveCreatorModal = function (CreatorModal) {
        $scope.CreatorModal = {};
        var age = "";
        angular.forEach($scope.AudienceAgeModal, function (item) {
            if (!angular.isUndefined(item) && item.checked) {
                age = age + item.AudienceAgeId + ",";
            }
        });

        var categories = "";
        angular.forEach($scope.ProductCategory, function (item) {
            if (!angular.isUndefined(item) && item.checked) {
                if (categories == "") {
                    categories = item.Name;
                } else {
                    categories = categories + ", " + item.Name ;
                }
            }
        });
        
        CreatorModal.TargetAudience = age;
        CreatorModal.Categories = categories;
        CreatorModal.DOB = $scope.CreatorDOB;
        fpService.postData($_Creator.Save, CreatorModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Information saved successfully.', "Success");
                $scope.fn_DefaultCreatorSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Information already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    }

    //Mail box
    $scope.fn_DefaultMailboxSettings = function () {
        var MailBoxFilterBy = $scope.MailBoxFilter;
        if (MailBoxFilterBy == "All")
            MailBoxFilterBy = "3";
        else if (MailBoxFilterBy == "Read")
            MailBoxFilterBy = "1";
        else
            MailBoxFilterBy = "0";

        $scope.fn_GetAllMailbox(0);
        $scope.MailboxModal = {};
        var param = { MailTypeId: MailTypeId, MailBoxFilterBy: MailBoxFilterBy };
    };

    $scope.fn_SearchMailbox = function () {
        var MailBoxSearchByText = $scope.MailBoxSearch;
        if (MailBoxSearchByText != "") {
            // SearchMailBoxByText
            $("#v-pills-1").show();
            $("#divViewmailBox").hide();
            $("#divReplyMailBox").hide();
            $scope.lstMailbox = {};
            $scope.gridLoader = true;
            //$scope.MailBoxSearch = "";
            var param = { MailBoxSearchByText: MailBoxSearchByText };
            fpService.getData($_Mailbox.Search, param, function (response) {
                var responseJson = response.data;
                if (responseJson.statusCode === 200) {
                    if (responseJson.data == "logOut") {
                        RedirectToLogin();
                    }
                    $scope.lstMailbox = responseJson.data;
                }
                if (responseJson.statusCode === 204) {
                    toastr.error('Error in getting data.');
                }
                $scope.gridLoader = false;
            });
            $scope.MailboxModal = {};
        }
        else {
            fn_GetAllMailbox(0);
        }
    }

    $scope.fn_GetAllMailbox = function (MailTypeId) {
        $("#v-pills-1").show();
        $("#divViewmailBox").hide();
        $("#divReplyMailBox").hide();
        $scope.lstMailbox = {};
        $scope.gridLoader = true;
        var MailBoxFilterBy = $scope.MailBoxFilter;
        if (MailBoxFilterBy == "All")
            MailBoxFilterBy = "3";
        else if (MailBoxFilterBy == "Read")
            MailBoxFilterBy = "1";
        else
            MailBoxFilterBy = "0";

        // alert($scope.MailBoxFilter);

        var param = { MailTypeId: MailTypeId, MailBoxFilterBy: MailBoxFilterBy };
        fpService.getData($_Mailbox.Get, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstMailbox = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
        $scope.MailboxModal = {};
    };


    $scope.fn_ViewMailbox = function (MailboxId) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId };
        fpService.getData($_Mailbox.View, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {

                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }

                $scope.MailboxModal = responseJson.data;
                $("#v-pills-1").hide();
                $("#divViewmailBox").show();
                $("#divReplyMailBox").hide();

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };



    $scope.fn_SaveMailbox = function (MailboxModal) {
        fpService.postData($_Mailbox.Save, MailboxModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {

                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }

                toastr.success('Mailbox saved successfully.', "Success");
                $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Mailbox name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };

    $scope.fn_ReplyMailboxModal = function (MailboxModal) {
        $scope.MailboxModal = {};
        //var param = { MailboxId: MailboxId };
        fpService.postData($_Mailbox.Save, MailboxModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {

                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }

                toastr.success('Mailbox reply successfully.', "Success");
                $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Mailbox name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in replying.', "Error");
            }
        });
    };

    $scope.fn_EditMailbox = function (MailboxId) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId };
        fpService.getData($_Mailbox.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }

                $scope.MailboxModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_ReplyMailbox = function (MailboxId) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId };

        //  MailboxModal.Message = "";
        fpService.getData($_Mailbox.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }

                $scope.MailboxModal = responseJson.data;
                $("#v-pills-1").hide();
                $("#divViewmailBox").hide();
                $("#divReplyMailBox").show();
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveMailbox = function (MailboxId, MailTypeId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { MailboxId: MailboxId };
                    fpService.postData($_Mailbox.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Mailbox removed successfully.', "Success");
                            $scope.fn_GetAllMailbox(MailTypeId);
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in removing records.', "Error");
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

    $scope.fn_CampaignByFillter = function (CampaignFillterModal) {
      //  $scope.CampaignFillterModal = {};
        $scope.CampaignModal = {};
        fpService.getData($_Creator.GetCampaignByFillter, CampaignFillterModal, function (response) {
            var responseJson = response.data;
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CampaignModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
        $scope.CampaignFillterModal.BudgetId = CampaignFillterModal.BudgetId;
        $scope.CampaignFillterModal.CampaignDurationId = CampaignFillterModal.CampaignDurationId;
        $scope.CampaignFillterModal.ProductCategoryId = CampaignFillterModal.ProductCategoryId;
        $scope.CampaignFillterModal.CountryId = CampaignFillterModal.CountryId;
    }

    $scope.fn_ViewCreatorProfile = function (value) {
        //  $scope.fn_GetCountry();
        //$scope.divProfile.show();
        //$scope.divDashboard.hide();
       
        $("#divCreatorProfile").show();

        $("#divAllCreator").hide();
        $scope.fn_GetCreatorInfoForClient(value);
    }

    $scope.fn_GetCreatorInfoForClient = function (value) {
        $scope.CreatorModal = {};
        var param = { value: value };
        fpService.getData($_Creator.GetCreatorInfo, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CreatorModal = responseJson.data;

                var audienceArry = responseJson.data.TargetAudience
                var audienceArryNew = audienceArry.split(',')

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
    };

});
