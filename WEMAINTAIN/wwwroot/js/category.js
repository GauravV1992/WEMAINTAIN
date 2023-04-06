function BindCategoryDatatable() {
	debugger;
	$("#categoryGrid").DataTable({

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
		"bSort": true,
		"bDestroy": true,
		"searching": false,
		"ajax": {
			"url":"/Category/GetAll",
			"type": "POST",
			"datatype": "json",
			"data": function (d) {
				d.RequestVerificationToken = $(document).find('input [name=__RequestVerificationToken]').val();
			},
		},
		"columns": [{ "data": "id" },
		{ "data": "name" },
		{
			"name": "Action",
			render: function (data, type, row) {
				debugger;
				return CreateActionButton(row.Id);
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
		],
		"FnDrawCallback": function (a, b, c) {

		}
	});
}
function CreateActionButton(row) {

}