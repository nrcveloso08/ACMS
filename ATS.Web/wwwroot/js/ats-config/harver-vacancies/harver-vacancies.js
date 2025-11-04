// =======================================
// Harver Vacancies Maintenance Script (Final Fixed and Working)
// =======================================

"use strict";

var HarverVacanciesPage = (function () {
    let DataTable = null;

    // =========================
    // 🔹 Initialize DataTable
    // =========================
    const initDataTable = function () {
        DataTable = $('#tblHarverVacancies').DataTable({
            processing: true,
            responsive: true,
            autoWidth: true,
            destroy: true,
            search: true,
            ajax: {
                url: "/ATSConfig/GetHarverVacancies",
                type: "GET",
                dataSrc: function (json) {
                    console.log("📦 API Response:", json);
                    let raw = json?.data || json?.Data || json;
                    let data = [];

                    if (typeof raw === "string") {
                        try { data = JSON.parse(raw); } catch (e) { console.error("❌ JSON parse failed:", e); }
                    } else if (Array.isArray(raw)) {
                        data = raw;
                    } else if (raw && typeof raw === "object" && typeof raw.data === "string") {
                        try { data = JSON.parse(raw.data); } catch (e) { console.error("❌ Nested JSON parse failed:", e); }
                    }

                    return data;
                },
                error: function (xhr, status, error) {
                    console.error("❌ DataTable load failed:", error);
                    AppUtils.toastMessage("Failed to load Harver Vacancies.", "error");
                }
            },
            columns: [
                { data: "Id", title: "ID", visible: false },
                { data: "Name", title: "Vacancy Name" },
                { data: "VacancyId", title: "Vacancy Code" },
                { data: "JobAdvertisement_Id", title: "Job Advertisement ID" },
                {
                    data: "IsDeleted",
                    title: "Status",
                    render: function (data) {
                        const isDeleted = data === true;
                        const color = isDeleted ? "secondary" : "success";
                        const label = isDeleted ? "Inactive" : "Active";
                        return `<span class="badge bg-${color}">${label}</span>`;
                    }
                },
                {
                    data: null,
                    title: "Actions",
                    orderable: false,
                    className: "text-center",
                    render: function () {
                        return `
                            <a class="btn btn-sm btn-light-primary action-item" href="#" 
                               data-action="editVacancy" title="Edit">
                                <i class="fa fa-edit"></i>
                            </a>`;
                    }
                }
            ],
            dom: `<'row'<'col-sm-12 col-md-6'f>>
                  <'row'<'col-sm-12'tr>>
                  <'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
            pageLength: 10
        });

        // ✅ Attach Edit click (inside scope)
        DataTable.on("click", "a.action-item", function (e) {
            e.preventDefault();
            const $btn = $(this);
            const action = $btn.data("action");
            const rowData = DataTable.row($btn.parents("tr")).data();

            if (!rowData) {
                console.warn("⚠️ Missing rowData for clicked row");
                return;
            }

            console.log("🟢 Selected Row Data:", rowData);

            if (action === "editVacancy") {
                renderEditModal(rowData);
            }
        });
    };

    // =========================
    // 🔹 New Button
    // =========================
    // =========================
    // 🔹 New Button (with Job Advertisement Dropdown)
    // =========================
    const initNewButton = function () {
        $(document).on("click", "#btnNewVacancy", async function (e) {
            e.preventDefault();

            const modalId = "newHarverVacancyModal_" + Date.now();
            const bodyHtml = `
            <form id="${modalId}_frmNewVacancy">
                <div class="form-group mb-3">
                    <label class="fw-semibold">Vacancy Code</label>
                    <input type="text" id="${modalId}_newVacancyCode" class="form-control form-control-sm" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Vacancy Name</label>
                    <input type="text" id="${modalId}_newVacancyName" class="form-control form-control-sm" required />
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Job Advertisement</label>
                    <select id="${modalId}_newJobAdId" class="form-select form-select-sm form-control">
                        <option value="">Loading...</option>
                    </select>
                </div>
                <div class="form-group mb-3">
                    <label class="fw-semibold">Status</label>
                    <select id="${modalId}_newStatus" class="form-select form-select-sm">
                        <option value="Active">Active</option>
                        <option value="Inactive">Inactive</option>
                    </select>
                </div>
            </form>`;

            AppUtils.loadModal(modalId, {
                title: "<i class='fas fa-plus-circle text-success me-2'></i> New Harver Vacancy",
                body: bodyHtml,
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "<i class='fas fa-save me-2'></i> Save",
                        btnClass: "btn btn-success btn-sm",
                        autoDismiss: false,
                        action: async () => {
                            const vacancyCode = $(`#${modalId}_newVacancyCode`).val().trim();
                            const vacancyName = $(`#${modalId}_newVacancyName`).val().trim();
                            const jobAdIdValue = $(`#${modalId}_newJobAdId`).val();
                            const parsedJobAdId = jobAdIdValue ? parseInt(jobAdIdValue, 10) : null;
                            const status = $(`#${modalId}_newStatus`).val();

                            if (!vacancyCode || !vacancyName || !parsedJobAdId) {
                                AppUtils.toastMessage("Please complete all required fields.", "warning");
                                return;
                            }

                            const payload = {
                                Id: 0,
                                VacancyId: vacancyCode,
                                Name: vacancyName,
                                JobAdvertisement_Id: parsedJobAdId,
                                IsDeleted: (status === "Inactive")
                            };

                            console.log("📤 Sending new vacancy payload:", payload);

                            await AppUtils.ajaxCall({
                                url: "/ATSConfig/AddOrUpdate",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                data: JSON.stringify(payload),
                                successMessage: `"${vacancyName}" added successfully.`,
                                onSuccess: (res) => {
                                    if (res.success) HarverVacanciesPage.Refresh();
                                },
                                onError: (xhr) => {
                                    console.error("❌ Error adding vacancy:", xhr);
                                    AppUtils.toastMessage("Failed to add vacancy.", "error");
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

            // ✅ Fetch and populate Job Advertisements dropdown
            try {
                const jobAdsResponse = await $.ajax({
                    url: "/ATSConfig/GetJobAdvertisements",
                    type: "GET"
                });

                console.log("📄 Job Ads Response (New Modal):", jobAdsResponse);

                // Extract actual job ads list (based on your backend structure)
                let jobAds = [];
                if (Array.isArray(jobAdsResponse?.message)) {
                    jobAds = jobAdsResponse.message;
                } else if (Array.isArray(jobAdsResponse?.data)) {
                    jobAds = jobAdsResponse.data;
                } else if (Array.isArray(jobAdsResponse?.Data)) {
                    jobAds = jobAdsResponse.Data;
                } else {
                    jobAds = jobAdsResponse;
                }

                const dropdown = $(`#${modalId}_newJobAdId`);
                dropdown.empty();

                if (Array.isArray(jobAds) && jobAds.length > 0) {
                    jobAds.forEach(ad => {
                        const id = ad.Id || ad.id || "";
                        const displayText = `[${ad.CATSJobId || ad.catsJobId || id}] ${ad.Name || ad.name || ad.PublishedTitle || ad.publishedTitle || "Job Advertisement"}`;

                        dropdown.append(`
                        <option value="${id}">
                            ${escapeHtml(displayText)}
                        </option>
                    `);
                    });
                } else {
                    dropdown.append(`<option value="">No Job Advertisements found</option>`);
                }
            } catch (err) {
                console.error("❌ Failed to load Job Advertisements:", err);
                $(`#${modalId}_newJobAdId`)
                    .html('<option value="">Failed to load options</option>');
            }
        });
    };


    // =========================
    // 🔹 Edit Modal Renderer (with Job Advertisement Dropdown)
    // =========================
    const renderEditModal = async function (rowData) {
        console.log("📦 renderEditModal triggered!");
        const modalId = "editHarverVacancyModal_" + Date.now();

        // Build initial modal layout (dropdown placeholder)
        const bodyHtml = `
        <form id="${modalId}_frmEditVacancy">
            <div class="form-group mb-3">
                <label class="fw-semibold">Vacancy Code</label>
                <input type="text" id="${modalId}_editVacancyCode" class="form-control form-control-sm"
                    value="${escapeHtml(rowData.VacancyId || "")}" required />
            </div>
            <div class="form-group mb-3">
                <label class="fw-semibold">Vacancy Name</label>
                <input type="text" id="${modalId}_editVacancyName" class="form-control form-control-sm"
                    value="${escapeHtml(rowData.Name || "")}" required />
            </div>
            <div class="form-group mb-3">
                <label class="fw-semibold">Job Advertisement</label>
                <select id="${modalId}_editJobAdId" class="form-select form-select-sm form-control">
                    <option value="">Loading...</option>
                </select>
            </div>
            <div class="form-group mb-3">
                <label class="fw-semibold">Status</label>
                <select id="${modalId}_editStatus" class="form-select form-select-sm form-control">
                    <option value="Active" ${rowData.IsDeleted ? "" : "selected"}>Active</option>
                    <option value="Inactive" ${rowData.IsDeleted ? "selected" : ""}>Inactive</option>
                </select>
            </div>
        </form>`;

        // Render modal first
        AppUtils.loadModal(modalId, {
            title: "<i class='fas fa-edit text-primary me-2'></i> Edit Harver Vacancy",
            body: bodyHtml,
            buttons: {
                Save: {
                    Enabled: true,
                    text: "<i class='fas fa-save me-2'></i> Update",
                    btnClass: "btn btn-primary btn-sm",
                    autoDismiss: false,
                    action: async () => {
                        const jobAdIdValue = $(`#${modalId}_editJobAdId`).val();
                        const parsedJobAdId = jobAdIdValue ? parseInt(jobAdIdValue, 10) : null;

                        const payload = {
                            Id: rowData.Id,
                            VacancyId: $(`#${modalId}_editVacancyCode`).val().trim(),
                            Name: $(`#${modalId}_editVacancyName`).val().trim(),
                            JobAdvertisement_Id: $(`#${modalId}_editJobAdId`).val(),
                            IsDeleted: $(`#${modalId}_editStatus`).val() === "Inactive"
                        };

                        console.log("📤 Sending Payload to AddOrUpdate:", payload);

                        await $.ajax({
                            url: "/ATSConfig/HarverVacancyAddOrUpdate",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({
                                Id: rowData.Id,
                                Name: $(`#${modalId}_editVacancyName`).val().trim(),
                                VacancyId: $(`#${modalId}_editVacancyCode`).val().trim(),
                                JobAdvertisement_Id: parseInt($(`#${modalId}_editJobAdId`).val(), 10),
                                IsDeleted: $(`#${modalId}_editStatus`).val() === "Inactive"
                            }),
                            success: function (res) {
                                console.log("✅ Response:", res);
                                if (res.success) {
                                    AppUtils.toastMessage("Vacancy updated successfully!", "success");
                                    HarverVacanciesPage.Refresh();
                                } else {
                                    AppUtils.toastMessage(res.message || "Update failed", "warning");
                                }
                            },
                            error: function (xhr, status, err) {
                                console.error("❌ Update failed:", err);
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

        // ✅ Fetch and populate Job Advertisements dropdown
        try {
            const jobAdsResponse = await $.ajax({
                url: "/ATSConfig/GetJobAdvertisements",
                type: "GET"
            });

            console.log("📄 Raw Job Ads Response:", jobAdsResponse);

            // ✅ Extract job ads array from the response
            let jobAds = [];

            if (Array.isArray(jobAdsResponse?.message)) {
                jobAds = jobAdsResponse.message; // ← this is where the actual array is
            } else if (Array.isArray(jobAdsResponse?.data)) {
                jobAds = jobAdsResponse.data;
            } else if (Array.isArray(jobAdsResponse?.Data)) {
                jobAds = jobAdsResponse.Data;
            } else {
                jobAds = jobAdsResponse;
            }

            console.log("📦 Normalized Job Ads (final):", jobAds);

            const dropdown = $(`#${modalId}_editJobAdId`);
            dropdown.empty();

            if (Array.isArray(jobAds) && jobAds.length > 0) {
                jobAds.forEach(ad => {
                    const id = ad.Id || ad.id || "";
                    const displayText = `[${ad.CATSJobId || ad.catsJobId || id}] ${ad.Name || ad.name || ad.PublishedTitle || ad.publishedTitle || "Job Advertisement"}`;

                    dropdown.append(`
                        <option value="${id}" ${id == rowData.JobAdvertisement_Id ? "selected" : ""}>
                            ${escapeHtml(displayText)}
                        </option>
                    `);
                });
            } else {
                dropdown.append(`<option value="">No Job Advertisements found</option>`);
            }
        } catch (err) {
            console.error("❌ Failed to load Job Advertisements:", err);
            $(`#${modalId}_editJobAdId`)
                .html('<option value="">Failed to load options</option>');
        }



    };


    const escapeHtml = (text) => {
        if (text === null || text === undefined) return "";
        if (typeof text !== "string") text = text.toString();

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
        Refresh: () => { if (DataTable) DataTable.ajax.reload(); }
    };
})();

$(document).ready(function () {
    HarverVacanciesPage.init();
});
