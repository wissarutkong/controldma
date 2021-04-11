$(document).ready(function () {

    //$("#save_add_valva").on("click", function (e) {
    //    e.preventDefault();

    //    $(this).prev().click();

    //});


})

//Check type of dvtype 
function Modal_save() {

    if (document.getElementById("save_remark").value == '') {
        document.getElementById("save_remark").focus();
        document.getElementById("save_remark").style.borderColor = "red";

        swalAlert('กรุณาป้อนหมายเหตุ', 'warning')

        return;
    }

    var type = document.getElementById('typepopup').value;
    if (type == "manual") {
        if (document.getElementById("txtdvtypeid").value == "2") {
            Post_Bv_manual();
        } else if (document.getElementById("txtdvtypeid").value == "3") {
            Post_prvt_manual();
        }
        else if (document.getElementById("txtdvtypeid").value == "4") {
            Post_stepping_manual();
        }
        else if (document.getElementById("txtdvtypeid").value == "6") {
            Post_Afv_manual();
        }
    } else if (type == "auto") {
        if (document.getElementById("txtdvtypeid").value == "2")
            Post_AutoBv();
        else if (document.getElementById("txtdvtypeid").value == "4")
            Post_Autostepping();
        else
            Post_AutoAfv();
    } else if (type == "auto_prv") {
        Post_AutoPrvt();
    }

}

//Save manual prv
function Post_prvt_manual() {
    if (document.getElementById("txtSolenoid").value == "") {
        document.getElementById("txtSolenoid").value = "0";
    }
    var template = sessionStorage.getItem('cycle_counter')
    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        solenoid: document.getElementById("txtSolenoid").value,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/' + (template == '6' ? 'AddManualPrvt' : 'AddManualPrvt_Template'),
       JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            $("#aboutModal_save").modal("hide");
            swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
            generateHtml_prv(data.dmacode, $('#txtdvtypeid').val()).then(() => {

            })
        }).catch((error) => {
            swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
        })


}

//Save auto prv
function Post_AutoPrvt() {
    var template = sessionStorage.getItem('cycle_counter')
    var cmdbvhead = [];
    var cmdbvdetail = [];
    cmdprvtdetail = getDatabeforsave_AutoPrvt();
    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        cmdprvtdetail: cmdprvtdetail,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value,
        template: template
    })

    CallAPI('/service/api.aspx/' + (template == '6' ? 'AddAutoPrvt' : 'AddAutoPrvt_Template'),
   JSON.stringify({ mainDataText: JSON.stringify(mainData) })
    ).then((data) => {
        $("#aboutModal_save").modal("hide");
        swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
        generateHtml_prv(data.dmacode, $('#txtdvtypeid').val()).then(() => {

        })
    }).catch((error) => {
        swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
    })
}

function getDatabeforsave_AutoPrvt() {
    var allRow = document.getElementById("txtRow").value
    var cmdprvtdetail = [];
    for (var i = 1; i <= allRow; i++) {

        cmdprvtdetail[i - 1] = {
            "order_time": i, "failure_mode": document.getElementById("selmode" + i).value,
            "time_start": document.getElementById("txttime" + i).value, "time_end": document.getElementById("txttime" + i).value,
            "pilot_no": document.getElementById("pilot_" + i).value.substring(1, 2)
        };
    }

    return cmdprvtdetail;
}

//Save manual bv
function Post_Bv_manual() {

    if (document.getElementById("txtvalve").value > 100) {
        swalAlert('ค่า valve(%) เกิน 100', 'warning')
        return;
    }

    var template = sessionStorage.getItem('cycle_counter')
    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        valve: document.getElementById("txtvalve").value,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/' + (template == '6' ? 'AddManualBv' : 'AddManualBv_Template'),
       JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            $("#aboutModal_save").modal("hide");
            swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
            generateHtml_bv(data.dmacode, $('#txtdvtypeid').val()).then(() => {

            })
        }).catch((error) => {
            swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
        })
}

