
function OnCreateEditPageLoad() {
	//OnCreatePageLoad();
	OnEditPageLoad();
	//BindPackageNames($('#PackageId'));
	//BindServiceNames($('#ServiceId'));
	//GetSubPackageOnPackageChange();
	//$('#PackageId').on("change", GetSubPackageOnPackageChange);
	//$('#AMCPeriod').select2({ placeholder: "Select AMC Period" });
}
function EditRequestLabour(id) {

	$("#divRequestLabour").empty();
	var jsonObject = { Id: id };
	$.ajax({
		type: "Get",
		url: '/RequestLabour/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			$('#divRequestLabour').html(response);
		},
		complete: function () {
			ScrollToTop();
		}
	});
}
function OnCancel() {
	$("#divRequestLabour").empty();
}

function BindRequestLabourDatatable() {
	$("#RequestLabourGrid").DataTable({

		"language": {
			"infoFiltered": "",
			"infoPostFix": ""
		},
		"processing": true,
		"serverSide": true,
		"bLengthChange": false,
		"pageLength": 10,
		"filter": false,
		"orderMulti": false,
		"bSort": false,
		"bDestroy": false,
		"searching": false,
		"ajax": {
			"url": "/RequestLabour/GetAll",
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
		{ "data": "userName" },
		{ "data": "labourCount" },
		{ "data": "assignDate" },
		{ "data": "status" },
		{ "data": "completedDate" },
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
	html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><button type='button' onclick='EditRequestLabour(" + id + ")' class='btn btn-sm btn-primary me-md-2'>Edit</button></div>"
	return html;
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
function OnEditPageLoad() {
	$("#frmEdit").on("submit", function (e) {
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

function ClearControl() {
	$("#divRequestLabour").empty();
}
function RefreshGrid() {
	var oTable = $('#RequestLabourGrid').DataTable();
	oTable.ajax.reload();
}