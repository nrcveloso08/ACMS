// ================================
// Flyout Script
// Handles Interview Flyout Load and Close Interactions
// ================================

"use strict";

var FlyoutPage = (function () {

    // =========================
    // 🔹 1. Private Functions
    // =========================

    // --- Load Interview Flyout ---
    function loadInterviewFlyout(applicantId) {
        console.log("Interview clicked for:", applicantId);

        // Remove any existing flyout
        $("#interviewFlyout").remove();

        $.get("/InterviewSchedule/LoadInterviewFlyout", { applicantId: applicantId })
            .done(function (html) {
                $("body").append(html);

                // Delay to trigger CSS transition smoothly
                setTimeout(() => $("#interviewFlyout").addClass("show"), 100);

                bindFlyoutCloseEvents();
            })
            .fail(function (xhr) {
                console.error("Flyout load failed:", xhr);
            });
    }

    // --- Handle Flyout Close Events ---
    function bindFlyoutCloseEvents() {
        $(document).on("click", "#btnCloseFlyout, #interviewFlyout", function (e) {
            if (e.target.id === "btnCloseFlyout" || e.target.id === "interviewFlyout") {
                $("#interviewFlyout").removeClass("show");
                setTimeout(() => $("#interviewFlyout").remove(), 350);
            }
        });
    }

    // --- Bind Events ---
    function bindEvents() {
        $(document).on("click", ".btnInterview", function () {
            const id = $(this).data("id");
            loadInterviewFlyout(id);
        });
    }

    // =========================
    // 🔹 2. Public Init
    // =========================
    function init() {
        bindEvents();
        console.log("✅ FlyoutPage initialized");
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
    FlyoutPage.init();
});
