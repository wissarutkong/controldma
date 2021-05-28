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

    pdfMake.fonts = {
        THSarabun: {
            normal: 'THSarabun.ttf',
            bold: 'THSarabun-Bold.ttf',
            italics: 'THSarabun-Italic.ttf',
            bolditalics: 'THSarabun-BoldItalic.ttf'
        }
    }

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
                         $('#dt_controlvalve').DataTable().columns.adjust().responsive.recalc();
                         resolve()
                     }).catch((error) => {
                         //swalAlert(error, 'error')
                         reject()
                     })
             } else { resolve() }
         })
    })
}

function CallApigettable_modal(Id, type) {
    return new Promise((resolve, reject) => {
        GenerateTablePage20(Id)
         .then((data) => {
             $documentableModal = data
             var tempdata
             if (type == '_Realtime') { tempdata = { remote_name: sessionStorage.getItem('cacheremotename') } }
             else { tempdata = { wwcode: sessionStorage.getItem('cachewwcode'), dmacode: sessionStorage.getItem('cachedmacode'), dvtype: sessionStorage.getItem('cachedvtype') } }
             CallAPI('/service/api.aspx/' + (type == '_Realtime' ? 'GetRealtimeDataCtr002' : 'GetHistoryDataCtr002'),
                           JSON.stringify(tempdata)
                     ).then((data) => {
                         $documentableModal.clear().rows.add(data).draw(true)
                         $('#' + Id).DataTable().columns.adjust().responsive.recalc();
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
        "destroy": true,
        "processing": true,
        "autoWidth": false,
        dom: 'Bfrtip',
        buttons: [
           {
               extend: 'excel',
               exportOptions: {
                   columns: [0, 1, 2, 3, 4, 5, 6, 7],
                   text: 'รายงานจุดติดตั้ง Dma Control',
               }
           },
           {
               extend: 'csv',
               exportOptions: {
                   columns: [0, 1, 2, 3, 4, 5, 6, 7]
               }
           },
           {
               extend: 'pdfHtml5',
               "pageSize": 'A4',
               orientation: 'landscape',
               pageSize: 'LEGAL',
               exportOptions: {
                   columns: [0, 1, 2, 3, 4, 5, 6, 7]
               },
               "customize": function (doc) {
                   doc.defaultStyle = {
                       font: 'THSarabun',
                       fontSize: 15
                   };


                   doc.content[1].table.widths = ['auto', 'auto', 'auto', 'auto', 'auto', 70, 70, '*'];
                   doc.styles.tableHeader.fontSize = 16;

                   var objLayout = {};
                   objLayout['hLineWidth'] = function (i) { return .5; };
                   objLayout['vLineWidth'] = function (i) { return .5; };
                   objLayout['hLineColor'] = function (i) { return '#aaa'; };
                   objLayout['vLineColor'] = function (i) { return '#aaa'; };
                   objLayout['paddingLeft'] = function (i) { return 4; };
                   objLayout['paddingRight'] = function (i) { return 4; };
                   doc.content[1].layout = objLayout;
                   var obj = {};
                   obj['hLineWidth'] = function (i) { return .5; };
                   obj['hLineColor'] = function (i) { return '#aaa'; };
               }

           },
           {
               extend: "print",
               exportOptions: {
                   columns: [0, 1, 2, 3, 4, 5, 6, 7],
                   text: 'รายงานจุดติดตั้ง Dma Control',
               },
               customize: function (win) {

                   var last = null;
                   var current = null;
                   var bod = [];

                   var css = '@page { size: landscape; }',
                       head = win.document.head || win.document.getElementsByTagName('head')[0],
                       style = win.document.createElement('style');
                    

                   style.type = 'text/css';
                   style.media = 'print';

                   if (style.styleSheet) {
                       style.styleSheet.cssText = css;
                   }
                   else {
                       style.appendChild(win.document.createTextNode(css));
                   }

                   head.appendChild(style);
               }
           }
        ]
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
        dom: 'Bfrtip',
        buttons: [
          {
              extend: 'excel'
          },
          {
              extend: "print",
              customize: function (win) {

                  var last = null;
                  var current = null;
                  var bod = [];

                  var css = '@page { size: landscape; }',
                      head = win.document.head || win.document.getElementsByTagName('head')[0],
                      style = win.document.createElement('style');


                  style.type = 'text/css';
                  style.media = 'print';

                  if (style.styleSheet) {
                      style.styleSheet.cssText = css;
                  }
                  else {
                      style.appendChild(win.document.createTextNode(css));
                  }

                  head.appendChild(style);
              }
          }
        ]
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
