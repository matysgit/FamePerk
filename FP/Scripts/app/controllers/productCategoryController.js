'use strict';

fpApp.controller("ProductCategoryController", function ($scope, fpService, $http) {

    $scope.fn_DefaultProductSettings = function () {
        $scope.fn_GetAllProductCategory();
        $scope.productCategoryModal = {};
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllProductCategory = function () {
        $scope.lstProductCategory = [];
        $scope.gridLoader = true;
        fpService.getData($_ProductCategory.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstProductCategory = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveProductCategory = function (productCategoryModal) {
        fpService.postData($_ProductCategory.Save, productCategoryModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Product category saved successfully.', "Success");
                $scope.fn_DefaultProductSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Product category name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };

    $scope.fn_EditProductCategory = function (ProductCategoryId) {
        $scope.productCategoryModal = {};
        var param = { ProductCategoryId: ProductCategoryId };
        fpService.getData($_ProductCategory.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.productCategoryModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.', "Error");
            }
        });
    };

    $scope.fn_RemoveProductCategory = function (ProductCategoryId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { productCategoryId: ProductCategoryId };
                    fpService.postData($_ProductCategory.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Product category removed successfully.', "Success");
                            $scope.fn_GetAllProductCategory();
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
