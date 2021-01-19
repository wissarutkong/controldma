$(document).ready(function () {

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


