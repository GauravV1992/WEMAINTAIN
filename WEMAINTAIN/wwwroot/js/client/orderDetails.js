
function OnClickBilling() {
	$("#ancBilling").click(function () {
		GoToBillingPage();
	});
}
function SetBillingObject() {
	if ($("input[type='radio'].form-check-input").is(':checked')) {
		amcPeriod = $("input[type='radio'].form-check-input:checked").val();
	}
	debugger;
	 servicesIds = $.map($(':checkbox[name="chkService"]:checked'), function (n, i) {
		return n.value;
	}).join(',');
}
function GoToBillingPage() {
	SetBillingObject();
	var data = { subPackageId: subPackageId, amcPeriod: amcPeriod, servicesIds: servicesIds };
	$('#ancBilling').prop('disabled', true);
	$.ajax({
		type: "Get",
		url: '/OrderDetails/Billing',
		data: data,
		datatype: "json",
		success: function (response) {
		},
		complete: function () {
		}
	});
}

