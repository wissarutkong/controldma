function CallAPI(link, data) {

    return new Promise((resolve, reject) => {

        $.ajax({
            type: 'POST',
            url: link,
            contentType: 'application/json; charset=utf-8',
            data: data,
            dataType: 'json',
            success: function (response) {
                var data = jQuery.parseJSON(response.d)

                if (data.redirec) {
                    Swal.fire({
                        title: 'หมดเวลาเข้าสู่ระบบ',
                        text: 'กรุณา Login เข้าสู่ระบบอีกครั้ง',
                        icon: 'warning',
                        timer: 3000
                    }).then(() => {
                        $(window).unbind('beforeunload')
                        window.location.replace(data.redirec)
                    },
                        (dismiss) => {
                            $(window).unbind('beforeunload')
                            window.location.replace(data.redirec)
                        }
                    )
                    reject(data)
                }
                else {
                    resolve(data)
                }

            },
            error: (err) => {
                //console.log(err)
                reject(err)
            }
        })

    })
}
