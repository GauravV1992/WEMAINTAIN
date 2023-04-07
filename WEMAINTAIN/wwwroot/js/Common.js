
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
