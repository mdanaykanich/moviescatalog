$(function () {
    $("#ddlGenres").append('<option value="all" selected="selected">Всі жанри</option>');
    $("#ddlCountries").append('<option value="all" selected="selected">Всі країни</option>');
    $("#ddlYears").append('<option value="0" selected="selected">Всі роки</option>');
    $("#btnextsearch").click(function () {
        window.location.replace(`http://${document.location.host}/Home/Films?year=${$("#ddlYears").val()}&genre=${$("#ddlGenres").val()}&country=${$("#ddlCountries").val()}`);
    });
});