//Save auto bv
function Post_AutoBv() {
    var template = sessionStorage.getItem('cycle_counter')
    var cmdbvhead = [];
    var cmdbvdetail = [];
    cmdbvdetail = getDatabeforsave_AutoBv();
    if (template == 6) {
        cmdbvhead[0] = {
            "failure_mode": document.getElementById("failure_mode").value,
            "step_control_delay": document.getElementById("step_control_delay").value,
            "time_loop": document.getElementById("time_loop").value,
            "limit_min": document.getElementById("limit_min").value,
            "deadband_pressure": document.getElementById("deadband_pressure").value,
            "deadband_flow": document.getElementById("deadband_flow").value
        };
    } else {
        cmdbvhead[0] = {
            //"failure_mode": document.getElementById("failure_mode").value,
            "step_control_delay": document.getElementById("step_control_delay").value,
            "time_loop": document.getElementById("time_loop").value,
            "limit_min": document.getElementById("limit_min").value,
            "deadband_pressure": document.getElementById("deadband_pressure").value,
            "deadband_flow": document.getElementById("deadband_flow").value,
            "template": sessionStorage.getItem('cycle_counter')
        };
    }


    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        cmdbvhead: cmdbvhead,
        cmdbvdetail: cmdbvdetail,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/' + (template == '6' ? 'AddAutoBv' : 'AddAutoBv_Template'),
   JSON.stringify({ mainDataText: JSON.stringify(mainData) })
    ).then((data) => {
        $("#aboutModal_save").modal("hide");
        swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
        generateHtml_bv(data.dmacode, $('#txtdvtypeid').val(), sessionStorage.getItem('cycle_counter')).then(() => { })
    }).catch((error) => {
        swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
    })
}

//====================Detail=========================================================

function getDatabeforsave_AutoBv() {
    var allRow = document.getElementById("txtRow").value
    var cmdbvdetail = [];
    for (var i = 1; i <= allRow; i++) {
        var selmode = document.getElementById("selmode" + i).value;
        var txttime = document.getElementById("txttime" + i).value;
        var txtPressure = document.getElementById("txtPressure" + i).value;
        var txtFlow = document.getElementById("txtFlow" + i).value;
        var txtValve = document.getElementById("txtValve" + i).value;
        cmdbvdetail[i - 1] = {
            "order_time": i, "failure_mode": document.getElementById("selmode" + i).value,
            "time_start": document.getElementById("txttime" + i).value, "time_end": document.getElementById("txttime" + i).value,
            "pressure_value": document.getElementById("txtPressure" + i).value, "flow_value": document.getElementById("txtFlow" + i).value,
            "valve_value": document.getElementById("txtValve" + i).value
        };
    }
    return cmdbvdetail;
}


function getDatabeforsave_AutoStepping() {
    var allRow = document.getElementById("txtRow").value
    var cmdbvdetail = [];
    for (var i = 1; i <= allRow; i++) {
        var selmode = document.getElementById("selmode" + i).value;
        var txttime = document.getElementById("txttime" + i).value;
        var txtPressure = document.getElementById("txtPressure" + i).value;
        var txtFlow = document.getElementById("txtFlow" + i).value;
        var txtValve = "";
        cmdbvdetail[i - 1] = {
            "order_time": i, "failure_mode": document.getElementById("selmode" + i).value,
            "time_start": document.getElementById("txttime" + i).value, "time_end": document.getElementById("txttime" + i).value,
            "pressure_value": document.getElementById("txtPressure" + i).value, "flow_value": document.getElementById("txtFlow" + i).value,
            "valve_value": ""
        };
    }
    return cmdbvdetail;
}

