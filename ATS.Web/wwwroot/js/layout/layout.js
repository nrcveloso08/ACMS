// ================================
// Layout Script
// Handles Sidebar Toggle, Submenu Behavior, and Mini-Aside Logic
// ================================

"use strict";

var LayoutPage = (function () {

    // =========================
    // 🔹 1. Private Functions
    // =========================

    // --- First DOMContentLoaded: Basic Aside Minimize Toggle ---
    const initBodyAsideToggle = function () {
        document.addEventListener("DOMContentLoaded", function () {
            const toggleBtn = document.getElementById("kt_aside_toggle");
            const body = document.body;

            if (toggleBtn) {
                toggleBtn.addEventListener("click", function () {
                    body.classList.toggle("aside-minimize");
                });
            }
        });
    };

    // --- Second DOMContentLoaded: Body Minimize + Mobile Hide ---
    const initResponsiveAsideToggle = function () {
        document.addEventListener("DOMContentLoaded", function () {
            const body = document.body;
            const asideToggle = document.getElementById("kt_aside_toggle");

            if (asideToggle) {
                asideToggle.addEventListener("click", function () {
                    // Toggle minimize state
                    body.classList.toggle("aside-minimize");

                    // Optional: fully hide sidebar on mobile
                    if (window.innerWidth < 992) {
                        body.classList.toggle("aside-hidden");
                    }
                });
            }
        });
    };

    // --- jQuery Ready Block: Submenu, Mini-Aside & Tooltips ---
    const initJQueryHandlers = function () {
        $(document).ready(function () {

            /* ========= Submenu toggle (supports nested levels) ========= */
            $("[data-toggle='sidebar-collapse']").on("click", function (e) {
                e.preventDefault();

                const $this = $(this);
                const $submenu = $($this.attr("href"));

                // Allow multiple open within same parent, only close siblings
                const $siblings = $this.closest("ul").find("> .menu-item > .sidebar-submenu").not($submenu);
                $siblings.removeClass("show");
                $this.closest("ul").find("> .menu-item > .menu-link").not($this).removeClass("expanded");

                // Toggle clicked submenu
                $submenu.toggleClass("show");
                $this.toggleClass("expanded");

                // Chevron rotation
                $this.find(".fa-chevron-down, .fa-chevron-up")
                    .toggleClass("fa-chevron-down fa-chevron-up");
            });

            /* ========= Mini-aside toggle (auto-collapse all parents) ========= */
            $("#kt_aside_toggle").on("click", function () {
                const $aside = $(".app-aside");
                const isMini = !$aside.hasClass("aside-mini"); // true when we're about to collapse

                $aside.toggleClass("aside-mini");

                // Toggle the toggle-button icon direction
                $(this).find("i")
                    .toggleClass("fa-angle-double-left fa-angle-double-right");

                // ✅ When collapsing to mini, close all parent menus for clean layout
                if (isMini) {
                    $(".sidebar-submenu").removeClass("show"); // hide all submenus
                    $("[data-toggle='sidebar-collapse']").removeClass("expanded"); // reset active state
                    $(".menu-link i.fa-chevron-up")
                        .removeClass("fa-chevron-up")
                        .addClass("fa-chevron-down"); // reset chevrons
                }
            });

            /* ========= Tooltips for mini mode ========= */
            $(".menu-link").each(function () {
                const text = $(this).find(".menu-text").text().trim();
                if (text) $(this).attr("title", text);
            });

        });
    };

    // =========================
    // 🔹 2. Public Init
    // =========================
    const init = function () {
        initBodyAsideToggle();      // first process
        initResponsiveAsideToggle(); // second process
        initJQueryHandlers();        // third process

        console.log("✅ Layout initialized");
        console.log("Bootstrap version:", bootstrap?.Modal ? "v5+" : "v4 or older");
    };

    // Expose public method
    return {
        init: init
    };

})();

// =========================
// 🔹 3. Initialize on Ready
// =========================
$(document).ready(function () {
    LayoutPage.init();
});
