$(document).ready(function () {
    $(".config-list a").on("click", function (e) {
        e.preventDefault();
        const partialName = $(this).data("partial");
        const url = `/ATSConfig/${partialName}`;

        // Show a loading spinner
        $("#configContent").html(`
                <div class="text-center py-5">
                    <div class="spinner-border text-success" style="width:3rem;height:3rem;"></div>
                    <p class="mt-3 fw-semibold text-muted">Loading ${partialName}...</p>
                </div>
            `);

        // Fetch and load the partial
        $.get(url, function (html) {
            $("#configContent").hide().html(html).fadeIn(300);
            $("html, body").animate({ scrollTop: $("#configContent").offset().top - 80 }, 400);
        }).fail(function () {
            $("#configContent").html(`
                    <div class="alert alert-danger mt-4">
                        <i class="fas fa-exclamation-triangle me-2"></i> 
                        Failed to load the selected configuration.
                    </div>
                `);
        });
    });
});