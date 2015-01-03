$(document).on('click', '#btn_addLearnLine', function () {
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain omschrijving;


    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableLearnLine tr").each(function () {
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

    var index = $("#LearnLineTable tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var learnline = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    //maak van class="form-control hidden"
    //In de entity module hebben we learnline zitten. 
    //We maken hier dus een learnline "aan" zodat ons model het herkent. Dus eig zeggen we hier gewoon Module.LearnLine[0].LearnLine_ID
    //Hierdoor herkent het model de learnline en kunne we deze meesturen. 
    //De de rest van de code moet je zelf even toeveogen en aanpassen naar wens. 
    var hiddenfield = '<td><input type="number" id="LearnLine[' + (index - 1) + '].Learnline_ID" name="LearnLine[' + (index - 1) + '].Learnline_ID" class="form-control" value=' + id + ' /></td>'

    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteLearnline" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#LearnLineTable').append(learnline + hiddenfield + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});