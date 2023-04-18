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
		debugger;
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

	var date = new Date();
	var ST = new Date(date.getFullYear(), date.getMonth()-1, date.getDate());
	var ED = new Date(date.getFullYear(), date.getMonth(), date.getDate());

	$('.StartDate').datepicker({
		format: 'dd/mm/yyyy',
		todayBtn: 'linked'
	});
	$('.EndDate').datepicker({
		format: 'dd/mm/yyyy',
		todayBtn: 'linked'
	});
	$('.StartDate').datepicker('setDate', ST);
	$('.EndDate').datepicker('setDate', ED);

});


function BindPackageNames() {
	$('#loading').show();
	$('#PackageId').select2({ placeholder: "Select Package" });
	$.ajax({
		type: "GET",
		url: '/Category/GetPackageNames',
		data: null,
		datatype: "json",
		success: function (result) {
			debugger;
			var controlId = $('#PackageId');
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
function BindSubPackageNames(packageId) {
	debugger;
	$('#SubPackageId').select2({ placeholder: "Select Sub Package" });
	var jsonObject = { packageId: packageId };
	$.ajax({
		type: "GET",
		url: '/SubPackage/GetSubPackageNames',
		data: jsonObject,
		datatype: "json",
		success: function (result) {
			var controlId = $('#SubPackageId');
			controlId.empty();
			$.each(result.data, function (i, data) {
				controlId.append(new Option(data.text, data.value));
			});
			debugger;
			if (!CheckUndefinedBlankAndNull(subPackageId)) {
				controlId.val(subPackageId);
			}
		},
		complete: function () {
		}
	});
}
function BindServiceNames(subPacakageId) {
	$('#ServiceId').select2({ placeholder: "Select Service" });
	var jsonObject = { SubPackageId: subPacakageId };
	$.ajax({
		type: "GET",
		url: '/Service/GetServiceNames',
		data: jsonObject,
		datatype: "json",
		success: function (result) {
			debugger;
			var controlId = $('#ServiceId');
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
