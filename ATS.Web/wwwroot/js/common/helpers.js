// wwwroot/js/common/helpers.js

window.AppUtils = window.AppUtils || {};

/**
 * Ensure a div container exists in the DOM.
 */
AppUtils.addDivContainer = function (containerId) {
    let tempContainer = $(containerId);
    if (tempContainer.length === 0) {
        tempContainer = $('<div></div>', { id: containerId.replace('#', '') });
        $('body').append(tempContainer);
    }
    return tempContainer;
};


/**
 * Dynamically builds and shows a modal (Bootstrap 4/5 compatible)
 */
AppUtils.loadModal = function (modalId, options) {
    const defaults = {
        title: "Dialog",
        body: "",
        buttons: {},
        settings: "modal-dialog-centered modal-md"
    };
    const config = $.extend(true, {}, defaults, options);

    // Remove any existing modal with the same ID
    $('#' + modalId).remove();

    // Build footer buttons
    const footerButtons = Object.keys(config.buttons)
        .map(key => {
            const btn = config.buttons[key];
            if (!btn.Enabled) return '';
            const safeKey = key.replace(/\s+/g, '_');
            const dismissAttr = btn.autoDismiss ? 'data-bs-dismiss="modal"' : '';
            return `<button type="button" id="${modalId}_${safeKey}" class="btn ${btn.btnClass}" ${dismissAttr}>${btn.text}</button>`;
        }).join('');

    // Modal HTML
    const modalHtml = `
        <div class="modal fade" id="${modalId}" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog ${config.settings}" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">${config.title}</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">${config.body}</div>
                    <div class="modal-footer d-flex justify-content-end gap-3 flex-wrap">
                        ${footerButtons}
                    </div>
                </div>
            </div>
        </div>
    `;

    // Append modal to body
    $('body').append(modalHtml);

    const $modal = $('#' + modalId);

    // Attach button actions
    Object.keys(config.buttons).forEach(key => {
        const btn = config.buttons[key];
        if (btn.Enabled && typeof btn.action === 'function') {
            const safeKey = key.replace(/\s+/g, '_');
            const selector = `#${modalId}_${safeKey}`;
            $(document).off('click', selector).on('click', selector, ev => {
                ev.preventDefault();
                btn.action(ev);
            });
        }
    });

    // Attach dismiss logic for Cancel/OK buttons
    AppUtils.attachDismissHandlers($modal[0]);

    // Show modal (Bootstrap 4)
    $modal.modal({
        backdrop: 'static',
        keyboard: true,
        show: true
    });

    // Cleanup on hidden
    $modal.on('hidden.bs.modal', function () {
        $(this).remove();
    });
};

/**
 * Ensures dynamically added modals and dismiss buttons work in Bootstrap 4 or 5.
 */
AppUtils.attachDismissHandlers = function (modalEl) {
    if (!modalEl) return;

    // Works for Bootstrap 4 (.modal('hide')) and Bootstrap 5 (.hide())
    modalEl.querySelectorAll('[data-bs-dismiss="modal"]').forEach(btn => {
        btn.addEventListener('click', () => {
            try {
                if (typeof $ !== "undefined" && typeof $(modalEl).modal === "function") {
                    // ✅ Bootstrap 4 fallback
                    $(modalEl).modal('hide');
                } else if (window.bootstrap && bootstrap.Modal) {
                    // ✅ Bootstrap 5+
                    const inst = bootstrap.Modal.getInstance(modalEl) || new bootstrap.Modal(modalEl);
                    inst.hide();
                }
            } catch (err) {
                console.error("Dismiss handler error:", err);
                // Always fallback to jQuery
                if (typeof $ !== "undefined" && typeof $(modalEl).modal === "function") {
                    $(modalEl).modal('hide');
                }
            }
        });
    });
};

AppUtils.maskPhoneInput = function (selector) {
    const formatPhone = (value) => {
        // Remove all non-digits except '+'
        let digits = value.replace(/[^\d+]/g, '');

        // If starts with '+', keep it
        const hasPlus = digits.startsWith('+');
        if (hasPlus) {
            digits = '+' + digits.slice(1).replace(/\D/g, '');
        } else {
            digits = digits.replace(/\D/g, '');
        }

        // Example pattern grouping (works for various lengths)
        let formatted = digits;

        // Remove + temporarily for formatting
        const prefix = hasPlus ? '+' : '';
        let cleanDigits = digits.replace('+', '');

        if (cleanDigits.length <= 4) {
            formatted = prefix + cleanDigits;
        } else if (cleanDigits.length <= 7) {
            formatted = prefix + cleanDigits.replace(/(\d{3})(\d{1,4})/, '$1 $2');
        } else if (cleanDigits.length <= 11) {
            formatted = prefix + cleanDigits.replace(/(\d{3})(\d{3})(\d{1,5})/, '$1 $2 $3');
        } else {
            formatted = prefix + cleanDigits.replace(/(\d{2,3})(\d{3})(\d{3})(\d{0,4})/, '$1 $2 $3 $4').trim();
        }

        return formatted.trim();
    };

    $(document).on('input', selector, function () {
        const cursorPos = this.selectionStart;
        const valueBefore = $(this).val();
        const formatted = formatPhone(valueBefore);
        $(this).val(formatted);
        this.selectionEnd = cursorPos; // preserve caret
    });
};

