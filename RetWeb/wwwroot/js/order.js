let dataTable;
$(document).ready(() => loadDataTable());

const loadDataTable = () => {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/order/getall' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'user.email', "width": "25%" },
            { data: 'orderStatus', "width": "5%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "width": "15%",
                "render": (data) => {
                    return `<div class="w-75 btn-group" role="group">
                                <a href = "/admin/order/details?orderId=${data}" asp-controller="Order" asp-action="details" asp-route-id="@obj.Id" class=" mx-2">
                                    <i class="bi bi-pencil-square"></i> 
                                </a>
                            </div>`
                }
            },
        ]
    });
}

