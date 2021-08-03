'use strict';

fpApp.controller("BankDetailsController", function ($scope, fpService, $http) {
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);
    $scope.fn_DefaultBankSettings = function () {
        $scope.fn_GetUnReadMsg();
        $scope.SaveBank = "Save";
        $("#divBankList").show();
        $("#divAddBank").hide();
        $scope.fn_GetAllBankDetails();
        $scope.BankDetailModal = {};
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

    $scope.fn_AddBankDetails = function () {
        $scope.lstBankDetail = [];
        $scope.gridLoader = true;
        $scope.BankDetailModal = {};

        $("#divAddBank").show();
        $("#divBankList").hide();
    };

    $scope.fn_BackBankDetails = function () {
        fn_DefaultBankSettings();
    };


    $scope.fn_GetAllBankDetails = function () {
        $scope.lstBankDetail = [];
        $scope.gridLoader = true;
        //$("#divAddNewBank").show();
        fpService.getData($_BankDetails.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstBankDetail = responseJson.data;
                if ($scope.lstBankDetail != "")
                    $("#divAddNewBank").hide();
                else
                    $("#divAddNewBank").show();
                
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveBankDetails = function (BankDetailModal) {
        fpService.postData($_BankDetails.Save, BankDetailModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Data saved successfully.', "Success");
                $scope.fn_DefaultBankSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Data already exists.',"Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.',"Error");
            }
        });
    };

    $scope.fn_EditBankDetails = function (ID) {
        $scope.BankDetailModal = {};
        var param = { ID: ID };
        fpService.getData($_BankDetails.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.BankDetailModal = responseJson.data;
             
                $("#divAddBank").show();
                $("#divBankList").hide();
                $scope.SaveBank = "Update";
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveBank = function (ID) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { ID: ID };
                    fpService.postData($_BankDetails.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            
                            toastr.success('Record Deleted successfully.','Success');
                            $scope.fn_GetAllBankDetails();
                        }
                        if (responseJson.statusCode === 204) {
                            toastr.error('Error in removing records.','Error');
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
