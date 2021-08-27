'use strict';

fpApp.controller("MailboxController", function ($scope, fpService, $http) {

   
  
    setInterval(function () {
        $scope.fn_GetUnReadMsg();
    }, 5000);

    function RedirectToLogin() {
        location.href = '/Account/Login';
    }

    $scope.fn_DefaultMailboxSettings = function () {
        $scope.fn_GetCurrencyType();
        $scope.fn_GetUnReadMsg();
        $scope.ShowUrl = false;
        $scope.ShowStatus = false;
        $("#btnApproveUpdate").hide();
        $("#btnRejectUpdate").hide();
        $scope.MailBoxFilter = "All";
        $scope.lstDocuments = [];
        //$scope.imgProfile = '../Images/profile.png';
        //$scope.imgLoader = false;
        $scope.docLoader = false;

        var MailBoxFilterBy = $scope.MailBoxFilter;
        if (MailBoxFilterBy == "All")
            MailBoxFilterBy = "3";
        else if (MailBoxFilterBy == "Read")
            MailBoxFilterBy = "1";
        else
            MailBoxFilterBy = "0";

       // $scope.fn_GetUnReadMsg();
        $scope.fn_GetAllMailbox(0);
        $scope.MailboxModal = {};
        var param = { MailTypeId: MailTypeId, MailBoxFilterBy: MailBoxFilterBy };

     //   $scope.fn_GetUnReadMsg();
       
    };

    $scope.CurrencyTypeModal = {};
    $scope.fn_ChangeCurrencyType = function (CurrencyTypeModal) {
        fpService.getData($_Creator.SetCurrencyType, CurrencyTypeModal, function (response) {
            var responseJson = response.data;
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Currency = {};
                $scope.CurrencyTypeModal = {};
                $scope.Currency = responseJson.data;
                $scope.CurrencyTypeModal.CurrencyId = response.data.currentCureency;

                $scope.fn_GetAllMailbox(0);
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    }

    $scope.fn_GetCurrencyType = function () {
        $scope.Currency = {};
        $scope.CurrencyTypeModal = {};
        fpService.getData($_Creator.GetCurrencyType, "", function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.Currency = responseJson.data;
                $scope.CurrencyTypeModal.CurrencyId = response.data.currentCureency;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }

        });
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
    //$scope.fn_DefaultProfileSettings = function () {
    //    $scope.lstDocuments = [];
    //    $scope.imgProfile = '../Images/profile.png';
    //    $scope.imgLoader = false;
    //    $scope.docLoader = false;
    //};

    $scope.fn_SearchMailbox = function () {
        var MailBoxSearchByText = $scope.MailBoxSearch;
        if (MailBoxSearchByText != "") {
            // SearchMailBoxByText
            $("#v-pills-1").show();
            $("#divViewmailBox").hide();
            $("#divReplyMailBox").hide();
            $scope.lstMailbox = [];
            $scope.gridLoader = true;
            //$scope.MailBoxSearch = "";
            var param = { MailBoxSearchByText: MailBoxSearchByText };
            fpService.getData($_Mailbox.Search, param, function (response) {
                var responseJson = response.data;
                if (responseJson.statusCode === 200) {
                    if (responseJson.data == "logOut") {
                        RedirectToLogin();
                    }
                    $scope.lstMailbox = responseJson.data;
                }
                if (responseJson.statusCode === 204) {
                    toastr.error('Error in getting data.');
                }
                $scope.gridLoader = false;
            });
            $scope.MailboxModal = {};
        }
        else {
            fn_GetAllMailbox(0);
        }
    }

    $scope.fn_GetAllMailbox = function (MailTypeId) {
        if (MailTypeId == "6") {
            $scope.MailBoxFilter = "All";
        }
        $("#v-pills-1").show();
        $("#divViewmailBox").hide();
        $("#divReplyMailBox").hide();
        $scope.lstMailbox = [];
        $scope.gridLoader = true;
        var MailBoxFilterBy = $scope.MailBoxFilter;
        if (MailBoxFilterBy == "All")
            MailBoxFilterBy = "3";
        else if (MailBoxFilterBy == "Read")
            MailBoxFilterBy = "1";
        else
            MailBoxFilterBy = "0";

       // alert($scope.MailBoxFilter);

        var param = { MailTypeId: MailTypeId, MailBoxFilterBy: MailBoxFilterBy};
        fpService.getData($_Mailbox.Get, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstMailbox = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
            $scope.gridLoader = false;
        });
        $scope.MailboxModal = {};
    };
  

    $scope.fn_ViewMailbox = function (MailboxId) {
        $scope.MailboxModal = {};
        $scope.CreatorModal = {};
        var param = { MailboxId: MailboxId };
       // var 
        fpService.getData($_Mailbox.View, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.MailboxModal = responseJson.data;
                $("#v-pills-1").hide();
                $("#divViewmailBox").show();
                $("#divReplyMailBox").hide();
                if (responseJson.data.ProjectProposalId != null) {
                    $("#divProposalMessage").show();
                    $("#divMessage").hide();

                    if (responseJson.data.MailTypeId != '1') {
                        $("#btnApprove").hide();

                    }
                    else {
                        $("#btnApprove").show();
                    }

                    if (responseJson.data.PaymentType == 'FixedCost') {
                        $("#divMilestone").hide();
                        $("#divFixedCost").show();
                    }
                    else {
                        $("#divMilestone").show();
                        $("#divFixedCost").hide();
                        if (responseJson.data.NoOfMilestone == '1') {
                            $("#lblMilestone1").show();
                            $("#lblMilestone2").hide();
                            $("#lblMilestone3").hide();
                        }
                        else if (responseJson.data.NoOfMilestone == '2') {
                            $("#lblMilestone1").show();
                            $("#lblMilestone2").show();
                            $("#lblMilestone3").hide();
                        }
                        else if (responseJson.data.NoOfMilestone == '3') {
                            $("#lblMilestone1").show();
                            $("#lblMilestone2").show();
                            $("#lblMilestone3").show();
                        }
                    }
                   
                }
                else {
                    $("#btnApprove").hide();
                    $scope.ShowUrl = false;
                    $scope.ShowStatus = false;
                    $("#btnApproveUpdate").hide();
                    $("#btnRejectUpdate").hide();
                    if (responseJson.data.ProjectProposalUpdateId != null) {
                        $scope.ShowUrl = true;
                        if (responseJson.data.Status == null) {
                            $("#btnApproveUpdate").show();
                            $("#btnRejectUpdate").show();
                        }
                        else {
                            $scope.ShowStatus = true;
                            if (responseJson.data.Status == "True") {
                                $scope.MailboxModal.Status = 'Approved';
                            }
                            else {
                                $scope.MailboxModal.Status = 'Rejected';
                            }
                        }
                       
                    }

                    $("#divMessage").show();
                    $("#divProposalMessage").hide();
                }

                var Email = $scope.MailboxModal.MailFrom;
                var param1 = { Email: Email };
                fpService.getData($_Mailbox.GetCreatorInfoMail, param1, function (response) {
                    var responseJson = response.data;
                    if (responseJson.statusCode === 200) {
                        $scope.CreatorModal = responseJson.data;

                        //var audienceArry = responseJson.data.TargetAudience
                        //var audienceArryNew = audienceArry.split(',')

                        //audienceArryNew.forEach(function (itemArr) {
                        //    angular.forEach($scope.AudienceAgeModal, function (item) {
                        //        if (parseInt(itemArr) === item.AudienceAgeId) {
                        //            item.isChecked = true;
                        //        }
                        //    });
                        //});
                        var UserId = $scope.CreatorModal.UserId;
                        param1 = { UserId: UserId };
                        fpService.postData($_Creator.GetImg, param1, function (response) {
                            var responseJson = response.data;

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

        fpService.getData($_Mailbox.GetDocument, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.lstDocument = responseJson.data;

            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_DownloadDocument = function (FilePath) {
        //$scope.MailboxModal = {};
        //var param = { BrandMailFileId: BrandMailFileId };
        //fpService.getData($_Mailbox.DownloadDocument, param, function (response) {
        //    var responseJson = response.data;
        //    if (responseJson.statusCode === 200) {
        //        $window.open(responseJson)
        //        toastr.success('Documant Downloaded.');
        //       // $scope.fn_DefaultMailboxSettings();
        //    }
        //    if (responseJson.statusCode === 409) {
        //        toastr.error('Error in dowloading.');
        //    }
        //    if (responseJson.statusCode === 204) {
        //        toastr.error('Error in dowloading.');
        //    }
        //});
        window.open(FilePath);
    };

    $scope.fn_SaveMailbox = function (MailboxModal) {
        fpService.postData($_Mailbox.Save, MailboxModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Mailbox saved successfully.', "Success");
                $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Mailbox name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in saving.', "Error");
            }
        });
    };

    $scope.fn_ReplyMailboxModal = function (MailboxModal) {
        $scope.MailboxModal = {};
        //var param = { MailboxId: MailboxId };
        fpService.postData($_Mailbox.Save, MailboxModal, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Mailbox reply successfully.', "Success");
                $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 409) {
                toastr.warning('Mailbox name already exists.', "Warning");
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in replying.', "Error");
            }
        });
    };

    $scope.fn_EditMailbox = function (MailboxId) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId };
        fpService.getData($_Mailbox.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.MailboxModal = responseJson.data;
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_ReplyMailbox = function (MailboxId) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId };
        
      //  MailboxModal.Message = "";
        fpService.getData($_Mailbox.Edit, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                $scope.MailboxModal = responseJson.data;
                $scope.MailboxModal.Message = "";
                $("#v-pills-1").hide();
                $("#divViewmailBox").hide();
                $("#divReplyMailBox").show();
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.');
            }
        });
    };

    $scope.fn_RemoveMailbox = function (MailboxId, MailTypeId) {
        BootstrapDialog.show({
            title: 'Warning',
            message: 'Are you sure you want to delete this record?',
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    dialog.close();
                    var jObject = { MailboxId: MailboxId };
                    fpService.postData($_Mailbox.Remove, jObject, function (response) {
                        var responseJson = response.data;
                        if (responseJson.statusCode === 200) {
                            if (responseJson.data == "logOut") {
                                RedirectToLogin();
                            }
                            toastr.success('Mailbox removed successfully.', "Success");
                            $scope.fn_GetAllMailbox(MailTypeId);
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

                    $http.post($_Mailbox.UploadDocument, formData, {
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


    $scope.fn_RemoveDocument = function (lst) {
        fpService.getData($_Mailbox.RemoveDocument, { fileName: lst.UplodedFileName }, function () {
            $scope.lstDocuments.splice($scope.lstDocuments.indexOf(lst), 1);
        });
    };

    $scope.fn_ViewDocument = function (lst) {
        window.open("../Upload/TempDir/" + lst.UplodedFileName);
    };

    $scope.fn_ApproveProposal = function (MailboxId) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId };
        fpService.postData($_Mailbox.ApproveProposal, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Proposal approved successfully.', "Success");
                $("#divViewmailBox").hide();
                $("#divReplyMailBox").show();
                $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.', "Error");
            }
        });
    };

    $scope.fn_ProposalUpdate = function (MailboxId, UpdateValue) {
        $scope.MailboxModal = {};
        var param = { MailboxId: MailboxId, UpdateValue: UpdateValue };
        fpService.postData($_Mailbox.SendProposalUpdate, param, function (response) {
            var responseJson = response.data;
            if (responseJson.statusCode === 200) {
                if (responseJson.data == "logOut") {
                    RedirectToLogin();
                }
                toastr.success('Successfully.', "Success");
                $("#divViewmailBox").hide();
                $("#divReplyMailBox").show();
                $scope.fn_DefaultMailboxSettings();
            }
            if (responseJson.statusCode === 204) {
                toastr.error('Error in getting data.', "Error");
            }
        });
    };
});
