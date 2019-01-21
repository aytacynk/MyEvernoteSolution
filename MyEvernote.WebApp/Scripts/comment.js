var noteid = -1;
var modalCommentBodyId = "#modal_comment_body";

$(function () {

    $('#modal_comment').on('show.bs.modal', function (e) {

        var btn = $(e.relatedTarget);
        noteid = btn.data("note-id");

        $("#modal_comment_body").load("/Comment/ShowNoteComments/" + noteid);
        //İçine veri yüklemek istenilen elemente 'load' metoduyla yükleme gerçekleştirilir.
    });

});

function doComment(btn, e, commentId, spanId) {

    var button = $(btn);
    var mode = button.data('edit-mode');

    if (e == 'edit_clicked') {

        if (!mode) {

            button.data("edit-mode", true);
            button.removeClass('btn-warning');
            button.addClass('btn-success');
            var btnSpan = button.find("span");
            btnSpan.removeClass('glyphicon-edit');
            btnSpan.addClass('glyphicon-ok');

            $(spanId).addClass('editable');
            $(spanId).attr('contenteditable', true);
            $(spanId).focus();
        } else {

            button.data("edit-mode", false);
            button.addClass('btn-warning');
            button.removeClass('btn-success');
            var btnSpan = button.find("span");
            btnSpan.addClass('glyphicon-edit');
            btnSpan.removeClass('glyphicon-ok');

            $(spanId).removeClass('editable');
            $(spanId).attr('contenteditable', false);

            var txt = $(spanId).text();
            console.log(txt);
            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentId,
                data: { text: txt }
            }).done(function (data) {

                if (data.result == true) {

                    // Yorumlar partial tekrar yüklenir.
                    $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + noteid);

                } else {
                    alert("Yorum güncellenemedi.");
                }
            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            });

        }
    }

    else if (e == 'delete_clicked') {

        var diaglog = confirm("Yorum silinmesini onaylıyor musunuz?");

        if (diaglog.result == false) {
            return false;
        }

        $.ajax({
            method: "POST",
            url: "/Comment/Delete/" + commentId
        }).done(function (data) {

            if (data.result) {
                $("#modal_comment_body").load("/Comment/ShowNoteComments/" + noteid);
                //İçine veri yüklemek istenilen elemente 'load' metoduyla yükleme gerçekleştirilir.

            } else {
                alert("Yorum silinemedi.");
            }

        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");

        });
    }

    else if (e == "new_clicked") {

        var txt = $("#new_comment_text").val();

        $.ajax({
            method: "POST",
            url: "/Comment/Create",
            data: { "text": txt, "noteid": noteid }
        }).done(function (data) {

            if (data.result) {
                $("#modal_comment_body").load("/Comment/ShowNoteComments/" + noteid);
                //İçine veri yüklemek istenilen elemente 'load' metoduyla yükleme gerçekleştirilir.

            } else {
                alert("Yorum eklenemedi.");
            }

        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }
}
