'use strict';

fpApp.controller("CampaignDurationController", function ($scope, fpService, $http) {

    $scope.fn_DefaultCampaignDurationSettings = function () {
        $scope.fn_GetAllCampaignDuration();
        $scope.CampaignDurationModal = {};
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllCampaignDuration = function () {
        $scope.lstCampaignDuration = [];
        $scope.gridLoader = true;
        fpService.getData($_CampaignDuration.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstCampaignDuration = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveCampaignDuration = function (CampaignDurationModal) {
        fpService.postData($_CampaignDuration.Save, CampaignDurationModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Campaign Duration saved successfully.');
                $scope.fn_DefaultCampaignDurationSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Campaign Duration already exists.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.');
            }
        });
    };

    $scope.fn_EditCampaignDuration = function (CampaignDurationId) {
        $scope.CampaignDurationModal = {};
        var param = { CampaignDurationId: CampaignDurationId };
        fpService.getData($_CampaignDuration.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CampaignDurationModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveCampaignDuration = function (CampaignDurationId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { CampaignDurationId: CampaignDurationId };
                    fpService.postData($_CampaignDuration.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Campaign Duration removed successfully.');
                            $scope.fn_GetAllCampaignDuration();
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
