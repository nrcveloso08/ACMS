// =======================================
// Job Advertisements Maintenance Script
// =======================================

"use strict";

var JobAdvertisementPage = (function () {
    let DataTable = null;

    // =========================
    // 🔹 Initialize DataTable (Unchanged)
    // =========================
    const initDataTable = function () {
        DataTable = $('#tblJobAdvertisements').DataTable({
            processing: true,
            responsive: true,
            autoWidth: true,
            destroy: true,
            search: true,
            ajax: {
                url: "/ATSConfig/GetJobAdvertisements",
                type: "GET",
                dataSrc: function (json) {
                    console.log("📦 Job Ads API Response:", json);

                    if (json && json.success && Array.isArray(json.message)) {
                        console.log("✅ Parsed Job Ads Count:", json.message.length);
                        console.log("🔍 Sample Record:", json.message[0]);
                        return json.message;
                    }

                    console.warn("⚠️ Unexpected response format:", json);
                    AppUtils.toastMessage("Failed to parse Job Advertisements.", "error");
                    return [];
                },
                error: function (xhr, status, error) {
                    console.error("❌ DataTable load failed:", error);
                }
            },
            columns: [
                { data: "id", title: "ID", visible: false },
                { data: "name", title: "Name" },
                { data: "publishedTitle", title: "Published Title" },
                { data: "languageId", title: "Language" },
                { data: "locationId", title: "Location" },
                { data: "legacyRecruitmentLob", title: "Group" },
                {
                    data: "isPublishedToLiveJobs",
                    title: "Live Jobs Publishing",
                    render: (data) =>
                        data
                            ? `<span class="badge bg-success">Active</span>`
                            : `<span class="badge bg-secondary">Inactive</span>`
                },
                {
                    data: "selfSchedulerAppointmentEnabled",
                    title: "Appointment Page",
                    render: (data) =>
                        data
                            ? `<span class="badge bg-success">Active</span>`
                            : `<span class="badge bg-secondary">Inactive</span>`
                },
                {
                    data: "isSMSEnabled",
                    title: "SMS Service",
                    render: (data) =>
                        data
                            ? `<span class="badge bg-success">Active</span>`
                            : `<span class="badge bg-secondary">Inactive</span>`
                },
                {
                    data: "isReferralEnabled",
                    title: "Referral Link",
                    render: (data) =>
                        data
                            ? `<span class="badge bg-success">Active</span>`
                            : `<span class="badge bg-secondary">Inactive</span>`
                },
                {
                    data: "isSMSOptEnabled",
                    title: "SMS OPT",
                    render: (data) =>
                        data
                            ? `<span class="badge bg-success">Active</span>`
                            : `<span class="badge bg-secondary">Inactive</span>`
                },
                {
                    data: "isDeleted",
                    title: "Status",
                    render: (data) =>
                        !data
                            ? `<span class="badge bg-success">Active</span>`
                            : `<span class="badge bg-secondary">Inactive</span>`
                },
                {
                    data: null,
                    title: "Actions",
                    orderable: false,
                    className: "text-center",
                    render: function () {
                        return `
                            <a class="btn btn-sm btn-light-primary action-item" 
                               data-action="editJobAd" href="#" title="Edit">
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

        // ✅ Handle Edit Click
        DataTable.on("click", "a.action-item", function (e) {
            e.preventDefault();
            const rowData = DataTable.row($(this).parents("tr")).data();
            if (rowData) renderJobAdModal(rowData);
        });
    };

    // =========================
    // 🔹 Render Modal (New/Edit)
    // =========================
    const renderJobAdModal = function (rowData = null) {
        const isEdit = !!rowData;
        const modalId = (isEdit ? "editJobAdModal_" : "newJobAdModal_") + Date.now();

        // ✅ Normalize field casing (convert from camelCase → PascalCase)
        const job = isEdit
            ? {
                Id: rowData.id || rowData.Id || 0,
                Name: rowData.name || rowData.Name || "",
                PublishedTitle: rowData.publishedTitle || rowData.PublishedTitle || "",
                LegacyRecruitmentLob: rowData.legacyRecruitmentLob || rowData.LegacyRecruitmentLob || "",
                IsPublishedToLiveJobs: rowData.isPublishedToLiveJobs ?? rowData.IsPublishedToLiveJobs ?? false,
                SelfSchedulerAppointmentEnabled: rowData.selfSchedulerAppointmentEnabled ?? rowData.SelfSchedulerAppointmentEnabled ?? false,
                IsSMSEnabled: rowData.isSMSEnabled ?? rowData.IsSMSEnabled ?? false,
                IsReferralEnabled: rowData.isReferralEnabled ?? rowData.IsReferralEnabled ?? false,
                IsSMSOptEnabled: rowData.isSMSOptEnabled ?? rowData.IsSMSOptEnabled ?? false,
                HarverVacancyURL: rowData.harverVacancyURL || rowData.HarverVacancyURL || "",
                IsDeleted: rowData.isDeleted ?? rowData.IsDeleted ?? false,
                CatsJobId: rowData.catsJobId || rowData.CatsJobId || "",
                Location: rowData.location || rowData.Location || "",
                Language: rowData.language || rowData.Language || "",
                Address: rowData.address || rowData.Address || "",
                PhoneNumber: rowData.phoneNumber || rowData.PhoneNumber || "",
                ZipCode: rowData.zipCode || rowData.ZipCode || "",
                JobDescription: rowData.jobDescription || rowData.JobDescription || "",
                InstructionHTML: rowData.instructionHTML || rowData.InstructionHTML || ""
            }
            : {
                Id: 0,
                Name: "",
                PublishedTitle: "",
                LegacyRecruitmentLob: "",
                IsPublishedToLiveJobs: false,
                SelfSchedulerAppointmentEnabled: false,
                IsSMSEnabled: false,
                IsReferralEnabled: false,
                IsSMSOptEnabled: false,
                HarverVacancyURL: "",
                IsDeleted: false,
                CatsJobId: "",
                Location: "",
                Language: "",
                Address: "",
                PhoneNumber: "",
                ZipCode: "",
                JobDescription: "",
                InstructionHTML: ""
            };

        const bodyHtml = getJobAdModalBody(job, modalId);

        AppUtils.loadModal(modalId, {
            title: `<i class='fas ${isEdit ? "fa-edit text-primary" : "fa-plus-circle text-success"} me-2'></i>${isEdit ? "Edit Job Advertisement" : "New Job Advertisement"}`,
            body: bodyHtml,
            buttons: {
                Save: {
                    Enabled: true,
                    text: `<i class='fas fa-save me-2'></i>${isEdit ? "Update" : "Save"}`,
                    btnClass: "btn btn-primary btn-sm",
                    autoDismiss: false,
                    action: async () => {
                        const payload = collectJobAdFormData(modalId, job);
                        await AppUtils.ajaxCall({
                            url: "/ATSConfig/JobAdAddOrUpdate",
                            type: "POST",
                            data: payload,
                            successMessage: `"${payload.Name}" saved successfully.`,
                            onSuccess: (response) => {
                                if (response.success) JobAdvertisementPage.Refresh();
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

        // ✅ Keep toggle behavior
        $(document).on("click", `#${modalId} .toggle-flag`, function () {
            const flag = $(this).data("flag");
            job[flag] = !job[flag];
            const isActive = job[flag];
            $(this)
                .removeClass("bg-success bg-danger")
                .addClass(isActive ? "bg-success" : "bg-danger")
                .text(isActive ? "ACTIVE" : "INACTIVE");
        });

        // ✅ Widen modal
        setTimeout(() => {
            const $modalDialog = $(`#${modalId}`).find(".modal-dialog");
            $modalDialog.css({
                "max-width": "900px",
                "width": "90%"
            });
        }, 100);
    };


    // =========================
    // 🔹 Generic Modal Body (Your version)
    // =========================
    function getJobAdModalBody(job, modalId) {
        return `
        <form id="${modalId}_frmJobAd">
            <div class="row mb-2">
                <div class="col-md-3">
                    <label class="fw-semibold">ID</label>
                    <input type="text" class="form-control form-control-sm" value="${job.Id || ""}" readonly>
                </div>
                <div class="col-md-3">
                    <label class="fw-semibold">Group (optional)</label>
                    <input type="text" id="${modalId}_LegacyRecruitmentLob" class="form-control form-control-sm" value="${job.LegacyRecruitmentLob || ""}">
                </div>
                <div class="col-md-3">
                    <label class="fw-semibold">Status</label><br/>
                    <span class="badge ${!job.IsDeleted ? "bg-success" : "bg-secondary"}">
                        ${!job.IsDeleted ? "ACTIVE" : "INACTIVE"}
                    </span>
                </div>
            </div>

<div class="form-group mb-3">
    <label class="fw-semibold d-block mb-2">Feature Toggles</label>
    <div class="d-flex flex-wrap align-items-center gap-3 justify-content-start">
        ${renderToggle(modalId, "IsPublishedToLiveJobs", job.IsPublishedToLiveJobs, "Live Jobs Publishing")}
        ${renderToggle(modalId, "IsSMSEnabled", job.IsSMSEnabled, "Email Parser SMS Invite")}
        ${renderToggle(modalId, "IsReferralEnabled", job.IsReferralEnabled, "Referral Link")}
        ${renderToggle(modalId, "SelfSchedulerAppointmentEnabled", job.SelfSchedulerAppointmentEnabled, "Appointment Page")}
        ${renderToggle(modalId, "IsSMSOptEnabled", job.IsSMSOptEnabled, "SMS OPT")}
    </div>
</div>



            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="fw-semibold">CATS Job ID</label>
                    <input type="text" id="${modalId}_CatsJobId" class="form-control form-control-sm" value="${job.CatsJobId || ""}">
                </div>
                <div class="col-md-4">
                    <label class="fw-semibold">Name</label>
                    <input type="text" id="${modalId}_Name" class="form-control form-control-sm" value="${job.Name || ""}" required>
                </div>
                <div class="col-md-4">
                    <label class="fw-semibold">Published Title</label>
                    <input type="text" id="${modalId}_PublishedTitle" class="form-control form-control-sm" value="${job.PublishedTitle || ""}" required>
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="fw-semibold">Phone Number</label>
                    <input type="text" id="${modalId}_PhoneNumber" class="form-control form-control-sm" value="${job.PhoneNumber || ""}">
                </div>
                <div class="col-md-4">
                    <label class="fw-semibold">Zip Code</label>
                    <input type="text" id="${modalId}_ZipCode" class="form-control form-control-sm" value="${job.ZipCode || ""}">
                </div>
                <div class="col-md-4">
                    <label class="fw-semibold">Harver Vacancy URL</label>
                    <input type="url" id="${modalId}_HarverVacancyURL" class="form-control form-control-sm" value="${job.HarverVacancyURL || ""}">
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label class="fw-semibold">Location</label>
                    <input type="text" id="${modalId}_Location" class="form-control form-control-sm" value="${job.Location || ""}">
                </div>
                <div class="col-md-6">
                    <label class="fw-semibold">Language</label>
                    <input type="text" id="${modalId}_Language" class="form-control form-control-sm" value="${job.Language || ""}">
                </div>
            </div>

            <div class="form-group mb-3">
                <label class="fw-semibold">Address</label>
                <input type="text" id="${modalId}_Address" class="form-control form-control-sm" value="${job.Address || ""}">
            </div>

            <div class="form-group mb-3">
                <label class="fw-semibold">Job Description</label>
                <textarea id="${modalId}_JobDescription" class="form-control form-control-sm" rows="6">${job.JobDescription || ""}</textarea>
            </div>

            <div class="form-group mb-3">
                <label class="fw-semibold">Instruction HTML</label>
                <textarea id="${modalId}_InstructionHTML" class="form-control form-control-sm" rows="4">${job.InstructionHTML || ""}</textarea>
            </div>
        </form>`;
    }

    function renderToggle(modalId, flag, value, label) {
        const isActive =
            value === true || value === "true" || value === 1 || value === "1";

        const badgeClass = isActive ? "bg-success" : "bg-danger";
        const text = isActive ? "ACTIVE" : "INACTIVE";

        return `
        <div class="mb-2">
            <label class="fw-semibold d-block mb-1">${label}</label>
            <span class="toggle-flag badge ${badgeClass} px-3 py-2"
                  data-flag="${flag}"
                  style="cursor: pointer; font-size: 0.85rem; user-select: none;">
                ${text}
            </span>
        </div>`;
    }



    function collectJobAdFormData(modalId, job) {
        return {
            Id: job.Id,
            Name: $(`#${modalId}_Name`).val(),
            PublishedTitle: $(`#${modalId}_PublishedTitle`).val(),
            LegacyRecruitmentLob: $(`#${modalId}_LegacyRecruitmentLob`).val(),
            CatsJobId: $(`#${modalId}_CatsJobId`).val(),
            Location: $(`#${modalId}_Location`).val(),
            Language: $(`#${modalId}_Language`).val(),
            Address: $(`#${modalId}_Address`).val(),
            PhoneNumber: $(`#${modalId}_PhoneNumber`).val(),
            ZipCode: $(`#${modalId}_ZipCode`).val(),
            HarverVacancyURL: $(`#${modalId}_HarverVacancyURL`).val(),
            JobDescription: $(`#${modalId}_JobDescription`).val(),
            InstructionHTML: $(`#${modalId}_InstructionHTML`).val(),
            IsPublishedToLiveJobs: job.IsPublishedToLiveJobs,
            IsSMSEnabled: job.IsSMSEnabled,
            IsReferralEnabled: job.IsReferralEnabled,
            IsSMSOptEnabled: job.IsSMSOptEnabled,
            SelfSchedulerAppointmentEnabled: job.SelfSchedulerAppointmentEnabled,
            IsDeleted: job.IsDeleted
        };
    }

    const init = function () {
        initDataTable();
        $(document).on("click", "#btnNewJobAd", function (e) {
            e.preventDefault();
            renderJobAdModal();
        });
    };

    return {
        init: init,
        Refresh: function () {
            if (DataTable) DataTable.ajax.reload();
        }
    };
})();

$(document).ready(function () {
    JobAdvertisementPage.init();
});
