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
    }, 10000);

    //$('#m_smartlogger').change(function () {
    //    console.log($(this).prop('checked'))
    //})

})

function onchangeddldvtype(value) {
    if (value == 3) { $('#_divtotlepilot').show(); $('#div_pilot_num').show(); }
    else {
        $('#_divtotlepilot').hide(); $('#div_pilot_num').hide();
        $('#pilot_pressure1').val('')
        $('#pilot_pressure2').val('')
        $('#pilot_pressure3').val('')
        $('#pilot_pressure4').val('')
    }
}

function onChangepilotnum(value) {
    var x = value
    switch (x) {
        case '1':
            $('#pilot_pressure1').prop("disabled", false)
            $('#pilot_pressure2').prop("disabled", true)
            $('#pilot_pressure3').prop("disabled", true)
            $('#pilot_pressure4').prop("disabled", true)
            break;
        case '2':
            $('#pilot_pressure1').prop("disabled", false)
            $('#pilot_pressure2').prop("disabled", false)
            $('#pilot_pressure3').prop("disabled", true)
            $('#pilot_pressure4').prop("disabled", true)
            break;
        case '3':
            $('#pilot_pressure1').prop("disabled", false)
            $('#pilot_pressure2').prop("disabled", false)
            $('#pilot_pressure3').prop("disabled", false)
            $('#pilot_pressure4').prop("disabled", true)
            break;
        case '4':
            $('#pilot_pressure1').prop("disabled", false)
            $('#pilot_pressure2').prop("disabled", false)
            $('#pilot_pressure3').prop("disabled", false)
            $('#pilot_pressure4').prop("disabled", false)
            break;
    }
}

$(document).on('show.bs.modal', '.modal', function (event) {
    var zIndex = 1040 + (10 * $('.modal:visible').length);
    $(this).css('z-index', zIndex);
    setTimeout(function () {
        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
    }, 0);
});

$(document).on("click", "#realtime_prvrefresh", function () {
    $(this).prop("disabled", true);
    $(this).html('<i class="spinner-border spinner-border-sm"></i> Loading...');
    $('dt_grid_realtime').DataTable().clear();
    CallApigettable_modal('dt_grid_realtime', '_Realtime').then(() => {
        $(this).html('<i class="fas fa-redo"></i> refresh');
        $(this).prop("disabled", false);
    })
})


$(document).on("click", "#realtime_bvrefresh", function () {
    $(this).prop("disabled", true);
    $(this).html('<i class="spinner-border spinner-border-sm"></i> Loading...');
    $('dt_grid_realtime_bv').DataTable().clear();
    CallApigettable_modal('dt_grid_realtime_bv', '_Realtime').then(() => {
        $(this).html('<i class="fas fa-redo"></i> refresh');
        $(this).prop("disabled", false);
    })
})

$(document).on("click", "#realtime_afvrefresh", function () {
    $(this).prop("disabled", true);
    $(this).html('<i class="spinner-border spinner-border-sm"></i> Loading...');
    $('dt_grid_realtime_afv').DataTable().clear();
    CallApigettable_modal('dt_grid_realtime_afv', '_Realtime').then(() => {
        $(this).html('<i class="fas fa-redo"></i> refresh');
        $(this).prop("disabled", false);
    })
})


$(document).on("click", ".infovalva", function () {
    var dmacode = $(this).val();
    var datatype = $(this).attr("data-type")
    var x = $('.afv_smartloggermqtt_info')
    var divmodeldialog = $('#Modal_info_valva').find('div.modal-dialog')
    if (datatype == 6) {
        divmodeldialog.removeClass("modal-lg").addClass("modal-xl");
        x.show();
        document.getElementById('afv_iframe_mqtt').src += 'http://smartlogger.pwa.co.th/afv/index.php?device=' + $(this).attr("data-remote");
    }
    else {
        x.hide();
        divmodeldialog.removeClass("modal-xl").addClass("modal-lg");
    }
    getinfovalva(dmacode)
})

