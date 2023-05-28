function OnRegisterCalled() {

	debugger;
	OnLoginClick();
	OnRegisterClick();
	OnCreatePageLoad();
	OnCheckLogin();
}
function OnLoginClick() {
	$(".LoginPopup").click(function () {
		$("#SignInSignUpModal").modal('show');
	});
}
function toggleSignInSignUp() {
	$(".form-signin").toggleClass("form-signin-left");
	$(".form-signup").toggleClass("form-signup-left");
	$(".frame").toggleClass("frame-long");
	$(".signup-inactive").toggleClass("signup-active");
	$(".signin-active").toggleClass("signin-inactive");
	$(".forgot").toggleClass("forgot-left");
	$(this).removeClass("idle").addClass("active");
}
function OnRegisterClick() {
	$(".Register").click(function () {
		toggleSignInSignUp();
	});
}
function OnCreatePageLoad() {
	debugger;
	$("#frmCreateSignUp").on("submit", function (e) {
		e.preventDefault();
		debugger;
		//if (!ValidateForm()) {
		//	return;
		//}
		$(':submit', this).attr('disabled', 'disabled');
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: '/User/Save',
				data: $(this).serialize(),
				success: function (data) {
					debugger;
					if (data.data > 0) {
						clearRegisterForm();
						toastr.success('User registration successful! Please sign in.');
						toggleSignInSignUp();
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

function OnCheckLogin() {
	debugger;
	$("#frmCreate").on("submit", function (e) {
		e.preventDefault();
		debugger;
		if (!ValidateLoginForm()) {
			return;
		}
		$(':submit', this).attr('disabled', 'disabled');
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: '/User/Login',
				data: $(this).serialize(),
				success: function (data) {
					debugger;
					if (data) {
						clearRegisterForm();
						location.reload(true);
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
function clearRegisterForm() {
	$('input[type=text]').each(function () {
		$(this).val('');
	});
}
function ValidateLoginForm() {
	$("#spnError").text('');
	if (CheckUndefinedBlankAndNull($("#PhoneNumber").val())) {
		$("#spnError").text('Please Enter Mobile Number')
		return false;
	}
	 else if (CheckUndefinedBlankAndNull($("#Credential").val())) {
		$("#spnError").text('Please Enter Password')
		return false;
	}
	return true;
}
function ValidateForm() {

	$("#spnError").text('');
	if (CheckUndefinedBlankAndNull($("#FirstName").val())) {
		$("#spnError").text('Please Enter First Name')
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#LastName").val())) {
		$("#spnError").text('Please Enter Last Name')
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#MobileNo").val())) {
		$("#spnError").text('Please Enter Mobile Number')
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Email").val())) {
		$("#spnError").text('Please Enter Email Address')
		return false;
	}
	if (CheckUndefinedBlankAndNull($("#Password").val())) {
		$("#spnError").text('Please Enter Password')
		return false;
	}
	return true;
}

