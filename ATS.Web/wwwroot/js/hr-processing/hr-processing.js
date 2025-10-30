// ================================
// HR Processing Page Script
// Handles Table Initialization and Filter Actions
// ================================

"use strict";

var HRProcessingPage = (function () {

    let table;

    // Dummy dataset
    const dummyData = [
        { name: "Amber L", email: "test@sample.com", phone: "+9 (999) 999-9999", status: "Hired", wave: "Marshmallow - Mesa - (Wave 15)", wage: 15.5, empType: "FullTime", fcra: "Not started", rehire: "No", hireType: "External" },
        { name: "Arshdeep K", email: "test@sample.com", phone: "+9 (999) 999-9999", status: "Hired", wave: "Marshmallow - Polo Park - (Wave 2C)", wage: 13.4, empType: "FullTime", fcra: "Not started", rehire: "No", hireType: "External" },
        { name: "Arshpreet K", email: "test@sample.com", phone: "+9 (999) 999-9999", status: "Hired", wave: "Marshmallow Polo Plaza - (Wave 56B)", wage: 13.4, empType: "FullTime", fcra: "Started", rehire: "No", hireType: "External" },
        { name: "Meenakshi", email: "test@sample.com", phone: "+1 (204) 999-9999", status: "Hired", wave: "Marshmallow Polo Plaza L1 - (Wave 69A)", wage: 0, empType: "FullTime", fcra: "Not started", rehire: "No", hireType: "External" },
        { name: "Yanarah Mer", email: "test@sample.com", phone: "+1 (561) 999-9999", status: "Hired", wave: "Sephora - Orlando - Voice - (Wave 92)", wage: 13.65, empType: "FullTime", fcra: "Not started", rehire: "No", hireType: "External" },
        { name: "Puneet Sin", email: "test@sample.com", phone: "+0 (999) 999-9999", status: "Hired", wave: "Marshmallow Polo Plaza L1 - (Wave 72B)", wage: 15, empType: "FullTime", fcra: "In Progress", rehire: "No", hireType: "External" },
        { name: "Henreca Dav", email: "test@sample.com", phone: "+0 (999) 999-9999", status: "Hired", wave: "PNI Media - Winnipeg - Waverley - (Wave 31)", wage: 0, empType: "FullTime", fcra: "Started", rehire: "No", hireType: "External" },
        { name: "Rakeem Gutter", email: "test@sample.com", phone: "+0 (999) 999-9999", status: "Hired", wave: "New York Times - Las Vegas - (Wave 16)", wage: 13, empType: "FullTime", fcra: "Not started", rehire: "No", hireType: "External" },
        { name: "Gessel Duar", email: "test@sample.com", phone: "+9 (999) 999-9999", status: "Hired", wave: "A & F - Orlando - (Wave 191)", wage: 13, empType: "FullTime", fcra: "Started", rehire: "No", hireType: "External" },
        { name: "Brenda Jones", email: "test@sample.com", phone: "+9 (999) 999-9999", status: "Hired", wave: "Marshmallow - Polo Park - (Wave 89)", wage: 14.2, empType: "FullTime", fcra: "Not started", rehire: "No", hireType: "External" }
    ];

    const initDataTable = function () {
        // Prevent reinitialization
        if ($.fn.DataTable.isDataTable("#tblHRProcessing")) {
            $("#tblHRProcessing").DataTable().clear().destroy();
        }

        table = $("#tblHRProcessing").DataTable({
            data: dummyData,
            destroy: true,
            columns: [
                { data: "name" },
                { data: "email" },
                {
                    data: "phone",
                    render: (p) => `<i class="fa fa-star text-danger mr-1"></i> ${p} <i class="fa fa-copy text-muted ml-1"></i>`
                },
                { data: "status" },
                {
                    data: "wave",
                    render: (data, type, row) => {
                        // use javascript:void(0) to avoid default navigation and ensure clickable link
                        return `<a href="javascript:void(0)" class="text-primary font-weight-bold wave-link" 
                                    data-name="${escapeHtml(row.name)}" 
                                    data-wave="${escapeHtml(row.wave)}" 
                                    data-location="${escapeHtml(row.location)}" 
                                    data-wage="${row.wage}" 
                                    data-hiretype="${escapeHtml(row.hireType)}"
                                    style="cursor:pointer;">
                                    ${escapeHtml(data)}
                                </a>`;
                    }
                },
                { data: "wage" },
                { data: "empType" },
                { data: "fcra" },
                { data: "rehire" },
                { data: "hireType" }
            ],
            responsive: true,
            autoWidth: false,
            ordering: true,
            pageLength: 10,
            dom: "<'table-responsive'tr>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            language: { emptyTable: "No applicants available." }
        });
    };

    const bindEvents = function () {
        // delegated event binding on tbody (works reliably with DataTables)
        $("#tblHRProcessing tbody").off("click", "a.wave-link")
            .on("click", "a.wave-link", function (e) {
                e.preventDefault();
                // read attributes explicitly (safer than .data() with hyphens)
                const info = {
                    name: $(this).attr("data-name"),
                    wave: $(this).attr("data-wave"),
                    location: $(this).attr("data-location"),
                    wage: $(this).attr("data-wage"),
                    hireType: $(this).attr("data-hiretype")
                };
                // ensure the flyout module is present
                if (window.HRFlyout && typeof window.HRFlyout.load === "function") {
                    HRFlyout.load(info);
                } else {
                    console.warn("HRFlyout not available");
                }
            });

        $("#btnSearch").off("click").on("click", function () {
            // simple filter example: filter by lineOfBusiness or wave (client-side)
            const lob = $("#lineOfBusiness").val();
            const waveSel = $("#wave").val();

            const filtered = dummyData.filter(item => {
                let ok = true;
                if (lob && item.wave.toLowerCase().indexOf(lob.toLowerCase()) === -1) ok = false;
                if (waveSel && item.wave.toLowerCase().indexOf(waveSel.toLowerCase()) === -1) ok = false;
                return ok;
            });

            table.clear().rows.add(filtered).draw();
        });

        $("#btnReset").off("click").on("click", function () {
            $("#frmSearch")[0].reset();
            table.clear().rows.add(dummyData).draw();
        });
    };

    const init = function () {
        initDataTable();
        bindEvents();
    };

    // small helper to avoid XSS in attributes
    function escapeHtml(text) {
        if (text === null || text === undefined) return "";
        return String(text).replace(/[&<>"'`=\/]/g, function (s) {
            return ({
                '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;',
                "'": '&#39;', '/': '&#x2F;', '`': '&#x60;', '=': '&#x3D;'
            })[s];
        });
    }

    return { init: init };

})();

$(document).ready(function () {
    HRProcessingPage.init();
});