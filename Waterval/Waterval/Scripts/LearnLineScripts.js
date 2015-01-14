$(document).ready(function () {
    $("#ValidationMes").hide();
    fillDefinition();
});

function fillMod() {
    $("#ModulesTogether").val(null);

    var temp = []
    $("#ModTable tr").each(function () {

        if ($(this).attr('id') != null) {
            temp.push($(this).attr('id'))
        }
    });

    var tValue = temp.join('-');

    $('[name=ModulesTogether]').val(tValue);

}

$(document).on('click', '#addMod', function () {
    var id = $(this).attr('data-id');

    var tablevalue = [];

    $("#TableMod tr").each(function () {

        if ($(this).closest('tr').attr('id') == id) {
            var trdingm = $(this).closest('tr');


            $(trdingm).children("td").each(function () {

                tablevalue.push($(this).html());
            })
            return false;
        }
    })

    var coursecode = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    var coursedefi = '<td>' + tablevalue[1] + '</td>';
    var buttonDelete = '<td><input type="button" value="Verwijderen" id="btn_delete" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    $('#ModTable').append(coursecode + coursedefi + buttonDelete);
    $(this).closest('tr').remove();

    fillMod();
});

$(document).on('click', '#btn_delete', function () {
    if (confirm('Deze gegevens verwijderen?')) {

        var id = $(this).attr('data-id');

        var tablevalue = [];

        $("#ModTable tr").each(function () {

            if ($(this).closest('tr').attr('id') == id) {
                var trdingm = $(this).closest('tr');


                $(trdingm).children("td").each(function () {

                    tablevalue.push($(this).html());
                })
                return false;
            }
        })

        var coursecode = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var coursedefi = '<td>' + tablevalue[1] + '</td>';
        var buttonDelete = '<td><input type="button" value="Toevoegen" id="addMod" data-id="' + id + '" class="btn btn-avans"/></td></tr>';

        $('#TableMod').append(coursecode + coursedefi + buttonDelete);

        $(this).closest('tr').remove(); fillDefinition();
    }
});

//keyup
//change
//Whenever text get changed and the textbox gets leaved this function will be called
//fillDefinition will get called
$(document).on('change', '#field_description', function () {
    fillDefinition();
});

$('input[name="btn_collapseGoal"] ').click(function () {
    $('[name=TempValue]').val(null);
}
);

$('input[name="btn_Save"] ').click(function () {
    if (!$('[id=long]').val()) {
        $("#ValidationMes").show();
    }
    else {
        $("#ValidationMes").hide();
    }


}
);