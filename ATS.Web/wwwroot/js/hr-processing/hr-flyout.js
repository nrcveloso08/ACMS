// wwwroot/js/hr-processing/hr-flyout.js
"use strict";

var HRFlyout = (function () {

    const containerSelector = "#hrFlyoutContainer";

    function load(data) {
        $.get("/HrProcessing/LoadFlyout", data)
            .done(function (html) {
                $("#hrFlyoutContainer").html(html);

                const $flyout = $("#hrFlyout");
                if ($flyout.length) {
                    $flyout.css({
                        display: "block",
                        right: "-520px",
                        transition: "right 240ms ease"
                    });
                    setTimeout(function () {
                        $flyout.css("right", "0");
                    }, 10);
                }

                attachClose();
            })
            .fail(function (xhr) {
                console.error("❌ Failed to load flyout content:", xhr.responseText);
            });
    }

    function attachClose() {
        // close button inside flyout partial
        $(document).off("click", "#btnCloseFlyout").on("click", "#btnCloseFlyout", function (e) {
            e.preventDefault();
            close();
        });

        // optional: click outside to close
        $(document).off("click", "#hrFlyoutBackdrop").on("click", "#hrFlyoutBackdrop", function () {
            close();
        });
    }

    function close() {
        const $flyout = $("#hrFlyout");
        if ($flyout.length) {
            $flyout.css("right", "-520px");
            setTimeout(function () {
                $flyout.remove();
            }, 250);
        }
        // remove backdrop if present
        $("#hrFlyoutBackdrop").remove();
    }

    function init() {
        // nothing to init, but expose API
    }

    return {
        init: init,
        load: load,
        close: close
    };

})();

$(document).ready(function () {
    HRFlyout.init();
});
