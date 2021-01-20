﻿$(document).ready(function () {

    //$("#save_add_valva").on("click", function (e) {
    //    e.preventDefault();

    //    $(this).prev().click();

    //});


})


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
            //Post_Bv_manual();
        } else if (document.getElementById("txtdvtypeid").value == "3") {
            Post_prvt_manual();
        }
    } else if (type == "auto") {
        //Post_AutoBv();
    } else if (type == "auto_prv") {
        Post_AutoPrvt();
    }

}


function Post_prvt_manual() {
    if (document.getElementById("txtSolenoid").value == "") {
        document.getElementById("txtSolenoid").value = "0";
    }

    let mainData = []
    mainData.push({
        solenoid: document.getElementById("txtSolenoid").value,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/AddManualPrvt',
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

function Post_AutoPrvt() {
    var cmdbvhead = [];
    var cmdbvdetail = [];
    cmdprvtdetail = getDatabeforsave_AutoPrvt();
    let mainData = []
    mainData.push({
        cmdprvtdetail: cmdprvtdetail,
        dvtypeid: document.getElementById("txtdvtypeid").value,
        remark: document.getElementById("save_remark").value
    })

    CallAPI('/service/api.aspx/AddAutoPrvt',
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

$(document).on("click", "#_Automatic_PRV input[type='checkbox']", function () {
    var $box = $(this);
    console.log($box)
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

// ทำการ delete แถว
function delRow(r) {
    if (parseInt(document.getElementById("txtRow").value) > 1) {
        document.getElementById("tblBvAutomatic").deleteRow(parseInt(document.getElementById("txtRow").value));
        document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) - 1;
        document.getElementById("btnAdd").disabled = false;
    }


}


//ทำการ ADD แถวใหม่่
function insertRow(r) {
    if (document.getElementById("txtRow").value == "6") {
        document.getElementById("btnAdd").disabled = true;
        //document.getElementById("btnAdd").style.backgroundColor = "red";
        return;
    }

    var searchStr = "btnAdd";
    var replaceStr = "";
    var re = new RegExp(searchStr, "g");
    var result = document.getElementById("txtRow").value;

    console.log(result)

    document.getElementById("txtRow").value = parseInt(document.getElementById("txtRow").value) + 1;
    var allRow = document.getElementById("txtRow").value;
    r = parseInt(result) + 1;

    console.log(allRow)
    console.log(r)

    var row = document.getElementById("tblBvAutomatic").insertRow(r);

    var c1 = row.insertCell(0);
    var c2 = row.insertCell(1);
    var c3 = row.insertCell(2);

    c1.innerHTML = '<div  id = "txtRef' + r + '">' + r + '</div>';
    c2.innerHTML = '<select id = "selmode' + r + '" class="form-control" onchange="ChangeMode(this.id);"><option value="4">Eanble</option><option value="0">Disable</option></select>';
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

function ChangeMode(id) { }


