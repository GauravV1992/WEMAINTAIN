
function OnRegisterCalled() {
	OnCreatePageLoad();
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
	else if (CheckUndefinedBlankAndNull($("#Email").val())) {
		toastr.error('Please Enter EmailAddress');
		return false;
	}
	else if (!CheckEmailAddress($("#Email").val())) {
		toastr.error('Invalid EmailAddress!');
		return false;
	}
	else if (!CheckEmailAddress($("#Username").val())) {
		toastr.error('Please Enter Username');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Password").val())) {
		toastr.error('Please Enter Password');
		return false;
	}
	return true;
}

function OnCreatePageLoad() {
	debugger;
	$("#frmCreate").on("submit", function (e) {
		e.preventDefault();
		if (!ValidateForm()) {
			return;
		}
		$(':submit', this).attr('disabled', 'disabled');
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: $(this).attr('action'),
				data: $(this).serialize(),
				success: function (data) {
					
					if (data.data > 0) {
						toastr.success(suceessMsg);
					} else if (data.data < 0) {
						toastr.warning(dataExists);
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
 
 

 