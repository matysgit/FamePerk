﻿
fpApp.controller("ProfileController", function ($scope, fpService, $http) {
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);
    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_DefaultProfileSettings = function () {
        $scope.fn_GetUnReadMsg();
        $scope.lstDocuments = [];
        $scope.imgProfile = '../Images/profile.png';
        $scope.imgLoader = false;
        $scope.docLoader = false;
    };
    $scope.fn_GetUnReadMsg = function () {
        $scope.UnReadMsgModal = {};
        //var param = { MailboxId: MailboxId };
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

    $scope.fn_DocumentUpload = function (uploadedFile) {
        $scope.docLoader = true;
        var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".mp4", ".mpeg", ".mkv", ".xls", ".xlsx", ".pdf", ".doc", ".docx", ".csv", ".mp3", "wav", ".txt", ".avi", ".asf", ".mov", ".AVCHD", ".flv", ".mpg", ".WMV", ".DivX", ".H.264"];
        if (uploadedFile.length > 0) {
            if (uploadedFile[0]) {
                if (uploadedFile[0].size > 0) {
                    var blnValid = false;
                    var sFileName = uploadedFile[0].name;
                    var sFileSize = uploadedFile[0].size;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            break;
                        }
                    }
                    if (!blnValid) {
                        $scope.IsUploading = false;
                        toastr["warning"]("Please upload a valid document.", "Warning");
                        return true;
                    }
                    if (blnValid && sFileSize > (1024 * 1024 * 200)) {
                        $scope.IsUploading = false;
                        toastr["warning"]("Upload document size exceeded allowed limits(Maximum 200MB).", "Warning");
                        return true;
                    }
                    var formData = new FormData();
                    for (var i in uploadedFile) {
                        formData.append('file', uploadedFile[i]);
                    }

                    $http.post($_UploadDocument, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (result) {
                        var response = result.data;
                        angular.forEach(response.ActualFileName, function (value, index) {
                            $scope.lstDocuments.push({
                                FileName: value,
                                UplodedFileName: response.UplodedFileName[index]
                            });
                        });
                        $scope.docLoader = false;
                    });
                }
            }
            // angular.element("input[type='file']").val(null);
        }
    };


    $scope.fn_ImageUpload = function (uploadedFile) {
        $scope.imgLoader = true;
        var _validFileExtensions = [".jpg", ".jpeg", ".gif", ".png"];
        if (uploadedFile.length > 0) {
            if (uploadedFile[0]) {
                if (uploadedFile[0].size > 0) {
                    var blnValid = false;
                    var sFileName = uploadedFile[0].name;
                    var sFileSize = uploadedFile[0].size;
                    for (var j = 0; j < _validFileExtensions.length; j++) {
                        var sCurExtension = _validFileExtensions[j];
                        if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                            blnValid = true;
                            break;
                        }
                    }
                    if (!blnValid) {
                        $scope.IsUploading = false;
                        toastr["warning"]("Please upload a valid document.", "Warning");
                        return true;
                    }
                    if (blnValid && sFileSize > (1024 * 1024 * 200)) {
                        $scope.IsUploading = false;
                        toastr["warning"]("Upload document size exceeded allowed limits(Maximum 200MB).", "Warning");
                        return true;
                    }
                    var formData = new FormData();
                    for (var i in uploadedFile) {
                        formData.append('file', uploadedFile[i]);
                    }

                    $http.post($_UploadDocument, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (result) {
                        $scope.imgProfile = '../Upload/TempDir/' + result.data.UplodedFileName[0];
                        $scope.imgLoader = false;
                    });
                }
            }
            // angular.element("input[type='file']").val(null);
        }
    };

    $scope.fn_RemoveDocument = function (lst) {
        fpService.getData($_RemoveDocument, { fileName: lst.UplodedFileName }, function () {
            $scope.lstDocuments.splice($scope.lstDocuments.indexOf(lst), 1);
        });
    };

    $scope.fn_ViewDocument = function (lst) {
        window.open("../Upload/TempDir/" + lst.UplodedFileName);
    };

});