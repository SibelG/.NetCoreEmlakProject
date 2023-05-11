$(document).ready(function () {
   
    $("#DistrictId").attr('disabled', true);
    $("#NeighbourhoodId").attr('disabled', true);
    $('#TypeId').attr('disabled', true);
    $('#CityId').change(function () {
        $("#DistrictId").attr('disabled', false);
        var cityId = $(this).val();
        $("#DistrictId").empty();
        $("#DistrictId").append('<option> Select </option>');
       
       
           $.ajax({
                contentType:"application/json",
                dataType:"json",
                type:"Get",
                url:"/Admin/Advert/DistrictGet/",
                data: {CityId: cityId},
               success: function (response) {
                   console.log(response)

                    let w = JSON.parse(response);
                    console.log(w);

                    $.each(w, function (index, district) {
                        $("#DistrictId").append('<option value=' + district.DistrictId + '>' +
                            district.DistrictName + '</option>');
                    });

                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }

            });

        
   
    });

    $('#DistrictId').change(function () {
        $("#NeighbourhoodId").attr('disabled', false);
        var districtId = $(this).val();
        $("#NeighbourhoodId").empty();
        $("#NeighbourhoodId").append('<option> Select </option>');
        debugger
        $.ajax({
            contentType: "application/json",
            dataType: "json",
            type: "Get",
            url: "/Admin/Advert/NeighbourhoodGet/",
            data: { DistrictId: districtId },
            success: function (response) {
                debugger

                console.log(response)

                let w = JSON.parse(response);
                console.log(w);


                $.each(w, function (index, type) {
                    $("#NeighbourhoodId").append('<option value=' + type.TypeId + '>' +
                        type.TypeName + '</option>');
                });

            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }

        });
    });

    $('#CategoryId').change(function () {
        $('#TypeId').attr('disabled', false);
        var categoryId = $(this).val();
        $("#TypeId").empty();
        $("#TypeId").append('<option> Select </option>');
        debugger

        $.ajax({
            contentType: "application/json",
            dataType: "json",
            type: "Get",
            url="/Admin/Advert/TypeGet/",
            data: { CategoryId: categoryId },
            success: function (response) {
                debugger
                console.log(response)

                let w = JSON.parse(response);
                console.log(w);

                $.each(w, function (index, type) {
                    $("#TypeId").append('<option value=' + type.TypeId + '>' +
                        type.TypeName + '</option>');
                });

            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }

        });
    });
  
});
