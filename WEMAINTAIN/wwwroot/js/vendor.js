function AddVendor() {
	$("#divVendor").empty();
	$.ajax({
		type: "Get",
		url: '/Admin/Vendor/Create',
		data: null,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divVendor').html(response);
		},
		complete: function () {
		}
	});
}
function EditVendor(id) {
	debugger;
	$("#divVendor").empty();
	var jsonObject = { Id: id };
	$.ajax({
		type: "Get",
		url: '/Admin/Vendor/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divVendor').html(response);
		},
		complete: function () {
		}
	});
}
function OnCancel() {
	$("#divVendor").empty();
}
function BindVendorDatatable() {
	$("#VendorGrid").DataTable({

		"language": {
			"infoFiltered": "",
			"infoPostFix": ""
		},
		"processing": false,
		"serverSide": true,
		"bLengthChange": false,
		"pageLength": 10,
		"filter": false,
		"orderMulti": false,
		"bSort": false,
		"bDestroy": false,
		"searching": false,
		"ajax": {
			"url": "/Admin/Vendor/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "companyName" },
		{ "data": "firstName" },
		{ "data": "lastName" },
		{ "data": "address" },
		{ "data": "country" },
		{ "data": "state" },
		{ "data": "city" },
		{ "data": "gst" },
		{ "data": "pincode" },
		{ "data": "emailAddress" },
		{ "data": "phone" },
		{
			"name": "Download",
			render: function (data, type, row) {
				return CreateDownloadIcon(row.id, row.ext);
			}
		},
		{
			"name": "Action",
			render: function (data, type, row) {
				return CreateActionButton(row.id,row.ext);
			}
		},
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

function CreateImage(id, ext) {
	var html = '';
	html = CheckUndefinedBlankAndNull(ext) ? "" : html + "<img height='100px' width='150px' src=" + imagePath + id + ext + "></img>";
	return html;
}
function CreateDownloadIcon(id, ext) {
	var html = '';
	html = CheckUndefinedBlankAndNull(ext) ? "" : html + '<a href="#" onclick="Download(' + id + ',\'' + ext + '\',\'Vendor\' )">download</a>';
	return html;
}
function CreateActionButton(id,ext) {
	var html = '';
	html = html + '<div class="d-grid gap-2 d-md-flex justify-content-md-end""><button type="button" onclick="EditVendor(' + id + ')" class="btn btn-sm btn-primary me-md-2">Edit</button><button type="button" onclick="Delete(' + id + ',\'' + ext + '\')" class="btn btn-sm btn-danger me-md-2">Delete</button></div>'
	return html;
}
function OnCreateEditPageLoad() {
	OnCreatePageLoad();
	OnEditPageLoad();

}
function ValidateForm() {
	if (CheckUndefinedBlankAndNull($("#CompanyName").val())) {
		toastr.error('Please Enter Company Name');
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
		var data = new FormData();
		var files = $("#Image").get(0).files;
		if (files.length > 0) {
			data.append("Image", files[0]);
		}
		data.append("CompanyName", $("#CompanyName").val());
		data.append("FirstName", $("#FirstName").val());
		data.append("LastName", $("#LastName").val());
		data.append("Address", $("#Address").val());
		data.append("Country", $("#Country").val());
		data.append("State", $("#State").val());
		data.append("City", $("#City").val());
		data.append("GST", $("#GST").val());
		data.append("Pincode", $("#Pincode").val());
		data.append("EmailAddress", $("#EmailAddress").val());
		data.append("Phone", $("#Phone").val());
		$(':submit', this).attr('disabled', 'disabled');
		showLoader("divCreate");
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				processData: false,
				contentType: false,
				url: $(this).attr('action'),
				data: data,
				success: function (data) {
					debugger;
					if (data.data > 0) {
						toastr.success(suceessMsg);
						RefreshGrid();
						ClearControl();
					} else if (data.data < 0) {
						toastr.warning(dataExists);
					} else {
						toastr.error(data);
					}
				},
				complete: function () {
					$(':submit').prop('disabled', false);

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
		var data = new FormData();
		var files = $("#Image").get(0).files;
		if (files.length > 0) {
			data.append("Image", files[0]);
		}
		data.append("CompanyName", $("#CompanyName").val());
		data.append("FirstName", $("#FirstName").val());
		data.append("LastName", $("#LastName").val());
		data.append("Address", $("#Address").val());
		data.append("Country", $("#Country").val());
		data.append("State", $("#State").val());
		data.append("City", $("#City").val());
		data.append("GST", $("#GST").val());
		data.append("Pincode", $("#Pincode").val());
		data.append("EmailAddress", $("#EmailAddress").val());
		data.append("Phone", $("#Phone").val());
		data.append("Id", $("#Id").val());
		$(':submit', this).attr('disabled', 'disabled');
		showLoader("divEdit");
		$.ajax(
			{
				cache: false,
				async: true,
				type: "POST",
				processData: false,
				contentType: false,
				url: $(this).attr('action'),
				data: data,
				success: function (data) {
					if (data.data > 0) {
						toastr.success(suceessMsg);
						RefreshGrid();
						ClearControl();
					} else {
						toastr.error(data);
					}
				},
				complete: function () {
					$(':submit').prop('disabled', false);

					hideLoader();
				}
			});
	});
}
function Delete(Id,ext) {
	if (confirm(confirmDeleteRecord)) {
		var jsonObject = { Id: Id, ext :ext};
		$.ajax({
			type: "POST",
			url: "/Admin/Vendor/Delete",
			data: jsonObject,
			async: false,
			datatype: "json",
			success: function (data) {
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
	$("#divVendor").empty();
}

function RefreshGrid() {

	var oTable = $('#VendorGrid').DataTable();
	oTable.ajax.reload();
}