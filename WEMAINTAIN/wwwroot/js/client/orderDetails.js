
function OnClickBilling() {
	debugger;
	if (CheckUndefinedBlankAndNull($('#ancSignOut').text())) {
		$('#SignInSignUpModal').modal('show');
		return false;
	}
	else {
		SetBillingObject();
	}
}
function setCookie(cname, cvalue, exdays) {
	const d = new Date();
	d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
	let expires = "expires=" + d.toUTCString();
	document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function deleteCookie(cname) {
	document.cookie = "name=" + cname + " ;max-age=0";
}
function SetBillingObject() {
	debugger;
	if ($("input[type='radio'].form-check-input").is(':checked')) {
		amcPeriod = $("input[type='radio'].form-check-input:checked").val();
	}
	servicesIds = $.map($(':checkbox[name="chkService"]:checked'), function (n, i) {
		return n.value;
	}).join(',');
	debugger;
	var data = { 'amcPeriod': amcPeriod, 'subPackageId': subPackageId, 'servicesIds': servicesIds };
	setCookie('cartItem', JSON.stringify(data), 1);
}
//function GoToBillingPage() {
//	SetBillingObject();
//	var data = { subPackageId: subPackageId, amcPeriod: amcPeriod, servicesIds: servicesIds };
///*	$('#ancBilling').prop('disabled', true);*/
//	$.ajax({
//		type: "Get",
//		url: '/orderdetails/billing',
//		data: data,
//		datatype: "json",
//		success: function (response) {
//			$('#divBody').html(response);
//		},
//		complete: function () {
//		}
//	});
//}

