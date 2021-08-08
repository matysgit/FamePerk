'use strict';

fpApp.controller("CreateCampaignController", function ($scope, fpService, $http) {
    $scope.ProductCategoryModal = [];
    $scope.example14model = [];
    $scope.setting1 = {
        scrollableHeight: '200px',
        scrollable: true,
        enableSearch: true
    };

    $scope.setting2 = {
        scrollableHeight: '200px',
        scrollable: true,
        enableSearch: false
    };
    
    
    $scope.example1model = [];

    $scope.CreateCampaignModal={ };
    $scope.fn_DefaultCampaignSettings = function () {
        $scope.fn_GetCurrencyType();
        $scope.ShowSaveCampaign = true;

        $scope.fn_GetCurrencyType();
        //$scope.fn_GetYouTubeType();
        //$scope.fn_GetSupplementalChannel();
        $("#divYouTubeVideoType").hide();
        $scope.CreateCampaignModal.Status = "Draft";
        $("#divDraft").show();
        $("#divPublish").show();
        $("#divSendToPlatform").show();

        var url;
        url = window.location.href;
        var campaignData = url.split("?");
        var isCamapaignId = campaignData[1].split("=");
        if ("CampaignId" == isCamapaignId[0]) {
            $scope.fn_ViewCampaign(isCamapaignId[1]);
        }
        else {
            $scope.fn_GetYouTubeType();
            $scope.fn_GetSupplementalChannel();
            $scope.fn_GetProductCategory();
            $scope.fn_GetCampaignDuration();
            $scope.fn_GetBudget();
            $scope.fn_GetAge();
            $scope.fn_GetCountry();

        }
       
    };

    $scope.CurrencyTypeModal = {};
    $scope.fn_ChangeCurrencyType = function (CurrencyTypeModal) {
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

                $scope.fn_DefaultCampaignSettings();
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

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

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
       
        $scope.ProductCategory = {};
        fpService.getData($_ProductCategory.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.ProductCategory = responseJson.data;
                $scope.CreateCampaignModal.ProductCategoryId = $scope.ProductCategory[0].ProductCategoryId;

              //  $scope.example1data = responseJson.data;

                $scope.searchTerm = '';
                $scope.clearSearchTerm = function () {
                    $scope.searchTerm = '';
                };
                // The md-select directive eats keydown events for some quick select
                // logic. Since we have a search input here, we don't need that logic.
                $element.find('input').on('keydown', function (ev) {
                    ev.stopPropagation();
                });

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
           
        });

       
    };

    $scope.fn_GetCountry = function () {
        $scope.Country = {};
        $scope.example1data = {};
        fpService.getData($_Creator.GetCountry, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Country = responseJson.data;
              //  $scope.CreateCampaignModal.CountryId = $scope.Country[0].CountryId;

                $scope.CountryModal = [];
               // console.log(responseJson.data);
                $scope.CountryList = responseJson.data;
             

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

    $scope.fn_ShowNextDiv = function (DivId, BtnId) {
        $("#" + DivId+"").show();
        $("#" + BtnId + "").hide();
    }
    $scope.fn_SaveCampaign = function (CreateCampaignModal, Status) {
        var age = "";
        angular.forEach($scope.AudienceAgeModal, function (item) {
            if (item.selected) {
                if(age=="")
                    age = item.AudienceAgeId;
                else
                    age = age +","+item.AudienceAgeId;
            }
        });

        var YouTubeVideoType = ""; 
        angular.forEach($scope.YouTubeVideoTypeModal, function (item) {
            if (item.isChecked) {
                if (YouTubeVideoType == "")
                    YouTubeVideoType = item.YouTubeVideoTypeId;
                else
                    YouTubeVideoType = YouTubeVideoType + "," + item.YouTubeVideoTypeId;
            }
        });

        var SupplementalChannels = "";
        angular.forEach($scope.SupplementalChannelModal, function (item) {
            if (item.isChecked) {
                if (SupplementalChannels == "")
                    SupplementalChannels = item.SupplementalId;
                else
                    SupplementalChannels = SupplementalChannels + "," + item.SupplementalId;
            }
        });   

        var countryList = "";
        angular.forEach($scope.CountryModal, function (item) {
            if (countryList == "") {
                countryList = item.label;
            } else {
                    countryList = countryList + ", " + item.label ;
            }
        });

        CreateCampaignModal.SupplementalChannels = SupplementalChannels;
        CreateCampaignModal.YouTubeVideoType = YouTubeVideoType;
        CreateCampaignModal.AudienceAgeId = age; 
        CreateCampaignModal.Status = Status;
        CreateCampaignModal.Country = countryList;
        fpService.postData($_CreateCampaign.Save, CreateCampaignModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Campaign save successfully.', "Success");

                setTimeout(function () {
                    window.location.href = '/Client/Projects';
                }, 5000);
               
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Campaign already exists.',"Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };


    $scope.fn_ShowYouTubeVideoType = function () {
        if ($scope.chkYouTube) {
            $("#divYouTubeVideoType").show();
            $scope.fn_GetYouTubeType();
        }
        else {
            $("#divYouTubeVideoType").hide();
        }
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

                    $http.post($_CreateCampaign.UploadImage, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (result) {
                        
                        $scope.CreateCampaignModal.ProductPhoto = '../Upload/ProductPhoto/' + result.data.UserId + '/' + result.data.UplodedFileName[0];
                    });
                }
            }
            
        }
    };



    $scope.fn_ViewCampaign = function (ProjectId) {
       
        $scope.ShowSaveCampaign = true;
        $scope.CreateCampaignModal = {};
        var param = { ProjectId: ProjectId };
        fpService.getData($_Project.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CreateCampaignModal = responseJson.data[0];
                var youTubeTypeArry = responseJson.data[0].YouTubeVideoType;
                if (youTubeTypeArry == null || youTubeTypeArry=="") {
                    $scope.fn_GetYouTubeType();
                }
                else {
                    var youTubeTypeArryNew = youTubeTypeArry.split(',');
                    $scope.YouTubeVideoTypeModal = {};

                    fpService.getData($_Creator.GetYouTubeType, "", function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            $scope.YouTubeVideoTypeModal = responseJson.data;
                            if (youTubeTypeArry != "") {
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
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in getting data.');
                        }
                    });
                }
                    
                //todo
                var supplementalArry = responseJson.data[0].SupplementalChannels;
                if (supplementalArry == null || supplementalArry == "") {
                    $scope.fn_GetSupplementalChannel();

                }
                else {
                    var supplementalArryNew = supplementalArry.split(',');
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
                }
                
                $scope.ProductCategory = {};
                fpService.getData($_ProductCategory.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.ProductCategory = responseJson.data;
                       
                        angular.forEach($scope.ProductCategory, function (item) {
                            if (parseInt($scope.CreateCampaignModal.ProductCategoryId) === item.ProductCategoryId) {
                                $scope.CreateCampaignModal.ProductCategoryId = item.ProductCategoryId
                                item.selected = true;
                                }
                            });
                       
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

                //bug
                $scope.Budget = {};

                fpService.getData($_Budget.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.Budget = responseJson.data;
                        //$scope.CreateCampaignModal.BudgetId = $scope.Budget[0].BudgetId;
                        angular.forEach($scope.Budget, function (item) {
                            if (parseInt($scope.CreateCampaignModal.BudgetId) === item.BudgetId) {
                                $scope.CreateCampaignModal.BudgetId = item.BudgetId;
                                $scope.ReviewBudget = item.Title;
                                item.selected = true;
                            }
                        });
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

                $scope.CampaignDuration = {};
                
                fpService.getData($_CampaignDuration.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.CampaignDuration = responseJson.data;
                       // $scope.CreateCampaignModal.CampaignDurationId = $scope.CampaignDuration[0].CampaignDurationId;
                        angular.forEach($scope.CampaignDuration, function (item) {
                            if (parseInt($scope.CreateCampaignModal.CampaignDurationId) === item.CampaignDurationId) {
                                item.selected = true;
                                $scope.CreateCampaignModal.CampaignDurationId = item.CampaignDurationId;
                            }
                        });
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

                var audienceArry = responseJson.data[0].AudienceAgeId;
                if (audienceArry == null || audienceArry == "") {
                    $scope.fn_GetAge();

                }
                else {
                    var audienceArryNew = audienceArry.split(',');
                    var age = "";
                    $scope.AudienceAgeModal = {};
                    fpService.getData($_AudienceAge.Get, "", function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            $scope.AudienceAgeModal = responseJson.data;
                            //todo
                            audienceArryNew.forEach(function (itemArr) {
                                angular.forEach($scope.AudienceAgeModal, function (item) {
                                    if (parseInt(itemArr) === item.AudienceAgeId || itemArr === "All") {
                                        item.selected = true;
                                        if (age == "") {
                                            age = item.Title;
                                        }
                                        else {
                                            age = age, item.Title;
                                        }

                                    }
                                });
                            });

                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in getting data.');
                        }

                    });
                    if (age == "") {
                        age = "All";
                    }
                    $scope.ReviewAudienceAge = age;
                }
                
                var countryArry = responseJson.data[0].Country;
                if (countryArry == null || countryArry == "") {
                    $scope.fn_GetCountry();
                }
                else {
                    var countryArryNew = countryArry.split(',');
                    $scope.Country = {};
                    fpService.getData($_Creator.GetCountry, "", function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            $scope.Country = responseJson.data;
                            $scope.CountryModal = [];
                            $scope.CountryList = responseJson.data;

                            countryArryNew.forEach(function (itemArr) {

                                
                                $scope.CountryModal.push($scope.CountryList.filter((a) => {
                                    // return a.CountryId == +itemArr
                                    return a.Country == +itemArr
                                })[1]);

                            });

                            countryArryNew.forEach(function (itemArr) {
                                angular.forEach($scope.CountryList, function (item) {
                                    if (itemArr == item.Name) {
                                        item.isChecked = true;
                                    }
                                });
                            });

                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in getting data.');
                        }

                    });


                   
                }

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
                if (responseJson.data[0].Status != "Draft") {
                    $scope.ShowSaveCampaign = false;
                    $("#divDraft").hide();
                    $("#divPublish").hide();
                    $("#divSendToPlatform").hide();
                }


               
              //  $scope.fn_GetReview();
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
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
                            toastr.success('Campaign Deleted successfully.', "Success");
                            $scope.fn_GetAllProposal();
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in removing Campaign.', "Error");
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

    $scope.toggleAll = function () {
        var toggleStatus = !$scope.isAllSelected;
        angular.forEach($scope.AudienceAgeModal, function (itm) { itm.selected = toggleStatus; });

    }

    $scope.fn_GetReview = function () {
    //    $scope.isAllSelected = $scope.AudienceAgeModal.every(function (itm) { return itm.selected; })
               
    //    var age = "";
    //    angular.forEach($scope.AudienceAgeModal, function (item) {
    //        if ( item.selected) {
    //            if (age == "") {
    //                age = item.Title;// + "-" + item.MaxValue; 
    //            }
    //            else
    //                //age = age + ", "+ item.MinValue + "-" + item.MaxValue;
    //                age = age + ", " + item.Title;// + "-" + item.MaxValue;
    //        }
    //    });
    //    if (age == "") {
    //        $scope.ReviewAudienceAge = "All";
    //    }
    //    $scope.ReviewAudienceAge = age;

    //   // var budget = "";
    //    fpService.getData($_Budget.Get, "", function (response) {
    //        var responseJson = response.data;
    //        if (responseJson.statusCode === 200) {
    //            if (responseJson.data == "logOut") {
    //                RedirectToLogin();
    //            }
    //            angular.forEach(responseJson.data, function (item) {
    //                if (!angular.isUndefined(item) && item.BudgetId == $scope.CreateCampaignModal.BudgetId) {
    //                 //   budget = item.Title;
    //                    $scope.ReviewBudget = item.Title;
    //                }
    //            });

    //        }
           

    //    });
       
       
    }

    ////$scope.toggleSelect = function () {
    ////    angular.forEach($scope.AudienceAgeModal, function (item) {
    ////        item.selected = event.target.checked;
    ////    });
    ////}
});
