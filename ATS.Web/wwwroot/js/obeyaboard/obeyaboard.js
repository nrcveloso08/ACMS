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
    // Public API
    // ----------------------------------------
    return {
        init
    };
})();

// Initialize when DOM is ready
$(document).ready(function () {
    ObeyaBoard.init();
});