function getDatabeforsave_AutoAfv() {
    var allRow = document.getElementById("txtRow").value
    var cmdafvdetail = [];
    for (var i = 1; i <= allRow; i++) {
        cmdafvdetail[i - 1] = {
            "order_time": i,
            "date_worker": document.getElementById("txtdate" + i).value,
            "time_start": document.getElementById("txttime" + i).value,
            "time_end": document.getElementById("txttime" + i).value,
            "txtMode": document.getElementById("txtMode" + i).value,
            "time_worker": document.getElementById("txttimer" + i).value,
            "flow_worker": document.getElementById("txtFlow" + i).value
        };
    }
    return cmdafvdetail;
}

//====================Detail=========================================================

//====================Head=========================================================
function getDatabeforesave_cmdbvheadStepping(template) {
    var cmdbvhead = []
    if (template == 6) {
        cmdbvhead[0] = {
            "failure_mode": document.getElementById("failure_mode").value,
            "step_control_delay": document.getElementById("step_control_delay").value,
            "time_loop": document.getElementById("time_loop").value,
            "limit_min": document.getElementById("limit_min").value,
            "deadband_pressure": document.getElementById("deadband_pressure").value,
            "deadband_flow": document.getElementById("deadband_flow").value
        };
    } else {
        cmdbvhead[0] = {
            //"failure_mode": document.getElementById("failure_mode").value,
            "step_control_delay": document.getElementById("step_control_delay").value,
            "time_loop": document.getElementById("time_loop").value,
            "limit_min": document.getElementById("limit_min").value,
            "headlost": document.getElementById("headlost").value,
            "deadband_pressure": document.getElementById("deadband_pressure").value,
            "deadband_flow": document.getElementById("deadband_flow").value,
            "template": sessionStorage.getItem('cycle_counter')
        };
    }

    return cmdbvhead;
}

//====================Head=========================================================

//Save manual Stepping
function Post_stepping_manual() {

    var template = sessionStorage.getItem('cycle_counter')
    if (template == 6) {
        if (document.getElementById("txtvalve").value > 100) {
            swalAlert('ค่า valve(%) เกิน 100', 'warning')
            console.log("step6")
            return;
        }
    }

    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        valve: (template == 6 ? document.getElementById("txtvalve").value : $('#txtvalva_ct8').prop('checked')),
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/' + (template == '6' ? 'AddManualBv' : 'AddManualStepping_Template'),
       JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            $("#aboutModal_save").modal("hide");
            swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
            generateHtml_prvstepping(data.dmacode, $('#txtdvtypeid').val(), sessionStorage.getItem('cycle_counter')).then(() => { })
        }).catch((error) => {
            swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
        })
}

//Save auto Stepping
function Post_Autostepping() {
    var template = sessionStorage.getItem('cycle_counter')
    var cmdbvhead = [];
    var cmdbvdetail = [];
    cmdbvdetail = getDatabeforsave_AutoStepping();
    cmdbvhead = getDatabeforesave_cmdbvheadStepping(template);

    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        cmdbvhead: cmdbvhead,
        cmdbvdetail: cmdbvdetail,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/' + (template == '6' ? 'AddAutoBv' : 'AddAutoStepping_Template'),
   JSON.stringify({ mainDataText: JSON.stringify(mainData) })
    ).then((data) => {
        $("#aboutModal_save").modal("hide");
        swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
        generateHtml_prvstepping(data.dmacode, $('#txtdvtypeid').val(), sessionStorage.getItem('cycle_counter')).then(() => { })
    }).catch((error) => {
        swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
    })
}

//Save manual Afv
function Post_Afv_manual() {
    if ($('#txtvalva_afv').prop('checked')) {
        if ($('#txtafv_timeoutmin').val() == 0 || $('#txtafv_timeoutmin').val() == null) {
            swalAlert('กรุณากรอกเวลาปิด', 'warning')
            console.log($('#txtvalva_afv').prop('checked'))
            return;
        }
    }

    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        valve: $('#txtvalva_afv').prop('checked'),
        timeout_min: $('#txtafv_timeoutmin').val(),
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    console.log(mainData)

    CallAPI('/service/api.aspx/AddManualAfv_Template',
       JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            $("#aboutModal_save").modal("hide");
            swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
            generateHtml_Afv(data.dmacode, $('#txtdvtypeid').val(), sessionStorage.getItem('cycle_counter')).then(() => { })
        }).catch((error) => {
            swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
        })
}

