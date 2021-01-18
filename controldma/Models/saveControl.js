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
        //Post_AutoPrvt();
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
            console.log(data)
            swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
        }).catch((error) => {
            swalAlert('บันทึกข้อมูลไม่สำเร็จ โปรดลองใหม่', 'error')
        })


    console.log(dataObject)
}
