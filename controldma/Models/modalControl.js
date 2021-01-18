$(document).ready(function () {

    $('#Modal_info_valva').on('hidden.bs.modal', function () {
        $('.modal_title_ml').attr('dmacode', '')
    });

    $('.Modal_edit').on('hidden.bs.modal', function () {
        // destroy
    });

    window.setInterval(function () {
        var dmacode = $('.modal_title_ml').attr('dmacode')
        getinfovalva(dmacode)
    }, 5000);


})

$(document).on('show.bs.modal', '.modal', function (event) {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex -1 ).addClass('modal-stack');
    }, 0);
});


$(document).on("click", ".infovalva", function () {
    var dmacode = $(this).val();
    getinfovalva(dmacode)
})

$(document).on("click", ".editvalva", function () {
    var datatype = $(this).attr("data-type")
    hidePage_content_modal()
    $('#txtdvtypeid').val(datatype)
    document.getElementById('typepopup').value = "";
    let element = document.getElementById('overlay_modal');
    //element.style.visibility = null;
    if ($(this).val() != '' && $(this).val() != null && getCookie('_wwcode') != '' && getCookie('_wwcode') != null) {
        var dmacode = $(this).val();
        let mainData = []
        mainData.push({
            $_dmacode: dmacode,
            $_remote_name: $(this).attr('data-remote'),
            $_wwcode: getCookie('_wwcode')
        })
        Setvariableapi(mainData);
        //element.style.display = null;
        if (datatype == 2) {
            $('#Modal_edit_bv').modal('show');
            $('.modal_title_setting').text("BV - ตั้งค่าควบคุมประตูน้ำจุดติดตั้ง : " + dmacode)
            element.style.visibility = "collapse";
        }
        else if (datatype == 3) {
            $('#Modal_edit_prv').modal('show');
            $('.modal_title_setting').text("PRV - ตั้งค่าควบคุมประตูน้ำจุดติดตั้ง : " + dmacode)
            generateHtml_prv(getCookie('_wwcode'), dmacode, datatype).then((data) => {
                $('#_Manual_PRV').html(data._manual)              
                $('#_Automatic_PRV').html(data._Automatic)
                $('#_Realtime_PRV').html(data._Realtime)
                $('#_History_PRV').html(data._History)
                GeneratePRV('tblBvAutomatic')
            }).then(() => {
                CallApigettable_modal('dt_grid_realtime', '_Realtime').then(() => {
                    CallApigettable_modal('dt_grid_history', '_History').then(() => {
                        showPage_content_modal();
                    })
                })                
            })
            //element.style.visibility = "collapse";
        }
        else {
            $('#Modal_warning').modal('show');
            $('.modal_title_setting').text("ไม่สามารถ Control จุดติดตั้ง : " + dmacode)
        }
        //element.parentNode.removeChild(element);
        //element.style.display = "none";
    }
})

$(document).on("click", ".addvalva", function () {
    if ($(this).val() != '' && $(this).val() != null && getCookie('_wwcode') != '' && getCookie('_wwcode') != null) {
        var dmacode = $(this).val();
        $('.modal_title_add_valva').text("กำหนดจุดติดตั้งประตูน้ำ : "+dmacode)
    }
})

