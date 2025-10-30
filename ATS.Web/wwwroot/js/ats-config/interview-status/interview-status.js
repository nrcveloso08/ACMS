// =======================================
// Interview Status Maintenance Script
// =======================================

"use strict";

var InterviewStatusPage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyStatuses = [
        { id: 1, name: "New Applicant", active: "Yes", nextStatus: "In Testing" },
        { id: 2, name: "Scheduled", active: "Yes", nextStatus: "In Testing" },
        { id: 3, name: "In Testing", active: "Yes", nextStatus: "Ready For Interview, Not Right Now, On Hold" },
        { id: 4, name: "Ready For Interview", active: "Yes", nextStatus: "In Interview" },
        { id: 5, name: "In Interview", active: "Yes", nextStatus: "CSI Interview, PreHire, On Hold, Not Right Now" },
        { id: 6, name: "CSI Interview", active: "Yes", nextStatus: "HR Processing, On Hold, Not Right Now, PreHire" },
        { id: 7, name: "HR Processing", active: "No", nextStatus: "On Hold, Not Right Now, PreHire" },
        { id: 8, name: "On Hold", active: "Yes", nextStatus: "Not Right Now, PreHire" },
        { id: 9, name: "PreHire", active: "Yes", nextStatus: "Hired" },
        { id: 10, name: "Not Right Now", active: "Yes", nextStatus: "Archived" }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================
    const initDataTable = function () {
        table = $("#tblInterviewStatus").DataTable({
            data: dummyStatuses,
            columns: [
                { data: "id", title: "ID" },
                { data: "name", title: "Name" },
                {
                    data: "active",
                    title: "Active?",
                    render: function (data) {
                        const color = data === "Yes" ? "success" : "secondary";
                        return `<span class="badge badge-${color}">${data}</span>`;
                    }
                },
                { data: "nextStatus", title: "Next Status (Default)" },
                {
                    data: null,
                    title: "Action",
                    orderable: false,
                    className: "text-center",
                    render: function () {
                        return `
                            <div class="dropdown">
                                <button class="btn btn-sm btn-light btn-icon" data-toggle="dropdown">
                                    <i class="fa fa-cog text-secondary"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-right shadow-sm">
                                    <a class="dropdown-item editStatus" href="#">
                                        <i class="fas fa-edit text-primary me-2"></i> Edit
                                    </a>
                                    <a class="dropdown-item detailsStatus" href="#">
                                        <i class="fas fa-info-circle text-info me-2"></i> Details
                                    </a>
                                </div>
                            </div>`;
                    }
                }
            ],
            responsive: true,
            pageLength: 10,
            autoWidth: false,
            ordering: true,
            dom: "Bfrtip",
            buttons: [
                {
                    extend: "excelHtml5",
                    text: '<i class="fas fa-file-excel me-2"></i> Export to Excel',
                    className: "btn btn-success btn-sm shadow-sm d-none"
                }
            ]
        });
    };

    const bindEvents = function () {

        // Export to Excel
        $("#btnExportExcel").on("click", function () {
            table.button(".buttons-excel").trigger();
        });

        // Add New Interview Status
        $("#btnNewStatus").on("click", function () {
            AppUtils.loadModal("newInterviewStatusModal", {
                title: "<i class='fas fa-plus-circle me-2 text-primary'></i> New Interview Status",
                body: `
                    <form id="frmNewStatus">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Status Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtStatusName" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Is Active?</label>
                            <select id="selActive" class="form-select form-select-sm">
                                <option value="Yes">Yes</option>
                                <option value="No">No</option>
                            </select>
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Next Status (Default)</label>
                            <input type="text" id="txtNextStatus" class="form-control form-control-sm" placeholder="Comma-separated values" />
                        </div>
                    </form>
                `,
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "<i class='fas fa-save me-2'></i> Save",
                        btnClass: "btn btn-primary btn-sm",
                        autoDismiss: false,
                        action: function () {
                            if (!AppUtils.validateForm("#frmNewStatus")) return;

                            const newStatus = {
                                id: table.data().count() + 1,
                                name: $("#txtStatusName").val().trim(),
                                active: $("#selActive").val(),
                                nextStatus: $("#txtNextStatus").val().trim()
                            };

                            table.row.add(newStatus).draw(false);
                            AppUtils.showToast?.("New interview status added successfully!", "success");
                            $("#newInterviewStatusModal").modal("hide");
                        }
                    },
                    Cancel: {
                        Enabled: true,
                        text: "Cancel",
                        btnClass: "btn btn-secondary btn-sm",
                        autoDismiss: true
                    }
                }
            });
        });
    };

    // =========================
    // 🔹 3. Public Init
    // =========================
    const init = function () {
        initDataTable();
        bindEvents();
    };

    return { init: init };

})();

// =========================
// 🔹 4. Initialize on Ready
// =========================
$(document).ready(function () {
    InterviewStatusPage.init();
});