// wwwroot/js/common/helpers.js

// wwwroot/js/common/helpers.js


/**
 * Generic form validation utility (required-only version with radio/checkbox support).
 * Validates only fields marked as `required`.
 * Supports text, numbers, letters, dates, dropdowns, radio buttons, and checkboxes.
 * Returns true if valid, false otherwise.
 */
AppUtils.validateForm = function (formSelector) {
    let isValid = true;
    const $form = $(formSelector);

    // Remove previous error styles/messages
    $form.find('.is-invalid').removeClass('is-invalid');
    $form.find('.invalid-feedback').remove();

    // Validate only required inputs, selects, and textareas
    $form.find('input[required], select[required], textarea[required]').each(function () {
        const $field = $(this);
        const type = $field.attr('type');
        const tag = $field.prop('tagName').toLowerCase();
        const fieldType = ($field.attr('data-type') || '').toLowerCase();
        const name = $field.attr('name');
        const value = $field.val()?.trim() || '';

        // --- Text-based fields ---
        if (['text', 'password', 'email', 'date', 'number', 'textarea'].includes(type) || tag === 'textarea') {
            // Required empty check
            if (value === '') {
                markInvalid($field, 'This field is required.');
                isValid = false;
                return;
            }

            // Type-based validation
            if (fieldType === 'letters' && !/^[A-Za-z\s]+$/.test(value)) {
                markInvalid($field, 'Only letters are allowed.');
                isValid = false;
            }

            if (fieldType === 'numbers' && !/^\d+$/.test(value)) {
                markInvalid($field, 'Only numbers are allowed.');
                isValid = false;
            }

            // Date validation
            if (type === 'date') {
                const date = new Date(value);
                if (isNaN(date.getTime())) {
                    markInvalid($field, 'Invalid date.');
                    isValid = false;
                }
            }
        }

        // --- Dropdowns ---
        else if (tag === 'select') {
            if (!value || value === '0' || value === '-1') {
                markInvalid($field, 'Please select a value.');
                isValid = false;
            }
        }

        // --- Radio buttons (validate group by name) ---
        else if (type === 'radio') {
            if ($form.find(`input[name='${name}']:checked`).length === 0) {
                markInvalidGroup($form.find(`input[name='${name}']`).last(), 'Please select an option.');
                isValid = false;
            }
        }

        // --- Checkboxes ---
        else if (type === 'checkbox') {
            // If part of a group (multiple checkboxes with same name)
            if ($form.find(`input[name='${name}']`).length > 1) {
                if ($form.find(`input[name='${name}']:checked`).length === 0) {
                    markInvalidGroup($form.find(`input[name='${name}']`).last(), 'Please select at least one option.');
                    isValid = false;
                }
            } else {
                // Single checkbox (like Terms & Conditions)
                if (!$field.is(':checked')) {
                    markInvalidGroup($field, 'This checkbox is required.');
                    isValid = false;
                }
            }
        }
    });

    return isValid;

    // --- Helper: mark invalid input fields ---
    function markInvalid($field, message) {
        $field.addClass('is-invalid');
        if ($field.next('.invalid-feedback').length === 0) {
            $field.after(`<div class="invalid-feedback">${message}</div>`);
        }
    }

    // --- Helper: mark invalid radio/checkbox groups ---
    function markInvalidGroup($field, message) {
        if ($field.closest('.form-group').find('.invalid-feedback').length === 0) {
            const $feedback = $(`<div class="invalid-feedback d-block">${message}</div>`);
            $field.closest('.form-group').append($feedback);
        }
    }
};

/**
 * Retrieves all required fields within a form.
 * Returns an array of objects containing field details:
 * { name, id, label, type, tag, element }
 */
