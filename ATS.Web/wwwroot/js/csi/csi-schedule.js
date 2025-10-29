// ================================
// CSI Schedule Page Script
// Handles CSI Schedule creation modal, form, and save process
// ================================

"use strict";

var CsiSchedulePage = (function () {

    // =========================
    // 🔹 1. Private Functions
    // =========================

    // --- Modal Body Builder ---
    function getCsiModalBody() {
        return `
            <form id="frmCsiSchedule">
                <div class="form-group">
                    <label>Location</label>
                    <select id="LocationId" name="LocationId" class="form-control" required>
                        <option value="">Select a Location</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Program</label>
                    <select id="ProgramId" name="ProgramId" class="form-control" required>
                        <option value="">Select a Program</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Schedule</label>
                    <div class="d-flex">
                        <input type="datetime-local" id="StartDateTime" name="StartDateTime" class="form-control mr-2" required />
                        <input type="datetime-local" id="EndDateTime" name="EndDateTime" class="form-control" required />
                    </div>
                </div>
                <div class="form-group">
                    <label>Capacity</label>
                    <input type="number" id="Capacity" name="Capacity" class="form-control" value="0" required min="1" />
                </div>
            </form>
        `;
    }

    // --- Save CSI Schedule Handler ---
    function saveCsiSchedule() {
        if (!AppUtils.validateForm("#frmCsiSchedule")) {
            return;
        }

        const data = {
            LocationId: $("#LocationId").val(),
            ProgramId: $("#ProgramId").val(),
            StartDateTime: $("#StartDateTime").val(),
            EndDateTime: $("#EndDateTime").val(),
            Capacity: $("#Capacity").val()
        };

        AppUtils.ajaxCall({
            url: "/CsiSchedule/Create",
            type: "POST",
            data: data,
            successMessage: "CSI Schedule created successfully!",
            onSuccess: function () {
                $("#csiScheduleModal").modal("hide");
                loadCsiScheduleList();
            }
        });
    }

    // --- Reload Schedule List (Placeholder) ---
    function loadCsiScheduleList() {
        console.log("Reload CSI schedule table...");
        // Future: Integrate DataTables or AJAX refresh
    }

    // --- Bind Events ---
    function bindEvents() {
        // Create New CSI Schedule Button
        $(document).on("click", "#btnCreateCsiSchedule", function () {
            AppUtils.loadModal("csiScheduleModal", {
                title: "Scheduler",
                body: getCsiModalBody(),
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "Save",
                        btnClass: "btn btn-primary",
                        action: saveCsiSchedule
                    },
                    Cancel: {
                        Enabled: true,
                        text: "Cancel",
                        btnClass: "btn btn-secondary",
                        autoDismiss: true
                    }
                },
                settings: "modal-dialog-centered modal-md"
            });
        });
    }

    // =========================
    // 🔹 2. Public Init Function
    // =========================
    function init() {
        bindEvents();
        console.log("✅ CSI Schedule page initialized");
    }

    // Expose public methods
    return {
        init: init
    };

})();

// =========================
// 🔹 3. Initialize on Ready
// =========================
$(document).ready(function () {
    CsiSchedulePage.init();
});
