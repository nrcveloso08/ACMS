// =======================================
// Advertisement Source Maintenance Script
// =======================================

"use strict";

var AdvertisementSourcePage = (function () {
    let table;

    const escapeHtml = (text) => {
        if (!text) return "";
        return text
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    };

    const dummyAdvertisementSources = [
        { id: 1, sourceName: "JobStreet", description: "Primary job posting platform for PH region.", createdBy: "Admin", dateCreated: "2025-10-01", status: "Active" },
        { id: 2, sourceName: "LinkedIn", description: "Used for executive and professional roles.", createdBy: "HR Manager", dateCreated: "2025-10-02", status: "Active" },
        { id: 3, sourceName: "Indeed", description: "Popular global job aggregator.", createdBy: "Recruitment Team", dateCreated: "2025-10-05", status: "Inactive" },
        { id: 4, sourceName: "Internal Referral", description: "Referral program for existing employees.", createdBy: "HR Admin", dateCreated: "2025-10-06", status: "Active" }
    ];

    // =========================
    // 🔹 Initialize DataTable
    // =========================
    const initDataTable = function () {
        table = $("#tblAdvertisementSource").DataTable({
            data: dummyAdvertisementSources,
            columns: [
                { data: "id", title: "ID" },
                { data: "sourceName", title: "Source Name" },
                { data: "description", title: "Description" },
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
                    render: function () {
                        return `
                            <div class="dropdown text-center">
                                <button class="btn btn-sm btn-light btn-icon" data-toggle="dropdown" aria-expanded="false">
                                    <i class="fa fa-cog text-secondary"></i>
                                </button>
                                <div class="dropdown-menu dropdown-menu-right shadow-sm">
                                    <a class="dropdown-item editSource" href="#">
                                        <i class="fas fa-edit text-primary me-2"></i> Edit
                                    </a>
                                    <a class="dropdown-item deactivateSource" href="#">
                                        <i class="fas fa-ban text-danger me-2"></i> Deactivate
                                    </a>
                                    <a class="dropdown-item detailsSource" href="#">
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

    // =========================
    // 🔹 Event Binding
    // =========================
    const bindEvents = function () {
        // Export button
        $("#btnExportExcel").on("click", function () {
            table.button(".buttons-excel").trigger();
        });

        // ➕ Add New Source
        $("#btnAddSource").on("click", function () {
            AppUtils.loadModal("newAdvertisementSourceModal", {
                title: "<i class='fas fa-plus-circle me-2 text-primary'></i> New Advertisement Source",
                body: `
                    <form id="frmNewSource">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Source Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtSourceName" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Description</label>
                            <textarea id="txtDescription" class="form-control form-control-sm" rows="3"></textarea>
                        </div>
                    </form>`,
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "<i class='fas fa-save me-2'></i> Save",
                        btnClass: "btn btn-primary btn-sm",
                        autoDismiss: false,
                        action: function () {
                            const name = $("#txtSourceName").val().trim();
                            const desc = $("#txtDescription").val().trim();

                            if (!name) {
                                AppUtils.toastMessage("Please enter a source name.", "warning");
                                return;
                            }

                            const newItem = {
                                id: table.data().count() + 1,
                                sourceName: name,
                                description: desc,
                                createdBy: "Current User",
                                dateCreated: new Date().toISOString().split("T")[0],
                                status: "Active"
                            };

                            table.row.add(newItem).draw(false);
                            AppUtils.toastMessage(`"${name}" added successfully.`, "success");
                            $("#newAdvertisementSourceModal").modal("hide");
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
            $("#newAdvertisementSourceModal").modal("show");
        });
        // ✅ FINAL FIX — works with dropdowns & responsive DataTables
        $("body").on("click", ".editSource", function (e) {
            e.preventDefault();

            const table = $("#tblAdvertisementSource").DataTable();
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
                AppUtils.toastMessage("Unable to find source data for this row.", "warning");
                console.warn("⚠️ DataTable row lookup failed for:", this);
                return;
            }

            // ✅ Proceed to build modal
            const modalId = "editAdvertisementSourceModal_" + Date.now();

            const bodyHtml = `
                    <form id="${modalId}_frmEditSource">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Source Name</label>
                            <input type="text" id="${modalId}_editSourceName"
                                   class="form-control form-control-sm"
                                   value="${escapeHtml(rowData.sourceName)}" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Description</label>
                            <textarea id="${modalId}_editDescription"
                                      class="form-control form-control-sm"
                                      rows="3">${escapeHtml(rowData.description || "")}</textarea>
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Status</label>
                            <select id="${modalId}_editStatus" class="form-control form-control-sm">
                                <option value="Active" ${rowData.status === "Active" ? "selected" : ""}>Active</option>
                                <option value="Inactive" ${rowData.status === "Inactive" ? "selected" : ""}>Inactive</option>
                            </select>
                        </div>
                    </form>`;

            AppUtils.loadModal(modalId, {
                title: "<i class='fas fa-edit text-primary me-2'></i> Edit Advertisement Source",
                body: bodyHtml,
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "<i class='fas fa-save me-2'></i> Update",
                        btnClass: "btn btn-primary btn-sm",
                        autoDismiss: false,
                        action: function () {
                            const updatedName = $(`#${modalId}_editSourceName`).val().trim();
                            const updatedDesc = $(`#${modalId}_editDescription`).val().trim();
                            const updatedStatus = $(`#${modalId}_editStatus`).val();

                            if (!updatedName) {
                                AppUtils.toastMessage("Please enter a valid source name.", "warning");
                                return;
                            }

                            // ✅ Update DataTable
                            rowData.sourceName = updatedName;
                            rowData.description = updatedDesc;
                            rowData.status = updatedStatus;
                            row.data(rowData).invalidate().draw(false);

                            AppUtils.toastMessage(`"${updatedName}" updated successfully.`, "success");

                            const modalEl = document.getElementById(modalId);
                            const bsModal = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
                            bsModal.hide();
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

    const init = function () {
        initDataTable();
        bindEvents();
    };

    return { init: init };
})();

$(document).ready(function () {
    AdvertisementSourcePage.init();
});
