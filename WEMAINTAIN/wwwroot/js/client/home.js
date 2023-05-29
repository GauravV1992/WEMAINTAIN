function OnHomePageLoad() {
	LoadPackageSection();
}
function LoadPackageSection() {
	$("#divCategroy").empty();
	$.ajax({
		type: "Get",
		url: '/Home/GetCategorySection',
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
	$("#exampleModal").empty();
	$.ajax({
		type: "Get",
		url: '/Home/GetSubPackageSection?packageId=' + packageId + '',
		data: null,
		datatype: "json",
		success: function (response) {
			$('#exampleModal').html(response);
		},
		complete: function () {
			$('#exampleModal').modal()
		}
	});
}