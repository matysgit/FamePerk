'use strict';

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

        $scope.fn_GetCountry();
        $scope.fn_GetAge();
        $scope.fn_GetProductCategory();
       // $scope.fn_GetCreatorInfo("");
        $scope.fn_GetProfileImg();
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

    //
    $scope.fn_GetAge = function () {

        $scope.AudienceAgeModal = {};
        fpService.getData($_AudienceAge.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.AudienceAgeModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetCountry = function () {

        $scope.Country = {};
        fpService.getData($_Creator.GetCountry, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                responseJson.data.unshift({ CountryId: 0, Name: 'Select Country' })
                $scope.Country = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetProductCategory = function () {

        $scope.ProductCategory = {};
        fpService.getData($_ProductCategory.Get, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.ProductCategory = responseJson.data;
                // $scope.ProductCategory[0].ProductCategoryId; 
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
    };

    $scope.fn_GetCreatorInfo = function (value) {
        $scope.CreatorModal = {};
        var param = { value: value };
        fpService.getData($_Creator.Get, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.CreatorModal = responseJson.data;

                $scope.CreatorDOB = {
                    DOB: new Date(responseJson.data.DOB)
                };

                angular.forEach($scope.Country, function (item) {
                    if (parseInt($scope.CreatorModal.CountryId) === item.CountryId) {
                        $scope.CreatorModal.CountryId = item.CountryId;

                    }
                });

                var audienceArry = responseJson.data.TargetAudience
                var audienceArryNew = audienceArry.split(',')

                $scope.AudienceAgeModal = {};
                fpService.getData($_AudienceAge.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.AudienceAgeModal = responseJson.data;
                        audienceArryNew.forEach(function (itemArr) {
                            angular.forEach($scope.AudienceAgeModal, function (item) {
                                if (parseInt(itemArr) === item.AudienceAgeId) {
                                    item.isChecked = true;
                                }
                            });
                        });
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

                var categoriesArry = responseJson.data.Categories
                var categoriesArryNew = categoriesArry.split(', ')

                $scope.ProductCategory = {};
                fpService.getData($_ProductCategory.Get, "", function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        if (responseJson.data == "logOut") {
                            RedirectToLogin();
                        }
                        $scope.ProductCategory = responseJson.data;

                        categoriesArryNew.forEach(function (itemArr) {
                            angular.forEach($scope.ProductCategory, function (item) {
                                if ((itemArr) === item.Name) {
                                    item.isChecked = true;
                                }
                            });
                        });

                        // $scope.ProductCategory[0].ProductCategoryId; 
                    }
                    if (responseJson.statusCode === 204) {
                        toastr.error('Error in getting data.');
                    }

                });

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_SaveCreatorModal = function (CreatorModal) {
        $scope.CreatorModal = {};
        var age = "";
        angular.forEach($scope.AudienceAgeModal, function (item) {
            if (!angular.isUndefined(item) && item.checked) {
                age = age + item.AudienceAgeId + ",";
            }
        });

        var categories = "";
        angular.forEach($scope.ProductCategory, function (item) {
            if (!angular.isUndefined(item) && item.checked) {
                if (categories == "") {
                    categories = item.Name;
                } else {
                    categories = categories + ", " + item.Name;
                }
            }
        });

        CreatorModal.TargetAudience = age;
        CreatorModal.Categories = categories;

        fpService.postData($_Creator.Save, CreatorModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Information saved successfully.', "Success");
                $scope.fn_DefaultCreatorSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Information already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });

        location.href = '/Creator/Profile';
    }

    //$scope.fn_AuthenticateSocialLogin = function (providerName) {
    //    alert(providerName);
    //}

    $scope.fn_ImageUploadCreator = function (uploadedFile) {
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

                    $http.post($_UploadImage, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    }).then(function (result) {
                        $scope.imgProfile = '../Upload/Profile/' + result.data.UserId + '/' + result.data.UplodedFileName[0];
                        $scope.imgLoader = false;
                    });
                }
            }
            // angular.element("input[type='file']").val(null);
        }
    }

    $scope.fn_GetProfileImg = function () {
        fpService.postData($_Creator.GetImg, "", function (response) {
            var responseJson = response.data;

            if (responseJson.data == "logOut") {
                RedirectToLogin();
            }
            if (responseJson.FileName == "") {
                $scope.imgProfile = '../Images/profile.png';
                //$scope.imgLoader = true;
            }
            else {

                $scope.imgProfile = '../Upload/Profile/' + responseJson.UserId + '/' + responseJson.FileName;
                $scope.imgLoader = false;
            }

            //if (responseJson.statusCode === 204) {
            //    toastr.error('Error in getting data.');
            //}
        });
    };

    $scope.fn_SaveCreatorYoutubeUrl = function (CreatorModal) {

        fpService.postData($_Creator.SaveYoutubeUrl, CreatorModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                location.href = '/Creator/Index';
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Invalid url.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });

        
    }

});