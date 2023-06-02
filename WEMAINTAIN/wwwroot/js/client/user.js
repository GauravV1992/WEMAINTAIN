function EditUser(id) {
	debugger;
	$("#divUser").empty();
	var jsonObject = { Id: id };
	$.ajax({
		type: "Get",
		url: '/User/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divUser').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
			ScrollToTop();
		}
	});
}
function OnCancel() {
	$("#divUser").empty();
}
 
function CreateActionButton(id) {
	var html = '';
	html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><button type='button' onclick='EditUser(" + id + ")' class='btn btn-sm btn-primary me-md-2'>Edit</button><button type='button' onclick='Delete(" + id + ")' class='btn btn-sm btn-danger me-md-2'>Delete</button></div>"
	return html;
}


function OnCreateEditPageLoad() {
	//OnCreatePageLoad();
	OnEditPageLoad();

}

function ValidateForm() {
	if (CheckUndefinedBlankAndNull($("#FirstName").val())) {
		toastr.error('Please Enter First Name');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#LastName").val())) {
		toastr.error('Please Enter Last Name');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#MobileNo").val())) {
		toastr.error('Please Enter Mobile Number');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Address").val())) {
		toastr.error('Please Enter Address');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Email").val())) {
		toastr.error('Please Enter EmailAddress');
		return false;
	}
	else if (!CheckEmailAddress($("#Email").val())) {
		toastr.error('Invalid EmailAddress!');
		return false;
	}
	return true;
}
//function OnCreatePageLoad() {
//	$("#frmCreate").on("submit", function (e) {
//		e.preventDefault();
//		if (!ValidateForm()) {
//			return;
//		}
//		$(':submit', this).attr('disabled', 'disabled');
//		showLoader("divCreate");
//		$.ajax(
//			{
//				cache: false,
//				async: true,
//				type: "POST",
//				url: $(this).attr('action'),
//				data: $(this).serialize(),
//				success: function (data) {
//					if (data.data > 0) {
//						toastr.success(suceessMsg);
//					} else if (data.data < 0) {
//						toastr.warning(dataExists);
//					} else {
//						toastr.error(errorMsg);
//					}
//				},
//				complete: function () {
//					/*$('#loading').hide();*/
//					$(':submit').prop('disabled', false);
//					ClearControl();
//					RefreshGrid();
//					hideLoader();
//				}
//			});
//	});
//}
function OnEditPageLoad() {
	$("#frmEdit").on("submit", function (e) {
		e.preventDefault();
		if (!ValidateForm()) {
			return;
		}
		$(':submit', this).attr('disabled', 'disabled');
		showLoader("divCreate");
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: $(this).attr('action'),
				data: $(this).serialize(),
				success: function (data) {
					debugger;
					if (data.data > 0) {
						toastr.success(suceessMsg);
					} else {
						toastr.error(errorMsg);
					}
				},
				complete: function () {
					$(':submit').prop('disabled', false);
					ClearControl();
				}
			});
	});
}
 
function ClearControl() {
	$("#divUser").empty();
}
 