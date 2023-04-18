
function OnCreateEditPageLoad() {
	OnCreatePageLoad();
	/*OnEditPageLoad();*/
	BindPackageNames();
	BindServiceNames($('#SubPackageId').val());
	GetSubPackageOnPackageChange();
	$('#PackageId').on("change", GetSubPackageOnPackageChange);
}
function AddPurchaseDetails() {
	$("#divPurchaseDetails").empty();
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

function OnCancel() {
	$("#divPurchaseDetails").empty();
}

$('#PurchaseDetailsGrid').on('click', '.viewcs', function () {
	debugger;

	var tr = $(this).closest('tr');
	var row = $('#PurchaseDetailsGrid').DataTable().row(tr);
	var rowData = row.data();
	//$('.edit-row' + rowData.id + '').remove();
	$('.edit-row').remove();
	$.ajax({
		type: "Get",
		url: '/PurchaseDetails/PurchaseServicesById/?id=' + rowData.id + '',
		data: null,
		datatype: "json",
		success: function (response) {
			debugger;
			$('#divPurchaseServices').html(response);
		},
		complete: function () {
			/*$('#loading').hide();*/
		}
	});

	$('<tr class="edit-row"><td colspan="7"><div id="divPurchaseServices"></div></td></tr>').insertAfter(tr);
})



function BindPurchaseDetailsDatatable() {
	$("#PurchaseDetailsGrid").DataTable({

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
			"url": "/PurchaseDetails/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
			"dataSrc": function (response) {
				debugger;
				return response.data.data.purchaseDetails;
			}
		},
		"columns": [{ "data": "id" },
		{
			"name": "View",
			render: function (data, type, row) {
				return CreateActionButton(row.id);
			}
		},
		{ "data": "mobileNo" },
		{ "data": "userName" },
		{ "data": "packageName" },
		{ "data": "subPackageName" },
		{ "data": "packageAmount" },
		{ "data": "createdOn" },
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
	html = html + "<div class='d-grid gap-2 d-md-flex justify-content-md-end'><a class='viewcs' href='#'>Services</></div>"
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
	else if (CheckUndefinedBlankAndNull($("#AMCPeriod").val())) {
		toastr.error('Please Select AMC Period');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Rate").val())) {
		toastr.error('Please Enter Rate');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#Discount").val())) {
		toastr.error('Please Enter Discount');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#PackageAmount").val())) {
		toastr.error('Please Enter Package Amount');
		return false;
	}
	else if (CheckUndefinedBlankAndNull($("#ServiceId").val())) {
		toastr.error('Please Select Service Name');
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


function ClearControl() {
	$("#divPurchaseDetails").empty();
}
function RefreshGrid() {
	var oTable = $('#PurchaseDetailsGrid').DataTable();
	oTable.ajax.reload();
}