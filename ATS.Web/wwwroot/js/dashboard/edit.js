// =======================================
// Candidate Details Edit Page Script
// =======================================

"use strict";

var CandidateEditPage = (function () {

    // =========================
    // 🔹 Private Variables
    // =========================
    let applicantId = null;

    // =========================
    // 🔹 Initialize Page
    // =========================
    const init = function () {
        // Extract "applicant" query param from URL
        const urlParams = new URLSearchParams(window.location.search);
        applicantId = urlParams.get("applicant");

        if (!applicantId) {
            console.warn("⚠️ No applicantId found in URL.");
            return;
        }

        loadApplicantDetails(applicantId);
    };

    // =========================
    // 🔹 Load Applicant Details
    // =========================
    const loadApplicantDetails = async function (id) {
        try {
            const response = await fetch(`/Dashboard/GetApplicantDetails?applicantId=${id}`);
            if (!response.ok) throw new Error("❌ Failed to load applicant details.");

            const data = await response.json();
            if (!data) return;

            console.log("📦 Applicant Details Loaded:", data);

            populatePersonalInformation(data.data);
            populateAdditionalInformation(data);
            handleCountryToggles(data);

        } catch (error) {
            console.error("❌ Error loading applicant details:", error);
        }
    };

    // =========================
    // 🔹 Populate Personal Information
    // ========================= 
        setValue("#FirstName", model.firstName);
        setValue("#LastName", model.lastName);
        setValue("#Email", model.emailAddresses?.[0]?.emailAddress);
        setValue("#PhoneNumber", model.phoneNumbers?.[0]?.phoneNumber);
        setValue("#Street", model.street?.street);
        setValue("#Country", model.countryName);
        setValue("#GeoLocation", model.geoLocationName);
        setValue("#InternationalCode", model.internationalCode);
        setValue("#PhoneMaskingFormat", model.phoneMaskingFormat);
        setValue("#EmploymentStatusGroupName", model.employmentStatusGroupName);
        setValue("#StatusReasonName", model.statusReasonName);
    };

    // =========================
    // 🔹 Populate Additional Information
    // =========================
    const populateAdditionalInformation = function (model) {
        setValue("#JobAdRequisitionId", model.jobAdRequisitionId);
        setValue("#RecentApplicationId", model.recentApplicationId);
        setValue("#HarverVacancyURL", model.harverVacancyURL);
        setValue("#HarverProfileUrl", model.harverProfileUrl);
        setValue("#HarverVideoUrl", model.harverVideoUrl);

        // Jamaica Fields
        setValue("#TRN", model.trn);
        setValue("#NIS", model.nis);
        setValue("#IdNumber", model.idNumber);
        setValue("#IdType", model.idType);

        // Guatemala Fields
        setValue("#BirthDate", model.birthDate);

        // Hyderabad Fields
        setValue("#EmployeeRole", model.employeeRole);
        setValue("#WaveCategory", model.waveCategory);
        setValue("#GrossSalary", model.grossSalary);
        setValue("#HourlyRate", model.hourlyRate);
    };

    // =========================
    // 🔹 Handle Country Toggles (for _AdditionalInformation.cshtml only)
    // =========================
    const handleCountryToggles = function (model) {
        const country = (model.applicationCountry || "").trim().toLowerCase();

        const jamaicaSection = $("#jamaicaAdditionalSection");
        const guatemalaSection = $("#guatemalaAdditionalSection");
        const hyderabadSection = $("#hyderabadAdditionalSection");

        // Hide all by default
        jamaicaSection.hide();
        guatemalaSection.hide();
        hyderabadSection.hide();

        // Show based on country
        if (country === "jamaica") {
            jamaicaSection.show();
        } else if (country === "guatemala") {
            guatemalaSection.show();
        } else if (country === "india" || country === "hyderabad") {
            hyderabadSection.show();
        }
    };

    // =========================
    // 🔹 Utility: Set Field Value
    // =========================
    const setValue = function (selector, value) {
        const el = $(selector);
        if (el.length) el.val(value ?? "");
    };

    // =========================
    // 🔹 Public API
    // =========================
    return {
        init: init,
        Refresh: function () {
            if (applicantId) {
                loadApplicantDetails(applicantId);
            }
        }
    };

})();

// =========================
// 🔹 Document Ready
// =========================
$(document).ready(function () {
    CandidateEditPage.init();
});
