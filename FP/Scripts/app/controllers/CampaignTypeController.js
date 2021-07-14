'use strict';

fpApp.controller("CampaignTypeController", function ($scope, fpService, $http) {

    $scope.fn_DefaultCampaignTypeSettings = function () {
        $scope.fn_GetAllCampaignType();
        $scope.CampaignTypeModal = {};
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllCampaignType = function () {
        $scope.lstCampaignType = [];
        $scope.gridLoader = true;
        fpService.getData($_CampaignType.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstCampaignType = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveCampaignType = function (CampaignTypeModal) {
        fpService.postData($_CampaignType.Save, CampaignTypeModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Campaign Type saved successfully.');
                $scope.fn_DefaultCampaignTypeSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Campaign Type already exists.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.');
            }
        });
    };

    $scope.fn_EditCampaignType = function (CampaignTypeId) {
        $scope.CampaignTypeModal = {};
        var param = { CampaignTypeId: CampaignTypeId };
        fpService.getData($_CampaignType.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CampaignTypeModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveCampaignType = function (CampaignTypeId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { CampaignTypeId: CampaignTypeId };
                    fpService.postData($_CampaignType.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Campaign Type removed successfully.');
                            $scope.fn_GetAllCampaignType();
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.success('Error in removing records.');
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

});
