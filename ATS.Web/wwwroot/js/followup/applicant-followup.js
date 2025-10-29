// ================================
// Applicant Follow Up Page Script
// Handles Search, Filter, and Table Rendering
// ================================

"use strict";

var ApplicantFollowUpPage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    // Dummy JSON data (replace with AJAX later)
    const dummyData = [
        {
            name: "John Doe",
            description: "Interview follow-up required",
            date: "2025-10-20",
            requestedBy: "HR Admin",
            createdBy: "Jane Smith",
            createdOn: "2025-10-18",
            completedBy: "",
            completedOn: ""
        },
        {
            name: "Anna Cruz",
            description: "Awaiting response from applicant",
            date: "2025-10-23",
            requestedBy: "Recruitment Manager",
            createdBy: "System",
            createdOn: "2025-10-20",
            completedBy: "Mark J.",
            completedOn: "2025-10-25"
        }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================

    // Initialize DataTable
    const initDataTable = function () {
        table = $("#tblFollowUp").DataTable({
            data: dummyData,
            columns: [
                {
                    data: null,
                    className: "text-center",
                    orderable: false,
                    render: function () {
                        return `<i class="fa fa-user-clock text-primary"></i>`;
                    }
                },
                { data: "name" },
                { data: "description" },
                { data: "date" },
                { data: "requestedBy" },
                { data: "createdBy" },
                { data: "createdOn" },
                {
                    data: "completedBy",
                    render: function (data) {
                        return data ? `<span class="text-success">${data}</span>` : `<span class="text-muted">Pending</span>`;
                    }
                },
                {
                    data: "completedOn",
                    render: function (data) {
                        return data ? data : `<span class="text-muted">--</span>`;
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            pageLength: 10,
            ordering: true,
            dom:
                "<'table-responsive'tr>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            language: {
                emptyTable: "No Results available"
            }
        });
    };

    // Reload or search function
    const searchFollowUps = function () {
        const filters = {
            startDate: $("#startDate").val(),
            endDate: $("#endDate").val(),
            lineOfBusiness: $("#lineOfBusiness").val(),
            wave: $("#wave").val(),
            includeCompleted: $("#includeCompleted").is(":checked")
        };

        console.log("Filters applied:", filters);

        // Simulate search refresh (replace with AJAX later)
        table.clear().rows.add(dummyData).draw();

        AppUtils.showToast("Search executed successfully!", "info");
    };

    // Reset Filters
    const resetFilters = function () {
        $("#filterForm")[0].reset();
        table.clear().draw();
        AppUtils.showToast("Filters reset.", "secondary");
    };

    // =========================
    // 🔹 3. Event Handlers
    // =========================

    const bindEvents = function () {
        $("#btnSearch").on("click", function () {
            searchFollowUps();
        });

        $("#btnReset").on("click", function () {
            resetFilters();
        });
    };

    // =========================
    // 🔹 4. Public Init Function
    // =========================

    const init = function () {
        initDataTable();
        bindEvents();
    };

    // Expose public methods
    return {
        init: init
    };

})();

// =========================
// 🔹 5. Initialize on Ready
// =========================
$(document).ready(function () {
    ApplicantFollowUpPage.init();
});
