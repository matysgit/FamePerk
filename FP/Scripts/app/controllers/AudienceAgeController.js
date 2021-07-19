'use strict';

fpApp.controller("AudienceAgeController", function ($scope, fpService, $http) {

    $scope.fn_DefaultAudienceAgeSettings = function () {
        $scope.fn_GetAllAudienceAge();
        $scope.AudienceAgeModal = {};
    };


    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllAudienceAge = function () {
        $scope.lstAudienceAge = [];
        $scope.gridLoader = true;
        fpService.getData($_AudienceAge.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstAudienceAge = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveAudienceAge = function (AudienceAgeModal) {
        fpService.postData($_AudienceAge.Save, AudienceAgeModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Audience Age saved successfully.', "Success");
                $scope.fn_DefaultAudienceAgeSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Audience Age name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };

    $scope.fn_EditAudienceAge = function (AudienceAgeId) {
        $scope.AudienceAgeModal = {};
        var param = { AudienceAgeId: AudienceAgeId };
        fpService.getData($_AudienceAge.Edit, param, function (response) {
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

    $scope.fn_RemoveAudienceAge = function (AudienceAgeId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { AudienceAgeId: AudienceAgeId };
                    fpService.postData($_AudienceAge.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Audience Age removed successfully.', "Success");
                            $scope.fn_GetAllAudienceAge();
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

});
