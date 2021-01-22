function AjaxGetddlkhet(Id) {
    return new Promise((resolve, reject) => {
        $('#' + Id + '').empty()
        $('#' + Id + '').append('<option value="">Loading...</option>')
        //$('#' + Id + '').select2('refresh')
        CallAPI('/service/api.aspx/Getddlkhet',
                JSON.stringify({ khet: getCookie('_zone') })
        ).then((data) => {
            $('#' + Id + '').html(data.option)
            //$('#' + Id + '').prop('selectedIndex', getCookie('_zone'))
            selectElement(Id, getCookie('_zone'))
            $('#' + Id + '').trigger('change');
            resolve()
        }).catch((error) => {
            swalAlert(error, 'error')
            reject()
        })
    })
}

function AjaxGetddlsite(Id) {
    return new Promise((resolve, reject) => {
            $('#' + Id + '').empty()
            $('#' + Id + '').append('<option value="">Loading...</option>')
            CallAPI('/service/api.aspx/Getddlsite',
                    JSON.stringify({ khet_id: getCookie('_zone'), wwcode_id: getCookie('_wwcode') })
            ).then((data) => {
                $('#' + Id + '').html(data.option)
                //$('#' + Id + '').prop('selectedIndex', getCookie('_zone'))
                //$('#' + Id + '').prop('selectedIndex', 0)
                getCookie('_wwcode') != '' ? selectElement(Id, getCookie('_wwcode')) : $('#' + Id + '').prop('selectedIndex', 0)
                //selectElement(Id, getCookie('_wwcode'))
                //$('#' + Id + '').trigger('change');
                resolve()
            }).catch((error) => {
                swalAlert(error, 'error')
                reject()
            })
    
    })


}

function selectElement(id, valueToSelect) {
    let element = document.getElementById(id);
    element.value = valueToSelect;
}