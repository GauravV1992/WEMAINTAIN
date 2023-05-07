function AddProduct() {
	$("#divProduct").empty();
	$.ajax({
		type: "Get",
		url: '/Admin/Product/Create',
		data: null,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divProduct').html(response);
		},
		complete: function () {
		}
	});
}
function EditProduct(id) {
	debugger;
	$("#divProduct").empty();
	var jsonObject = { Id: id };
	$.ajax({
		type: "Get",
		url: '/Admin/Product/Edit',
		data: jsonObject,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divProduct').html(response);
		},
		complete: function () {
		}
	});
}
function OnCancel() {
	$("#divProduct").empty();
}
function BindProductDatatable() {
	$("#ProductGrid").DataTable({
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
			"url": "/Admin/Product/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "vendor" },
		{ "data": "productName" },
		{ "data": "productCode" },
		{ "data": "package" },
		{ "data": "subPackage" },
		{ "data": "price" },
		{ "data": "showOnHomePage" },
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
	html = CheckUndefinedBlankAndNull(ext) ? "" : html + '<a href="#" onclick="Download(' + id + ',\'' + ext + '\',\'Product\' )">download</a>';
	return html;
}
function CreateActionButton(id,ext) {
	var html = '';
	html = html + '<div class="d-grid gap-2 d-md-flex justify-content-md-end""><button type="button" onclick="EditProduct(' + id + ')" class="btn btn-sm btn-primary me-md-2">Edit</button><button type="button" onclick="Delete(' + id + ',\'' + ext + '\')" class="btn btn-sm btn-danger me-md-2">Delete</button></div>'
	return html;
}
function OnCreateEditPageLoad() {
	debugger;
	OnCreatePageLoad();
	OnEditPageLoad();
	BindVendorNames();
	BindPackageNames($('#PackageId'));
	BindSubPackageNames($('#SubPackageId'), packageId);
	$('#PackageId').change(function () {
		BindSubPackageNames($('#SubPackageId'), $('#PackageId').val());
	});
}
function ValidateForm() {
	//if (CheckUndefinedBlankAndNull($("#VendorId").val())) {
	//	toastr.error('Please Enter Product Name');
	//	return false;
	//}
	return true;
}


function OnCreatePageLoad() {
	$("#frmCreate").on("submit", function (e) {
		e.preventDefault();
		//if (!ValidateForm()) {
		//	return;
		//}
		var data = new FormData();
		var files = $("#Image").get(0).files;
		if (files.length > 0) {
			data.append("Image", files[0]);
		}
		data.append("VendorId", $("#VendorId").val());
		data.append("ProductName", $("#ProductName").val());
		data.append("ProductCode", $("#ProductCode").val());
		data.append("PackageId", $("#PackageId").val());
		data.append("SubPackageId", $("#SubPackageId").val());
		data.append("ProductDescription", $("#ProductDescription").text());
		data.append("Price", $("#Price").val());
		data.append("GST", $("#GST").val());
		data.append("ShowOnHomePage", $("#ShowOnHomePage").val());
		data.append("Discount", $("#Discount").val());
		data.append("GoldColor", $("#GoldColor").val());
		data.append("GoldKT", $("#GoldKT").val());
		data.append("GoldWeight", $("#GoldWeight").val());
		data.append("DiamondClarity", $("#DiamondClarity").val());
		data.append("DiamondColor", $("#DiamondColor").val());
		data.append("DiamondWeight", $("#DiamondWeight").val());
		data.append("DiamondShape", $("#DiamondShape").val());
		data.append("DiamondSize", $("#DiamondSize").val());
		data.append("MakeCountry", $("#MakeCountry").val());
		data.append("MaleFemale", $("#MaleFemale").val());
		data.append("IsActive", $("#IsActive").val());
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
		data.append("Id", $("#Id").val());
		data.append("VendorId", $("#VendorId").val());
		data.append("ProductName", $("#ProductName").val());
		data.append("ProductCode", $("#ProductCode").val());
		data.append("PackageId", $("#PackageId").val());
		data.append("SubPackageId", $("#SubPackageId").val());
		data.append("ProductDescription", $("#ProductDescription").val());
		data.append("Price", $("#Price").val());
		data.append("GST", $("#GST").val());
		data.append("ShowOnHomePage", $("#ShowOnHomePage").val());
		data.append("Discount", $("#Discount").val());
		data.append("GoldColor", $("#GoldColor").val());
		data.append("GoldKT", $("#GoldKT").val());
		data.append("GoldWeight", $("#GoldWeight").val());
		data.append("DiamondClarity", $("#DiamondClarity").val());
		data.append("DiamondColor", $("#DiamondColor").val());
		data.append("DiamondWeight", $("#DiamondWeight").val());
		data.append("DiamondShape", $("#DiamondShape").val());
		data.append("DiamondSize", $("#DiamondSize").val());
		data.append("MakeCountry", $("#MakeCountry").val());
		data.append("MaleFemale", $("#MaleFemale").val());
		data.append("IsActive", $("#IsActive").val());
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
			url: "/Admin/Product/Delete",
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
				$('#tr_' + jsonObject.Id).remove();
			}
		});

	}
}
function ClearControl() {
	$("#divProduct").empty();
}

function RefreshGrid() {
	var oTable = $('#ProductGrid').DataTable();
	oTable.ajax.reload();
}