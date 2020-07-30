
    // constructs the suggestion engine
var vm = { movieIds:[] };
    var customers = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#customer').typeahead({
        minLength: 3,
        highlight:true
    }, {
        name: 'customers',
        display: 'name',
        source: customers
    }).on("typeahead:select",
        function(e,customer) {
            vm.customerId = customer.id;
        
        });

    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });
 
    $('#movie').typeahead({
        minLength: 3,
        highlight: true
    }, {
        name: 'movie',
        display: 'name',
        source: movies
    }).on("typeahead:select",
        function (e, movie) {
            console.log(movie);
            $("#movies").append("<li class='list-group-item'>" + movie.name + "</li>");
            $("#movie").typeahead("val", "");
            vm.movieIds.push(movie.id);

            console.log("hellloo");
            console.log(vm);
    });

    $.validator.addMethod("validCustomer", function () {
        return vm.customerId && vm.customerId !== 0;
}, "Please select a valid customer.");

    $.validator.addMethod("atLeastOneMovie", function () {
        return vm.movieIds.length > 0;
    }, "Please select at least one movie.");


var validator = $('#RentalForm').validate({
    submitHandler: function() {
        $.ajax({
                url: "/api/Rental",
                method: "post",
                data: vm

            })
            .done(function () {
                toastr.success("Rentals successfully recorded .");
                $("#movies").empty();
                $("#customer").typeahead("val", "");
                $("#movie").typeahead("val", "");
                vm = { movieIds: [] };
                validator.resetForm();
            })
            .fail(function () {
                toastr.error("Something unexpected happened.");
            });
        return false;
    }
});
