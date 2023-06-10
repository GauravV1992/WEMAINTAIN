function OnEdit() {
	debugger;
	OnEdit1();
	OnPasswordUpdate();
}

function ValidatePass() {
	if (CheckUndefinedBlankAndNull($("#Pass").val())) {
		toastr.error('Please Enter Password');
		return false;
	}
	return true;
}
function ValidateProfileForm() {
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

function OnEdit1() {
	debugger;
	$('button.submitForm').click(function (e) {
		e.preventDefault();
		if (!ValidateProfileForm()) {
			return;
		}
		$(':submit', this).attr('disabled', 'disabled');
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: '/User/UpdateProfile',
				data: $('#frmUserUpdate').serialize(),
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

function OnPasswordUpdate() {
	debugger;
	$('button.submitPass').click(function (e) {
		e.preventDefault();
		if (!ValidatePass()) {
			return;
		}
		$(':submit', this).attr('disabled', 'disabled');
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: '/User/UpdatePassword',
				data: $('#frmPasswordUpdate').serialize(),
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
