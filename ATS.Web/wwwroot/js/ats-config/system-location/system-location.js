// ================================
// Alternate System Locations Maintenance Script
// ================================

"use strict";

var SystemLocationPage = (function () {

    // =========================
    // 🔹 1. Private Variables
    // =========================
    let table;

    const dummyLocations = [
        { id: 1, kittName: "1 (Manila)", altLocation: "UPTC, Quezon City, Philippines", systemName: "Recruitment - ATS" },
        { id: 2, kittName: "2 (Toronto)", altLocation: "Toronto, Ontario, Canada", systemName: "Recruitment - ATS" },
        { id: 3, kittName: "3 (Cebu)", altLocation: "Cebu City, Philippines", systemName: "Recruitment - ATS" },
        { id: 4, kittName: "4 (Tampa)", altLocation: "Clearwater, Florida, United States", systemName: "Recruitment - ATS" },
        { id: 5, kittName: "5 (Denver)", altLocation: "Aurora, Colorado, United States", systemName: "Recruitment - ATS" },
        { id: 6, kittName: "6 (WFH)", altLocation: "Remote / WFH", systemName: "Recruitment - ATS" },
        { id: 7, kittName: "7 (Guadalajara)", altLocation: "Guadalajara, Mexico", systemName: "Recruitment - ATS" },
        { id: 8, kittName: "8 (Bogota)", altLocation: "Bogotá, Colombia", systemName: "Recruitment - ATS" },
        { id: 9, kittName: "9 (Montevideo)", altLocation: "Montevideo, Uruguay", systemName: "Recruitment - ATS" },
        { id: 10, kittName: "10 (Lisbon)", altLocation: "Lisbon, Portugal", systemName: "Recruitment - ATS" }
    ];

    // =========================
    // 🔹 2. Private Functions
    // =========================
    const initDataTable = function () {
        table = $("#tblSystemLocations").DataTable({
            data: dummyLocations,
            columns: [
                { data: "id", title: "Id" },
                { data: "kittName", title: "Location Id (KITT Name)" },
                { data: "altLocation", title: "Alternate System Location Name" },
                { data: "systemName", title: "Alternate System Name" },
                {
                    data: null,
                    title: "Action",
                    orderable: false,
                    className: "text-center",
                    render: function () {
                        return `
                            <button class="btn btn-light btn-sm text-primary border editLocation" title="Edit">
                                <i class="fas fa-edit"></i>
                            </button>`;
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            pageLength: 10,
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

        // Open "New Location" Modal
        $("#btnNewLocation").on("click", function () {
            AppUtils.loadModal("newLocationModal", {
                title: "<i class='fas fa-plus-circle me-2 text-primary'></i> New System Location",
                body: `
                    <form id="frmNewLocation">
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Location Id (KITT Name) <span class="text-danger">*</span></label>
                            <input type="text" id="txtKittName" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Alternate System Location Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtAltLocation" class="form-control form-control-sm" required />
                        </div>
                        <div class="form-group mb-3">
                            <label class="fw-semibold">Alternate System Name <span class="text-danger">*</span></label>
                            <input type="text" id="txtSystemName" class="form-control form-control-sm" required />
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
                            if (!AppUtils.validateForm("#frmNewLocation")) return;

                            const newLocation = {
                                id: table.data().count() + 1,
                                kittName: $("#txtKittName").val().trim(),
                                altLocation: $("#txtAltLocation").val().trim(),
                                systemName: $("#txtSystemName").val().trim()
                            };

                            table.row.add(newLocation).draw(false);
                            AppUtils.showToast?.("New location added successfully!", "success");
                            $("#newLocationModal").modal("hide");
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

        // Edit Button
        $("body").on("click", ".editSource", function (e) {
            e.preventDefault();
            e.stopPropagation();

            // ✅ Always get correct row even when responsive child rows are used
            let $tr = $(this).closest('tr');
            if ($tr.hasClass('child')) $tr = $tr.prev();
            const rowRef = table.row($tr);
            const originalData = rowRef.data();

            if (!originalData) {
                console.warn("Edit clicked but no row data found.");
                AppUtils.toastMessage("Unable to find source data for this row.", "warning");
                return;
            }

            const rowData = $.extend(true, {}, originalData);
            const modalId = "editAdvertisementSourceModal_" + Date.now();

            const bodyHtml = `
        <form id="${modalId}_frmEditSource">
            <div class="form-group mb-3">
                <label class="fw-semibold">Source Name</label>
                <input type="text" id="${modalId}_editSourceName" class="form-control form-control-sm"
                       value="${escapeHtml(rowData.sourceName)}" required />
            </div>
            <div class="form-group mb-3">
                <label class="fw-semibold">Description</label>
                <textarea id="${modalId}_editDescription" class="form-control form-control-sm" rows="3">
                    ${escapeHtml(rowData.description || "")}
                </textarea>
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

                            rowData.sourceName = updatedName;
                            rowData.description = updatedDesc;
                            rowData.status = updatedStatus;

                            rowRef.data(rowData).invalidate().draw(false);
                            AppUtils.toastMessage(`"${updatedName}" updated successfully.`, "success");
                            $(`#${modalId}`).modal("hide");
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

            $(`#${modalId}`).modal("show");
        });

    };

    // =========================
    // 🔹 3. Public Init Function
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
    SystemLocationPage.init();
});
