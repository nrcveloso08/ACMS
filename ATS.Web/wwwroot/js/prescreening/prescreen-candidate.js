// ================================
// Prescreening Candidate Page Script
// Handles Search, Reset, New Applicant, and Email Modals
// ================================

"use strict";

var PrescreeningPage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyData = [
        {
            name: "Test100 Test100",
            email: "test100@gmail.com",
            phone: "9999 999 9999",
            location: "Manila - UPTC",
            job: "62 - Manila (CSR)",
            source: "EmployeeReferral",
            updated: "2025-09-30 06:17 PM",
            status: "First Round Interview"
        },
        {
            name: "Test101 Test101",
            email: "test101@gmail.com",
            phone: "9999 999 999",
            location: "Manila - UPTC",
            job: "62 - Manila (CSR)",
            source: "EmployeeReferral",
            updated: "2025-09-30 06:18 PM",
            status: "First Round Interview"
        },
        {
            name: "Test102 Sample",
            email: "test102@gmail.com",
            phone: "+9 (999) 999 9999",
            location: "Toronto - Remote",
            job: "63 - Toronto (CSR)",
            source: "ExternalReferral",
            updated: "2025-10-01 01:22 PM",
            status: "New Applicant"
        }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================

    // Initialize DataTable
    const initDataTable = function () {
        table = $("#tblPrescreening").DataTable({
            data: dummyData,
            columns: [
                { data: "name" },
                { data: "email" },
                {
                    data: "phone",
                    render: function (data) {
                        return `<i class="fa fa-star text-danger mr-1"></i> ${data} <i class="fa fa-copy text-muted ml-1"></i>`;
                    }
                },
                { data: "location" },
                { data: "job" },
                { data: "source" },
                { data: "updated" },
                {
                    data: "status",
                    render: function (data) {
                        const color = data.toLowerCase().includes("interview") ? "primary" : "success";
                        return `<a href="#" class="text-${color}">${data}</a>`;
                    }
                },
                {
                    data: null,
                    orderable: false,
                    render: function (row) {
                        return `
                        <div class="dropdown">
                            <button class="btn btn-sm btn-light btn-icon" data-toggle="dropdown">
                                <i class="fa fa-ellipsis-v"></i>
                            </button>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item btnSendEmail" href="#" data-email="${row.email}">
                                    <i class="fa fa-envelope mr-2 text-primary"></i> Email
                                </a>
                                <a class="dropdown-item" href="#"><i class="fa fa-edit mr-2 text-warning"></i> Edit Details</a>
                                <a class="dropdown-item" href="#"><i class="fa fa-user mr-2 text-success"></i> Employment Application</a>
                                <a class="dropdown-item" href="#"><i class="fa fa-question-circle mr-2 text-info"></i> Prescreen Questions</a>
                                <a class="dropdown-item btnUpdateHireType" href="#" data-name="${row.name}"><i class="fa fa-briefcase mr-2 text-info"></i> Update Hire Type
                                    </a>
                            </div>
                        </div>`;
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            pageLength: 10,
            ordering: true,
            // Hide search & length dropdown
            dom:
                "<'table-responsive'tr>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"
        });
    };

    // =========================
    // 🔹 HIRE TYPE MODAL
    // =========================
    const openHireTypeModal = function (name) {
        const hireTypeBody = `
            <form id="frmHireType" class="container-fluid">
                <div class="form-group">
                    <label>Hire Type</label>
                    <select id="hireTypeSelect" class="form-control" required>
                        <option value="">Select Hire Type</option>
                        <option value="Direct Hire">Direct Hire</option>
                        <option value="Contractual">Contractual</option>
                        <option value="Probationary">Probationary</option>
                        <option value="Part-time">Part-time</option>
                    </select>
                </div>
            </form>
        `;

        AppUtils.loadModal("hireTypeModal", {
            title: `${name} - Hire Type`,
            body: hireTypeBody,
            settings: "modal-dialog-centered",
            buttons: {
                Save: {
                    Enabled: true,
                    text: "Save",
                    btnClass: "btn btn-primary",
                    action: function () {
                        const selected = $("#hireTypeSelect").val();
                        if (!selected) {
                            AppUtils.showToast("Please select a Hire Type.", "warning");
                            return;
                        }

                        AppUtils.ajaxCall({
                            url: "/Prescreening/UpdateHireType",
                            type: "POST",
                            data: { name, hireType: selected },
                            successMessage: "Hire Type updated successfully!",
                            onSuccess: function () {
                                $("#hireTypeModal").modal("hide");
                            }
                        });
                    }
                },
                Cancel: {
                    Enabled: true,
                    text: "Cancel",
                    btnClass: "btn btn-secondary",
                    autoDismiss: true
                }
            }
        });
    };

    // Build Email Modal dynamically
    const openEmailModal = function (email) {
        const emailModalBody = `
            <form id="frmSendEmail" class="container-fluid">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label>To</label>
                        <input type="email" class="form-control" id="emailTo" value="${email}" required>
                    </div>
                    <div class="col-md-6 mb-3">
                        <label>CC</label>
                        <input type="text" class="form-control" id="emailCc" placeholder="Separate multiple addresses with commas">
                    </div>

                    <div class="col-md-6 mb-3">
                        <label>BCC</label>
                        <input type="text" class="form-control" id="emailBcc" placeholder="Separate multiple addresses with commas">
                    </div>
                    <div class="col-md-6 mb-3">
                        <label>Subject</label>
                        <input type="text" class="form-control" id="emailSubject" placeholder="Enter subject" value="Next Steps: Employment Form" required>
                    </div>

                    <div class="col-12 mb-3">
                        <label>Body</label>
                        <textarea id="emailBody" class="form-control" rows="10">
                            Hi ${email.split('@')[0]},
                            <br><br>
                            To move on to the next step in the application process, please review and complete the information using the link below.
                            <br><br>
                            <a href="#">Online Application Form</a>
                            <br><br>
                            If you have any questions, please reach out to us.
                            <br><br>
                            Sincerely,<br>
                            Recruiting Team<br>
                            IntouchCX
                        </textarea>
                    </div>
                </div>
            </form>
        `;

        AppUtils.loadModal("sendEmailModal", {
            title: "Send Email",
            body: emailModalBody,
            settings: "modal-dialog-centered modal-xl",
            buttons: {
                Send: {
                    Enabled: true,
                    text: "Send",
                    btnClass: "btn btn-primary",
                    action: function () {
                        if (AppUtils.validateForm("#frmSendEmail")) {
                            sendEmail();
                        }
                    }
                },
                Close: {
                    Enabled: true,
                    text: "Close",
                    btnClass: "btn btn-secondary",
                    autoDismiss: true
                }
            }
        });
    };

    // Dummy Send Email handler
    const sendEmail = function () {
        const payload = {
            to: $("#emailTo").val(),
            cc: $("#emailCc").val(),
            bcc: $("#emailBcc").val(),
            subject: $("#emailSubject").val(),
            body: $("#emailBody").val()
        };

        AppUtils.ajaxCall({
            url: "/Prescreening/SendEmail",
            type: "POST",
            data: payload,
            successMessage: "Email sent successfully!",
            onSuccess: function () {
                $("#sendEmailModal").modal("hide");
            }
        });
    };

    // =========================
    // 🔹 3. Event Handlers
    // =========================

    const bindEvents = function () {
        // Search Button
        $("#btnSearch").on("click", function () {
            console.log("Search triggered...");
            // Future: integrate AJAX search logic
        });

        // Reset Filters
        $("#btnReset").on("click", function () {
            $("#frmSearch")[0].reset();
        });

        // New Applicant Button
        $("#btnNewApplicant").on("click", function () {
            AppUtils.loadModal("newApplicantModal", {
                title: "New Applicant",
                body: "<p>Form goes here...</p>",
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "Save",
                        btnClass: "btn btn-primary",
                        action: function () {
                            console.log("Applicant saved!");
                        }
                    },
                    Cancel: {
                        Enabled: true,
                        text: "Cancel",
                        btnClass: "btn btn-secondary",
                        autoDismiss: true
                    }
                }
            });
        });

        // Email Button (inside table)
        $(document).on("click", ".btnSendEmail", function () {
            const email = $(this).data("email");
            openEmailModal(email);
        });

        // Open Hire Type Modal
        $(document).on("click", ".btnUpdateHireType", function () {
            const name = $(this).data("name");
            openHireTypeModal(name);
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
    PrescreeningPage.init();
});
