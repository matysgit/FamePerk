var fpApp = angular.module("fpApp", ['angularjs-dropdown-multiselect']);
//We Create UserServices.js file
fpApp.service("fpService", function ($http) {

    // post data with params for non stringify
    this.postData = function (pageUrl, pageParam, callback) {
        $http({
            method: 'POST',
            url: pageUrl,
            data: pageParam,
            dataType: "json"
        }).then(callback);
    };

    //Get data with params for non stringify
    this.getData = function (pageUrl, pageParam, callback) {
        $http({
            method: 'GET',
            url: pageUrl,
            params: pageParam
        }).then(callback);
    };

});
