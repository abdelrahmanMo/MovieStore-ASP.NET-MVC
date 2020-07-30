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
            }
        ]
    });

})