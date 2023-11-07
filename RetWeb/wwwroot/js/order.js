let dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    }
    else {
        if (url.includes("completed")) {
            loadDataTable("completed");
        }
        else {
            if (url.includes("pending")) {
                loadDataTable("pending");
            }
            else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }

});

const loadDataTable = (status) => {
    dataTable = $('#tblData').DataTable({
        ajax: { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'user.email', "width": "25%" },
            { data: 'orderStatus', "width": "5%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id',
                "width": "10%",
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

