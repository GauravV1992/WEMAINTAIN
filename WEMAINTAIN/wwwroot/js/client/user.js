function OnEditPageLoad() {
	debugger;
	OnEdit();
}
function ValidateForm() {
	debugger;

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

function OnEdit() {
	debugger;
	$("#frmUserUpdate").on("submit", function (e) {
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
				}
			});
	});
}
