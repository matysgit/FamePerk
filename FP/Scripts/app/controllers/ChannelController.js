'use strict';

fpApp.controller("ChannelController", function ($scope, fpService, $http) {

    $scope.fn_DefaultChannelSettings = function () {
        $scope.fn_GetAllChannel();
        $scope.ChannelModal = {};
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllChannel = function () {
        $scope.lstChannel = [];
        $scope.gridLoader = true;
        fpService.getData($_Channel.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstChannel = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveChannel = function (ChannelModal) {
        fpService.postData($_Channel.Save, ChannelModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Channel saved successfully.', "Success");
                $scope.fn_DefaultChannelSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Channel already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };

    $scope.fn_EditChannel = function (ChannelId) {
        $scope.ChannelModal = {};
        var param = { ChannelId: ChannelId };
        fpService.getData($_Channel.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.ChannelModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.', "Error");
            }
        });
    };

    $scope.fn_RemoveChannel = function (ChannelId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { ChannelId: ChannelId };
                    fpService.postData($_Channel.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Channel removed successfully.', "Success");
                            $scope.fn_GetAllChannel();
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
