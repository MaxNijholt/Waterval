// This function gets called when the document is ready
// It only has one purpose and that is calling fillDefinition
$(document).ready(function () {
    $("#ValidationMes").hide();
    fillDefinition();
    //  fillMod();
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
        var buttonDelete = '<td><input type="button" value="-" id="btn_delete" class="btn btn-danger"/></td></tr>';
        $('#LongTable').append(inputfield + buttonDelete);
        $('[name=TempValue]').val(null);
        fillDefinition();
    }
});

//This function gets called when we press the button btn_Add
//This function will make a table containing all the modules that will need to combined with competence.
//#btn_addMod is the HTML ID
$(document).on('click', '#btn_addMod', function () {
    //The data-id of this button contains the module id.
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain course code;
    //Index[1] will contain course title;
    //Index[2] will contain course coursedefination;

    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableMod tr").each(function () {
        //Here say if the id of this row is the same as the id  then we go in the statement.
        if ($(this).attr('id') == id) {
            //We will loop through all the td  in the tr.
            //And we pust them to the tablevalue array.
            //If we are done we return the function so the loop will cancel
            $(this).children("td").each(function () {
                tablevalue.push($(this).html());
            })
            return false;
        }
    })

    //We ask how long the ModTable is (The table where we will store the values)
    //We use this value to create a working index for the model to know.
    var index = $("#ModTable tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var coursecode = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    //We fill the the next td with the array value
    var coursetitle = '<td>' + tablevalue[1] + '</td>';
    //We fill the the next td with the array value
    var coursedefi = '<td>' + tablevalue[2] + '</td>';

    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
    var inputfield = '<td><input type="number" class="form-control" id="Level[' + (index - 1) + '].Level1" name="Level[' + (index - 1) + '].Level1" value="0" />'
    var hiddenfield = '<input type="number" id="Level[' + (index - 1) + '].Module_ID" name="Level[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>'

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteMod" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#ModTable').append(coursecode + coursetitle + coursedefi + inputfield + hiddenfield + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteMod', function () {
    //Als er bij het dialog op ok word gedrukt word deze code uitgevoerd.
    if (confirm('Deze gegevens verwijderen?')) {
        //data-id van de button ophalen. 
        var id = $(this).attr('data-id');

        //This is an empty array that will be containing the values from the table row.
        //Index[0] will contain course code;
        //Index[1] will contain course title;
        //Index[2] will contain course coursedefination;
        var tablevalue = [];

        //We loop through the table
        $("#ModTable tr").each(function () {
            //IF the current  tr has an id, then loop through the td 
            //and at those values to the array.
            //if that happend exit the function
            if ($(this).attr('id') == id) {
                $(this).children("td").each(function () {
                    tablevalue.push($(this).html());
                })
                return false;
            }
        })
        //Build the new table
        var coursecode = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var coursetitle = '<td>' + tablevalue[1] + '</td>';
        var coursedefi = '<td>' + tablevalue[2] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addMod" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableMod').append(coursecode + coursetitle + coursedefi + buttonAdd);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var index = 0;

        //Loop through the table
        //If the row contains an id
        //Loop through the row and find all inputs that are of the number type.
        //If the input ends with ID then fill if for the Module 
        //else it fills for th elevel.
        //Then we up 1 the index.
        $('#ModTable tr').each(function () {
            if ($(this).attr('id') != null) {
                $(this).find("input[type=number]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'Level[' + (index) + '].Module_ID')
                        $(this).attr('id', 'Level[' + (index) + '].Module_ID')
                    }
                    else {
                        $(this).attr('name', 'Level[' + (index) + '].Level1')
                        $(this).attr('id', 'Level[' + (index) + '].Level1')
                    }
                });
                index += 1;
            }
        });
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
        var buttonDelete = '<td><input type="button" value="-" id="btn_delete"  class="btn btn-danger"/></td></tr>';
        $('#LongTable > tbody > tr').eq(index - 1).after(inputfield + buttonDelete);
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
    if (!$('[id=long]').val()) {
        $("#ValidationMes").show();
        return;
    }
    else {
        $("#ValidationMes").hide();
    }
}
);

function fillMod() {

    alert('Wow wait I should not be visible!')
    /*
    $("#ModulesTogether").val(null);

    var temp = []
    $("#ModTable tr").each(function () {

        if ($(this).attr('id') != null) {
            temp.push($(this).attr('id'))
        }
    });

    var tValue = temp.join('-');

    $('[name=ModulesTogether]').val(tValue);
    */
}