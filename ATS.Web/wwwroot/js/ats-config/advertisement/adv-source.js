// =======================================
// Advertisement Source Maintenance Script
// =======================================

"use strict";

var AdvertisementSourcePage = (function () {
    let DataTable = null;

    // =========================
    // 🔹 Initialize DataTable
    // =========================
    const initDataTable = function () {
        DataTable = $('#tblAdvertisementSource').DataTable({
            processing: true,
            responsive: true,
            autoWidth: true,
            destroy: true,
            search: true,
            ajax: {
                url: "/ATSConfig/GetAdvertisementSources",
                type: "GET",
                dataSrc: function (json) {
                    console.log("📦 API Response:", json); // 👀 check console output
                    if (!json || !json.success) {
                        AppUtils.toastMessage("Failed to load Advertisement Sources.", "error");
                        return [];
                    }
                    return json.data; // ✅ this must return the array
                },
                error: function (xhr, status, error) {
                    console.error("❌ DataTable load failed:", error);
                }
            },
            columns: [
                { data: "id", title: "ID", visible: false },
                { data: "name", title: "Source Name" },
                {
                    data: null,
                    title: "Actions",
                    orderable: false,
                    className: "text-center",
                    render: function (data, type, full, meta) {
                        return `
                            <a class="btn btn-sm btn-light-primary action-item" href="#" 
                               data-action="editAdSource" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>`;
                    }
                }
            ],
            dom: `<'row'<'col-sm-12 col-md-6'f>>
                  <'row'<'col-sm-12'tr>>
                  <'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
            pageLength: 10,
            language: {
                processing: `
                    <div style="display:flex;justify-content:center;align-items:center;height:100%;">
                        <i class="fa fa-spinner fa-spin fa-3x text-primary"></i>
                    </div>`
            }

        });

        // ✅ replicate your working “click + action-item” pattern
        DataTable.on("click", "a.action-item", function (e) {
            e.preventDefault();
            let $btn = $(this);
            let action = $btn.data("action");
            let rowData = DataTable.row($btn.parents("tr")).data();

            if (!rowData) {
                AppUtils.toastMessage("⚠️ Unable to fetch row data.", "warning");
                console.warn("⚠️ Missing rowData for clicked button");
                return;
            }

            console.log("🟢 Selected Row Data:", rowData);

            if (action === "editAdSource") {
                renderEditModal(rowData);
            }
        });
    };

    // =========================
    // 🔹 New Advertisement Source Button
    // =========================
    const initNewButton = function () {
        $(document).on("click", "#btnNewAdvertisementSource", function (e) {
            e.preventDefault();

            const modalId = "newAdvertisementSourceModal_" + Date.now();

            const bodyHtml = `
            <form id="${modalId}_frmNewSource">
                <div class="form-group mb-3">
                    <label class="fw-semibold">Source Name</label>
                    <input type="text" id="${modalId}_newSourceName"
                           class="form-control form-control-sm" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Description</label>
                    <textarea id="${modalId}_newDescription"
                              class="form-control form-control-sm" rows="3"></textarea>
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Status</label>
                    <select id="${modalId}_newStatus" class="form-control form-control-sm">
                        <option value="Active">Active</option>
                        <option value="Inactive">Inactive</option>
                    </select>
                </div>
            </form>`;

            AppUtils.loadModal(modalId, {
                title: "<i class='fas fa-plus-circle text-success me-2'></i> New Advertisement Source",
                body: bodyHtml,
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "<i class='fas fa-save me-2'></i> Save",
                        btnClass: "btn btn-success btn-sm",
                        autoDismiss: false,
                        action: async () => {
                            const newName = $(`#${modalId}_newSourceName`).val().trim();
                            const newDesc = $(`#${modalId}_newDescription`).val().trim();
                            const newStatus = $(`#${modalId}_newStatus`).val();

                            if (!newName) {
                                AppUtils.toastMessage("Please enter a source name.", "warning");
                                return;
                            }

                            await AppUtils.ajaxCall({
                                url: "/ATSConfig/AddOrUpdate",
                                type: "POST",
                                data: {
                                    id: 0,
                                    name: newName,
                                    description: newDesc,
                                    status: newStatus
                                },
                                successMessage: `"${newName}" added successfully.`,
                                onSuccess: (response) => {
                                    if (response.success) {
                                        AdvertisementSourcePage.Refresh();
                                    }
                                },
                                onError: (xhr) => {
                                    console.error("❌ Error adding source:", xhr);
                                    AppUtils.toastMessage("Failed to add new source.", "error");
                                }
                            });

                            // ✅ close modal safely
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
        });
    };


    // =========================
    // 🔹 Edit Modal Renderer
    // =========================
    const renderEditModal = function (rowData) {
        const modalId = "editAdvertisementSourceModal_" + Date.now();

        const bodyHtml = `
        <form id="${modalId}_frmEditSource">
            <div class="form-group mb-3">
                <label class="fw-semibold">Source Name</label>
                <input type="text" id="${modalId}_editSourceName"
                       class="form-control form-control-sm"
                       value="${escapeHtml(rowData.name || "")}" required />
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

        // ✅ capture rowData into a local constant
        const selectedRow = rowData;

        AppUtils.loadModal(modalId, {
            title: "<i class='fas fa-edit text-primary me-2'></i> Edit Advertisement Source",
            body: bodyHtml,
            buttons: {
                Save: {
                    Enabled: true,
                    text: "<i class='fas fa-save me-2'></i> Update",
                    btnClass: "btn btn-primary btn-sm",
                    autoDismiss: false,
                    // ✅ closure captures selectedRow
                    action: async () => {
                        const updatedName = $(`#${modalId}_editSourceName`).val().trim();
                        const updatedDesc = $(`#${modalId}_editDescription`).val().trim();
                        const updatedStatus = $(`#${modalId}_editStatus`).val();

                        if (!updatedName) {
                            AppUtils.toastMessage("Please enter a valid source name.", "warning");
                            return;
                        }

                        const payload = {
                            id: selectedRow.id || "", // ✅ from captured variable
                            name: updatedName,
                            description: updatedDesc,
                            status: updatedStatus
                        };

                        await AppUtils.ajaxCall({
                            url: "/ATSConfig/AddOrUpdate",
                            type: "POST",
                            data: payload,
                            successMessage: `"${updatedName}" saved successfully.`,
                            onSuccess: (response) => {
                                if (response.success) {
                                    AdvertisementSourcePage.Refresh();
                                } else {
                                    AppUtils.toastMessage(response.message || "Failed to save record.", "error");
                                }
                            },
                            onError: (xhr) => {
                                console.error("❌ Error saving Advertisement Source:", xhr);
                                AppUtils.toastMessage("Error saving Advertisement Source.", "error");
                            }
                        });

                        try {
                            const modalEl = document.getElementById(modalId);

                            // ✅ Bootstrap 5
                            if (window.bootstrap && bootstrap.Modal && bootstrap.Modal.getInstance) {
                                const bsModal = bootstrap.Modal.getInstance(modalEl);
                                if (bsModal) bsModal.hide();
                            }
                            // ✅ Bootstrap 4 fallback
                            else if (window.jQuery) {
                                $(`#${modalId}`).modal('hide');
                            }
                            else {
                                console.warn("⚠️ No compatible Bootstrap modal instance found.");
                            }
                        } catch (err) {
                            console.error("❌ Error closing modal:", err);
                        }

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
    };


    const escapeHtml = (text) => {
        if (!text) return "";
        return text
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    };

    const init = function () {
        initDataTable();
        initNewButton();
    };

    return {
        init: init,
        Refresh: function () {
            if (DataTable) DataTable.ajax.reload();
        }
    };
})();

$(document).ready(function () {
    AdvertisementSourcePage.init();
});
