let dataTable;
$(document).ready(() => loadDataTable());

const loadDataTable = () => {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/company/getall' },
        "columns": [
            { data: 'name', "width": "25%" },
            
            { data: 'streetAddress', "width": "10%" },
            { data: 'city', "width": "15%" },
            { data: 'stateOrProvince', "width": "15%" },
            { data: 'postalCode', "width": "5%" },
            { data: 'phoneNumber', "width": "5%" },
            {
                data: 'id',
                "width": "25%",
                "render": (data) => {
                    return `<div class="w-75 btn-group" role="group">
                                <a href = "/admin/company/upsert?id=${data}" asp-controller="Company" asp-action="Upsert" asp-route-id="@obj.Id" class=" mx-2">
                                    <i class="bi bi-pencil-square"></i> 
                                </a>
                                <a onClick = Delete("/admin/company/delete/${data}") asp-controller="Company" asp-action="Delete" asp-route-id="@obj.Id" class="mx-2" style="color: salmon;">
                                    <i class="bi bi-trash-fill" ></i>
                                </a>
                            </div>`
                }
            },
        ]
    });
}

const Delete = (url) => {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: (data) => {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
} 
