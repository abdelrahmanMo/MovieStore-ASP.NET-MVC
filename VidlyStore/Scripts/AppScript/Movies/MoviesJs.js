$(document).ready(function () {
    $('#myModal').on('shown.bs.modal', function () { $('#myInput').trigger('focus'); });
   var table= $('#movies').DataTable({
        ajax: {
            url: "/api/Movies",
            dataSrc:""
        },
        columns:[
            {
                data: "name",
                render:function(data,type,movie) {
                    return "<a href='/Movies/Detail/" + movie.id + "'>" + movie.name + "</a>";
                }
            },
            {
                data:"genre.name"
            },
            {
                data: "id",
                render: function (data) {
                    return ("<a class='btn btn-primary fa fa-edit' href= '/Movies/Edit/" +
                        data +
                        "'></a>" +
                        "<button class = 'btn btn-danger js-delete' data-movie-id =" +
                        data +
                        "><i class ='fa fa-user-times'></i></button>");

                }
            }

        ]
    });
    $('#movies').on('click',
        '.js-delete',
        function () {
            var button = $(this);
            bootbox.confirm('Are you sure you want to delete this movie ?',
                function(result) {
                    if (result) {
                        $.ajax({
                            url: "/api/Movies/" + button.attr('data-movie-id'),
                            method: 'DELETE',
                            success: function() {
                                table.row(button.parents("tr")).remove().draw();
                          
                            }
                        });
                    }
                });

        });
})