//Save auto Afv
function Post_AutoAfv() {
    var template = sessionStorage.getItem('cycle_counter')
    var cmdafvhead = [];
    var cmdafvdetail = [];
    cmdafvdetail = getDatabeforsave_AutoAfv();

    cmdafvhead[0] = {
        "pipe_size": document.getElementById("pipe_size").value,
        "time_out_min_afv": document.getElementById("time_out_min_afv").value,
        "template": sessionStorage.getItem('cycle_counter')
    };

    let mainData = []
    mainData.push({
        wwcode: sessionStorage.getItem('cachewwcode'),
        dmacode: sessionStorage.getItem('cachedmacode'),
        remote_name: sessionStorage.getItem('cacheremotename'),
        cmdafvhead: cmdafvhead,
        cmdafvdetail: cmdafvdetail,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    console.log(mainData)

    CallAPI('/service/api.aspx/AddAutoAfv_Template',
    JSON.stringify({ mainDataText: JSON.stringify(mainData) })
     ).then((data) => {
         $("#aboutModal_save").modal("hide");
         swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
         generateHtml_Afv(data.dmacode, $('#txtdvtypeid').val(), sessionStorage.getItem('cycle_counter')).then(() => { })
     }).catch((error) => {
         swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
     })
}


$(document).on("click", "#_Automatic_PRV input[type='checkbox']", function () {
    var $box = $(this);
    document.getElementById($box.attr("id").substring(0, 7)).value = "";
    if ($box.is(":checked")) {

        document.getElementById($box.attr("id").substring(0, 7)).value = $box.attr("value");
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
})

// ทำการ delete แถว prv
function delRow(r) {
    if (parseInt(document.getElementById("txtRow").value) > 1) {
        document.getElementById("tblPrvAutomatic").deleteRow(parseInt(document.getElementById("txtRow").value));
        document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) - 1;
        document.getElementById("btnAdd_prv").disabled = false;
    }
}


//ทำการ ADD แถวใหม่่ prv
function insertRow(r) {
    if (document.getElementById("txtRow").value == sessionStorage.getItem('cycle_counter')) {
        document.getElementById("btnAdd_prv").disabled = true;
        //document.getElementById("btnAdd").style.backgroundColor = "red";
        return;
    }

    var searchStr = "btnAdd";
    var replaceStr = "";
    var re = new RegExp(searchStr, "g");
    var result = document.getElementById("txtRow").value;

    document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) + 1;
    var allRow = document.getElementById("txtRow").value;
    r = parseInt(result) + 1;


    var row = document.getElementById("tblPrvAutomatic").insertRow(r);

    var c1 = row.insertCell(0);
    var c2 = row.insertCell(1);
    var c3 = row.insertCell(2);

    c1.innerHTML = '<div  id = "txtRef' + r + '">' + r + '</div>';
    c2.innerHTML = '<select id = "selmode' + r + '" class="form-control" onchange="ChangeMode(this.id);"><option value="4">Eanble</option><option value="0" disabled>Disable</option></select>';
    c3.innerHTML = '<select id="txttime' + r + '"  name="txttime' + r + '"  class="form-control"><option value="00:00">00:00</option><option value="00:30">00:30</option><option value="01:00">01:00</option><option value="01:30">01:30</option><option value="02:00">02:00</option><option value="02:30">02:30</option><option value="03:00">03:00</option><option value="03:30">03:30</option><option value="04:00">04:00</option><option value="04:30">04:30</option><option value="05:00">05:00</option><option value="05:30">05:30</option><option value="06:00">06:00</option><option value="06:30">06:30</option><option value="07:00">07:00</option><option value="07:30">07:30</option><option value="08:00">08:00</option><option value="08:30">08:30</option><option value="09:00">09:00</option><option value="09:30">09:30</option><option value="10:00">10:00</option><option value="10:30">10:30</option><option value="11:00">11:00</option><option value="11:30">11:30</option><option value="12:00">12:00</option><option value="12:30">12:30</option><option value="13:00">13:00</option><option value="13:30">13:30</option><option value="14:00">14:00</option><option value="14:30">14:30</option><option value="15:00">15:00</option><option value="15:30">15:30</option><option value="16:00">16:00</option><option value="16:30">16:30</option><option value="17:00">17:00</option><option value="17:30">17:30</option><option value="18:00">18:00</option><option value="18:30">18:30</option><option value="19:00">19:00</option><option value="19:30">19:30</option><option value="20:00">20:00</option><option value="20:30">20:30</option><option value="21:00">21:00</option><option value="21:30">21:30</option><option value="22:00">22:00</option><option value="22:30">22:30</option><option value="23:00">23:00</option><option value="23:30">23:30</option></select>';

    for (k = 1; k <= document.getElementById("txtpilot_num").value; k++) {

        var id = "pilot_" + (parseInt(result) + 1) + k;

        var pilotid = "pilot_" + (parseInt(result) + 1);

        var pilot_hidden = "";
        if (k == 1) {
            // pilot_hidden = "<input type='text' id=" + pilotid + " />";
        }
        row.insertCell(2 + parseInt(k)).innerHTML = " <input   id=" + id + " name=" + pilotid + " class='form-control' type='checkbox' value=" + (parseInt(result) + 1) + k + " />" + pilot_hidden;


    }

}

// ทำการ delete แถว bv
function delRow_bv(r) {
    if (parseInt(document.getElementById("txtRow").value) > 1) {
        if ($('#txtdvtypeid').val() != 4) { document.getElementById("tblBvAutomatic").deleteRow(parseInt(document.getElementById("txtRow").value)); }
        else { document.getElementById("tblSteppingAutomatic").deleteRow(parseInt(document.getElementById("txtRow").value)); }


        document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) - 1;
        document.getElementById("btnAdd_bv").disabled = false;
    }


}
//ทำการ ADD แถวใหม่่ bv
function insertRow_bv(r) {
    if (document.getElementById("txtRow").value == sessionStorage.getItem('cycle_counter')) {
        document.getElementById("btnAdd_bv").disabled = true;
        //document.getElementById("btnAdd").style.backgroundColor = "red";
        return;
    }
    if ($('#txtdvtypeid').val() != 4) {

        var searchStr = "btnAdd";
        var replaceStr = "";
        var re = new RegExp(searchStr, "g");
        var result = document.getElementById("txtRow").value;

        document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) + 1;
        var allRow = document.getElementById("txtRow").value;
        r = parseInt(result) + 1;

        var row = document.getElementById("tblBvAutomatic").insertRow(r);

        var c1 = row.insertCell(0);
        var c2 = row.insertCell(1);
        var c3 = row.insertCell(2);
        var c4 = row.insertCell(3);
        var c5 = row.insertCell(4);
        var c6 = row.insertCell(5);

        c1.innerHTML = '<div style="text-align: center;"  id = "txtRef' + r + '">' + r + '</div>';
        c2.innerHTML = '<select id = "selmode' + r + '" class="form-control" onchange="ChangeMode(this.id);"><option value="1">Pressure</option><option value="2">Flow</option><option value="3">Valve</option></select>';
        c3.innerHTML = '<select id="txttime' + r + '"  name="txttime' + r + '"  class="form-control"><option value="00:00">00:00</option><option value="00:30">00:30</option><option value="01:00">01:00</option><option value="01:30">01:30</option><option value="02:00">02:00</option><option value="02:30">02:30</option><option value="03:00">03:00</option><option value="03:30">03:30</option><option value="04:00">04:00</option><option value="04:30">04:30</option><option value="05:00">05:00</option><option value="05:30">05:30</option><option value="06:00">06:00</option><option value="06:30">06:30</option><option value="07:00">07:00</option><option value="07:30">07:30</option><option value="08:00">08:00</option><option value="08:30">08:30</option><option value="09:00">09:00</option><option value="09:30">09:30</option><option value="10:00">10:00</option><option value="10:30">10:30</option><option value="11:00">11:00</option><option value="11:30">11:30</option><option value="12:00">12:00</option><option value="12:30">12:30</option><option value="13:00">13:00</option><option value="13:30">13:30</option><option value="14:00">14:00</option><option value="14:30">14:30</option><option value="15:00">15:00</option><option value="15:30">15:30</option><option value="16:00">16:00</option><option value="16:30">16:30</option><option value="17:00">17:00</option><option value="17:30">17:30</option><option value="18:00">18:00</option><option value="18:30">18:30</option><option value="19:00">19:00</option><option value="19:30">19:30</option><option value="20:00">20:00</option><option value="20:30">20:30</option><option value="21:00">21:00</option><option value="21:30">21:30</option><option value="22:00">22:00</option><option value="22:30">22:30</option><option value="23:00">23:00</option><option value="23:30">23:30</option></select>';

        c4.innerHTML = '<input type="text" onkeypress="return isNumberKey(event)"  class="form-control"  id="txtPressure' + r + '" style="width:90%" maxlength="8" value="" onkeypress="EnterEvent(event,7777' + r + ')">';
        c5.innerHTML = '<input type="text"  onkeypress="return isNumberKey(event)" class="form-control"  id="txtFlow' + r + '" style="width:90%" maxlength="8" onkeypress="EnterEvent(event,8888' + r + ')">';
        c6.innerHTML = '<input type="text"  onkeypress="return isNumberKey(event)" class="form-control"  id="txtValve' + r + '" style="width:90%" maxlength="8" onblur="fnMeasureCheck(this);" onkeypress="EnterEvent(event,9999' + r + ')">'

        ChangeMode("selmode" + r);
    } else {
        var searchStr = "btnAdd";
        var replaceStr = "";
        var re = new RegExp(searchStr, "g");
        var result = document.getElementById("txtRow").value;

        document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) + 1;
        var allRow = document.getElementById("txtRow").value;
        r = parseInt(result) + 1;

        var row = document.getElementById("tblSteppingAutomatic").insertRow(r);

        var c1 = row.insertCell(0);
        var c2 = row.insertCell(1);
        var c3 = row.insertCell(2);
        var c4 = row.insertCell(3);
        var c5 = row.insertCell(4);
        //var c6 = row.insertCell(5);

        c1.innerHTML = '<div style="text-align: center;"  id = "txtRef' + r + '">' + r + '</div>';
        c2.innerHTML = '<select id = "selmode' + r + '" class="form-control" onchange="ChangeMode(this.id);"><option value="1">Pressure</option><option value="2">Flow</option></select>';
        c3.innerHTML = '<select id="txttime' + r + '"  name="txttime' + r + '"  class="form-control"><option value="00:00">00:00</option><option value="00:30">00:30</option><option value="01:00">01:00</option><option value="01:30">01:30</option><option value="02:00">02:00</option><option value="02:30">02:30</option><option value="03:00">03:00</option><option value="03:30">03:30</option><option value="04:00">04:00</option><option value="04:30">04:30</option><option value="05:00">05:00</option><option value="05:30">05:30</option><option value="06:00">06:00</option><option value="06:30">06:30</option><option value="07:00">07:00</option><option value="07:30">07:30</option><option value="08:00">08:00</option><option value="08:30">08:30</option><option value="09:00">09:00</option><option value="09:30">09:30</option><option value="10:00">10:00</option><option value="10:30">10:30</option><option value="11:00">11:00</option><option value="11:30">11:30</option><option value="12:00">12:00</option><option value="12:30">12:30</option><option value="13:00">13:00</option><option value="13:30">13:30</option><option value="14:00">14:00</option><option value="14:30">14:30</option><option value="15:00">15:00</option><option value="15:30">15:30</option><option value="16:00">16:00</option><option value="16:30">16:30</option><option value="17:00">17:00</option><option value="17:30">17:30</option><option value="18:00">18:00</option><option value="18:30">18:30</option><option value="19:00">19:00</option><option value="19:30">19:30</option><option value="20:00">20:00</option><option value="20:30">20:30</option><option value="21:00">21:00</option><option value="21:30">21:30</option><option value="22:00">22:00</option><option value="22:30">22:30</option><option value="23:00">23:00</option><option value="23:30">23:30</option></select>';

        c4.innerHTML = '<input type="text" onkeypress="return isNumberKey(event)"  class="form-control"  id="txtPressure' + r + '" style="width:90%" maxlength="8" value="" onkeypress="EnterEvent(event,7777' + r + ')">';
        c5.innerHTML = '<input type="text"  onkeypress="return isNumberKey(event)" class="form-control"  id="txtFlow' + r + '" style="width:90%" maxlength="8" onkeypress="EnterEvent(event,8888' + r + ')">';
        //c6.innerHTML = '<input type="text"  onkeypress="return isNumberKey(event)" class="form-control"  id="txtValve' + r + '" style="width:90%" maxlength="8" onblur="fnMeasureCheck(this);" onkeypress="EnterEvent(event,9999' + r + ')">'

        ChangeMode("selmode" + r);
    }
}

