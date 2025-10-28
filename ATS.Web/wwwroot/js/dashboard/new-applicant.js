// Handles "New Applicant" actions
$(document).ready(function () {

    // Back to list
    $("#btnBackToList").on("click", function () {
        window.location.href = "/Prescreening/Search";
    });

    // Create applicant (dummy)
    $("#frmNewApplicant").on("submit", function (e) {
        e.preventDefault();
        AppUtils.loadModal("successModal", {
            title: "Applicant Created",
            body: "<p>The applicant has been successfully added!</p>",
            buttons: {
                Ok: {
                    Enabled: true,
                    text: "OK",
                    btnClass: "btn-success",
                    autoDismiss: true
                }
            }
        });
    });
});
