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
    var coursecode = '<tr id="' + id + '"><td>' + tablevalue[0] + '<input type="number" id="Module[' + (index - 1) + '].Module_ID" name="Module[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>';
    //We fill the the next td with the array value
    var coursetitle = '<td>' + tablevalue[1] + '</td>';
    //We fill the the next td with the array value
    var coursedefi = '<td>' + tablevalue[2] + '</td>';

    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
   

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteMod" data-id="' + id + '" class="btn btn-avans"/></td></tr>';

    //We add this row to the ModTable.
    $('#ModTable').append(coursecode + coursetitle + coursedefi +  buttonDelete);

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
                        $(this).attr('name', 'Module[' + (index) + '].Module_ID')
                        $(this).attr('id', 'Module[' + (index) + '].Module_ID')
                    }
                });
                index += 1;
            }
        });
    }
});