// ทำการ delete แถว afv
function delRow_afv(r) {
    if (parseInt(document.getElementById("txtRow").value) > 1) {
        document.getElementById("tblAfvAutomatic").deleteRow(parseInt(document.getElementById("txtRow").value));
        document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) - 1;
        document.getElementById("btnAdd_afv").disabled = false;
    }
}

//ทำการ ADD แถวใหม่่ afv
function insertRow_afv(r) {
    if (document.getElementById("txtRow").value == sessionStorage.getItem('cycle_counter')) {
        document.getElementById("btnAdd_afv").disabled = true;
        //document.getElementById("btnAdd").style.backgroundColor = "red";
        return;
    }

    var searchStr = "btnAdd";
    var replaceStr = "";
    var re = new RegExp(searchStr, "g");
    var result = document.getElementById("txtRow").value;

    document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) + 1;
    var allRow = document.getElementById("txtRow").value;
    r = parseInt(result) + 1;

    var row = document.getElementById("tblAfvAutomatic").insertRow(r);

    var c1 = row.insertCell(0);
    var c2 = row.insertCell(1);
    var c3 = row.insertCell(2);
    var c4 = row.insertCell(3);
    var c5 = row.insertCell(4);
    var c6 = row.insertCell(5);

    c1.innerHTML = '<div style="text-align: center;"  id = "txtRef' + r + '">' + r + '</div>';
    c2.innerHTML = '<select id="txtdate' + r + '"  name="txtdate' + r + '"  class="form-control">' + localStorage.getItem("DateOpt") + '</select>';
    c3.innerHTML = '<select id="txttime' + r + '"  name="txttime' + r + '"  class="form-control">' + localStorage.getItem("timeOpt") + '</select>';
    c4.innerHTML = '<select id = "txtMode' + r + '" class="form-control" onchange="ChangeMode(this.id);"><option value="1" selected>Timer</option><option value="2">Flow</option></select>';
    c5.innerHTML = '<input type="text" onkeypress="return isNumberKey(event)"  class="form-control"  id="txttimer' + r + '" style="width:90%" maxlength="8" value="" >';
    c6.innerHTML = '<input type="text"  onkeypress="return isNumberKey(event)" class="form-control"  id="txtFlow' + r + '" style="width:90%" maxlength="8" >';

    ChangeMode("txtMode" + r);
}

