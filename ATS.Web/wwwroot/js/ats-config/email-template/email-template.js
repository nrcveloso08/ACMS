// =======================================
// Email Templates Maintenance Script
// =======================================

"use strict";

var EmailTemplatePage = (function () {

    let table;

    const dummyEmailTemplates = [
        { id: 1, name: "Interview Invitation", subject: "Interview Invitation - [Applicant.FirstName]", category: "Recruitment", createdBy: "Admin", dateCreated: "2025-09-01", status: "Active" },
        { id: 2, name: "Offer Letter", subject: "Congratulations, [Applicant.FirstName]!", category: "Onboarding", createdBy: "HR Manager", dateCreated: "2025-09-05", status: "Active" },
        { id: 3, name: "Application Received", subject: "We’ve received your application", category: "Recruitment", createdBy: "System", dateCreated: "2025-09-10", status: "Active" },
        { id: 4, name: "Assessment Reminder", subject: "Reminder: Complete your assessment today", category: "Recruitment", createdBy: "Recruitment Team", dateCreated: "2025-09-15", status: "Active" },
        { id: 5, name: "Rejection Notice", subject: "Application Update - [PositionTitle]", category: "Recruitment", createdBy: "HR Staff", dateCreated: "2025-09-20", status: "Inactive" },
        { id: 6, name: "Background Check Request", subject: "Background Check Required - [Applicant.FirstName]", category: "Compliance", createdBy: "Admin", dateCreated: "2025-09-25", status: "Active" },
        { id: 7, name: "Schedule Confirmation", subject: "Your interview is confirmed", category: "Recruitment", createdBy: "Recruiter", dateCreated: "2025-10-01", status: "Active" },
        { id: 8, name: "Preboarding Information", subject: "Welcome to the team!", category: "Onboarding", createdBy: "HR Coordinator", dateCreated: "2025-10-02", status: "Active" },
        { id: 9, name: "Contract Renewal Reminder", subject: "Contract Renewal Notice", category: "Admin", createdBy: "System", dateCreated: "2025-10-04", status: "Active" },
        { id: 10, name: "Exit Interview Invitation", subject: "Exit Interview Schedule", category: "Offboarding", createdBy: "HR Manager", dateCreated: "2025-10-08", status: "Inactive" }
    ];

    const initDataTable = function () {
        table = $("#tblEmailTemplates").DataTable({
            data: dummyEmailTemplates,
            columns: [
                { data: "id", title: "ID" },
                { data: "name", title: "Template Name" },
                { data: "subject", title: "Subject" },
                { data: "category", title: "Category" },
                { data: "createdBy", title: "Created By" },
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
                                    <a class="dropdown-item editTemplate" href="#">
                                        <i class="fas fa-edit text-primary me-2"></i> Edit
                                    </a>
                                    <a class="dropdown-item deactivateTemplate" href="#">
                                        <i class="fas fa-ban text-danger me-2"></i> Deactivate
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

        // Export
        $("#btnExportExcel").on("click", function () {
            table.button(".buttons-excel").trigger();
        });

        // Add new template
        $("#btnNewTemplate").on("click", function () {
            AppUtils.loadModal("newEmailTemplateModal", {
                title: "<i class='fas fa-plus-circle me-2 text-primary'></i> New Email Template",
                body: `
                    <form id="frmNewTemplate">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Template Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtTemplateName" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Subject <span class="text-danger">*</span></label>
                            <input type="text" id="txtSubject" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Category</label>
                            <input type="text" id="txtCategory" class="form-control form-control-sm" />
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
                            if (!AppUtils.validateForm("#frmNewTemplate")) return;

                            const newTemplate = {
                                id: table.data().count() + 1,
                                name: $("#txtTemplateName").val().trim(),
                                subject: $("#txtSubject").val().trim(),
                                category: $("#txtCategory").val().trim() || "General",
                                createdBy: "Current User",
                                dateCreated: new Date().toISOString().split("T")[0],
                                status: "Active"
                            };

                            table.row.add(newTemplate).draw(false);
                            AppUtils.showToast?.("New email template added successfully!", "success");
                            $("#newEmailTemplateModal").modal("hide");
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

        // ✅ Generic Edit Button Handler (works for dynamically rendered tables)
        $(document).on("click", ".editTemplate", function (e) {
            e.preventDefault();

            // Use the existing initialized DataTable
            const table = $("#tblEmailTemplates").DataTable();

            // This part ensures we always get the correct row
            const $btn = $(this);
            let $tr = $btn.closest("tr");

            // ✅ Handle responsive child rows (DataTables creates duplicates)
            if ($tr.hasClass("child")) {
                $tr = $tr.prev();
            }

            // ✅ Use row() with DOM element reference, not selector
            const row = table.row($tr.get(0));
            const rowData = row.data();

            if (!rowData) {
                console.warn("Edit clicked but no row data found.");
                AppUtils.showToast?.("Unable to find template data for this row.", "warning");
                return;
            }

            // ✅ Use modal helper to show edit form
            AppUtils.loadModal("editEmailTemplateModal", {
                title: "<i class='fas fa-edit text-primary me-2'></i> Edit Email Template",
                body: `
            <form id="frmEditTemplate">
                <div class="form-group mb-3">
                    <label class="fw-semibold">Template Name</label>
                    <input type="text" id="editTemplateName" class="form-control form-control-sm" value="${rowData.name}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Subject</label>
                    <input type="text" id="editSubject" class="form-control form-control-sm" value="${rowData.subject}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Category</label>
                    <input type="text" id="editCategory" class="form-control form-control-sm" value="${rowData.category}" />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Status</label>
                    <select id="editStatus" class="form-control form-control-sm">
                        <option value="Active" ${rowData.status === "Active" ? "selected" : ""}>Active</option>
                        <option value="Inactive" ${rowData.status === "Inactive" ? "selected" : ""}>Inactive</option>
                    </select>
                </div>
            </form>
        `,
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "<i class='fas fa-save me-2'></i> Update",
                        btnClass: "btn btn-primary btn-sm",
                        autoDismiss: false,
                        action: function () {
                            if (!AppUtils.validateForm("#frmEditTemplate")) return;

                            // Update the data in the DataTable
                            rowData.name = $("#editTemplateName").val().trim();
                            rowData.subject = $("#editSubject").val().trim();
                            rowData.category = $("#editCategory").val().trim();
                            rowData.status = $("#editStatus").val();

                            // ✅ Update DataTable row
                            row.data(rowData).invalidate().draw(false);

                            AppUtils.showToast?.("Template updated successfully!", "success");
                            $("#editEmailTemplateModal").modal("hide");
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

            $("#editEmailTemplateModal").modal("show");
        });
    };

    const init = function () {
        initDataTable();
        bindEvents();
    };

    return { init: init };

})();

$(document).ready(function () {
    EmailTemplatePage.init();
});
