$(() => {
    setInterval(() => {
        var id = $("#like-btn").val();
        $.get(`/home/getlikes?`, { id }, function (p) {
            $("#likes-count").text(p.likes);
        })
    }, 1000)

    $("#like-btn").on('click', function () {
        var id = $("#like-btn").val();
        $.post(`/home/addlikes?`, { id }, function (p) {
            $("#like-btn").prop("disabled", true);
        })
    })
})