$(document).on("click", ".commandinfo", function () {
    $('#Modal_commandinfo').modal('show');
    var lines = $(this).val().trim().split(',')
    var temp = ''
    jQuery.each(lines, function (k) {
        if (k != -1)
            temp += 'line[' + k + '] ===> ' + this + ',\n';
    });

    $('#command_cmd_desc').val(temp)
    $('#command_cmd_desc_real').val($(this).val().trim())
})

$(document).on("click", ".editvalva", function () {
    var datatype = $(this).attr("data-type")

    $('#txtdvtypeid').val(datatype)
    document.getElementById('typepopup').value = "";
    let element = document.getElementById('overlay_modal');
    //element.style.visibility = null;
    if ($(this).val() != '' && $(this).val() != null && getCookie('_wwcode') != '' && getCookie('_wwcode') != null) {
        var dmacode = $(this).val();
        var _templateCycle = $(this).attr('data-template')
        //let mainData = []
        //mainData.push({
        //    $_dmacode: dmacode,
        //    $_remote_name: $(this).attr('data-remote'),
        //    $_wwcode: getCookie('_wwcode')
        //})
        //Setvariableapi(mainData);
        SetSessionstorage_(getCookie('_wwcode'), dmacode, $(this).attr('data-remote'), datatype, _templateCycle)
        document.getElementById("btnAdd_bv").disabled = false;
        //element.style.display = null;
        if (datatype == 2) {
            $('#Modal_edit_bv').modal('show');
            generateHtml_bv(dmacode, $('#txtdvtypeid').val(), _templateCycle).then(() => {

            })
            //element.style.visibility = "collapse";
        }
        else if (datatype == 3) {
            $('#Modal_edit_prv').modal('show');
            generateHtml_prv(dmacode, $('#txtdvtypeid').val(), _templateCycle).then(() => {

            })
            //element.style.visibility = "collapse";
        }
        else if (datatype == 4) {
            $('#Modal_edit_bv').modal('show');
            generateHtml_prvstepping(dmacode, $('#txtdvtypeid').val(), _templateCycle).then(() => {

            })
        }
        else if (datatype == 6) {
            $('#Modal_edit_afv').modal('show');
            generateHtml_Afv(dmacode, $('#txtdvtypeid').val(), _templateCycle).then(() => {

            })
        }
        else {
            swalAlert('ไม่พบประเภทของจุดติดตั้ง : ' + dmacode, 'warning')
            //$('#Modal_warning').modal('show');
            //$('.modal_title_setting').text("ไม่สามารถ Control จุดติดตั้ง : " + dmacode)
        }
        //element.parentNode.removeChild(element);
        //element.style.display = "none";
    }
})

