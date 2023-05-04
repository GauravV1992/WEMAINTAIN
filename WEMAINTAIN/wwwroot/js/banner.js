function AddBanner() {
	$("#divBanner").empty();
	//var clientId = $('#ClientId').val();
	//var jsonObject = { clientId: clientId };
	/*$('#loading').show();*/
	$.ajax({
		type: "Get",
		url: '/Admin/Banner/Create',
		data: null,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divBanner').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
		}
	});
}

function OnCancel() {
	$("#divBanner").empty();
}
function BindBannerDatatable() {
	$("#BannerGrid").DataTable({

		"language": {
			/*"zeroRecords": "No records found.",*/
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
			"url": "/Admin/Banner/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "rank" },
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
	html = CheckUndefinedBlankAndNull(ext) ? "" : html + '<a href="#" onclick="Download(' + id + ',\'' + ext + '\',\'Banner\' )">download</a>';
	return html;
}
function CreateActionButton(id,ext) {
	var html = '';
	//html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><button type='button' onclick='EditBanner(" + id + ")' class='btn btn-sm btn-primary me-md-2'>Edit</button><button type='button' onclick='Delete(" + id + ")' class='btn btn-sm btn-danger me-md-2'>Delete</button></div>"
	html = html + '<div class="d-grid gap-2 d-md-flex justify-content-md-end""><button type="button" onclick="Delete(' + id + ',\'' + ext + '\')" class="btn btn-sm btn-danger me-md-2">Delete</button></div>'
	return html;
}
function OnCreateEditPageLoad() {
	OnCreatePageLoad();
}
function ValidateForm() {
	if (CheckUndefinedBlankAndNull($("#Rank").val())) {
		toastr.error('Please Enter Rank');
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
		data.append("Rank", $("#Rank").val());
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

function Delete(Id,ext) {
	if (confirm(confirmDeleteRecord)) {
		//window.addAntiForgeryToken = function (Id) {
		var jsonObject = { Id: Id, ext :ext};
		//	jsonObject._RequestVerificationToken = $("#lstLedger").find('input[name=_RequestVerificationToken]').val();
		//	return jsonObject;
		//};
		//var jsonObject = window.addAntiForgeryToken(Id);
		//$('#loading').show();
		$.ajax({
			type: "POST",
			url: "/Admin/Banner/Delete",
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
	$("#divBanner").empty();
}

function RefreshGrid() {
	var oTable = $('#BannerGrid').DataTable();
	oTable.ajax.reload();
}