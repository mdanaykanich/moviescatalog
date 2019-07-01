$(function () {
    $("#btnsearch").click(function () {

        $.ajax({
            url: "http://" + document.location.host + "/Home/GetFilmId",
            dataType: "json",
            data: { filmTitle: $("#inpsearch").val() },
            method: "POST",
            success: function (data) {
                if (data >= 0)
                    window.location.replace("http://" + document.location.host + "/Home/Details/" + data);
                else
                    window.location.replace("http://" + document.location.host + "/Home/Filmsearch?partialTitle=" + $("#inpsearch").val());
            }
        });

    });

    var input = document.getElementById('inpsearch');
    input.oninput = function () {
        $("#coincidences").html("");
        if ($("#inpsearch").val() !== "") {
            $.ajax({
                url: "http://" + document.location.host + "/Home/Coincidences?val=" + input.value,
                dataType: "json",
                method: "GET",
                success: function (data) {
                    var str = "";
                    for (var i = 0; i < data.length; i++) {
                        str += "<option value='" + data[i] + "'/>";
                    }

                    document.getElementById("coincidences").innerHTML = str;
                }
            });
        }

    };
});