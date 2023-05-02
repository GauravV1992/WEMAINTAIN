

function AddSubPackage() {
	$("#divSubPackage").empty();
	//var clientId = $('#ClientId').val();
	//var jsonObject = { clientId: clientId };

	$.ajax({
		type: "Get",
		url: '/Admin/SubPackage/Create',
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
	$("#divSubPackage").empty();
	var jsonObject = { Id: id };

	$.ajax({
		type: "Get",
		url: '/Admin/SubPackage/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divSubPackage').html(response);
		},
		complete: function () {


			ScrollToTop();

		}
	});
}
function OnCancel() {
	$("#divSubPackage").empty();
}
function BindSubPackageDatatable() {
	$("#SubPackageGrid").DataTable({
		"language": {
			/*"zeroRecords": "No records found.",*/
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
			"url": "/Admin/SubPackage/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();

			}
			//"dataSrc": function (d) {
			//	debugger;
			//	// Format API response for DataTables
			//	var response = d;
			//	if (typeof d.response != 'undefined') {
			//		response = d.response;
			//	}
			//	console.log(JSON.stringify(response)); // Output from this is below...
			//	return response;
			//}
		},

		"columns": [{ "data": "id" },
		{ "data": "packageName" },
		{ "data": "name" },
		{
				"name": "Image",
				render: function (data, type, row) {
					return CreateImage(row.id, row.ext);
				}
		},
		{
				"name": "Download",
				render: function (data, type, row) {
					return CreateDownloadIcon(row.id, row.ext);
				}
		},
		{ "data": "termsAndCondition" },
		{
			"name": "Action",
			render: function (data, type, row) {
				return CreateActionButton(row.id,row.ext);
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
function CreateActionButton(id,ext) {
	var html = '';
	html = html + '<div class="d-grid gap-2 d-md-flex justify-content-md-end""><button type="button" onclick="EditSubPackage(' + id + ')" class="btn btn-sm btn-primary me-md-2">Edit</button><button type="button" onclick="Delete('+ id + ',\'' + ext + '\')" class="btn btn-sm btn-danger me-md-2">Delete</button></div>'
	return html;
}


function CreateImage(id, ext) {
	var html = '';
	html = CheckUndefinedBlankAndNull(ext) ? "" : html + "<img height='100px' width='150px' src=" + imagePath + id + ext + "></img>";
	return html;
}
function CreateDownloadIcon(id, ext) {
	var html = '';
	html = CheckUndefinedBlankAndNull(ext) ? "" : html + '<a href="#" onclick="Download(' + id + ',\'' + ext + '\',\'SubPackage\' )">download</a>';
	return html;
}


function OnCreateEditPageLoad() {
	OnCreatePageLoad();
	OnEditPageLoad();
	BindPackageNames($("#PackageId"));
}

function ValidateForm() {
	if (CheckUndefinedBlankAndNull($("#Name").val())) {
		toastr.error('Please Enter Sub-Package Name');
		return false;
	}
	if ($("#PackageId").val() == "") {
		toastr.error('Please Select Package Name');
		return false;
	}
	//if (CheckUndefinedBlankAndNull($("#TermsAndCondition").val())) {
	//	toastr.error('Please Enter Terms And Condition');
	//	return false;
	//}
	return true;
}

function OnCreatePageLoad() {
	debugger;
	$("#frmCreate").on("submit", function (e) {
		debugger;
		e.preventDefault();
		if (!ValidateForm()) {
			return;
		}
		var data = new FormData();
		var files = $("#Image").get(0).files;
		if (files.length > 0) {
			data.append("Image", files[0]);
		}
		data.append("Name", $("#Name").val());
		data.append("PackageId", $("#PackageId").val());
		data.append("TermsAndCondition", $("#TermsAndCondition").val());
		$(':submit', this).attr('disabled', 'disabled');
		showLoader("divCreate");
		$.ajax(
			{
				cache: false,
				async: true,
				processData: false,
				contentType: false,
				type: "POST",
				url: $(this).attr('action'),
				data: data,
				success: function (data) {
					if (data.data > 0) {
						toastr.success(suceessMsg);
						ClearControl();
						RefreshGrid();
					} else if (data.data < 0) {
						toastr.warning(dataExists);
					}
					else {
						toastr.error(data);
					}
				},
				complete: function () {
					/*$('#loading').hide();*/
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
		data.append("Id", $("#Id").val());
		data.append("Name", $("#Name").val());
		data.append("PackageId", $("#PackageId").val());
		data.append("TermsAndCondition", $("#TermsAndCondition").val());
		$(':submit', this).attr('disabled', 'disabled');
		showLoader("divEdit");
		$.ajax(
			{
				cache: false,
				async: true,
				processData: false,
				contentType: false,
				type: "POST",
				url: $(this).attr('action'),
				data: data,
				success: function (data) {
					debugger;
					if (data.data > 0) {
						toastr.success(suceessMsg);
						ClearControl();
						RefreshGrid();
					} else if (data.data < 0) {
						toastr.warning(dataExists);
					}
					else {
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
		//window.addAntiForgeryToken = function (Id) {
		var jsonObject = { Id: Id, ext: ext };
		//	jsonObject._RequestVerificationToken = $("#lstLedger").find('input[name=_RequestVerificationToken]').val();
		//	return jsonObject;
		//};
		//var jsonObject = window.addAntiForgeryToken(Id);
		//$('#loading').show();
		$.ajax({
			type: "POST",
			url: "/Admin/SubPackage/Delete",
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
	$("#divSubPackage").empty();
}

function RefreshGrid() {
	var oTable = $('#SubPackageGrid').DataTable();
	oTable.ajax.reload();
}