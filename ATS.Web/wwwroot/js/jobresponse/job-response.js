// ================================
// Job Response Page Script
// Handles Search, Reset, and Table Filtering
// ================================

"use strict";

var JobResponsePage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyData = [
        // LATAM
        { name: "Juan Perez", email: "juan.perez@gmail.com", phone: "502-555-1111", geo: "LATAM", country: "Guatemala", location: "Guatemala", date: "2025-10-15" },
        { name: "Maria Lopez", email: "maria.lopez@gmail.com", phone: "502-555-2222", geo: "LATAM", country: "Guatemala", location: "Guatemala", date: "2025-10-12" },
        { name: "Luis Martinez", email: "luis.m@gmail.com", phone: "502-555-3333", geo: "LATAM", country: "Guatemala", location: "Guatemala", date: "2025-10-18" },
        { name: "Ana Gonzalez", email: "ana.g@gmail.com", phone: "502-555-4444", geo: "LATAM", country: "Guatemala", location: "Guatemala", date: "2025-10-20" },
        { name: "Carlos Diaz", email: "carlos.diaz@gmail.com", phone: "502-555-5555", geo: "LATAM", country: "Guatemala", location: "Guatemala", date: "2025-10-25" },

        // NORAM
        { name: "John Smith", email: "john.smith@gmail.com", phone: "1-416-111-1111", geo: "NORAM", country: "Canada", location: "Toronto", date: "2025-10-10" },
        { name: "Emily Johnson", email: "emily.johnson@gmail.com", phone: "1-416-222-2222", geo: "NORAM", country: "Canada", location: "Toronto", date: "2025-10-19" },
        { name: "Michael Brown", email: "michael.b@gmail.com", phone: "1-212-333-3333", geo: "NORAM", country: "USA", location: "New York", date: "2025-10-23" },
        { name: "Sarah Davis", email: "sarah.d@gmail.com", phone: "1-212-444-4444", geo: "NORAM", country: "USA", location: "New York", date: "2025-10-24" },
        { name: "David Wilson", email: "david.w@gmail.com", phone: "1-212-555-5555", geo: "NORAM", country: "USA", location: "New York", date: "2025-10-26" }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================

    const initDataTable = function () {
        table = $("#tblJobResponse").DataTable({
            data: dummyData,
            columns: [
                { data: "date" },
                { data: "name" },
                { data: "email" },
                { data: "phone" },
                { data: "geo" },
                { data: "country" },
                { data: "location" },
                {
                    data: null,
                    orderable: false,
                    render: function () {
                        return `
                             <a href="/JobResponse/Edit" class="btn btn-sm btn-light-primary">
                                <i class="fa fa-edit"></i> Edit
                             </a>`;
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            pageLength: 10,
            dom: "<'table-responsive'tr>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            language: {
                emptyTable: "No Results available"
            }
        });
    };

    const filterTable = function () {
        const geo = $("#geoLocation").val();
        const country = $("#country").val();
        const location = $("#location").val();

        const filtered = dummyData.filter(item => {
            let match = true;
            if (geo && item.geo !== geo) match = false;
            if (country && item.country !== country) match = false;
            if (location && item.location !== location) match = false;
            return match;
        });

        table.clear().rows.add(filtered).draw();
    };

    const resetFilters = function () {
        $("#frmJobResponseSearch")[0].reset();
        table.clear().rows.add(dummyData).draw();
    };

    // =========================
    // 🔹 3. Event Handlers
    // =========================

    const bindEvents = function () {
        $("#btnSearch").on("click", function () {
            filterTable();
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

    return {
        init: init
    };

})();

$(document).ready(function () {
    JobResponsePage.init();
});
