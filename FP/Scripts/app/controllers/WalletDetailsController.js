'use strict';

fpApp.controller("WalletDetailsController", function ($scope, fpService, $http) {
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);
    $scope.fn_DefaultWalletSettings = function () {
        $scope.fn_GetUnReadMsg();
        $scope.fn_GetAllWalletAmount();
        $scope.WalletAmountModal = {};
    };

    $scope.fn_GetUnReadMsg = function () {
        $scope.UnReadMsgModal = {};
        fpService.getData($_Mailbox.GetUnReadMsg, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data =="logOut") {
                    RedirectToLogin();  
                }
                $scope.UnReadMsgModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };
    function RedirectToLogin() {
        location.href = '/Account/Login';
    }
    $scope.fn_GetAllWalletAmount = function () {
        $scope.lstWalletAmount = [];
        $scope.gridLoader = true;
        fpService.getData($_WalletDetails.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstWalletAmount = responseJson.data;

                var totalAmount = 0;;
                angular.forEach($scope.lstWalletAmount, function (item) {
                    totalAmount += parseFloat(item.Amount);
                });
                $scope.TotalAmount = "$ "+totalAmount;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

    $scope.fn_SaveWalletDetails = function (WalletAmountModal) {
        fpService.postData($_WalletDetails.Save, WalletAmountModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Data saved successfully.');
                $scope.fn_DefaultWalletSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Account Number already exists.');
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.');
            }
        });
    };

    $scope.fn_Edit_WalletDetails = function (ID) {
        $scope.WalletAmountModal = {};
        var param = { ID: ID };
        fpService.getData($_WalletDetails.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.WalletAmountModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveWalletAmount = function (ID) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { ID: ID };
                    fpService.postData($_WalletDetails.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            toastr.success('Record Deleted successfully.');
                            $scope.fn_GetAllWalletAmount();
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

    $scope.fn_OpenStripePayemnt = function (amount) {
        //var res = "";
        var token = function (res) {
            var $id = $('<input type=hidden name=stripeToken />').val(res.id);
            var $email = $('<input type=hidden name=stripeEmail />').val(res.email);
            $('form').append($id).append($email).submit();
        };
        $scope.WalletAmountModal = {};
        $scope.WalletAmountModal.Amount = amount;
        $scope.WalletAmountModal.Token = token;


        var param = { amount: amount, token: token };
       

        $('form').attr("method", "post");
        $('form').attr("action", "/Client/Charge");
       // $('form').attr("action", "/FP/Client/Charge");
        StripeCheckout.open({
            key: 'pk_test_51ISlIhLdK4kakFbtgwXzBofdRl3AVlh0bEfZGsoryeqc3R4YXD9fulNki6zE3m7s5r5e7EGIAeoEicctZWJI889400H4EouFx7',//@ViewBag.StripePublishKey,
            amount: amount,
            name: 'Fame Perks',
            panelLabel: 'Pay',
            token: token,
            param: amount
        });
        return false;


    };


});
