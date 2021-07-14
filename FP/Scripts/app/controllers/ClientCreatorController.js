'use strict';

fpApp.controller("ClientCreatorController", function ($scope, fpService, $http) {
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);
    $scope.fn_DefaultCreatorSettings = function () {
        $scope.fn_GetUnReadMsg();
        //$("#divProfile").hide();
        $("#divAllCreator").show();
        $("#divCreatorProfile").hide();
        $scope.fn_GetCountry();
        $scope.fn_GetAge();
        $scope.fn_GetProfileImg();
        $scope.fn_GetCreatorList();
        $scope.fn_GetProductCategory();
        var url;
        url = window.location.href;
        var userData = url.split("?");
        var isUserId = userData[1].split("=");
        if ("Email" == isUserId[0]) {
            $scope.fn_GetCreatorInfoForClient(isUserId[1]);
        }
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

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


    $scope.fn_GetCreatorList = function () {

        $("#divAllCreator").show();
        $("#divCreatorProfile").hide();

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
                toastr.error('You are not approved yet.');
            }
        });
    };
    $scope.fn_GetProfileImg = function () {
        fpService.postData($_Creator.GetImg, "", function (response) {
            var responseJson = response.data;

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


    $scope.fn_GetCountry = function () {

        $scope.Country = [];
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
    }

    //$scope.fn_ViewCreatorProfile = function (value) {
    //    //  $scope.fn_GetCountry();
    //    //$scope.divProfile.show();
    //    //$scope.divDashboard.hide();

    //    $("#divCreatorProfile").show();

    //    $("#divAllCreator").hide();
    //    $scope.fn_GetCreatorInfoForClient(value);
    //}

    $scope.fn_GetCreatorInfoForClient = function (UserId) {
        $("#divCreatorProfile").show();

        $("#divAllCreator").hide();

        $scope.CreatorModal = {};
        var param = { UserId: UserId };
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


        fpService.postData($_Creator.GetImg, param, function (response) {
            var responseJson = response.data;

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

        $scope.CreatorFeedbackModal = {};
        fpService.getData($_Creator.GetCreatorFeedBack, param, function (response) {
            var responseJson = response.data;

            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CreatorFeedbackModal = responseJson.data;
                //$scope.imgLoader = true;
            }
            else {

               
            }



            //if (responseJson.statusCode === 204) {
            //    toastr.error('Error in getting data.');
            //}
        });


        
    };

    $scope.fn_ViewCreatorProfile = function (value) {
        //  $scope.fn_GetCountry();
        //$scope.divProfile.show();
        //$scope.divDashboard.hide();

       
        $scope.fn_GetCreatorInfoForClient(value);
    }

    $scope.GetCreatorByFillter = function (CreatorFillterModal) {
        $scope.CreatorModal = {};

        fpService.getData($_Creator.GetCreatorByFillter, CreatorFillterModal, function (response) {
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
        $scope.CreatorFillterModal.CountryId = CreatorFillterModal.CountryId;// Country.CountryId;
        $scope.CreatorFillterModal.AudienceAgeId = CreatorFillterModal.AudienceAgeId;// Country.CountryId;
    }
});
