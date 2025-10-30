// =======================================
// Job Advertisements Maintenance Script
// =======================================

"use strict";

var JobAdvertisementPage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyJobAds = [
        { id: 1, name: "Customer Service - Manila", title: "Customer Support Associate", lang: "English", location: "Manila, PH", group: "Support", liveJob: "Yes", appointmentPage: "Available", smsService: "Enabled", referralLink: "https://ref.job/1", smsOpt: "Yes", status: "Active" },
        { id: 2, name: "Software Engineer - Toronto", title: "Software Engineer", lang: "English", location: "Toronto, CA", group: "Engineering", liveJob: "Yes", appointmentPage: "Available", smsService: "Disabled", referralLink: "https://ref.job/2", smsOpt: "No", status: "Active" },
        { id: 3, name: "Sales Executive - Lisbon", title: "Sales Executive", lang: "Portuguese", location: "Lisbon, PT", group: "Sales", liveJob: "No", appointmentPage: "N/A", smsService: "Enabled", referralLink: "https://ref.job/3", smsOpt: "Yes", status: "Inactive" },
        { id: 4, name: "Recruiter - Guadalajara", title: "Recruitment Specialist", lang: "Spanish", location: "Guadalajara, MX", group: "HR", liveJob: "Yes", appointmentPage: "Available", smsService: "Enabled", referralLink: "https://ref.job/4", smsOpt: "Yes", status: "Active" },
        { id: 5, name: "Finance Officer - Denver", title: "Finance Officer", lang: "English", location: "Denver, US", group: "Finance", liveJob: "No", appointmentPage: "N/A", smsService: "Disabled", referralLink: "https://ref.job/5", smsOpt: "No", status: "Active" },
        { id: 6, name: "Trainer - Cebu", title: "Training Specialist", lang: "English", location: "Cebu, PH", group: "L&D", liveJob: "Yes", appointmentPage: "Available", smsService: "Enabled", referralLink: "https://ref.job/6", smsOpt: "Yes", status: "Inactive" },
        { id: 7, name: "QA Tester - Bogota", title: "QA Analyst", lang: "Spanish", location: "Bogotá, CO", group: "Quality", liveJob: "Yes", appointmentPage: "Available", smsService: "Enabled", referralLink: "https://ref.job/7", smsOpt: "Yes", status: "Active" },
        { id: 8, name: "Marketing - Tampa", title: "Marketing Coordinator", lang: "English", location: "Tampa, US", group: "Marketing", liveJob: "No", appointmentPage: "N/A", smsService: "Disabled", referralLink: "https://ref.job/8", smsOpt: "No", status: "Inactive" },
        { id: 9, name: "Data Analyst - Lisbon", title: "Data Analyst", lang: "Portuguese", location: "Lisbon, PT", group: "Analytics", liveJob: "Yes", appointmentPage: "Available", smsService: "Enabled", referralLink: "https://ref.job/9", smsOpt: "Yes", status: "Active" },
        { id: 10, name: "HR Assistant - WFH", title: "HR Assistant", lang: "English", location: "Remote / WFH", group: "HR", liveJob: "Yes", appointmentPage: "Available", smsService: "Enabled", referralLink: "https://ref.job/10", smsOpt: "Yes", status: "Active" }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================
    const initDataTable = function () {
        table = $("#tblJobAdvertisements").DataTable({
            data: dummyJobAds,
            columns: [
                { data: "id", title: "Id" },
                { data: "name", title: "Name" },
                { data: "title", title: "Published Title" },
                { data: "lang", title: "Language" },
                { data: "location", title: "Location" },
                { data: "group", title: "Group" },
                { data: "liveJob", title: "Live Jobs Publishing" },
                { data: "appointmentPage", title: "Appointment Page" },
                { data: "smsService", title: "SMS Service" },
                { data: "referralLink", title: "Referral Link", render: function (data) { return `<a href="${data}" target="_blank">${data}</a>`; } },
                { data: "smsOpt", title: "SMS OPT" },
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
                                    <a class="dropdown-item editJobAd" href="#">
                                        <i class="fas fa-edit text-primary me-2"></i> Edit
                                    </a>
                                    <a class="dropdown-item deactivateJobAd" href="#">
                                        <i class="fas fa-ban text-danger me-2"></i> Deactivate
                                    </a>
                                    <a class="dropdown-item detailsJobAd" href="#">
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

        // Add New Job Advertisement
        $("#btnNewJobAd").on("click", function () {
            AppUtils.loadModal("newJobAdModal", {
                title: "<i class='fas fa-plus-circle me-2 text-primary'></i> New Job Advertisement",
                body: `
                    <form id="frmNewJobAd">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtName" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Published Title <span class="text-danger">*</span></label>
                            <input type="text" id="txtTitle" class="form-control form-control-sm" required />
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="fw-semibold">Language</label>
                                <input type="text" id="txtLang" class="form-control form-control-sm" />
                            </div>
                            <div class="col-md-6">
                                <label class="fw-semibold">Location</label>
                                <input type="text" id="txtLocation" class="form-control form-control-sm" />
                            </div>
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Group</label>
                            <input type="text" id="txtGroup" class="form-control form-control-sm" />
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <label class="fw-semibold">Live Jobs Publishing</label>
                                <select id="selLiveJob" class="form-select form-select-sm">
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <label class="fw-semibold">Appointment Page</label>
                                <select id="selAppointmentPage" class="form-select form-select-sm">
                                    <option value="Available">Available</option>
                                    <option value="N/A">N/A</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">SMS Service</label>
                            <select id="selSMSService" class="form-select form-select-sm">
                                <option value="Enabled">Enabled</option>
                                <option value="Disabled">Disabled</option>
                            </select>
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Referral Link</label>
                            <input type="url" id="txtReferralLink" class="form-control form-control-sm" placeholder="https://..." />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">SMS OPT</label>
                            <select id="selSMSOpt" class="form-select form-select-sm">
                                <option value="Yes">Yes</option>
                                <option value="No">No</option>
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
                            if (!AppUtils.validateForm("#frmNewJobAd")) return;

                            const newAd = {
                                id: table.data().count() + 1,
                                name: $("#txtName").val().trim(),
                                title: $("#txtTitle").val().trim(),
                                lang: $("#txtLang").val().trim(),
                                location: $("#txtLocation").val().trim(),
                                group: $("#txtGroup").val().trim(),
                                liveJob: $("#selLiveJob").val(),
                                appointmentPage: $("#selAppointmentPage").val(),
                                smsService: $("#selSMSService").val(),
                                referralLink: $("#txtReferralLink").val().trim(),
                                smsOpt: $("#selSMSOpt").val(),
                                status: "Active"
                            };

                            table.row.add(newAd).draw(false);
                            AppUtils.showToast?.("New job advertisement added successfully!", "success");
                            $("#newJobAdModal").modal("hide");
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
    JobAdvertisementPage.init();
});