function getinfovalva(dmacode) {
    var element = document.getElementById('overlay_wrapper');
    element.style.display = null;
    //element.style.display = "none";
    if (dmacode != '' && dmacode != null && getCookie('_wwcode') != '' && getCookie('_wwcode') != null) {
        var dmacode = dmacode;
        $('.modal_title_ml').text(dmacode)
        $('.modal_title_ml').attr('dmacode', dmacode)
        let mainData = []
        mainData.push({
            $_wwcode: getCookie('_wwcode'),
            $_dmacode: dmacode
        })

        CallAPI('/service/api.aspx/GetCtr002_All',
                   JSON.stringify({ mainDataText: JSON.stringify(mainData) })
               ).then((data) => {
                   let obj = data[0];
                   //console.log(obj)
                   //console.log(obj.communication)
                   $('#valve_info_dma').text(obj.dmacode)
                   $('#valve_info_remotename').text(obj.remote_name)
                   $('#valve_info_devicetype').text(obj.dvtype_name)
                   $('.badgeinfo').removeClass("bg-success");
                   $('.badgeinfo').removeClass("bg-danger");
                   //$('#valve_info_door').text(obj.doorStatus == 1 ? 'เปิด' : 'ปิด')
                   if (obj.doorStatus != 1) { $('#valve_info_door').text('ปิด'); $('#valve_info_door').addClass("bg-danger"); }
                   else { $('#valve_info_door').text('เปิด'); $('#valve_info_door').addClass("bg-success"); }
                   //$('#valve_info_com').text(obj.communication == 1 ? 'ติดต่อได้' : 'ติดต่อไม่ได้')
                   if (obj.communication != 1) { $('#valve_info_com').text('ติดต่อไม่ได้'); $('#valve_info_com').addClass("bg-danger"); }
                   else { $('#valve_info_com').text('ติดต่อได้'); $('#valve_info_com').addClass("bg-success"); }
                   //$('#valve_info_power').text(obj.PowerLineStatus == 0 ? 'มีไฟ' : 'ไม่มีไฟ')
                   if (obj.PowerLineStatus != 0) { $('#valve_info_power').text('ไม่มีไฟ'); $('#valve_info_power').addClass("bg-danger"); }
                   else { $('#valve_info_power').text('มีไฟ'); $('#valve_info_power').addClass("bg-success"); }
                   $('#valve_info_switch').text(obj.auma_sw_status == 0 ? 'off' : obj.auma_sw_status == 1 ? 'local' : 'remote')
                   //$('#valve_info_runmode').text(obj.runing_mode_value != 4 && obj.runing_mode_value != 5 ? 'Auto' : 'Manual')
                   if (obj.runing_mode_value != 4 && obj.runing_mode_value != 5) { $('#valve_info_runmode').text('Auto'); $('#valve_info_runmode').addClass("bg-success"); }
                   else { $('#valve_info_runmode').text('Manual'); $('#valve_info_runmode').addClass("bg-danger"); }
                   $('#valve_info_status').text(obj.StatusName != null ? obj.StatusName : '')
                   $('#valve_info_lastupdate').text(obj.LastUpdate != null ? obj.LastUpdate.replace('T', ' ') : 'ไม่มีการอัพเดต')
                   element.style.display = "none";
               }).catch((error) => {
                   swalAlert('เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ', 'error')
                   //SomethingWrong('เกิดความผิด !', 'เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ')
               })
    }
}

function generateHtml_prv(_wwcode, dmacode, datatype) {
    return new Promise((resolve, reject) => {
        let mainData = []
        mainData.push({
            $_wwcode: _wwcode,
            $_dmacode: dmacode,
            $_datatype: datatype
        })
        CallAPI('/service/api.aspx/' + (datatype == 3 ? 'Getdata_prv' : ''),
                JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            //$('#_Manual_PRV').html(data.html)
            resolve(data)
        }).catch((error) => {
            //console.log(error)
            reject()
        })
    })
}



function Popup(id, type) {
    GetCommandTimeOut()
    document.getElementById("save_remark").style.borderColor = "";
    document.getElementById("save_remark").value = "";
    document.getElementById("save_remark").focus();
    if (type == "manual") {
        if (id == 1) {
            $("#aboutModal").modal("show");
        } else {
            $("#aboutModal_save").modal("show");
        }
        document.getElementById('typepopup').value = "manual";
    }
    else if (type == "auto") {
        if (id == 0) {
            $("#aboutModal_save").modal("show");
            document.getElementById('typepopup').value = "auto";
        } else if (id == 1) {
            $("#aboutModal_save").modal("show");
            document.getElementById('typepopup').value = "auto_prv";
        }
    }

}

var seconds = 0
function GetCommandTimeOut() {
    var isdata = false;
    CallAPI('/service/api.aspx/GetCommandTimeOut',
            ''
    ).then((data) => {
        //console.log(data)

        $.each(data, function (i, item) {
            document.getElementById("counter_s").style.display = "block";
            document.getElementById("button_save").style.display = "none";
            seconds = data[i].Value;
            document.getElementById("counter_s").value = seconds;

            if (isdisplay == false) {
                isdisplay = true;
                display();
            }

            isdata = true;
        });

        if (isdata == false) {

            seconds = 0;
            isdisplay == false;
            document.getElementById("counter_s").style.display = "none";
            document.getElementById("button_save").style.display = "block";
        }

    }).catch((error) => {
        //console.log(error)
        reject()
    })
}

var isdisplay = false;
function display() { //function ใช้ในการ นับถอยหลัง

    seconds -= 1;//ลบเวลาทีละหนึ่งวินาทีทุกครั้งที่ function ทำงาน

    if (seconds == -1) {
        document.getElementById("counter_s").style.display = "none";
        document.getElementById("button_save").style.display = "block";
        return;
    } //เมื่อหมดเวลาแล้วจะหยุดการทำงานของ function display

    document.getElementById("counter_s").innerHTML = 'กรุณารอสักครู่...<b style="color:red"> ' + seconds + '</b> วินาที';
    //document.getElementById("counter_s").value = seconds; //แสดงเวลาที่เหลือ
    setTimeout("display()", 1000);// สั่งให้ function display() ทำงาน หลังเวลาผ่านไป 1000 milliseconds ( 1000  milliseconds = 1 วินาที )
}