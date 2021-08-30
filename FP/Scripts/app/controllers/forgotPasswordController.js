'use strict';

fpApp.controller("ForgotPasswordController", function ($scope, fpService) {

    $scope.txtEmailAddress = "";
    $scope.isMailSent = false;

    $scope.fnSendRestPasswordLink = function (email) {
        $scope.isMailSent = false;
        if (validateEmail(email)) {
            fpService.postData($_Account.ForgotPassword, {
                email
            }, function (response) {
                var responseJson = response.data;
                if (responseJson.statusCode === 200) {
                    toastr.success('Password link sent successfully.', 'Success');
                    $scope.txtEmailAddress = "";
                    $scope.isMailSent = true;
                }
                if (responseJson.statusCode === 204) {
                    toastr.warning('Email not found.', 'Warning');
                }
            });
        }
    };

    function validateEmail(email) {
        if (!email) {
            toastr.warning('Email cannot be blank.', 'Warning');
            return false
        }

        var emailFormat = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
        if (!emailFormat.test(email)) {
            toastr.warning('Please enter valid email.', 'Warning');
            return false;
        }

        return true;
    }
});