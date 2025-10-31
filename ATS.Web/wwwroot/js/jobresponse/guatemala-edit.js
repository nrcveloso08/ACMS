"use strict";

// 👇 Make wizard global so we can control it from modal events
var wizard;

var GuatemalaEditPage = (function () {
    var currentStep = 1;
    var tblDocuments = null;

    function init() {
        wizard = new KTWizard("kt_wizard", {
            startStep: 1,
            clickableSteps: true
        });

        loadStep(1);
        toggleSubmit(1);

        wizard.on("change", function () {
            setTimeout(function () {
                var step = wizard.getStep();
                if (step !== currentStep) {
                    currentStep = step;
                    loadStep(step);
                    toggleSubmit(step);
                }
            }, 150);
        });

        wizard.on("click", function () {
            setTimeout(function () {
                var step = wizard.getStep();
                if (step !== currentStep) {
                    currentStep = step;
                    loadStep(step);
                    toggleSubmit(step);
                }
            }, 150);
        });
    }

    // ===================================
    // Load step content dynamically
    // ===================================
    function loadStep(step) {
        var container = $("#Step" + step + "Container");
        if (!container.data("loaded")) {
            container.html('<div class="text-center py-5">Loading...</div>');
            $.get("/JobResponse/LoadStep?step=" + step)
                .done(function (data) {
                    container.html(data);
                    container.data("loaded", true);

                    // ✅ Initialize DataTable if step 6 is loaded
                    if (step === 6) initDocumentsTable();
                    if (step === 7) initReferencesForm();
                })
                .fail(function (xhr) {
                    container.html(
                        '<div class="text-danger text-center py-5">Failed to load content. ' +
                        xhr.status +
                        "</div>"
                    );
                });
        }
    }

    // ===================================
    // Toggle submit visibility
    // ===================================
    function toggleSubmit(step) {
        step = parseInt(step);
        const $submit = $("#btnSubmitFinal");
        const $next = $('[data-wizard-type="action-next"]');

        if (step === 7) {
            $submit.removeClass("d-none").fadeIn(200);
            $next.hide();
        } else {
            $submit.addClass("d-none").hide();
            $next.show();
        }
    }

    // ===================================
    // Initialize Documents DataTable
    // ===================================
    function initDocumentsTable() {
        if (!$("#tblDocuments").length) return;

        if ($.fn.DataTable.isDataTable("#tblDocuments")) {
            $("#tblDocuments").DataTable().destroy();
            $("#tblDocuments tbody").empty();
        }

        tblDocuments = $("#tblDocuments").DataTable({
            ajax: {
                url: "/JobResponse/GetDocuments",
                type: "GET",
                dataSrc: ""
            },
            columns: [
                { data: "description" },
                { data: "content" },
                {
                    data: "id",
                    className: "text-center",
                    render: function (data, type, row) {
                        return `
                            <button class="btn btn-sm btn-primary btn-edit-doc" 
                                data-id="${row.id}" 
                                data-name="${row.description}">
                                Edit
                            </button>
                        `;
                    }
                }
            ],
            ordering: false,
            paging: false,
            searching: false,
            info: false,
            language: {
                emptyTable: "No se encontraron documentos para mostrar."
            }
        });
    }

    // ===================================
    // Reload Documents Table
    // ===================================
    function reloadDocuments() {
        if (tblDocuments) tblDocuments.ajax.reload(null, false);
    }

    // ===================================
    // Modal Handler
    // ===================================
    // ===================================
    // Modal Handler (Step 6 Edit)
    // ===================================
    // ===================================
    // Document Edit Modal Handler
    // ===================================
    $(document).on("click", ".btn-edit-doc", function (e) {
        e.preventDefault();

        const docId = $(this).data("id");
        const docName = $(this).data("name");

        const modalBody = `
        <div class="form-group mb-3">
            <label><strong>Subir Archivo</strong></label>
            <div class="input-group">
                <span class="input-group-text bg-success text-white">Upload</span>
                <input type="file" id="docFileInput" class="form-control">
            </div>
        </div>
    `;

        // Create modal via AppUtils
        AppUtils.loadModal("updateDocumentModal", {
            title: `Actualizar Documento - ${docName || "Desconocido"}`,
            body: modalBody,
            settings: "modal-dialog-centered modal-md",
            buttons: {
                Close: {
                    Enabled: true,
                    text: "Cerrar",
                    btnClass: "btn-danger",
                    autoDismiss: true
                },
                Save: {
                    Enabled: true,
                    text: "Guardar",
                    btnClass: "btn-success",
                    action: function () {
                        const fileInput = $("#docFileInput")[0];
                        if (!fileInput.files.length) {
                            AppUtils.toastMessage("Seleccione un archivo antes de guardar.", "warning");
                            return;
                        }

                        const formData = new FormData();
                        formData.append("documentId", docId);
                        formData.append("file", fileInput.files[0]);

                        $.ajax({
                            url: "/JobResponse/UploadDocument",
                            type: "POST",
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function () {
                                AppUtils.toastMessage("Documento actualizado correctamente.", "success");
                                $("#updateDocumentModal").modal("hide");
                                if (typeof tblDocuments !== "undefined") tblDocuments.ajax.reload(null, false);
                            },
                            error: function () {
                                AppUtils.toastMessage("Error al subir el documento.", "error");
                            }
                        });
                    }
                }
            }
        });

        // ✅ Ensure modal appears above wizard and prevent wizard reset
        setTimeout(() => {
            const $modal = $("#updateDocumentModal");

            // Move modal to body if misplaced
            if (!$modal.parent().is("body")) {
                $modal.detach().appendTo("body");
            }

            $modal.modal({
                backdrop: "static",
                keyboard: false
            });

            $modal.modal("show");
        }, 100);

        // Prevent wizard focus reset when modal interacts
        $(document).off("focusin.modalFix").on("focusin.modalFix", function (e) {
            if ($(e.target).closest(".modal").length) {
                e.stopImmediatePropagation();
            }
        });
    });

    // ===================================
    // Initialize References Form (Step 7)
    // ===================================
    function initReferencesForm() {
        const $form = $("#frmReferences");
        if (!$form.length) return; // skip if not loaded

        console.log("✅ References form initialized");

        // Handle form submit
        $form.on("submit", function (e) {
            e.preventDefault();

            const formData = $form.serialize();
            console.log("Submitting reference data:", formData);

            $.ajax({
                url: "/JobResponse/SaveReferences",
                type: "POST",
                data: formData,
                success: function (res) {
                    AppUtils.toastMessage("References saved successfully!", "success");
                },
                error: function () {
                    AppUtils.toastMessage("Failed to save references.", "danger");
                }
            });
        });
    }


    return { init };
})();