//จัดเรียงแถวใหม่ก่อนทำการ insert แถว
function sortNewBeforeAdd(r) {
    var allRow = document.getElementById("txtRow").value

    for (i = allRow - 1; i >= r; i--) {
        var txtRef = document.getElementById("txtRef" + i);
        var selmode = document.getElementById("selmode" + i);
        var txttime = document.getElementById("txttime" + i);
        var txtPressure = document.getElementById("txtPressure" + i);
        var txtFlow = document.getElementById("txtFlow" + i);
        var txtValve = document.getElementById("txtValve" + i);

        txtRef.id = "txtRef" + (i + 1);
        selmode.id = "selmode" + (i + 1);
        txttime.id = "txttime" + (i + 1);
        txtPressure.id = "txtPressure" + (i + 1);
        txtFlow.id = "txtFlow" + (i + 1);
        txtValve.id = "txtValve" + (i + 1);

    }
}

function ChangeMode(id) {
    if ($('#txtdvtypeid').val() == 2) {
        var i = id.replace("selmode", "");
        var txtPressure = document.getElementById("txtPressure" + i);
        var txtFlow = document.getElementById("txtFlow" + i);
        var txtValve = document.getElementById("txtValve" + i);
        txtPressure.style.backgroundColor = "#FFFFFF";
        txtFlow.style.backgroundColor = "#FFFFFF";
        txtValve.style.backgroundColor = "#FFFFFF";
        switch (document.getElementById(id).value) {
            case "0":
                txtPressure.style.backgroundColor = "#CCCCCC";
                txtFlow.style.backgroundColor = "#CCCCCC";
                txtValve.style.backgroundColor = "#CCCCCC";
                break;
            case "1":
                txtFlow.style.backgroundColor = "#CCCCCC";
                txtValve.style.backgroundColor = "#CCCCCC";
                //txtPressure.disabled = false;
                txtPressure.focus();
                break;
            case "2":
                //txtFlow.disabled = false;
                txtPressure.style.backgroundColor = "#CCCCCC";
                txtValve.style.backgroundColor = "#CCCCCC";
                txtFlow.focus();
                break;
            case "3":
                txtPressure.style.backgroundColor = "#CCCCCC";
                txtFlow.style.backgroundColor = "#CCCCCC";
                //txtValve.disabled = false;
                txtValve.focus();
                break;
        }
    }
    else if ($('#txtdvtypeid').val() == 4) {
        var i = id.replace("selmode", "");
        var txtPressure = document.getElementById("txtPressure" + i);
        var txtFlow = document.getElementById("txtFlow" + i);
        txtPressure.style.backgroundColor = "#FFFFFF";
        txtFlow.style.backgroundColor = "#FFFFFF";
        switch (document.getElementById(id).value) {
            case "0":
                txtPressure.style.backgroundColor = "#CCCCCC";
                txtFlow.style.backgroundColor = "#CCCCCC";
                break;
            case "1":
                txtFlow.style.backgroundColor = "#CCCCCC";
                txtPressure.focus();
                break;
            case "2":
                txtPressure.style.backgroundColor = "#CCCCCC";
                txtFlow.focus();
                break;
        }
    }
    else if ($('#txtdvtypeid').val() == 6) {
        var i = id.replace("txtMode", "");
        console.log(i)
        var txttimer = document.getElementById("txttimer" + i);
        var txtFlow = document.getElementById("txtFlow" + i);
        txttimer.style.backgroundColor = "#FFFFFF";
        txtFlow.style.backgroundColor = "#FFFFFF";
        switch (document.getElementById(id).value) {
            case "1":
                txtFlow.style.backgroundColor = "#CCCCCC";
                txttimer.focus();
                break;
            case "2":
                txttimer.style.backgroundColor = "#CCCCCC";
                txtFlow.focus();
                break;
        }
    }
    else {
        return;
    }
}


