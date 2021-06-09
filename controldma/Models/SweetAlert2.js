$(document).ready(function () {

    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    $('.swalSuccess').click(function () {
        Toast.fire({
            icon: 'success',
            title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
        })
    });
    $('.swalInfo').click(function () {
        Toast.fire({
            icon: 'info',
            title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
        })
    });
    $('.swalError').click(function () {
        Toast.fire({
            icon: 'error',
            title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
        })
    });
    $('.swalWarning').click(function () {
        Toast.fire({
            icon: 'warning',
            title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
        })
    });
    $('.swalQuestion').click(function () {
        Toast.fire({
            icon: 'question',
            title: 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr.'
        })
    });

})

function swalAlert(title, type) {
    if (type == "success") { toastr.success(title) }
    else if (type == "info") { toastr.info(title) }
    else if (type == "error") { toastr.error(title) }
    else if (type == "warning") { toastr.warning(title) }
}

function swalAlert2(title, type) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });
    Toast.fire({ icon: type, title: title })
}

function swalConfirmation(title, html) {
    return new Promise((resolve, reject) => {
        Swal.fire({
            title: title,
            text: html,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'ต้องการ',
            cancelButtonText: 'ยกเลิก'
        }).then((result) => {
            if (result.value) {
                Swal.fire({
                    input: 'text',
                    title: 'ป้อนหมายเหตุในการปิดใช้งาน?',
                    inputPlaceholder: 'ป้อนหมายเหตุ',
                    confirmButtonText: 'บันทึก',
                    inputValidator: (value) => {
                        if (!value) {
                            return 'กรุณาป้อนหมายเหตุ!'
                        } else {
                            resolve(value)
                        }
                    }
                })
            }
        })
    })
}

