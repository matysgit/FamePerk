'use strict';

fpApp.controller("LevelController", function ($scope, fpService, $http) {

    $scope.fn_DefaultLevelSettings = function () {
        $scope.fn_GetAllLevels();
        $scope.LevelModal = {};
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }
    $scope.fn_GetAllLevels = function () {
        $scope.button = "Save";
        $scope.lstLevel = [];
        $scope.gridLoader = true;
        fpService.getData($_Level.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstLevel = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveLevel = function (LevelModal) {
        fpService.postData($_Level.Save, LevelModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }

                toastr.success('Level saved successfully.');
                $scope.fn_DefaultLevelSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Level already exists.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.');
            }
        });
    };

    $scope.fn_EditLevel = function (LevelId) {
        $scope.LevelModal = {};
        var param = { LevelId: LevelId };
        fpService.getData($_Level.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.LevelModal = responseJson.data;
                $scope.button = "Update";
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveLevel = function (LevelId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { LevelId: LevelId };
                    fpService.postData($_Level.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Product category removed successfully.');
                            $scope.fn_DefaultLevelSettings();
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
