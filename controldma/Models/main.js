﻿$(document).ready(function () {

    var isMobile = /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
    var element = document.getElementById('text');
    if (!isMobile) {
        $('a[data-widget="pushmenu"]').click()
    } 

    $('#btn-LogOut').click(() => {
        Swal.fire({
            title: 'ต้องการออกจากระบบหรือไม่?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'ใช่',
            cancelButtonText: 'ไม่',
            showLoaderOnConfirm: true
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    type: 'POST',
                    url: 'login.aspx/Logout',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: (response) => {
                        let data = jQuery.parseJSON(response.d);

                        if (data.redirec)
                            window.location.replace(data.redirec)
                    },
                    error: (e) => {
                        Swal.fire({
                            icon: 'error',
                            title: 'เกิดข้อผิดพลาด...',
                            text: 'ไม่สามารถออกจากระบบได้ กรุณาลองอีกครั้ง',
                            footer: '<a href="https://www.facebook.com/dmapwa">กรุณาติดต่อผู้ดูแลระบบที่นี่</a>'
                        })
                    }
                })
            }
        }, (dismiss) => {
            if (dismiss === 'cancel') {
            }
        })
    })

    $('.select2').select2()

    //Initialize Select2 Elements
    $('.select2bs4').select2({
        theme: 'bootstrap4'
    })

    $("input[data-bootstrap-switch]").each(function () {
        $(this).bootstrapSwitch('state', $(this).prop('checked'));
    });



    //var myVar;
    //myVar = setTimeout(showPage, 1500);

    var path = window.location.pathname;
    var page = path.split("/").pop();
    

    initalselect_info().then(() => {
        if (page.indexOf('controlvalve.aspx') == 0) {
            CallApigetdatatable().then(() => { showPage(); showPage_content(); })
        } else {
            showPage();
        }
       
    })




    $('#_khet').change(function () {
        if ($(this).val() != getCookie('_zone')) {
            setCookie("_zone", $(this).val())
            setCookie("_wwcode", '')
            AjaxGetddlsite('_wwcode')
            console.log(getCookie('_zone'))
            //console.log('_wwcode=' + getCookie('_wwcode'))
        }      
    })

    $('#_wwcode').change(function () {
        setCookie("_wwcode", $(this).val().trim())
        if ($(this).val().trim('') != '' && $(this).val().trim('') != null) {
            // do something
            $('dt_controlvalve').DataTable().clear();
            hidePage_content()
            CallApigetdatatable().then(() => { showPage_content() })
        }
    })

    $('#refresh_table').click(() => {
        $('dt_controlvalve').DataTable().clear();
        hidePage_content()
        CallApigetdatatable().then(() => { showPage_content() })
    })

    

})


function initalselect_info() {
    return new Promise((resolve, reject) => {
        AjaxGetddlkhet('_khet').then(() => {
            AjaxGetddlsite('_wwcode')
            if (getCookie('_level') == 15) {
                $('#_khet').prop("disabled", true)
                $('#_wwcode').prop("disabled", true)
            } else if (getCookie('_level') == 10) { $('#_khet').prop("disabled", true) }
            resolve()
        })
    })
}


function showPage() {
    document.getElementById("loader").style.display = "none";
    document.getElementById("div_load").style.display = "block";
}

function hidePage_content() {
    document.getElementById("loader_content").style.display = "block";
    document.getElementById("div_content_load").style.display = "none";
}

function showPage_content() {
    document.getElementById("loader_content").style.display = "none";
    document.getElementById("div_content_load").style.display = "block";
}

function setCookie(cname, cvalue) {
    //var d = new Date();
    //d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    //var expires = "expires=" + d.toGMTString();

    var now = new Date();
    var minutes = 30;
    now.setTime(now.getTime() + (minutes * 60 * 1000));
    var expires = "expires=" + now.toUTCString();

    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

//function getCookie(cname) {
//    var name = cname + "=";
//    var decodedCookie = decodeURIComponent(document.cookie);
//    var ca = decodedCookie.split('');
//    for (var i = 0; i < ca.length; i++) {
//        var c = ca[i];
//        while (c.charAt(0) == ' ') {
//            c = c.substring(1);
//        }
//        if (c.indexOf(name) == 0) {
//            return c.substring(name.length, c.length);
//        }
//    }
//    return "";
//}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

$(document).on("click", "input:checkbox", function () {
    document.getElementById("txtSolenoid").value = "0";
    var $box = $(this);
    if ($box.is(":checked")) {
        document.getElementById("txtSolenoid").value = $box.attr("value");
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
})

function Setvariableapi(mainData) {
    CallAPI('/service/api.aspx/SetDataModal',
                JSON.stringify({ mainDataText: JSON.stringify(mainData) })
        ).then((data) => {
            resolve()
        }).catch((error) => {
            console.log(error)
            reject()
        })
}

