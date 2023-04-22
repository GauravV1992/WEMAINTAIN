function OnHomePageLoad() {

	LoadPackageSection();
}
function LoadPackageSection() {
	debugger;
	$("#divCategroy").empty();
	$.ajax({
		type: "Get",
		url: '/Client/Home/GetCategorySection',
		data: null,
		datatype: "json",
		success: function (response) {
			$('#divCategroy').html(response);
		},
		complete: function () {
		}
	});
}
function LoadSubPackageSection(packageId) {
	debugger;
	$("#exampleModal").empty();
	$.ajax({
		type: "Get",
		url: '/Client/Home/GetSubPackageSection?packageId=' + packageId + '',
		data: null,
		datatype: "json",
		success: function (response) {
			$('#exampleModal').html(response);
		},
		complete: function () {
			debugger;
			$('#exampleModal').modal()
		}
	});
}