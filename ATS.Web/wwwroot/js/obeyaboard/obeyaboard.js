"use strict";

var ObeyaBoard = (function () {

    // ----------------------------------------
    // Initialize the Obeya Board
    // ----------------------------------------
    function init() {
        console.log("✅ Obeya Board initialized");
        setupDragAndDrop();
        refreshEmptyStates();
    }

    // ----------------------------------------
    // Setup Drag & Drop
    // ----------------------------------------
    function setupDragAndDrop() {
        const cards = document.querySelectorAll(".obeya-card");
        const columns = document.querySelectorAll(".obeya-column");

        if (!cards.length || !columns.length) {
            console.warn("⚠️ No cards or columns found.");
            return;
        }

        console.log("🧩 Cards found:", cards.length, " | Columns found:", columns.length);

        let draggedCard = null;

        // ===== DRAG START =====
        cards.forEach(card => {
            card.addEventListener("dragstart", (e) => {
                draggedCard = card;
                e.dataTransfer.effectAllowed = "move";
                e.dataTransfer.setData("text/plain", card.dataset.cardId);
                card.classList.add("dragging");
                console.log(`🚀 Drag started: ${card.dataset.cardId}`);
            });

            card.addEventListener("dragend", () => {
                if (draggedCard) draggedCard.classList.remove("dragging");
                draggedCard = null;
                refreshEmptyStates(); // check columns after drag ends
            });
        });

        // ===== DROP ZONES =====
        columns.forEach(col => {
            const dropZone = col.querySelector(".obeya-card-list");

            const makeDroppable = (el) => {
                el.addEventListener("dragover", (e) => {
                    e.preventDefault();
                    e.dataTransfer.dropEffect = "move";
                    col.classList.add("drag-over");
                });

                el.addEventListener("dragleave", () => {
                    col.classList.remove("drag-over");
                });
                el.addEventListener("drop", (e) => {
                    e.preventDefault();
                    e.stopPropagation();
                    col.classList.remove("drag-over");

                    const cardId = e.dataTransfer.getData("text/plain");
                    const card = document.querySelector(`[data-card-id="${cardId}"]`);
                    const newStatus = col.dataset.status;

                    if (!card) {
                        console.warn("⚠️ Card not found for ID:", cardId);
                        return;
                    }

                    console.log(`✅ Dropped card ${cardId} into ${newStatus}`);

                    // ✅ Remove the placeholder FIRST
                    const placeholder = dropZone.querySelector(".no-cards-placeholder");
                    if (placeholder) placeholder.remove();

                    // ✅ Then append the card
                    dropZone.appendChild(card);

                    // ✅ Update and refresh
                    updateCardStatus(cardId, newStatus);
                    refreshEmptyStates();
                });

            };

            makeDroppable(col);
            makeDroppable(dropZone);
        });
    }

    // ----------------------------------------
    // Manage "No cards available" placeholders
    // ----------------------------------------
    function refreshEmptyStates() {
        const columns = document.querySelectorAll(".obeya-column");

        columns.forEach(col => {
            const dropZone = col.querySelector(".obeya-card-list");
            if (!dropZone) return;

            // Remove any old placeholders
            const existingPlaceholder = dropZone.querySelector(".no-cards-placeholder");
            if (existingPlaceholder) existingPlaceholder.remove();

            const cards = dropZone.querySelectorAll(".obeya-card");
            if (cards.length === 0) {
                const placeholder = document.createElement("div");
                placeholder.className = "text-muted small text-center py-3 no-cards-placeholder";
                placeholder.textContent = "No cards available";
                dropZone.appendChild(placeholder);
            }
        });
    }

    // ----------------------------------------
    // Simulated AJAX Status Update
    // ----------------------------------------
    function updateCardStatus(cardId, newStatus) {
        console.log(`📡 Simulated AJAX: updating ${cardId} to ${newStatus}`);
    }

    // ----------------------------------------
    // Switch between Card and Tabular views
    // ----------------------------------------
    function showView(view) {
        const cardView = document.getElementById("obeyaCardView");
        const tabularContainer = document.getElementById("tabularContainer");

        const btnCard = $("#btnCardView");
        const btnTabular = $("#btnTabularView");

        if (view === "card") {
            btnCard.addClass("btn-light-primary fw-semibold active")
                .removeClass("btn-light fw-semibold");
            btnTabular.removeClass("btn-light-primary active")
                .addClass("btn-light fw-semibold");

            $(tabularContainer).addClass("d-none");
            $(cardView).removeClass("d-none");
        } else {
            btnTabular.addClass("btn-light-primary fw-semibold active")
                .removeClass("btn-light fw-semibold");
            btnCard.removeClass("btn-light-primary active")
                .addClass("btn-light fw-semibold");

            $(cardView).addClass("d-none");

            if (!tabularContainer.dataset.loaded) {
                console.log("📡 Attempting to load /ObeyaBoard/LoadTabular...");
                $("#tabularContainer").load("/ObeyaBoard/LoadTabular", function (response, status, xhr) {
                    if (status === "error") {
                        console.error("❌ Failed to load Tabular view:", xhr.status, xhr.statusText);
                    } else {
                        console.log("✅ Tabular partial loaded successfully!");
                        tabularContainer.dataset.loaded = "true";
                        $(tabularContainer).removeClass("d-none");

                        // ✅ Initialize DataTable with dummy data
                        initializeTabularTable();
                    }
                });
            } else {
                console.log("🔄 Showing already-loaded Tabular view");
                $(tabularContainer).removeClass("d-none");
            }

        }
    }

    // ----------------------------------------
    // Initialize DataTable for Tabular View
    // ----------------------------------------
    function initializeTabularTable() {
        if (!$.fn.DataTable.isDataTable("#waveTable")) {

            // Dummy data
            const dummyData = [
                [17, "In Progress", "Mesa", "Marshmallow Mesa Tier 1", "1D", 37, 13, "35%", "English"],
                [18, "Not Started", "Mesa", "Marshmallow Mesa Tier 1", "28D", 25, 0, "0%", "English"],
                [76, "In Progress", "Mesa", "FireWire Mesa", "10D", 40, 18, "45%", "English"],
                [77, "Not Started", "Mesa", "FireWire Mesa", "30D", 50, 0, "0%", "English"],
                [88, "In Progress", "Guatemala", "Choco Factory", "12D", 70, 50, "71%", "Spanish"],
                [99, "In Progress", "Jamaica", "Lululemon Jamaica Montego Bay", "14D", 80, 75, "93%", "English"]
            ];

            // Initialize DataTable
            const table = $("#waveTable").DataTable({
                data: dummyData,
                columns: [
                    { title: "Wave" },
                    { title: "Status" },
                    { title: "Location" },
                    { title: "Line of Business" },
                    { title: "Days Left" },
                    { title: "Adjusted Head Count" },
                    { title: "In Progress + Hired" },
                    { title: "Total %" },
                    { title: "Language" }
                ],
                paging: true,
                searching: true,
                responsive: true,
                order: [[0, "asc"]],
                createdRow: function (row, data) {
                    const totalPercent = parseInt(data[7].replace("%", "")) || 0;
                    let bgColor = "";

                    if (totalPercent < 40) bgColor = "#fdecea";       // 🔴 light red
                    else if (totalPercent < 70) bgColor = "#fff8e1";  // 🟡 light yellow
                    else bgColor = "#e8f5e9";                         // 🟢 light green

                    // Apply colored background with rounded aesthetic
                    const $cell = $("td:eq(5)", row);
                    $cell.css({
                        "position": "relative",
                        "padding": "0.4rem 0.75rem",
                        "font-weight": "600",
                        "overflow": "hidden"
                    });

                    // Create an inner rounded element for soft aesthetic color
                    $cell.html(`<div class="cell-highlight">${data[5]}</div>`);

                    const $highlight = $cell.find(".cell-highlight");
                    $highlight.css({
                        "background-color": bgColor,
                        "border-radius": "8px",
                        "padding": "6px 10px",
                        "display": "inline-block",
                        "min-width": "45px",
                        "text-align": "center",
                        "font-weight": "600"
                    });

                }
            });

            console.log("📊 DataTable initialized with dummy data");
        }
    }


    // ----------------------------------------
    // Public API
    // ----------------------------------------
    return {
        init,
        showView
    };
})();

// Initialize when DOM is ready
$(document).ready(function () {
    ObeyaBoard.init();

    // ✅ Event bindings for header buttons
    $(document).on("click", "#btnCardView", function () {
        ObeyaBoard.showView("card");
    });

    $(document).on("click", "#btnTabularView", function () {
        ObeyaBoard.showView("tabular");
    });
});
