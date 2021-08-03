'use strict';

fpApp.controller("BudgetController", function ($scope, fpService, $http) {

    $scope.fn_DefaultBudgetSettings = function () {
        $scope.fn_GetAllBudget();
        $scope.BudgetModal = {};
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllBudget = function () {
        $scope.lstBudget = [];
        $scope.gridLoader = true;
        fpService.getData($_Budget.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstBudget = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveBudget = function (BudgetModal) {
        fpService.postData($_Budget.Save, BudgetModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Budget saved successfully.', "Success");
                $scope.fn_DefaultBudgetSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Budget name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };

    $scope.fn_EditBudget = function (BudgetId) {
        $scope.BudgetModal = {};
        var param = { BudgetId: BudgetId };
        fpService.getData($_Budget.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.BudgetModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.', "Error");
            }
        });
    };

    $scope.fn_RemoveBudget = function (BudgetId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { BudgetId: BudgetId };
                    fpService.postData($_Budget.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Budget removed successfully.', "Success");
                            $scope.fn_GetAllBudget();
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
