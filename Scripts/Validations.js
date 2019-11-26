function OnlyAlphabets(e, field) {
    var val = field.value;
    var re = /^[a-zA-Z ]*$/;
    var re1 = /^[a-zA-Z ]*/;
    if (re.test(val)) {
        //do something here

    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            field.value = "";
        }
    }
}

function AlphabetsandDash(e, field) {
    var val = field.value;
    var re = /^([a-zA-Z-]+)$/g;
    var re1 = /^([a-zA-Z-]+)/g;
    if (re.test(val)) {

    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            field.value = "";
        }
    }
}

function AlphabetsandSomeSpecialChar(e, field) {
    var val = field.value;
    var re = /^([a-zA-Z&,-/' ]+)$/g;
    var re1 = /^([a-zA-Z&,-/' ]+)/g;
    if (re.test(val)) {

    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            field.value = "";
        }
    }
}




function NumwithTwoDecimals(e, field) {
    var val = field.value;
    var re = /^((\d+)(\.\d{1,2})|(\.\d{1,2}))$/g;
    var re1 = /^((\d+)(\.\d{1,2})|(\.\d{1,2}))/g;
    if (re.test(val)) {

    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            if (isNaN(field.value) && field.value != '.') {
                field.value = "";
            }
        }
    }
}
function NumAndEnterOnly(e, field) {
    var val = field.value;
    //'/^[0-9 ]+$/'
    var re = /^([0-9\n]+)$/g;
    var re1 = /^([0-9\n]+)/g;
    if (re.test(val)) {
    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            field.value = "";
        }
    }
}


function NumMinusAndEnterOnly(e, field) {
    var val = field.value;
    //'/^[0-9 ]+$/'
    var re = /^([0-9-\n]+)$/g;
    var re1 = /^([0-9-\n]+)/g;
    if (re.test(val)) {
    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            field.value = "";
        }
    }
}


function NumWithFourDecimals(e, field) {
    var val = field.value;
    var re = /^((\d+)(\.\d{1,4})|(\.\d{1,4}))$/g;
    var re1 = /^((\d+)(\.\d{1,4})|(\.\d{1,4}))/g;

    if (re.test(val)) {
        //do something here

    } else {
        val = re1.exec(val);
        if (val) {
            field.value = val[0];
        } else {
            // field.value = "";
            if (isNaN(field.value) && field.value != '.') {
                field.value = "";
            }
        }
    }
}