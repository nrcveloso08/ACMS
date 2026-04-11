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
                url: "/api/vacancy/GetAll",
                type: "GET",
                dataSrc: function (json) {
                    // ATSResult may wrap the data as a JSON string, or just be array
                    let raw = json?.data || json?.Data || json;
                    let data = [];
                    if (typeof raw === "string") {
                        try { data = JSON.parse(raw); } catch (e) { data = []; }
                    } else if (Array.isArray(raw)) {
                        data = raw;
                    }
                    return data;
                },
                error: function (xhr, status, error) {
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
                renderEditModal(rowData);
            }
        });
    };

    // =============================
    // Dynamic Add/Edit Modal
    // =============================
    function renderEditModal(rowData) {
        const isEdit = !!rowData;
        const modalId = (isEdit ? "editVacancyModal_" : "newVacancyModal_") + Date.now();
        const bodyHtml = `
            <form id="${modalId}_frmVacancy">
                <div class="form-group mb-3">
                    <label class="fw-semibold">Vacancy Name</label>
                    <input type="text" id="${modalId}_vacancyName" class="form-control form-control-sm" value="${isEdit ? rowData.Name : ""}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Vacancy Code</label>
                    <input type="text" id="${modalId}_vacancyCode" class="form-control form-control-sm" value="${isEdit ? rowData.VacancyId : ""}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Job Advertisement ID</label>
                    <input type="number" id="${modalId}_jobAdId" class="form-control form-control-sm" value="${isEdit ? rowData.JobAdvertisement_Id : ""}" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Status</label>
                    <select id="${modalId}_isActive" class="form-select form-select-sm">
                        <option value="active" ${isEdit && !rowData.IsDeleted ? "selected" : ""}>Active</option>
                        <option value="inactive" ${isEdit && rowData.IsDeleted ? "selected" : ""}>Inactive</option>
                    </select>
                </div>
            </form>
        `;

        AppUtils.Modal({
            id: modalId,
            title: isEdit ? "Edit Vacancy" : "New Vacancy",
            size: "modal-md",
            body: bodyHtml,
            buttons: {
                Save: {
                    Enabled: true,
                    text: "<i class='fas fa-save me-2'></i> Save",
                    btnClass: "btn btn-primary btn-sm",
                    autoDismiss: false,
                    action: function () {
                        const vacancy = {
                            Id: isEdit ? rowData.Id : 0,
                            Name: $(`#${modalId}_vacancyName`).val().trim(),
                            VacancyId: $(`#${modalId}_vacancyCode`).val().trim(),
                            JobAdvertisement_Id: parseInt($(`#${modalId}_jobAdId`).val(), 10) || 0,
                            IsDeleted: $(`#${modalId}_isActive`).val() === "inactive"
                        };
                        $.ajax({
                            url: "/api/vacancy/AddOrUpdate",
                            type: "POST",
                            contentType: "application/json",
                            data: JSON.stringify(vacancy),
                            success: function () {
                                AppUtils.toastMessage("Vacancy saved successfully!", "success");
                                DataTable.ajax.reload(null, false);
                                $(`#${modalId}`).modal("hide");
                            },
                            error: function () {
                                AppUtils.toastMessage("An error occurred.", "error");
                            }
                        });
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

    // =============================
    // New Vacancy Button
    // =============================
    const initNewButton = function () {
        $(document).on("click", "#btnNewVacancy", function (e) {
            e.preventDefault();
            renderEditModal(null);
        });
    };

    // =============================
    // Public API
    // =============================
    return {
        Init: function () {
            initDataTable();
            initNewButton();
        }
    };
})();

$(document).ready(function () {
    VacanciesPage.Init();
});