AppUtils.getRequiredFields = function (formSelector) {
    const $form = $(formSelector);
    const requiredFields = [];

    $form.find('input[required], select[required], textarea[required]').each(function () {
        const $field = $(this);
        const type = $field.attr('type') || '';
        const tag = $field.prop('tagName').toLowerCase();
        const name = $field.attr('name') || '';
        const id = $field.attr('id') || '';
        const label = getLabelText($field);

        requiredFields.push({
            name,
            id,
            label,
            type,
            tag,
            element: $field
        });
    });

    return requiredFields;

    // --- Helper: try to find the label text for the field ---
    function getLabelText($field) {
        // 1️⃣ Match <label for="id">
        const id = $field.attr('id');
        if (id) {
            const $label = $form.find(`label[for='${id}']`);
            if ($label.length) return $label.text().trim();
        }

        // 2️⃣ If wrapped inside a .form-group with <label>
        const $groupLabel = $field.closest('.form-group').find('label').first();
        if ($groupLabel.length) return $groupLabel.text().trim();

        // 3️⃣ Fallback: use placeholder or name
        return $field.attr('placeholder') || name || '(Unnamed field)';
    }
};

/**
* Validate an email address string.
* Returns true if valid, false otherwise.
*/
AppUtils.validateEmail = function (email) {
    if (!email || typeof email !== 'string') return false;
    const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return pattern.test(email.trim());
};


/**
* Generic AJAX utility.
* Handles GET, POST, PUT, DELETE requests with standard success/error handling.
* Returns a Promise so you can chain .then() / .catch().
*/
AppUtils.ajaxCall = function (options) {
    const defaults = {
        url: '',
        type: 'GET',              // or POST, PUT, DELETE
        data: {},                 // request body or params
        dataType: 'json',         // expected response type
        contentType: 'application/json; charset=utf-8',
        async: true,
        showLoader: true,         // show a loading spinner (optional)
        loaderTarget: 'body',     // where to attach loader overlay
        successMessage: null,     // message to show on success
        errorMessage: 'Something went wrong. Please try again later.',
        onSuccess: null,          // function(response)
        onError: null             // function(xhr)
    };

    const config = $.extend(true, {}, defaults, options);

    // --- Show loader (optional) ---
    if (config.showLoader) AppUtils.showLoader(config.loaderTarget);

    return new Promise((resolve, reject) => {
        $.ajax({
            url: config.url,
            type: config.type,
            data: config.type.toUpperCase() === 'GET' ? config.data : JSON.stringify(config.data),
            dataType: config.dataType,
            contentType: config.contentType,
            async: config.async,
            success: function (response) {
                if (config.showLoader) AppUtils.hideLoader(config.loaderTarget);

                if (config.successMessage) {
                    AppUtils.loadModal('successModal', {
                        title: "Success",
                        body: `<p>${config.successMessage}</p>`,
                        buttons: { Ok: { Enabled: true, text: "OK", btnClass: "btn-success" } }
                    });
                }

                if (typeof config.onSuccess === 'function') config.onSuccess(response);
                resolve(response);
            },
            error: function (xhr) {
                if (config.showLoader) AppUtils.hideLoader(config.loaderTarget);

                AppUtils.loadModal('errorModal', {
                    title: "Error",
                    body: `<p>${config.errorMessage}</p>`,
                    buttons: { Ok: { Enabled: true, text: "OK", btnClass: "btn-danger" } }
                });

                if (typeof config.onError === 'function') config.onError(xhr);
                reject(xhr);
            }
        });
    });
};

/**
 * Show loader overlay
 */
AppUtils.showLoader = function (target) {
    const $target = $(target);
    if ($target.find('.app-loader').length === 0) {
        const loaderHtml = `
            <div class="app-loader" style="
                position: absolute; top: 0; left: 0; right: 0; bottom: 0;
                display: flex; justify-content: center; align-items: center;
                background: rgba(255,255,255,0.6); z-index: 9999;">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            </div>`;
        $target.css('position', 'relative').append(loaderHtml);
    }
};

/**
 * Hide loader overlay
 */
AppUtils.hideLoader = function (target) {
    $(target).find('.app-loader').remove();
};


AppUtils.toastMessage = function (message, type = "info") {
    const icons = {
        success: "fa-check-circle text-success",
        warning: "fa-exclamation-triangle text-warning",
        error: "fa-times-circle text-danger",
        info: "fa-info-circle text-info"
    };
    const icon = icons[type] || icons.info;

    const toast = $(`
        <div class="toast align-items-center text-white bg-dark border-0 show" role="alert"
             style="position: fixed; bottom: 20px; right: 20px; z-index: 99999; min-width: 280px;">
            <div class="d-flex p-2">
                <i class="fas ${icon} me-2 mt-1"></i>
                <div class="toast-body flex-grow-1">${message}</div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" aria-label="Close"></button>
            </div>
        </div>
    `);

    $("body").append(toast);

    // Auto close and manual close support
    toast.find(".btn-close").on("click", () => toast.remove());
    setTimeout(() => toast.fadeOut(500, () => toast.remove()), 4000);
};


