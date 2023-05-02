
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
var sdate = '';
var edate = '';
function SetDateFormat() {
	debugger;
	var startdt = "";
	var enddt = "";
	var date = new Date();
	startdt = new Date(date.getFullYear(), date.getMonth() - 1, date.getDate());
	enddt = new Date(date.getFullYear(), date.getMonth(), date.getDate());
	$('.StartDate').datepicker({
		format: 'dd/mm/yyyy',
		todayBtn: 'linked'
	});
	$('.EndDate').datepicker({
		format: 'dd/mm/yyyy',
		todayBtn: 'linked'
	});
	$('.StartDate').datepicker('setDate', startdt);
	$('.EndDate').datepicker('setDate', enddt);

	sdate = $('.StartDate').val().split('/');
	sdate = sdate[1] + '/' + sdate[0] + '/' + sdate[2];
	edate = edate = $('.EndDate').val().split('/');
	edate = edate[1] + '/' + edate[0] + '/' + edate[2];


}


function showLoader(divId) {
	if (divId != '') {
		jQuery("#" + divId).append("<div id='preloaded'><div class='preloaded'><img src='" + loaderPath + "'/></div></div>");
		$("#preloaded").css("display", "block");
	}
}
function hideLoader() {
	$("#preloaded").css("display", "none");
}
function CheckUndefinedBlankAndNull(object) {
	return (object === undefined || object === null || object === '');
}
function ScrollToTop() {
	$("html, body").animate({ scrollTop: 0 }, "slow");
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
function OnCloseDatatableEditRow() {
	$('.edit-row').remove();
}

function BindUserNames() {
	$('#loading').show();
	$('#UserId').select2({ placeholder: "Select User" });
	$.ajax({
		type: "GET",
		url: '/Admin/Coupon/GetUserNames',
		data: null,
		datatype: "json",
		success: function (result) {
			debugger;
			var controlId = $('#UserId');
			controlId.empty();
			$.each(result.data, function (i, data) {
				controlId.append(new Option(data.text, data.value));
			});
			if (!CheckUndefinedBlankAndNull(UserId)) {
				controlId.val(UserId);
			}
		},
		complete: function () {
			$('#loading').hide();
		}
	});
}

function BindPackageNames(controlId) {
	debugger;
	$('#loading').show();
	controlId.select2({ placeholder: "Select Package" });
	$.ajax({
		type: "GET",
		url: '/Admin/Category/GetPackageNames',
		data: null,
		datatype: "json",
		success: function (result) {
			controlId.empty();
			$.each(result.data, function (i, data) {
				controlId.append(new Option(data.text, data.value));
			});
			if (!CheckUndefinedBlankAndNull(packageId)) {
				controlId.val(packageId);
			}
		},
		complete: function () {
			$('#loading').hide();
		}
	});
}
function BindSubPackageNames(controlId, packageId) {
	debugger;
	controlId.select2({ placeholder: "Select Sub Package" });
	var jsonObject = { packageId: packageId };
	$.ajax({
		type: "GET",
		url: '/Admin/SubPackage/GetSubPackageNames',
		data: jsonObject,
		datatype: "json",
		success: function (result) {
			controlId.empty();
			$.each(result.data, function (i, data) {
				controlId.append(new Option(data.text, data.value));
			});
			if (!CheckUndefinedBlankAndNull(subPackageId)) {
				controlId.val(subPackageId);
			}
		},
		complete: function () {
		}
	});
}
function BindServiceNames(controlId) {
	controlId.select2({ placeholder: "Select Service" });
	var jsonObject = { SubPackageId: 0 };
	$.ajax({
		type: "GET",
		url: '/Admin/Service/GetServiceNames',
		data: jsonObject,
		datatype: "json",
		success: function (result) {
			controlId.empty();
			$.each(result.data, function (i, data) {
				controlId.append(new Option(data.text, data.value));
			});
			if (!CheckUndefinedBlankAndNull(serviceId)) {
				controlId.val(serviceId);
			}
		},
		complete: function () {
		}
	});
}
function OnFilterPageLoad() {
	BindPackageNames($('#FPackageId'));
	BindServiceNames($('#FServiceId'));
	BindSubPackageNames($('#FSubPackageId'), packageId);
	$('#FPackageId').change(function () {
		BindSubPackageNames($('#FSubPackageId'), $('#FPackageId').val());
	});
	SetDateFormat();
}
function Download(id, ext, controller) {
	$.ajax({
		method: "GET",
		xhrFields: { responseType: 'blob' },
		url: '/Admin/' + controller + '/Download?id=' + id + '&ext=' + ext,
		contentType: 'application/json; charset=utf-8',
		data: null,
		datatype: "json",
		success: function (data) {
			var a = document.createElement('a');
			var url = window.URL.createObjectURL(data);
			a.href = url;
			var today = new Date();
			var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
			var time = today.getHours() + "." + today.getMinutes() + "." + today.getSeconds();
			var currentDateTime = date + ' ' + time;
			var fileName = id + '_' + currentDateTime + ext;
			a.download = fileName;
			document.body.append(a);
			a.click();
			a.remove();
			window.URL.revokeObjectURL(url);
		}
	});
}