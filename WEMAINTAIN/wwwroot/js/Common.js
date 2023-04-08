
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
function CheckMobileNumber(control) {

}
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
			if (!CheckUndefinedBlankAndNull(packageId)) {
				controlId.val(packageId);
			}
		},
		complete: function () {
			$('#loading').hide();
		}
	});
}
