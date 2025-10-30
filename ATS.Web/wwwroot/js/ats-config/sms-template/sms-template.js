// =======================================
// SMS Template Maintenance Page Script
// Handles DataTable, Filters, and Actions
// =======================================

"use strict";

var SMSTemplatePage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyTemplates = [
        {
            templateName: "Appointment Confirmation (NORAM)",
            message: "Hi [Applicant.FirstName], your interview is on [Appointment.Date] at [Appointment.TimeSlot].",
            location: "Mesa / WFA / Arizona / Denver",
            provider: "SMS",
            status: "Active"
        },
        {
            templateName: "Assessment Reminder",
            message: "Hi [Applicant.FirstName], this is a reminder to complete your assessment today. Good luck!",
            location: "Toronto / WFH / Ontario",
            provider: "SMS",
            status: "Active"
        },
        {
            templateName: "Background Check Notification",
            message: "Hi [Applicant.FirstName], your background check is now in progress. We’ll notify you once completed.",
            location: "Mexico / Guadalajara / Remote",
            provider: "WhatsApp",
            status: "Inactive"
        },
        {
            templateName: "Offer Letter Sent",
            message: "Hi [Applicant.FirstName], congratulations! Your offer letter has been sent to your email.",
            location: "Manila / PH / Remote",
            provider: "SMS",
            status: "Active"
        }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================

    // Initialize DataTable
    const initDataTable = function () {
        table = $("#tblSMSTemplates").DataTable({
            data: dummyTemplates,
            columns: [
                { data: "templateName", title: "Template Name" },
                { data: "message", title: "Message" },
                { data: "location", title: "Location" },
                { data: "provider", title: "Provider" },
                { data: "status", title: "Status" },
                {
                    data: null,
                    title: "Action",
                    orderable: false,
                    render: function (data) {
                        return `
                            <div class="dropdown text-center">
                                <button class="btn btn-sm btn-light btn-icon" data-toggle="dropdown" aria-expanded="false" title="Actions">
                                    <i class="fa fa-cog text-secondary"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-right shadow-sm">
                                    <a class="dropdown-item editTemplate" href="#">
                                        <i class="fas fa-edit text-primary me-2"></i> Edit
                                    </a>
                                    <a class="dropdown-item copyTemplate" href="#">
                                        <i class="fas fa-copy text-secondary me-2"></i> Copy
                                    </a>
                                    <a class="dropdown-item detailsTemplate" href="#">
                                        <i class="fas fa-info-circle text-info me-2"></i> Details
                                    </a>
                                </div>
                            </div>`;
                    }
                }
            ],
            responsive: true,
            pageLength: 5,
            lengthChange: false,
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

    // Apply Filters
    const applyFilters = function () {
        $.fn.dataTable.ext.search.push(function (settings, data) {
            const activeFilter = $("#activeFilter").val();
            const providerFilter = $("#providerFilter").val();
            const status = data[4];
            const provider = data[3];

            let pass = true;
            if (activeFilter !== "All" && activeFilter !== status) pass = false;
            if (providerFilter !== "All" && providerFilter !== provider) pass = false;
            return pass;
        });

        $("#activeFilter, #providerFilter").on("change", function () {
            table.draw();
        });
    };

    // =========================
    // 🔹 3. Event Bindings
    // =========================
    const bindEvents = function () {
        // Search box
        $("#searchBox").on("keyup", function () {
            table.search(this.value).draw();
        });

        // Action Buttons
        $(document).on("click", ".editTemplate", function () {
            const row = table.row($(this).parents("tr")).data();
            alert(`Edit clicked for: ${row.templateName}`);
        });

        $(document).on("click", ".copyTemplate", function () {
            const row = table.row($(this).parents("tr")).data();
            alert(`Copied template: ${row.templateName}`);
        });

        $(document).on("click", ".detailsTemplate", function () {
            const row = table.row($(this).parents("tr")).data();
            alert(`Viewing details for: ${row.templateName}`);
        });

        // Export
        $("#btnExportExcel").on("click", function () {
            table.button(".buttons-excel").trigger();
        });

        // Sync WhatsApp
        $("#btnSyncWhatsApp").on("click", function () {
            alert("Starting WhatsApp synchronization...");
        });

        // Add New Template
        $("#btnAddTemplate").on("click", function () {
            alert("Open modal to add new SMS Template");
        });
    };

    // =========================
    // 🔹 4. Public Init
    // =========================
    const init = function () {
        initDataTable();
        applyFilters();
        bindEvents();
    };

    return {
        init: init
    };

})();

// =========================
// 🔹 5. Initialize on Ready
// =========================
$(document).ready(function () {
    SMSTemplatePage.init();
});
