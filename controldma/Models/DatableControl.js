$(document).ready(function () {

    let $documentable = null;
    let $documentableModal = null;
    //$('#dt_controlvalve thead tr').clone(true).appendTo('#dt_controlvalve thead');
    //$('#dt_controlvalve thead tr:eq(1) th').each(function (i) {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');

    //    $('input', this).on('keyup change', function () {
    //        if ($documentable.column(i).search() !== this.value) {
    //            $documentable.column(i).search(this.value).draw();
    //        }
    //    });
    //});

    GenerateTable('tblPrvAutomatic')
    GenerateTable('tblbvAutomatic')

})

function CallApigetdatatable() {
    return new Promise((resolve, reject) => {
        
        GenerateTable('dt_controlvalve')
         .then((data) => {
             $documentable = data
             if (getCookie('_wwcode') != '') {
                 CallAPI('/service/api.aspx/GetCtr002',
                            JSON.stringify({ wwcode_id: getCookie('_wwcode'), dmacode: '' })
                     ).then((data) => {
                         $documentable.clear().rows.add(data).draw(true)
                         resolve()
                     }).catch((error) => {
                         //swalAlert(error, 'error')
                         reject()
                     })
             } else { resolve() }
         })
    })
}

function CallApigettable_modal(Id,type) {
    return new Promise((resolve, reject) => {
        GenerateTablePage20(Id)
         .then((data) => {
             $documentableModal = data
             CallAPI('/service/api.aspx/' + (type == '_Realtime' ? 'GetRealtimeDataCtr002' : 'GetHistoryDataCtr002'),
                           ''
                     ).then((data) => {
                         $documentableModal.clear().rows.add(data).draw(true)
                         resolve()
                     }).catch((error) => {
                         //swalAlert(error, 'error')
                         reject()
                     })
         })
    })
}

function GenerateTable(Id) {

    var $table = $('#' + Id + '').DataTable({
        "oLanguage": {
            "sLengthMenu": "แสดง _MENU_ เร็คคอร์ด ต่อหน้า",
            "sZeroRecords": "ไม่เจอข้อมูลที่ค้นหา",
            "sInfo": "แสดง _START_ ถึง _END_ ของ _TOTAL_ทั้งหมด",
            "sInfoEmpty": "แสดง 0 ถึง 0 ของ 0 เร็คคอร์ด",
            "sInfoFiltered": "(จากเร็คคอร์ดทั้งหมด _MAX_ เร็คคอร์ด)",
            "sSearch": "ค้นหา :"
        },
        "responsive": true,
        "paging": false,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "fixedHeader": false,
        "info": true,
        "autoWidth": false,
        "destroy": true,
        "processing": true
    });

    return Promise.resolve($table)
}

function GenerateTablePage20(Id) {

    var $table = $('#' + Id + '').DataTable({
        "oLanguage": {
            "sLengthMenu": "แสดง _MENU_ เร็คคอร์ด ต่อหน้า",
            "sZeroRecords": "ไม่เจอข้อมูลที่ค้นหา",
            "sInfo": "แสดง _START_ ถึง _END_ ของ _TOTAL_ทั้งหมด",
            "sInfoEmpty": "แสดง 0 ถึง 0 ของ 0 เร็คคอร์ด",
            "sInfoFiltered": "(จากเร็คคอร์ดทั้งหมด _MAX_ เร็คคอร์ด)",
            "sSearch": "ค้นหา :"
        },
        "responsive": true,
        "pageLength": 20,
        "destroy": true,
        "searching": false,
        "info": true,
        "ordering": true,
        "paging": true,
    });

    return Promise.resolve($table)
}

function GeneratePRV(Id) {
    $('#' + Id + '').DataTable({
        "oLanguage": {
            "sLengthMenu": "แสดง _MENU_ เร็คคอร์ด ต่อหน้า",
            "sZeroRecords": "ไม่เจอข้อมูลที่ค้นหา",
            "sInfo": "แสดง _START_ ถึง _END_ ของ _TOTAL_ทั้งหมด",
            "sInfoEmpty": "แสดง 0 ถึง 0 ของ 0 เร็คคอร์ด",
            "sInfoFiltered": "(จากเร็คคอร์ดทั้งหมด _MAX_ เร็คคอร์ด)",
            "sSearch": "ค้นหา :"
        },
        "responsive": true,
        "paging": false,
        "lengthChange": true,
        "searching": false,
        "ordering": false,
        "fixedHeader": false,
        "info": true,
        "autoWidth": false,
        "destroy": true,
        "processing": true
    });
}
