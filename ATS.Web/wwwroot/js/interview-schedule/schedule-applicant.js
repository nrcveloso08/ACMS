﻿// ================================
// Schedule Applicant Script
// Handles Scheduling Modal and Candidate List Display
// ================================

"use strict";

var ScheduleApplicantPage = (function () {

    // =========================
    // 🔹 1. Private Functions
    // =========================

    // --- Build Modal Body ---
    function getModalBody() {
        return `
            <div class="mb-4">
                <div class="input-group">
                    <span class="input-group-text bg-light">
                        <i class="ki-outline ki-magnifier fs-3 text-muted"></i>
                    </span>
                    <input type="text" id="candidateSearch" class="form-control" placeholder="Search..." />
                </div>
            </div>

            <div class="table-responsive">
                <table id="scheduleCandidateTable" class="table table-striped table-bordered align-middle w-100">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email Address</th>
                            <th>Phone Number</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Dynamically loaded -->
                    </tbody>
                </table>
            </div>
        `;
    }

    // --- Load Modal ---
    function openScheduleModal() {
        AppUtils.loadModal("scheduleApplicantModal", {
            title: "Schedule Candidate",
            body: getModalBody(),
            buttons: {
                Close: {
                    Enabled: true,
                    text: "Close",
                    btnClass: "btn btn-light-danger",
                    autoDismiss: true
                },
                Schedule: {
                    Enabled: true,
                    text: "Schedule",
                    btnClass: "btn btn-primary",
                    autoDismiss: false,
                    action: function () {
                        alert("Schedule button clicked!");
                    }
                }
            },
            settings: "modal-dialog-centered modal-xl"
        });

        // Initialize DataTable after modal content renders
        setTimeout(initializeDataTable, 300);
    }

    // --- Initialize DataTable ---
    function initializeDataTable() {
        $("#scheduleCandidateTable").DataTable({
            paging: true,
            searching: true,
            responsive: true,
            language: { searchPlaceholder: "Search candidate..." }
        });
    }

    // --- Bind Events ---
    function bindEvents() {
        $("#btnScheduleApplicant").on("click", function () {
            openScheduleModal();
        });
    }

    // =========================
    // 🔹 2. Public Init
    // =========================
    function init() {
        bindEvents();
        console.log("✅ ScheduleApplicantPage initialized");
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
    ScheduleApplicantPage.init();
});