$(document).on("click", ".addvalva", function () {
    hidePage_content_modal()
    if ($(this).val() != '' && $(this).val() != null && getCookie('_wwcode') != '' && getCookie('_wwcode') != null) {
        $('#_divtotlepilot').hide();
        AjaxGetddl('m_dvtypeddl').then(() => {
            AjaxGetddl('m_controltype').then(() => {
                AjaxGetddl('m_Templatecontrol').then(() => {
                    var dmacode = $(this).val();
                    $('.modal_title_add_valva').text("กำหนดจุดติดตั้งประตูน้ำ : " + dmacode)
                    $('#Modal_add_valva').modal('show');
                    let mainData = []
                    mainData.push({
                        $_dmacode: dmacode,
                        $_wwcode: getCookie('_wwcode')
                    })
                    CallAPI('/service/api.aspx/GetCtr003_All',
                       JSON.stringify({ mainDataText: JSON.stringify(mainData) })
                   ).then((data) => {
                       let obj = data[0];
                       $('.modal_subtital_add_valva').text(obj.name)
                       $('#m_dvtypeddl').val(obj.dvtype_id).trigger('change');
                       $('#m_totlepilot').val(obj.pilot_num).trigger('change');
                       for (var i = 1; i <= obj.pilot_num; i++) {
                           var objpilotnum = obj.pilot_pressure + i;
                           $('#pilot_pressure' + i + '').val(obj['pilot_pressure' + i])
                           //console.log(obj['pilot_pressure' + i])
                       }
                       $('#m_controltype').val(obj.control_type).trigger('change');

                       $('#m_Templatecontrol').val(obj.cycle_id).trigger('change');

                       if (obj.is_smartlogger) { $('#m_smartlogger').attr('checked', true); }
                       else { $('#m_smartlogger').removeAttr('checked') }

                       $('#m_usereditor').text(obj.fullname == null ? 'ไม่มีการแก้ไข' : obj.fullname)
                       $('#m_lastupdate').text(obj.last_upd_dtm == null ? 'ไม่มีการแก้ไข' : obj.last_upd_dtm.replace('T', ' '))
                       SetSessionstorage_(getCookie('_wwcode'), dmacode, '', '')
                       showPage_content_modal();
                   }).catch((error) => {
                       ClearSessionstorage()
                       swalAlert('เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ', 'error')
                       //SomethingWrong('เกิดความผิด !', 'เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ')
                   })
                })
            })
        })

    }
})

var interval = null
$(document).on('show.bs.modal', '.Modal_edit ', function (event) {
    interval = setInterval(function () {
        //do somethings
        GetSyncRealtime()
    }, 5000)
})

$(document).on('hidden.bs.modal', '.Modal_edit ', function (event) {
    $('span[name="realtime_cmd_status"]').text("Wait...")
    window.clearInterval(interval)
})

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;

    return true;
}


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
                   $('#valve_match_time_no').text(obj.match_time_no)
                   $('#valve_match_time').text(obj.time_match)
                   $('#valva_p1').text(obj.pressure1)
                   $('#valva_p2').text(obj.Pressure)
                   $('#valva_flow').text(obj.Flow)
                   $('#valva_valva_percent').text(obj.Valve)
                   element.style.display = "none";
               }).catch((error) => {
                   swalAlert('เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ', 'error')
                   //SomethingWrong('เกิดความผิด !', 'เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ')
               })
    }
}

function getHtml(_wwcode, dmacode, datatype, _templateCycle) {
    return new Promise((resolve, reject) => {
        let mainData = []
        mainData.push({
            $_wwcode: _wwcode,
            $_dmacode: dmacode,
            $_datatype: datatype
        })
        CallAPI('/service/api.aspx/' + getJsontp(datatype, _templateCycle),
                JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            resolve(data)
        }).catch((error) => {
            //console.log(error)
            swalAlert('เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง', 'error')
            reject()
        })
    })
}

function getJsontp(dvtype_service, _templateCycle) {
    var clstp = '';
    if (_templateCycle == 6) {
        switch (dvtype_service) {
            case '2':
                clstp = 'Getdata_bv';
                break;
            case '3':
                clstp = 'Getdata_prv';
                break;
            case '4':
                clstp = 'Getdata_PrvStepping';
                break;
            case '5':
                clstp = '';
                break;
            case '6':
                clstp = 'Getdata_Afv';
                break;
            default:
                // code block
        }
    } else {
        switch (dvtype_service) {
            case '2':
                clstp = 'Getdata_bv_new';
                break;
            case '3':
                clstp = 'Getdata_prv';
                break;
            case '4':
                clstp = 'Getdata_PrvStepping_new';
                break;
            case '5':
                clstp = '';
                break;
            case '6':
                clstp = 'Getdata_Afv';
                break;
            default:
                // code block
        }
    }
    return clstp;
}

