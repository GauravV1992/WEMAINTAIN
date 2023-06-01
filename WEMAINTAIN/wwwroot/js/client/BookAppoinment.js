function OnRegisterCalled() {
	OnLoginClick();
	//OnCreatePageLoad();
}
function OnLoginClick() {
	$(".LoginPopup").click(function () {
		$("#BookAppoinmentModal").modal('show');
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

$('#BookAppoinmentModal').on('hidden.bs.modal', function () {
	$(this).find('form').trigger('reset');
})

 
function clearRegisterForm() {
	$('input[type=text]').each(function () {
		$(this).val('');
	});
}
function ValidateLoginForm() {
	$("#spnError").text('');
	$("#spnSignUpError").text('');
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
 

