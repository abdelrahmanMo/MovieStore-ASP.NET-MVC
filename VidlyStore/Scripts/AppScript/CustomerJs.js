$(document).ready(function () {
    $('#myModal').on('shown.bs.modal', function () { $('#myInput').trigger('focus'); });
    var table= $('#customers').DataTable({
        ajax: {
            url: "/api/customers",
            dataSrc:""
        },
        columns: [
            {
                data: "name",
                render: function (data, type, customer) {
                    return "<a href= '/Customers/Detail/" + customer.id + "'>" + customer.name + "</a>";

                }
            },
            {
                data:"memberShipType.name"
            },
            {
                data: "id",
                render:function(data) {
                    return("<a class='btn btn-primary fa fa-edit' href= '/Customers/Edit/" +
                        data +
                        "'></a>"+
                        "<button class = 'btn btn-danger js-delete' data-customer-id ="+
                        data +
                        "><i class ='fa fa-user-times'></i></button>");
                }

            }

        ]
    });
    $('#customers').on('click',".js-delete",
        function () {
            var button = $(this);
            bootbox.confirm("Are you sure you want to delete this customer ?",
                function (result) {
                    if (result) {
                        $.ajax({
                            url: "/api/customers/" + button.attr("data-customer-id"),
                            method: "DELETE",
                            success: function () {
                                table.row(button.parents("tr")).remove().draw();
                            }
                        });
                    }
                });
          
        });
});