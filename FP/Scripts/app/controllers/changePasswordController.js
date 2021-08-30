'use strict';

fpApp.controller("ChangePasswordController", function ($scope, fpService, $http) {

    $scope.fn_ValidateForm = function (changePasswordModel) {
        if (!changePasswordModel.isMinLength || !changePasswordModel.isLowercase
            || !changePasswordModel.isUppercase || !changePasswordModel.isNumeric
            || !changePasswordModel.isSpecialCharcter) {
            toastr.warning('Password criteria not matching.', "Warning");
            return false;
        }

        else if (changePasswordModel.NewPassword !== changePasswordModel.ConfirmPassword) {
            toastr.warning('New Password and Confirm Password does not match.', "Warning");
            return false;
        }
        return true;
    };

    $scope.fnCheckPassword = function (changePasswordModel) {
        var newPassword = changePasswordModel.NewPassword;
        changePasswordModel.isMinLength = false;
        changePasswordModel.isUppercase = false;
        changePasswordModel.isLowercase = false;
        changePasswordModel.isNumeric = false;
        changePasswordModel.isSpecialCharcter = false;

        var upperCase = newPassword.match(/[A-Z]/);

        if (newPassword.length > 5) {
            changePasswordModel.isMinLength = true;
        }

        if (newPassword.match(/[A-Z]/) != null) {
            changePasswordModel.isUppercase = true;
        }

        if (newPassword.match(/[a-z]/) != null) {
            changePasswordModel.isLowercase = true;
        }

        if (newPassword.match(/[0-9]/) != null) {
            changePasswordModel.isNumeric = true;
        }

        if (newPassword.match(/[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/g) != null) {
            changePasswordModel.isSpecialCharcter = true;
        }
    }

    $scope.fn_SavePassword = function (changePasswordModel) {
        fpService.postData($_Account.ChangePassword, changePasswordModel, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                toastr.success('Password changed successfully.', "Success");
            }
            if (responseJson.statusCode === 204) {
                toastr.warning('Invalid current password.', "Warning");
            }
            $scope.fn_DefaultForm();
        });
    };

    $scope.fn_DefaultForm = function () {
        $scope.changePasswordModel = {
            OldPassword: '',
            NewPassword: '',
            ConfirmPassword: '',
            isMinLength: false,
            isLowercase: false,
            isUppercase: false,
            isNumeric: false,
            isSpecialCharcter: false
        };
    }
});