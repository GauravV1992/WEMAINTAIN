﻿
var suceessMsg = "Record Saved Successfully!"
var updateMsg = "Record Updated Successfully!"
var suceessMsgDelete = "Record Deleted Successfully!"
var emailConfirm = "Are you sure you want to send email?";
var dataExists = "Data Already Exists!";
var suceessMsgVerify = "Are you sure you want to verify?";

var emailSucc = "Mail Sent Successfully!";
var errorMsg = "Oops! Something went wrong, Please Contact Admin!"
var confirmDeleteRecord = "Are you sure you want delete record?"
var confirmDeleteRecord = "Are you sure you want delete record?"
function showLoader(divId) {
	if (divId != '') {
		jQuery("#" + divId).append("<div class='k-loading-mask'><span class='k-loading-text'>Loading...</span><div class='k-loading-image'></div><div class='k-loading-color'></div></div>");
	}
}
function CheckUndefinedBlankAndNull(object) {
	return (object === undefined || object === null || object === '');
}
function CheckEmailAddress(email) {
	// Validate email format
	var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
	return expr.test(email);
}

$(document).ready(function () {
	$('.numberonly').keypress(function (e) {
		var charCode = (e.which) ? e.which : e.keyCode
		if (String.fromCharCode(charCode).match(/[^0-9]/g))
			return false;
	});    
});


function BindPackageNames() {
	var siteId = $('#SiteId').val();
	var jsonObject = { siteId: siteId };
	$('#loading').show();
	$.ajax({
		type: "GET",
		url: '/Category/GetPackageNames',
		data: jsonObject,
		datatype: "json",
		success: function (result) {
			debugger;
			var controlId = $('#PackageId');
			controlId.empty();
			$.each(result.data, function (i, data) {
				controlId.append(new Option(data.text, data.value));
			});
			if (!CheckUndefinedBlankAndNull(packageId)){
				controlId.val(packageId);
			}
		},
		complete: function () {
			$('#loading').hide();
		}
	});
}
