
function OnCreateEditPageLoad() {
	OnCreatePageLoad();
	OnEditPageLoad();
	BindPackageNames();
	BindServiceNames($('#SubPackageId').val());
	GetSubPackageOnPackageChange();
	$('#PackageId').on("change", GetSubPackageOnPackageChange);
}
function AddPackageRate() {
	$("#divPackageRate").empty();
	$.ajax({
		type: "Get",
		url: '/PurchaseDetails/Create',
		data: null,
		datatype: "json",
		success: function (response) {
			$('#divPurchaseDetails').html(response);
		},
		complete: function () {
		}
	});
}
function EditPackageRate(id) {

	$("#divPurchaseDetails").empty();
	var jsonObject = { Id: id };
	$.ajax({
		type: "Get",
		url: '/PurchaseDetails/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			$('#divPurchaseDetails').html(response);
		},
		complete: function () {
			ScrollToTop();
		}
	});
}
function OnCancel() {
	$("#divPurchaseDetails").empty();
}
function BindPackageRateDatatable() {
	$("#PurchaseDetailsGrid").DataTable({

		"language": {
			"infoFiltered": "",
			"infoPostFix": ""
		},
		"processing": false,
		"serverside": true,
		"bLengthChange": false,
		"pageLength": 10,
		"filter": false,
		"orderMulti": false,
		"bSort": false,
		"bDestroy": false,
		"searching": false,
		"ajax": {
			"url": "/PurchaseDetails/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "packageName" },
		{ "data": "subPackageName" },
		{ "data": "serviceName" },
		{ "data": "rate" },
		{ "data": "discount" },
		{ "data": "packageAmount" },
		{ "data": "amcPeriod" },
		{
			"name": "Action",
			render: function (data, type, row) {
				return CreateActionButton(row.id);
			}
		}
		],
		"columnDefs": [{
			"defaultContent": "-",
			"targets": "_all"
		},
		{
			"targets": [1, 2], "orderable": false
		},
		{
			"visible": false, "targets": [0]
		},
		],
		"FnDrawCallback": function (a, b, c) {

		},
		"createdRow": function (row, data) {
			var id = data.id;
			$(row).prop('id', 'tr_' + id).data('id', id);
		}
	});
}
function CreateActionButton(id) {
	var html = '';
	html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><button type='button' onclick='EditPackageRate(" + id + ")' class='btn btn-sm btn-primary me-md-2'>Edit</button><button type='button' onclick='Delete(" + id + ")' class='btn btn-sm btn-danger me-md-2'>Delete</button></div>"
	return html;
}

function GetSubPackageOnPackageChange() {
	debugger;
	BindSubPackageNames($('#PackageId').val());
}
function ValidateForm() {
	if (CheckUndefinedBlankAndNull($("#PackageId").val())) {
		toastr.error('Please Select Package Name');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#SubPackageId").val())) {
		toastr.error('Please Select Sub Package Name');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#ServiceId").val())) {
		toastr.error('Please Select Service Name');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Rate").val())) {
		toastr.error('Please Enter Rate');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#AMCPeriod").val())) {
		toastr.error('Please Select AMC Period');
		return false;
	}
	return true;
}
function OnCreatePageLoad() {
	$("#frmCreate").on("submit", function (e) {
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
					ClearControl();
					RefreshGrid();
					hideLoader();
				}
			});
	});
}
//function OnEditPageLoad() {
//	$("#frmEdit").on("submit", function (e) {
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
//					debugger;
//					if (data.data > 0) {
//						toastr.success(suceessMsg);
//					} else {
//						toastr.error(errorMsg);
//					}
//				},
//				complete: function () {
//					$(':submit').prop('disabled', false);
//					ClearControl();
//					RefreshGrid();
//					hideLoader();
//				}
//			});
//	});
//}
function Delete(Id) {
	if (confirm(confirmDeleteRecord)) {
		var jsonObject = { Id: Id };
		$.ajax({
			type: "POST",
			url: "/PurchaseDetails/Delete",
			data: jsonObject,
			async: false,
			datatype: "json",
			success: function (data) {
				debugger;
				if (data.data > 0) {
					toastr.success(suceessMsgDelete);
				} else {
					toastr.error(errorMsg);
				}
			},
			complete: function () {
				$('#tr_' + jsonObject.Id).remove();
			}
		});
	}
}
function ClearControl() {
	$("#divPurchaseDetails").empty();
}
function RefreshGrid() {
	var oTable = $('#PurchaseDetailsGrid').DataTable();
	oTable.ajax.reload();
}