/////////////////////////Gen html from api//////////////////////////////////
function generateHtml_prv(dmacode, datatype, _templateCycle) {
    return new Promise((resolve, reject) => {
        hidePage_content_modal()
        $('.modal_title_setting').text("PRV - ตั้งค่าควบคุมประตูน้ำจุดติดตั้ง : " + dmacode)
        getHtml(getCookie('_wwcode'), dmacode, datatype, _templateCycle).then((data) => {
            $('#_Manual_PRV').html(data._manual)
            $('#_Automatic_PRV').html(data._Automatic)
            $('#_Realtime_PRV').html(data._Realtime)
            $('#_History_PRV').html(data._History)
            $('#txtRow').val(data._txtRow)
        }).then(() => {
            CallApigettable_modal('dt_grid_realtime', '_Realtime').then(() => {
                CallApigettable_modal('dt_grid_history', '_History').then(() => {
                    GeneratePRV('tblPrvAutomatic')
                    resolve()
                    showPage_content_modal();
                }).catch((error) => {
                    swalAlert(error.status, 'error')
                    reject()
                })
            })
        }).catch((error) => {
            swalAlert('เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง', 'error')
            reject()
        })
    })
}

function generateHtml_bv(dmacode, datatype, _templateCycle) {
    return new Promise((resolve, reject) => {
        hidePage_content_modal()
        $('.modal_title_setting').text("BV - ตั้งค่าควบคุมประตูน้ำจุดติดตั้ง : " + dmacode)
        getHtml(getCookie('_wwcode'), dmacode, datatype, _templateCycle).then((data) => {
            $('#_Manual_Bv').html(data._manual)
            $('#_Automatic_Bv').html(data._Automatic)
            $('#_Realtime_Bv').html(data._Realtime)
            $('#_History_bv').html(data._History)
            $('#txtRow').val(data._txtRow)
            $('#_Automatic_Bv_footer').html(data._bodyfooter)
            if (_templateCycle == 6) { $("#failure_mode").val(data._failure_mode); }
            //$("#step_control_delay").val(data._step_control_delay);
            //$("#time_loop").val(data._time_loop);
            //$("#limit_min").val(data._limit_min);
            //$("#deadband_pressure").val(data._deadband_pressure);
            //$("#deadband_flow").val(data._deadband_flow);
            //sessionStorage.setItem('cycle_counter', data._template_counter_cycle);
            GeneratePRV('tblBvAutomatic')
        }).then(() => {
            CallApigettable_modal('dt_grid_realtime_bv', '_Realtime').then(() => {
                CallApigettable_modal('dt_grid_history_bv', '_History').then(() => {
                    resolve()
                    showPage_content_modal();
                }).catch((error) => {
                    swalAlert(error.status, 'error')
                    reject()
                })
            })
        }).catch((error) => {
            swalAlert('เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง', 'error')
            reject()
        })
    })
}

function generateHtml_prvstepping(dmacode, datatype, _templateCycle) {
    return new Promise((resolve, reject) => {
        hidePage_content_modal()
        $('.modal_title_setting').text("PRV Stepping - ตั้งค่าควบคุมประตูน้ำจุดติดตั้ง : " + dmacode)
        getHtml(getCookie('_wwcode'), dmacode, datatype, _templateCycle).then((data) => {
            $('#_Manual_Bv').html(data._manual)
            $('#_Automatic_Bv').html(data._Automatic)
            $('#_Realtime_Bv').html(data._Realtime)
            $('#_History_bv').html(data._History)
            $('#txtRow').val(data._txtRow)
            $('#_Automatic_Bv_footer').html(data._bodyfooter)
            if (_templateCycle == 6) $("#failure_mode").val(data._failure_mode);
            //$("#step_control_delay").val(data._step_control_delay);
            //$("#time_loop").val(data._time_loop);
            //$("#limit_min").val(data._limit_min);
            //$("#deadband_pressure").val(data._deadband_pressure);
            //$("#deadband_flow").val(data._deadband_flow);
            //sessionStorage.setItem('cycle_counter', data._template_counter_cycle);
            GeneratePRV('tblSteppingAutomatic')
        }).then(() => {
            CallApigettable_modal('dt_grid_realtime_bv', '_Realtime').then(() => {
                CallApigettable_modal('dt_grid_history_bv', '_History').then(() => {
                    resolve()
                    showPage_content_modal();
                }).catch((error) => {
                    swalAlert(error.status, 'error')
                    reject()
                })
            })
        }).catch((error) => {
            swalAlert('เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง', 'error')
            reject()
        })
    })
}

