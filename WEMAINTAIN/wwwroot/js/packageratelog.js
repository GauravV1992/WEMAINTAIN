function BindPackageRateLogDatatable() {
	$("#PackageRateLogGrid").DataTable({

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
			"url": "/PackageRateLog/GetAll",
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
			{ "data": "modifiedOn" },
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
 
function ClearControl() {
	$("#divPackageRateLog").empty();
}

function RefreshGrid() {

	var oTable = $('#PackageRateLogGrid').DataTable();
	oTable.ajax.reload();
}