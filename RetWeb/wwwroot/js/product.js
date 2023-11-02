let dataTable;
$(document).ready(() => loadDataTable());

const loadDataTable = () => {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "25%" },
            {
                data: 'description',
                "width": "25%",
                "render":  (data, type, row) => {
                    if (type === 'display' && data) {
                        // Split the description into words
                        const words = data.split(' ');

                        // Only show the first 10 words
                        const truncatedDescription = words.slice(0, 9).join(' ');

                        // Add ellipsis and a "Show More" link
                        if (words.length > 9) {
                            return `${truncatedDescription} <span class="show-more">...</span>`;
                        } else {
                            return truncatedDescription;
                        }
                    }
                    return data;
                }
            },
            { data: 'category.name', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "5%" },
            { data: 'price', "width": "5%" },
            { data: 'price50', "width": "5%" },
            { data: 'price100', "width": "5%" },
            {
                data: 'id',
                "width": "25%",
                "render": (data) => {
                    return `<div class="w-75 btn-group" role="group">
                                <a href = "/admin/product/upsert?id=${data}" asp-controller="Product" asp-action="Upsert" asp-route-id="@obj.Id" class=" mx-2">
                                    <i class="bi bi-pencil-square"></i> 
                                </a>
                                <a onClick = Delete("/admin/product/delete/${data}") asp-controller="Product" asp-action="Delete" asp-route-id="@obj.Id" class="mx-2" style="color: salmon;">
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