// AFV
function generateHtml_Afv(dmacode, datatype, _templateCycle) {
    return new Promise((resolve, reject) => {
        hidePage_content_modal()
        $('.modal_title_setting').text("AFV - ตั้งค่าควบคุมประตูน้ำจุดติดตั้ง : " + dmacode)
        getHtml(getCookie('_wwcode'), dmacode, datatype, _templateCycle).then((data) => {
            $('#_Manual_afv').html(data._manual)
            $('#_Automatic_afv').html(data._Automatic)
            $('#_Realtime_afv').html(data._Realtime)
            $('#_History_afv').html(data._History)
            $('#txtRow').val(data._txtRow)
            $('#_Automatic_Afv_footer').html(data._bodyfooter)
            GeneratePRV('tblAfvAutomatic')
            showPage_content_modal();
        }).then(() => {
            CallApigettable_modal('dt_grid_realtime_afv', '_Realtime').then(() => {
                CallApigettable_modal('dt_grid_history_afv', '_History').then(() => {
                    resolve()
                    showPage_content_modal();
                }).catch((error) => {
                    swalAlert(error.status, 'error')
                    reject()
                })
            })
        }).catch((error) => {
            swalAlert('เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง', 'error')
            reject()
        })
    })
}
//////////////////////////////////////////////////////////


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
            JSON.stringify({ remote_name: sessionStorage.getItem('cacheremotename') })
    ).then((data) => {
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
        swalAlert(error.status, 'error')
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

function add_valva_modal() {
    if ($('#m_dvtypeddl').val() == '3') {
        var dmaconfigpressure = [];
        dmaconfigpressure = getDatabeforsave_getPressure();
    } else {
        var dmaconfigpressure = ''
    }

    let mainData = []
    mainData.push({
        m_wwcode: sessionStorage.getItem('cachewwcode'),
        m_dmacode: sessionStorage.getItem('cachedmacode'),
        m_dvtypeddl: $('#m_dvtypeddl').val(),
        m_totlepilot: $('#m_totlepilot').val(),
        m_controltype: $('#m_controltype').val(),
        m_smartlogger: $('#m_smartlogger').prop('checked'),
        m_Templatecontrol: $('#m_Templatecontrol').val(),
        dmaconfigpressure: dmaconfigpressure
    })

    CallAPI('/service/api.aspx/UpdateDmavalvatype',
                JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            $("#Modal_add_valva").modal("hide");
            swalAlert('บันทึกข้อมูลสำเร็จ', 'success')
            $('#refresh_table').click()
        }).catch((error) => {
            swalAlert('เกิดข้อผิดพลาดกรุณาลองใหม่อีกครั้ง', 'error')
        })
}

function getDatabeforsave_getPressure() {
    var allRow = document.getElementById("m_totlepilot").value
    var dmaconfigpressure = [];
    for (var i = 1; i <= allRow; i++) {

        dmaconfigpressure[i - 1] = {
            "pilot_num_ord": i,
            "pilot_pressure": document.getElementById("pilot_pressure" + i).value
        };
    }
    return dmaconfigpressure;
}

function GetSyncRealtime() {
    CallAPI('/service/api.aspx/GetDataCommandqueue',
            JSON.stringify({ remote_name: sessionStorage.getItem('cacheremotename') })
    ).then((data) => {
        let obj = data[0];
        if(obj != null)
            $('span[name="realtime_cmd_status"]').text(obj.StatusName)
        else $('span[name="realtime_cmd_status"]').text("non-data")
    }).catch((error) => {
        swalAlert(error.status, 'error')
    })
}