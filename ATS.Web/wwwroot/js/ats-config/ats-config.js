"use strict";

var ATSConfigPage = (function () {
    const executeScripts = function (htmlContent) {
        if (!htmlContent || typeof htmlContent !== "string") {
            console.error("❌ Invalid HTML content, cannot execute scripts.");
            $("#configContent").html(`
            <div class="alert alert-danger mt-4">
                <i class="fas fa-exclamation-triangle me-2"></i> 
                Failed to render partial content. Invalid response.
            </div>
        `);
            return;
        }

        // Create a temporary container
        const tempDiv = $("<div>").html(htmlContent);
        const scriptTags = tempDiv.find("script");

        // Remove scripts from tempDiv so we inject HTML cleanly
        scriptTags.remove();

        // Inject HTML first
        $("#configContent").hide().html(tempDiv.html()).fadeIn(300);

        // ✅ STEP 1: Execute any scripts included in response (inline/external)
        scriptTags.each(function () {
            const src = $(this).attr("src");
            if (src) {
                $.getScript(src)
                    .done(() => console.log(`✅ Loaded external script: ${src}`))
                    .fail(() => console.warn(`⚠️ Failed to load external script: ${src}`));
            } else {
                try {
                    $.globalEval($(this).html());
                    console.log("✅ Executed inline script successfully.");
                } catch (err) {
                    console.error("⚠️ Inline script execution failed:", err);
                }
            }
        });

        // ✅ STEP 2: Detect @section Scripts content (since Razor strips it in partials)
        // If your partial contains a marker like <div id="section-scripts">...</div>, execute its content
        $("#configContent")
            .find("script")
            .each(function () {
                const src = $(this).attr("src");
                if (src) {
                    $.getScript(src)
                        .done(() => console.log(`✅ Loaded section script: ${src}`))
                        .fail(() => console.warn(`⚠️ Failed to load section script: ${src}`));
                } else {
                    try {
                        $.globalEval($(this).html());
                        console.log("✅ Executed section inline script successfully.");
                    } catch (err) {
                        console.error("⚠️ Failed to execute section inline script:", err);
                    }
                }
            });
    };


    const loadPartial = function (partialName) {
        if (!partialName) {
            console.error("❌ Invalid partial name provided.");
            return;
        }

        const url = `/ATSConfig/${partialName}`;
        console.log(`ℹ️ Loading partial: ${url}`);

        $("#configContent").html(`
            <div class="text-center py-5">
                <div class="spinner-border text-success" style="width:3rem;height:3rem;"></div>
                <p class="mt-3 fw-semibold text-muted">Loading ${partialName}...</p>
            </div>
        `);

        $.ajax({
            url: url,
            type: "GET",
            cache: false
        })
            .done(function (html) {
                if (html.includes("<title>") && html.includes("</html>")) {
                    // MVC returned a full layout instead of a partial
                    console.warn("⚠️ Full layout returned instead of partial — check Layout = null.");
                    $("#configContent").html(`
                    <div class="alert alert-warning mt-4">
                        <i class="fas fa-exclamation-circle me-2"></i>
                        The server returned a full layout instead of a partial view.
                    </div>
                `);
                    return;
                }

                executeScripts(html);
            })
            .fail(function (xhr) {
                console.error(`❌ Failed to load partial: ${url}`, xhr);
                $("#configContent").html(`
                <div class="alert alert-danger mt-4">
                    <i class="fas fa-exclamation-triangle me-2"></i>
                    Failed to load the selected configuration.<br>
                    <small>Status: ${xhr.status} - ${xhr.statusText}</small>
                </div>
            `);
            });
    };

    const bindEvents = function () {
        $(".config-list a").on("click", function (e) {
            e.preventDefault();
            const partialName = $(this).data("partial");
            loadPartial(partialName);
        });
    };

    const init = function () {
        bindEvents();
    };

    return {
        init: init
    };

})();

$(document).ready(function () {
    ATSConfigPage.init();
});
