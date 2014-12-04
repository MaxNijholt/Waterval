// This function gets called when the document is ready
// It only has one purpose and that is calling fillDefinition
$(document).ready(function () {
    $("#ValidationMes").hide();
    fillDefinition();
});

//This function will add data to the table
//if the value from the data is empty then it will not be done.
//Else we will make a variabele filled with the information of the input field,
//and one for the button.
//Then we will add the data to the table after that we clear the inputfield
//We will Fill the definition after that.
$('input[name="addGoal"] ').click(function () {
    var fieldvalue = $('[name=TempValue]').val();
    if (fieldvalue) {
        var inputfield = '<tr><td><input type="text" class="form-control" id="field_description" value="' + fieldvalue + '"/></td>';
        var buttonDelete = '<td><input type="button" value="Verwijderen" id="btn_delete" class="btn btn-danger"/></td></tr>';
        $('#LongTable').append(inputfield + buttonDelete);
        $('[name=TempValue]').val(null);
        fillDefinition();
    }
});
//This function will add data to the table at a selected place
//if the value from the data is empty or the index of the select is 0 then it will not be done.
//Else we will make a variabele filled with the information of the input field,
//and one for the button.
//Then we will add the data to the table at the selected location-1 otherwise it will be at the wrong location
//after that we clear the inputfield
//We will Fill the definition after that.
$('input[name="addGoalBetween"] ').click(function () {
    var fieldvalue = $('[name=TempValue]').val();
    var index = $("#selecteer").val();
    if (fieldvalue && index > 0) {
        var inputfield = '<tr><td><input type="text" class="form-control" id="field_description" value="' + fieldvalue + '"/></td>';
        var buttonDelete = '<td><input type="button" value="Verwijderen" id="btn_delete"  class="btn btn-danger"/></td></tr>';
        $('#LongTable > tbody > tr').eq(index-1).after(inputfield + buttonDelete);
        $('[name=TempValue]').val(null);

        fillDefinition();
    }
});


//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_delete', function () {
    if (confirm('Deze gegevens verwijderen?')) {
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

//When this function gets called it will clear the description_long box to remove error chances.
//After that we fill a variable with the options and clear this.
//We will create a base option so the user always has one value.
//We will create a variabele that knows at what index we are. This will start at 1.
//We create a text array for filling the description long.
//We loop through to the table and ad all the values in the text array and in the options
//we also +1 the index everytime.
//After that we join the text array together and fill them in the a hidden textarea. (Needed for the model)
function fillDefinition() {
    $('[id=long]').val(null);

    var options = $("#selecteer").empty();
    options.append($("<option  disabled selected/>").val(0).text("Maak een keuze"));
    var index = 1;

    var arrText = new Array();
    $('input[id=field_description]').each(function () {
        arrText.push($(this).val());
        options.append($("<option />").val(index).text($(this).val()));
        index++;
    })
    var tValue = arrText.join('\n');

    $('[id=long]').val(tValue);

}

//Everytime the accordion gets opened or closed the inputfield gets cleared.
$('input[name="btn_collapseGoal"] ').click(function () {
    $('[name=TempValue]').val(null);
}
);

$('input[name="btn_Save"] ').click(function () {
    if (!$('[id=long]').val())
    {
        $("#ValidationMes").show();
    }
    else
    {
        $("#ValidationMes").hide();
    }

        
}
);