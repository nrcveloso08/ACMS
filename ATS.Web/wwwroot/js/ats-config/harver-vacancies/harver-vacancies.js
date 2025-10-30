// =======================================
// Harver Vacancies Maintenance Script
// =======================================

"use strict";

var HarverVacanciesPage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyVacancies = [
        { id: 1, code: "VAC-001", name: "Customer Service Associate", unit: "Operations", location: "Manila, PH", dateCreated: "2024-01-10", status: "Active" },
        { id: 2, code: "VAC-002", name: "Software Developer", unit: "IT Department", location: "Toronto, CA", dateCreated: "2024-02-15", status: "Active" },
        { id: 3, code: "VAC-003", name: "Quality Analyst", unit: "QA", location: "Denver, US", dateCreated: "2024-03-01", status: "Inactive" },
        { id: 4, code: "VAC-004", name: "Recruitment Specialist", unit: "HR", location: "Manila, PH", dateCreated: "2024-03-20", status: "Active" },
        { id: 5, code: "VAC-005", name: "Finance Officer", unit: "Finance", location: "Cebu, PH", dateCreated: "2024-04-10", status: "Active" },
        { id: 6, code: "VAC-006", name: "Project Manager", unit: "PMO", location: "Remote / WFH", dateCreated: "2024-05-05", status: "Inactive" },
        { id: 7, code: "VAC-007", name: "Data Analyst", unit: "Analytics", location: "Lisbon, PT", dateCreated: "2024-06-10", status: "Active" },
        { id: 8, code: "VAC-008", name: "Trainer", unit: "L&D", location: "Tampa, US", dateCreated: "2024-07-01", status: "Active" },
        { id: 9, code: "VAC-009", name: "Sales Executive", unit: "Sales", location: "Guadalajara, MX", dateCreated: "2024-08-15", status: "Inactive" },
        { id: 10, code: "VAC-010", name: "Marketing Specialist", unit: "Marketing", location: "Cebu, PH", dateCreated: "2024-09-25", status: "Active" }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================
    const initDataTable = function () {
        table = $("#tblHarverVacancies").DataTable({
            data: dummyVacancies,
            columns: [
                { data: "id", title: "ID" },
                { data: "code", title: "Vacancy Code" },
                { data: "name", title: "Vacancy Name" },
                { data: "unit", title: "Business Unit" },
                { data: "location", title: "Location" },
                { data: "dateCreated", title: "Date Created" },
                {
                    data: "status",
                    title: "Status",
                    render: function (data) {
                        const color = data === "Active" ? "success" : "secondary";
                        return `<span class="badge badge-${color}">${data}</span>`;
                    }
                },
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
                                    <a class="dropdown-item editVacancy" href="#">
                                        <i class="fas fa-edit text-primary me-2"></i> Edit
                                    </a>
                                    <a class="dropdown-item detailsVacancy" href="#">
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

        // Add New Vacancy
        $("#btnNewVacancy").on("click", function () {
            AppUtils.loadModal("newHarverVacancyModal", {
                title: "<i class='fas fa-plus-circle me-2 text-primary'></i> New Harver Vacancy",
                body: `
                    <form id="frmNewVacancy">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Vacancy Code <span class="text-danger">*</span></label>
                            <input type="text" id="txtVacancyCode" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Vacancy Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtVacancyName" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Business Unit</label>
                            <input type="text" id="txtBusinessUnit" class="form-control form-control-sm" />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Location</label>
                            <input type="text" id="txtLocation" class="form-control form-control-sm" />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Status</label>
                            <select id="selStatus" class="form-select form-select-sm">
                                <option value="Active">Active</option>
                                <option value="Inactive">Inactive</option>
                            </select>
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
                            if (!AppUtils.validateForm("#frmNewVacancy")) return;

                            const newVacancy = {
                                id: table.data().count() + 1,
                                code: $("#txtVacancyCode").val().trim(),
                                name: $("#txtVacancyName").val().trim(),
                                unit: $("#txtBusinessUnit").val().trim(),
                                location: $("#txtLocation").val().trim(),
                                dateCreated: new Date().toISOString().split("T")[0],
                                status: $("#selStatus").val()
                            };

                            table.row.add(newVacancy).draw(false);
                            AppUtils.showToast?.("New vacancy added successfully!", "success");
                            $("#newHarverVacancyModal").modal("hide");
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
    HarverVacanciesPage.init();
});
