function AddUser() {
	$("#divUser").empty();
	$.ajax({
		type: "Get",
		url: '/Admin/User/Create',
		data: null,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divUser').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
		}
	});
}
function EditUser(id) {
	debugger;
	$("#divUser").empty();
	var jsonObject = { Id: id };
	/*$('#loading').show();*/
	$.ajax({
		type: "Get",
		url: '/Admin/User/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divUser').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
			ScrollToTop();
		}
	});
}
function OnCancel() {
	$("#divUser").empty();
}
function BindUserDatatable() {
	$("#UserGrid").DataTable({
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
		"bDestroy": true,
		"searching": false,
		"ajax": {
			"url": "/Admin/User/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				debugger;
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
				d.StartDate = sdate;
				d.EndDate = edate;
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "firstName" },
		{ "data": "lastName" },
		{ "data": "address" },
		{ "data": "email" },
		{ "data": "mobileNo" },
		{ "data": "username" },
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
	html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><button type='button' onclick='EditUser(" + id + ")' class='btn btn-sm btn-primary me-md-2'>Edit</button><button type='button' onclick='Delete(" + id + ")' class='btn btn-sm btn-danger me-md-2'>Delete</button></div>"
	return html;
}


function OnCreateEditPageLoad() {
	OnCreatePageLoad();
	OnEditPageLoad();
	
}

function ValidateForm() {
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
	//else if (!CheckEmailAddress($("#Username").val())) {
	//	toastr.error('Please Enter Username!');
	//	return false;
	//}
	
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
					/*$('#loading').hide();*/
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
					RefreshGrid();
					hideLoader();
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
			url: "/Admin/User/Delete",
			data: jsonObject,
			async: false,
			datatype: "json",
			success: function (data) {
				debugger;
				if (data.data > 0) {
					RefreshGrid();
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
	$("#divUser").empty();
}

function RefreshGrid() {

	var oTable = $('#UserGrid').DataTable();
	oTable.ajax.reload();
}