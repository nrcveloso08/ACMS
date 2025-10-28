$(document).ready(function () {
    $(document).on("click", ".btnInterview", function () {
        const id = $(this).data("id");

        console.log("Interview clicked for:", id);

        // Remove any existing flyout
        $("#interviewFlyout").remove();

        $.get("/InterviewSchedule/LoadInterviewFlyout", { applicantId: id })
            .done(function (html) {
                $("body").append(html);
                setTimeout(() => $("#interviewFlyout").addClass("show"), 100);

                $(document).on("click", "#btnCloseFlyout, #interviewFlyout", function (e) {
                    if (e.target.id === "btnCloseFlyout" || e.target.id === "interviewFlyout") {
                        $("#interviewFlyout").removeClass("show");
                        setTimeout(() => $("#interviewFlyout").remove(), 350);
                    }
                });
            })
            .fail(function (xhr) {
                console.error("Flyout load failed:", xhr);
            });
    });
});