jQuery(document).ready(function () {
    GuatemalaEditPage.init();

    // ==========================
    // Dynamic Language Handlers
    // ==========================
    $(document).on("click", "#addLanguage", function () {
        const newRow = `
            <div class="form-row align-items-center mb-3 language-row">
                <div class="form-group col-md-3">
                    <select class="form-control" name="LanguageName">
                        <option value="">Seleccione un idioma</option>
                        <option value="English">English</option>
                        <option value="Spanish">Spanish</option>
                        <option value="French">French</option>
                        <option value="German">German</option>
                        <option value="Mandarin">Mandarin</option>
                        <option value="Japanese">Japanese</option>
                        <option value="Other">Otro</option>
                    </select>
                </div>

                <div class="form-group col-md-3">
                    <select class="form-control" name="LanguageLevel">
                        <option value="">Seleccione nivel</option>
                        <option value="Basico">Básico</option>
                        <option value="Intermedio">Intermedio</option>
                        <option value="Avanzado">Avanzado</option>
                        <option value="Nativo">Nativo</option>
                    </select>
                </div>

                <div class="form-group col-md-3 d-flex align-items-center">
                    <div class="form-check mr-3">
                        <input class="form-check-input" type="checkbox" name="CanSpeak">
                        <label class="form-check-label">Hablar</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" name="CanWrite">
                        <label class="form-check-label">Escrito</label>
                    </div>
                </div>

                <div class="form-group col-md-1 text-center">
                    <button type="button" class="btn btn-danger btn-sm remove-language">
                        <i class="fa fa-times"></i>
                    </button>
                </div>
            </div>
        `;
        $("#languageContainer").append(newRow);
    });

    $(document).on("click", ".remove-language", function () {
        $(this).closest(".language-row").remove();
    });

    // ✅ Fix wizard reset when Bootstrap modal focuses elements
    $(document).on("focusin", function (e) {
        if ($(e.target).closest(".modal").length) {
            e.stopImmediatePropagation();
        }
    });
});
