//LEARNLINE
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

    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteLearnLine" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#LearnLineTable').append(learnline + hiddenfield + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteLearnLine', function () {
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
        $("#LearnLineTable tr").each(function () {
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
        var learnline = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addLearnLine" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableLearnLine').append(learnline + buttonAdd);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var index = 0;

    }
});
//END LEARNLINE




//LEARNGOAL
$(document).on('click', '#btn_addLearngoal', function () {
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain omschrijving;


    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableLearnGoal tr").each(function () {
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

    var index = $("#LearnGoalTable tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var learngoal = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    //maak van class="form-control hidden"
    //In de entity module hebben we learnline zitten. 
    //We maken hier dus een learnline "aan" zodat ons model het herkent. Dus eig zeggen we hier gewoon Module.LearnLine[0].LearnLine_ID
    //Hierdoor herkent het model de learnline en kunne we deze meesturen. 
    //De de rest van de code moet je zelf even toeveogen en aanpassen naar wens. 
    var hiddenfield = '<td><input type="number" id="LearnGoal[' + (index - 1) + '].LearnGoal_ID" name="LearnGoal[' + (index - 1) + '].LearnGoal_ID" class="form-control" value=' + id + ' /></td>'

    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteLearnGoal" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#LearnLineTable').append(learngoal + hiddenfield + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteLearnGoal', function () {
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
        $("#LearnGoalTable tr").each(function () {
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
        var learngoal = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addLearngoal" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableLearnGoal').append(learngoal + buttonAdd);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var index = 0;

    }
});
//END LEARNGOAL




//LEARNTOOL
$(document).on('click', '#btn_addLearningtool', function () {
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain omschrijving;


    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableLearnTool tr").each(function () {
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

    var index = $("#LearnToolTable tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var learntool = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    //maak van class="form-control hidden"
    //In de entity module hebben we learnline zitten. 
    //We maken hier dus een learnline "aan" zodat ons model het herkent. Dus eig zeggen we hier gewoon Module.LearnLine[0].LearnLine_ID
    //Hierdoor herkent het model de learnline en kunne we deze meesturen. 
    //De de rest van de code moet je zelf even toeveogen en aanpassen naar wens. 
    var hiddenfield = '<td><input type="number" id="LearnTool[' + (index - 1) + '].LearnTool_ID" name="LearnTool[' + (index - 1) + '].LearnTool_ID" class="form-control" value=' + id + ' /></td>'

    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteLearnTool" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#LearnToolTable').append(learntool + hiddenfield + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteLearnTool', function () {
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
        $("#LearnToolTable tr").each(function () {
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
        var learntool = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addLearningtool" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableLearnTool').append(learntool + buttonAdd);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var index = 0;

    }
});
//END LEARNTOOL




//THEME
$(document).on('click', '#btn_addTheme', function () {
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain omschrijving;


    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableTheme tr").each(function () {
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

    var index = $("#ThemeTable tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var theme = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    //maak van class="form-control hidden"
    //In de entity module hebben we learnline zitten. 
    //We maken hier dus een learnline "aan" zodat ons model het herkent. Dus eig zeggen we hier gewoon Module.LearnLine[0].LearnLine_ID
    //Hierdoor herkent het model de learnline en kunne we deze meesturen. 
    //De de rest van de code moet je zelf even toeveogen en aanpassen naar wens. 
    var hiddenfields = '<td><input type="number" id="Theme[' + (index - 1) + '].Theme_ID" name="Theme[' + (index - 1) + '].Theme_ID" class="form-control" value=' + id + ' /></td>'

    var buttonDeletes = '<td><input type="button" value="-" id="btn_deleteTheme" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#ThemeTable').append(theme + hiddenfields + buttonDeletes);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteTheme', function () {
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
        $("#ThemeTable tr").each(function () {
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
        var themes = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdds = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addTheme" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableTheme').append(themes + buttonAdds);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var index = 0;

    }
});
//END THEME





//COMPETENCE
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
    var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';


    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
    var inputfield = '<td><input type="number" class="form-control" id="Level[' + (index - 1) + '].Level1" name="Level[' + (index - 1) + '].Level1" value="0" />'
    var hiddenfield = '<input type="number" id="Level[' + (index - 1) + '].Competence_ID" name="Level[' + (index - 1) + '].Competence_ID" class="form-control hidden" value=' + id + ' /></td>'

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteMod" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#ModTable').append(coursetitle + inputfield + hiddenfield + buttonDelete);

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
        var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addMod" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableMod').append(coursetitle + buttonAdd);

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
                        $(this).attr('name', 'Level[' + (index) + '].Competence_ID')
                        $(this).attr('id', 'Level[' + (index) + '].Competence_ID')
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
//END COMPETENCE






//Study
$(document).on('click', '#btn_addStudy', function () {
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain omschrijving;


    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableStudy tr").each(function () {
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

    var index = $("#StudyTable tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var study = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
    //maak van class="form-control hidden"
    //In de entity module hebben we learnline zitten. 
    //We maken hier dus een learnline "aan" zodat ons model het herkent. Dus eig zeggen we hier gewoon Module.LearnLine[0].LearnLine_ID
    //Hierdoor herkent het model de learnline en kunne we deze meesturen. 
    //De de rest van de code moet je zelf even toeveogen en aanpassen naar wens. 
    var hiddenfield = '<td><input type="number" id="Study[' + (index - 1) + '].Stuy_ID" name="Study[' + (index - 1) + '].Study_ID" class="form-control" value=' + id + ' /></td>'
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteStudy" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#StudyTable').append(study + hiddenfield + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteStudy', function () {
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
        $("#StudyTable tr").each(function () {
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
        var study = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addStudy" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableStudy').append(study + buttonAdd);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var index = 0;

    }
});
//END PROGRAM





//WORKFORM

$(document).on('click', '#btn_addModa', function () {
    //The data-id of this button contains the module id.
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain course code;
    //Index[1] will contain course title;
    //Index[2] will contain course coursedefination;

    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableModa tr").each(function () {
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
    var indexes = $("#ModTablea tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';


    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
    var inputfield = '<td><input type="number" class="form-control" id="ModelWithWorkform[' + (indexes - 1) + '].Duration" name="ModelWithWorkform[' + (indexes - 1) + '].Duration" value="0" />'
    var hiddenfield = '<input type="number" id="ModelWithWorkform[' + (indexes - 1) + '].Workform_ID" name="ModelWithWorkform[' + (indexes - 1) + '].Workform_ID" class="form-control hidden" value=' + id + ' /></td>'

    var inputfieldF = '<td><input type="text" class="form-control" id="ModelWithWorkform[' + (indexes - 1) + '].Frequency" name="ModelWithWorkform[' + (indexes - 1) + '].Frequency" value="0" />'
    var hiddenfieldF = '<input type="text" id="ModelWithWorkform[' + (indexes - 1) + '].Workform_ID" name="ModelWithWorkform[' + (indexes - 1) + '].Workform_ID" class="form-control hidden" value=' + id + ' /></td>'

    var inputfieldW = '<td><input type="number" class="form-control" id="ModelWithWorkform[' + (indexes - 1) + '].Workload" name="ModelWithWorkform[' + (indexes - 1) + '].Workload" value="0" />'
    var hiddenfieldW = '<input type="number" id="ModelWithWorkform[' + (indexes - 1) + '].Workform_ID" name="ModelWithWorkform[' + (indexes - 1) + '].Workform_ID" class="form-control hidden" value=' + id + ' /></td>'

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteModa" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.W
    $('#ModTablea').append(coursetitle + inputfield + inputfieldF + inputfieldW + buttonDelete);

    //We remove the row have choosen in TableMod;
    $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteModa', function () {
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
        $("#ModTablea tr").each(function () {
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
        var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addModa" class="btn btn-avans"/></td></tr>';
        //Add row to table
        $('#TableModa').append(coursetitle + buttonAdd);

        //Remove the row for table
        $(this).closest('tr').remove();

        //Make an index 0 for adding the correct index to the Module attribute
        var indexes = 0;

        //Loop through the table
        //If the row contains an id
        //Loop through the row and find all inputs that are of the number type.
        //If the input ends with ID then fill if for the Module 
        //else it fills for th elevel.
        //Then we up 1 the index.
        $('#ModTablea tr').each(function () {
            if ($(this).attr('id') != null) {
                $(this).find("input[type=number]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'ModelWithWorkform[' + (indexes) + '].Workform_ID')
                        $(this).attr('id', 'ModelWithWorkform[' + (indexes) + '].Workform_ID')
                    }
                    else {
                        $(this).attr('name', 'ModelWithWorkform[' + (indexes) + '].Duration')
                        $(this).attr('id', 'ModelWithWorkform[' + (indexes) + '].Duration')
                        $(this).attr('name', 'ModelWithWorkform[' + (indexes) + '].Workload')
                        $(this).attr('id', 'ModelWithWorkform[' + (indexes) + '].Workload')
                    }
                });
                $(this).find("input[type=text]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'ModelWithWorkform[' + (indexes) + '].Workform_ID')
                        $(this).attr('id', 'ModelWithWorkform[' + (indexes) + '].Workform_ID')
                    }
                    else {

                        $(this).attr('name', 'ModelWithWorkform[' + (indexes) + '].Frequency')
                        $(this).attr('id', 'ModelWithWorkform[' + (indexes) + '].Frequency')
                    }
                });
                indexes += 1;
            }
        });
    }
});

//END WORKFORM




//WEEKSCHEDULE
//This function gets called when we press the button btn_Add
//This function will make a table containing all the modules that will need to combined with competence.
//#btn_addMod is the HTML ID
$(document).on('click', '#btn_addModw', function () {
    //The data-id of this button contains the module id.
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain course code;
    //Index[1] will contain course title;
    //Index[2] will contain course coursedefination;

    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableModw tr").each(function () {
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
    var index = $("#ModTablew tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var coursetitle = '<tr id="' + id + '"><td class="hidden">' + tablevalue[0] + '</td>';


    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
    var inputfield = '<td><input type="number" class="form-control" id="WeekSchedule[' + (index - 1) + '].WeekNr" name="WeekSchedule[' + (index - 1) + '].WeekNr" value="0" />'
    var hiddenfield = '<input type="number" id="WeekSchedule[' + (index - 1) + '].Module_ID" name="WeekSchedule[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>'


    var inputfield2 = '<td><input type="text" class="form-control" id="WeekSchedule[' + (index - 1) + '].Description" name="WeekSchedule[' + (index - 1) + '].Description" value="0" />'
    var hiddenfield2 = '<input type="text" id="WeekSchedule[' + (index - 1) + '].Module_ID" name="WeekSchedule[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>'

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteModw" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#ModTablew').append(coursetitle + inputfield + inputfield2 + buttonDelete);

    //We remove the row have choosen in TableMod;
    //  $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteModw', function () {
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
        $("#ModTablew tr").each(function () {
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
        ////Build the new table
        //var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        //var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addModg" class="btn btn-avans"/></td></tr>';
        ////Add row to table
        //$('#TableModg').append(coursetitle + buttonAdd);

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
        $('#ModTablew tr').each(function () {
            if ($(this).attr('id') != null) {
                $(this).find("input[type=number]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'WeekSchedule[' + (index) + '].Module_ID')
                        $(this).attr('id', 'WeekSchedule[' + (index) + '].Module_ID')
                    }
                    else {
                        $(this).attr('name', 'WeekSchedule[' + (index) + '].WeekNr')
                        $(this).attr('id', 'WeekSchedule[' + (index) + '].WeekNr')
                    }
                });
                $(this).find("input[type=text]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'WeekSchedule[' + (index) + '].Module_ID')
                        $(this).attr('id', 'WeekSchedule[' + (index) + '].Module_ID')
                    }
                    else {
                        $(this).attr('name', 'WeekSchedule[' + (index) + '].Description')
                        $(this).attr('id', 'WeekSchedule[' + (index) + '].Description')
                    }
                });
                index += 1;
            }
        });
    }
});
//END WEEKSCHEDULE








//GRADETYPE
//This function gets called when we press the button btn_Add
//This function will make a table containing all the modules that will need to combined with competence.
//#btn_addMod is the HTML ID
$(document).on('click', '#btn_addModg', function () {
    //The data-id of this button contains the module id.
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain course code;
    //Index[1] will contain course title;
    //Index[2] will contain course coursedefination;

    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableModg tr").each(function () {
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
    var index = $("#ModTableg tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var coursetitle = '<tr id="' + id + '"><td class="hidden">' + tablevalue[0] + '</td>';


    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
    var inputfield = '<td><input type="text" class="form-control" id="GradeType[' + (index - 1) + '].GradeDescription" name="GradeType[' + (index - 1) + '].GradeDescription" value="0" />'
    var hiddenfield = '<input type="text" id="GradeType[' + (index - 1) + '].Module_ID" name="GradeType[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>'

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteModg" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#ModTableg').append(coursetitle + inputfield + buttonDelete);

    //We remove the row have choosen in TableMod;
  //  $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteModg', function () {
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
        $("#ModTableg tr").each(function () {
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
        ////Build the new table
        //var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        //var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addModg" class="btn btn-avans"/></td></tr>';
        ////Add row to table
        //$('#TableModg').append(coursetitle + buttonAdd);

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
        $('#ModTableg tr').each(function () {
            if ($(this).attr('id') != null) {
                $(this).find("input[type=text]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'GradeType[' + (index) + '].Module_ID')
                        $(this).attr('id', 'GradeType[' + (index) + '].Module_ID')
                    }
                    else {
                        $(this).attr('name', 'GradeType[' + (index) + '].GradeDescription')
                        $(this).attr('id', 'GradeType[' + (index) + '].GradeDescription')
                    }
                });
                index += 1;
            }
        });
    }
});
//END GRADETYPE



//ASSIGNMENTCODE
//This function gets called when we press the button btn_Add
//This function will make a table containing all the modules that will need to combined with competence.
//#btn_addMod is the HTML ID
$(document).on('click', '#btn_addModt', function () {
    //The data-id of this button contains the module id.
    var id = $(this).attr('data-id');

    //This is an empty array that will be containing the values from the table row.
    var tablevalue = [];
    //Index[0] will contain course code;
    //Index[1] will contain course title;
    //Index[2] will contain course coursedefination;

    //We are going to loop through the rows from TableMod (Use it HTML ID) 
    $("#TableModt tr").each(function () {
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
    var index = $("#ModTablet tr").length;
    //We create a tr that has an id and we combine a td next to it.
    var coursetitle = '<tr id="' + id + '"><td class="hidden">' + tablevalue[0] + '</td>';


    //Here we will create a input field for the Level1 string;
    //The ID and the Name needs to be same as the one you want to fill in the model.
    //The index is important to know what subitem in the model needs to get filled.
    var inputfield = '<td><input type="text" class="form-control" id="AssignmentCode[' + (index - 1) + '].Description" name="AssignmentCode[' + (index - 1) + '].Description" value="0" />'
    var hiddenfield = '<input type="text" id="AssignmentCode[' + (index - 1) + '].Module_ID" name="AssignmentCode[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>'

    var inputfield2 = '<td><input type="number" class="form-control" id="AssignmentCode[' + (index - 1) + '].EC" name="AssignmentCode[' + (index - 1) + '].EC" value="0" />'
    var hiddenfield2 = '<input type="number" id="AssignmentCode[' + (index - 1) + '].Module_ID" name="AssignmentCode[' + (index - 1) + '].Module_ID" class="form-control hidden" value=' + id + ' /></td>'

    //Add a delete button to the table, cause it looks nice....
    var buttonDelete = '<td><input type="button" value="-" id="btn_deleteModt" data-id="' + id + '" class="btn btn-danger"/></td></tr>';

    //We add this row to the ModTable.
    $('#ModTablet').append(coursetitle + inputfield + inputfield2 + buttonDelete);

    //We remove the row have choosen in TableMod;
    //  $(this).closest('tr').remove();
});

//This code gets called whenever one of the delete functions gets pressed.
//We will get a confirm dialog and choosing yes will remove the data and clean up the table.
//After that fillDefinition gets called to fill the selectbox and the Definition_long
$(document).on('click', '#btn_deleteModt', function () {
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
        $("#ModTablet tr").each(function () {
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
        ////Build the new table
        //var coursetitle = '<tr id="' + id + '"><td>' + tablevalue[0] + '</td>';
        //var buttonAdd = '<td><input type="button" value="+" data-id="' + id + '" id="btn_addModg" class="btn btn-avans"/></td></tr>';
        ////Add row to table
        //$('#TableModg').append(coursetitle + buttonAdd);

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
        $('#ModTablet tr').each(function () {
            if ($(this).attr('id') != null) {
                $(this).find("input[type=text]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'AssignmentCode[' + (index) + '].Module_ID')
                        $(this).attr('id', 'AssignmentCode[' + (index) + '].Module_ID')
                    }
                    else {
                        $(this).attr('name', 'AssignmentCode[' + (index) + '].Description')
                        $(this).attr('id', 'AssignmentCode[' + (index) + '].Description')
                    }
                });
                $(this).find("input[type=number]").each(function () {
                    if ($(this).attr('name').match("ID$")) {
                        $(this).attr('name', 'AssignmentCode[' + (index) + '].Module_ID')
                        $(this).attr('id', 'AssignmentCode[' + (index) + '].Module_ID')
                    }
                    else {
                        $(this).attr('name', 'AssignmentCode[' + (index) + '].EC')
                        $(this).attr('id', 'AssignmentCode[' + (index) + '].EC')
                    }
                });
                index += 1;
            }
        });
    }
});
//END ASSIGNMENTCODE














































