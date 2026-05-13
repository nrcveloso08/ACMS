"use strict";

var VacanciesPage = (function () {
    let DataTable = null;

    // =============================
    // Initialize DataTable
    // =============================
    const initDataTable = function () {
        DataTable = $('#tblVacancies').DataTable({
            processing: true,
            responsive: true,
            autoWidth: true,
            destroy: true,
            ajax: {
                url: "/ATSConfig/VacancyGetAll",
                type: "GET",
                dataSrc: function (json) {
                    let raw = json?.data || json?.Data || json;
                    let data = [];
                    if (typeof raw === "string") {
                        try { data = JSON.parse(raw); } catch (e) { data = []; }
                    } else if (Array.isArray(raw)) {
                        data = raw;
                    }
                    return data;
                },
                error: function () {
                    AppUtils.toastMessage("Failed to load vacancies.", "error");
                    return [];
                }
            },
            columns: [
                { data: "Id", title: "ID", visible: false },
                { data: "Name", title: "Vacancy Name" },
                { data: "VacancyId", title: "Vacancy Code" },
                { data: "JobAdvertisement_Id", title: "Job Advertisement ID" },
                {
                    data: "IsDeleted", title: "Status",
                    render: isDeleted => isDeleted ? "<span class='badge bg-secondary'>Inactive</span>" : "<span class='badge bg-success'>Active</span>"
                },
                {
                    data: null,
                    title: "Actions",
                    orderable: false,
                    className: "text-center",
                    render: function () {
                        return `<a class="btn btn-sm btn-light-primary action-item" href="#" data-action="editVacancy" title="Edit"><i class="fa fa-edit"></i></a>`;
                    }
                }
            ],
            pageLength: 10
        });

        DataTable.on("click", "a.action-item", function (e) {
            e.preventDefault();
            let $btn = $(this);
            let action = $btn.data("action");
            let rowData = DataTable.row($btn.parents("tr")).data();

            if (!rowData) {
                AppUtils.toastMessage("Unable to fetch row data.", "warning");
                return;
            }
            if (action === "editVacancy") {
                renderVacancyModal(rowData);
            }
        });
    };

    // =============================
    // New Vacancy Button
    // =============================
    const initNewButton = function () {
        $(document).on("click", "#btnNewVacancy", function (e) {
            e.preventDefault();
            renderVacancyModal(null);
        });
    };

    // =============================
    // Modal Renderer (Add/Edit)
    // =============================
    function renderVacancyModal(rowData) {
        const isEdit = !!rowData;
        const modalId = (isEdit ? "editVacancyModal_" : "newVacancyModal_") + Date.now();

        const vacancy = isEdit
            ? {
                Id: rowData.Id || 0,
                Name: rowData.Name || "",
                VacancyId: rowData.VacancyId || "",
                JobAdvertisement_Id: rowData.JobAdvertisement_Id || "",
                IsDeleted: !!rowData.IsDeleted
            }
            : {
                Id: 0,
                Name: "",
                VacancyId: "",
                JobAdvertisement_Id: "",
                IsDeleted: false
            };

        const bodyHtml = `
            <form id="${modalId}_frmVacancy">
                <div class="form-group mb-3">
                    <label class="fw-semibold">Vacancy Name</label>
                    <input type="text" id="${modalId}_vacancyName" class="form-control form-control-sm" value="${escapeHtml(vacancy.Name)}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Vacancy Code</label>
                    <input type="text" id="${modalId}_vacancyCode" class="form-control form-control-sm" value="${escapeHtml(vacancy.VacancyId)}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Job Advertisement ID</label>
                    <input type="number" id="${modalId}_jobAdId" class="form-control form-control-sm" value="${escapeHtml(vacancy.JobAdvertisement_Id)}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Status</label>
                    <select id="${modalId}_isActive" class="form-select form-select-sm">
                        <option value="active" ${!vacancy.IsDeleted ? "selected" : ""}>Active</option>
                        <option value="inactive" ${vacancy.IsDeleted ? "selected" : ""}>Inactive</option>
                    </select>
                </div>
            </form>
        `;

        AppUtils.loadModal(modalId, {
            title: isEdit ? "<i class='fas fa-edit text-primary me-2'></i> Edit Vacancy" : "<i class='fas fa-plus-circle text-success me-2'></i> New Vacancy",
            body: bodyHtml,
            buttons: {
                Save: {
                    Enabled: true,
                    text: "<i class='fas fa-save me-2'></i> Save",
                    btnClass: "btn btn-primary btn-sm",
                    autoDismiss: false,
                    action: async () => {
                        const name = $(`#${modalId}_vacancyName`).val().trim();
                        const code = $(`#${modalId}_vacancyCode`).val().trim();
                        const jobAdId = parseInt($(`#${modalId}_jobAdId`).val(), 10) || 0;
                        const isDeleted = $(`#${modalId}_isActive`).val() === "inactive";

                        if (!name || !code || !jobAdId) {
                            AppUtils.toastMessage("Please complete all required fields.", "warning");
                            return;
                        }

                        const payload = {
                            Id: vacancy.Id,
                            Name: name,
                            VacancyId: code,
                            JobAdvertisement_Id: jobAdId,
                            IsDeleted: isDeleted
                        };

                        await AppUtils.ajaxCall({
                            url: "/ATSConfig/VacancyAddOrUpdate",
                            type: "POST",
                            contentType: "application/json",
                            data: JSON.stringify(payload),
                            successMessage: `"${name}" saved successfully.`,
                            onSuccess: (res) => {
                                if (res && (res.success || res.Success)) {
                                    VacanciesPage.Refresh();
                                }
                            },
                            onError: (xhr) => {
                                console.error("❌ Error Details:", xhr.responseText);
                                AppUtils.toastMessage("An error occurred while saving the vacancy.", "error");
                            }
                        });

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
    }

function escapeHtml(text) {
    if (text === null || text === undefined) return "";
    if (typeof text !== "string") text = text.toString();
    return text
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

// =============================
// Public API
// =============================
return {
    Init: function () {
        initDataTable();
        initNewButton();
    },
    Refresh: function () {
        if (DataTable) DataTable.ajax.reload();
    }
};
}) ();

$(document).ready(function () {
    VacanciesPage.Init();
});