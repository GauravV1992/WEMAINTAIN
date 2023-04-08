

function AddSubPackage() {
	$("#divSubPackage").empty();
	//var clientId = $('#ClientId').val();
	//var jsonObject = { clientId: clientId };
	/*$('#loading').show();*/
	$.ajax({
		type: "Get",
		url: '/SubPackage/Create',
		data: null,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divSubPackage').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
		}
	});
}
function EditSubPackage(id) {
	debugger;
	$("#divSubPackage").empty();
	var jsonObject = { Id: id };
	/*$('#loading').show();*/
	$.ajax({
		type: "Get",
		url: '/SubPackage/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divSubPackage').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
		}
	});
}
function OnCancel() {
	$("#divSubPackage").empty();
}
function BindSubPackageDatatable() {
	debugger;
	$("#SubPackageGrid").DataTable({

		"language": {
			/*"zeroRecords": "No records found.",*/
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
			"url": "/SubPackage/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "packageName" },
		{ "data": "name" },
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
	html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><button type='button' onclick='EditSubPackage(" + id + ")' class='btn btn-sm btn-primary me-md-2'>Edit</button><button type='button' onclick='Delete(" + id + ")' class='btn btn-sm btn-danger me-md-2'>Delete</button></div>"
	return html;
}


function OnCreateEditPageLoad() {
	OnCreatePageLoad();
	OnEditPageLoad();
	BindPackageNames();
}

function ValidateForm() {
	if (CheckUndefinedBlankAndNull($("#Name").val())) {
		toastr.error('Please Enter Sub-Package Name');
		return false;
	}
	if ($("#PackageId").val()=="") {
		toastr.error('Please Select Package Name');
		return false;
	}
	return true;
}

function OnCreatePageLoad() {
	$("#frmCreate").on("submit", function (e) {
		debugger;
		e.preventDefault();
		if (!ValidateForm()) {
			return;
		}
		$(':submit', this).attr('disabled', 'disabled');
		/*$('#loading').show();*/
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
					/*$('#loading').hide();*/
					$(':submit').prop('disabled', false);
					ClearControl();
					RefreshGrid()
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
		/*	$('#loading').show();*/
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				url: $(this).attr('action'),
				data: $(this).serialize(),
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
					ClearControl();
					RefreshGrid()
				}
			});
	});
}
function Delete(Id) {
	if (confirm(confirmDeleteRecord)) {
		//window.addAntiForgeryToken = function (Id) {
		var jsonObject = { Id: Id };
		//	jsonObject._RequestVerificationToken = $("#lstLedger").find('input[name=_RequestVerificationToken]').val();
		//	return jsonObject;
		//};
		//var jsonObject = window.addAntiForgeryToken(Id);
		//$('#loading').show();
		$.ajax({
			type: "POST",
			url: "/SubPackage/Delete",
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
				//$('#loading').hide();
				$('#tr_' + jsonObject.Id).remove();
			}
		});

	}
}
function ClearControl() {
	$("#divSubPackage").empty();
}

function RefreshGrid() {
	var oTable = $('#SubPackageGrid').DataTable();
	oTable.ajax.reload();
}