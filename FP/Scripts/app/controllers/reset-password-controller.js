'use strict';

fpApp.controller("ResetPasswordController", function ($scope, fpService, $http) {

    $scope.fn_LoadDefaultForm = function () {
        $scope.resetPasswordModel = {
            NewPassword: '',
            ConfirmPassword: '',
            IsChanging: false,
            IsChanged: false
        }
    }
    $scope.fn_ResetPassword = function (resetPasswordModel) {
        if (fn_ValidateForm(resetPasswordModel)) {
            fpService.postData($_Account.ResetPassword, {
                Password: resetPasswordModel.NewPassword,
                Id: '',
                Code: ''
            }, function (response) {
                var responseJson = response.data;
                if (responseJson.statusCode === 200) {
                    toastr.success('New password set successfully.', 'Success');
                    $scope.fn_LoadDefaultForm();
                }
                else if (responseJson.statusCode === 204) {
                    toastr.warning('Invalid request. Please reset your password again', 'Warning');
                }
                else {
                    toastr.error('Server error. Please try again.', 'Error');
                }
            });
        }
    }

    function fn_ValidateForm(resetPasswordModel) {
        const newPassword = resetPasswordModel.NewPassword;
        if (newPassword.length < 5
            || newPassword.match(/[A-Z]/) === null
            || newPassword.match(/[a-z]/) === null
            || newPassword.match(/[0-9]/) === null
            || newPassword.match(/[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/g) === null) {
            toastr.warning('Password criteria not matching.', "Warning");
            return false;
        }

        else if (newPassword !== resetPasswordModel.ConfirmPassword) {
            toastr.warning('Password and Confirm Password does not match.', "Warning");
            return false;
        }

        return true;
    };

});