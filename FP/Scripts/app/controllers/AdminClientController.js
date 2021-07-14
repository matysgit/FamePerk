'use strict';

fpApp.controller("ClientController", function ($scope, fpService, $http) {

    $scope.fn_DefaultClientSettings = function () {
        $scope.fn_GetAllClient();
      
    };

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_GetAllClient = function () {
        $scope.lstClientList = [];
        $scope.gridLoader = true;
        fpService.getData($_AudienceAge.GetAllClient, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstClientList = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
    };

   
    $scope.fn_EditClient = function (Id, Type) {
        //$scope.AudienceAgeModal = {};
        var param = { Id: Id, Type: Type };
        fpService.postData($_AudienceAge.EditClient, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in updateing data.');
            }

            $scope.fn_GetAllClient();
        });
    };

   

});
