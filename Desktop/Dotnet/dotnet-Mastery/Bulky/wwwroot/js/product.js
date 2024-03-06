$(document).ready(function () {
    loadDataTable();
   // Delete();
});

function loadDataTable () {

    $('#tblData').DataTable( {
        "ajax": {url :'/admin/product/getall'},
        "columns": [
            { data: 'title', width: '20%' },
            { data: 'isbn', width: '10%' },
            { data: 'listPrice', width: '10%' },
            { data: 'author', width: '15%' },
            { data: 'category.name', width: '20%' },
            { data: 'id',
            render: function (data) {
                return `<div.w-75.button-group.text-center>
                <a href="/Admin/Product/Upsert?id/${data}" class=" btn btn-primary mx-2 mb-2 " ><i class="bi bi-pencil-square" ></i> Edit</a>
                <a  onclick=Delete("/admin/product/delete/${data}")  class=" btn btn-danger mx-2" ><i class="bi bi-trash-fill" ></i> Delete</a>
                </div>`
            }
        },
        ]
    },
   );

}


// function Delete(url) {
//     Swal.fire({
//         title: "Are you sure you want to delete?",
//         text: "You will not be able to revert this action!",
//         icon: "warning",
//         buttons: true,
//         showCancelButton: true,
//         confirmButtonColor: "#3085d6",
//         cancelButtoncolor: "#d33",
//         confirmButtonText: "Yes, delete it!",
//         cancelButtonText: "cancel",
//         dangerMode: true
//     }).then((result) => {
//         if (result) {
//             $.ajax({
//                 url: url,
//                 type: "DELETE",
//                 success: function (data) {
//                     if (data.success == true ) {
//                         toastr.success(data.message);
//                         loadDataTable();
//                     }
//                     else {
//                         toastr.error(data.message);
//                     }
//                 }
//             });
//         }
//     });
// }
function Delete(url) {
    Swal.fire({
        title: "Are you sure you want to delete?",
        text: "You will not be able to revert this action!",
        icon: "warning",
        buttons: true,
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "delete",
        cancelButtonText: "cancel",
        dangerMode: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success == true ) {
                        toastr.success(data.message);
                        loadDataTable();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        } else {
            // If cancel button is clicked, simply close the modal
            Swal.close();
        }
    });
}

