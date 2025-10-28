(() => {

    // 🔹 Template for Search Tab
    // ✅ FIXED VERSION — removes duplicate .tab-pane wrapper
    const getSearchTabTemplate = () => `
    <div class="row mb-3 align-items-center g-1">
        <label for="ddlHireType" class="col-auto col-form-label fw-bold fs-6 text-gray-700 mb-0">Hire Type</label>
        <div class="col-auto">
            <select id="ddlHireType" class="form-select w-auto form-control">
                <option value="External">External</option>
                <option value="Internal">Internal</option>
            </select>
        </div>
    </div>
    <div class="form-group mb-3">
        <input type="text" id="txtSearchEmail" class="form-control" placeholder="Search by Email (e.g. test@email.com)">
    </div>
    <table id="tblSearchResults" class="table table-bordered table-hover">
        <thead>
            <tr>
                <th>Name</th>
                <th>Email Address</th>
                <th>Phone Number</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr><td colspan="4" class="text-center text-muted">No data available</td></tr>
        </tbody>
    </table>
`;


    // 🔹 Template for Create Tab
    const getCreateTabTemplate = () => `
        <form id="frmCreateApplicant" class="pt-4">
            <div class="mb-4">
                <label class="fw-bold mb-2">Hire Type</label>
                <select id="ddlHireTypeCreate" class="form-select w-auto form-control">
                    <option value="External">External</option>
                    <option value="Internal">Internal</option>
                </select>
            </div>
            <div class="mb-4">
                <label class="fw-bold mb-2">Name</label>
                <div class="row g-2">
                    <div class="col-md-6">
                        <input type="text" id="txtFirstName" class="form-control" placeholder="First" required />
                    </div>
                    <div class="col-md-6">
                        <input type="text" id="txtLastName" class="form-control" placeholder="Last" required />
                    </div>
                </div>
            </div>
            <div class="mb-4">
                <label class="fw-bold mb-2">Contact Info</label>
                <div class="row g-2">
                    <div class="col-md-6">
                        <input type="text" id="txtPhoneNumber" class="form-control" placeholder="Phone number" required />
                    </div>
                    <div class="col-md-6">
                        <input type="email" id="txtEmailAddress" class="form-control" placeholder="Email" required />
                    </div>
                </div>
            </div>
            <div class="mb-4">
                <label class="fw-bold mb-2">Address</label>
                <div class="row gy-3 gx-3">
                    <div class="col-md-6 mb-2">
                        <input type="text" id="txtStreet" class="form-control" placeholder="Street" />
                    </div>
                    <div class="col-md-6 mb-2">
                        <input type="text" id="txtAptNumber" class="form-control" placeholder="Apartment Number" />
                    </div>
                    <div class="col-md-6 mb-2">
                        <input type="text" id="txtCity" class="form-control" placeholder="City" />
                    </div>
                    <div class="col-md-6 mb-2">
                        <input type="text" id="txtState" class="form-control" placeholder="State" />
                    </div>
                    <div class="col-md-6 mb-2">
                        <input type="text" id="txtPostalCode" class="form-control" placeholder="Postal Code" />
                    </div>
                    <div class="col-md-6 mb-2">
                        <input type="text" id="txtCountry" class="form-control" placeholder="Country Name" />
                    </div>
                </div>
            </div>
        </form>
    `;

    // 🔹 Master template builder (tab headers + content)
    const buildModalBody = () => {
        const tabs = [
            { id: 'pane-search', title: '🔍 Search', content: getSearchTabTemplate(), active: true },
            { id: 'pane-create', title: '➕ Create', content: getCreateTabTemplate(), active: false }
        ];

        const navTabs = `
        <ul class="nav nav-tabs" id="addApplicantTabs" role="tablist">
            ${tabs.map(tab => `
                <li class="nav-item" role="presentation">
                    <button class="nav-link ${tab.active ? 'active' : ''}"
                        id="${tab.id}-tab"
                        data-bs-toggle="tab"
                        data-bs-target="#${tab.id}"
                        type="button"
                        role="tab"
                        aria-controls="${tab.id}"
                        aria-selected="${tab.active}">
                        ${tab.title}
                    </button>
                </li>
            `).join('')}
        </ul>
    `;

        const tabContent = `
        <div class="tab-content mt-4" id="addApplicantTabContent">
            ${tabs.map(tab => `
                <div class="tab-pane fade ${tab.active ? 'show active' : ''}"
                    id="${tab.id}"
                    role="tabpanel"
                    aria-labelledby="${tab.id}-tab">
                    ${tab.content}
                </div>
            `).join('')}
        </div>
    `;

        return navTabs + tabContent;
    };



    // 🔹 Show modal
    $(document).on('click', '#btnAddApplicant', function () {
        console.log("🟢 Add Applicant clicked");

        const modalBody = buildModalBody();

        if (typeof AppUtils === "undefined" || typeof AppUtils.loadModal === "undefined") {
            console.error("❌ helpers.js not loaded or AppUtils not available");
            alert("helpers.js not loaded properly.");
            return;
        }

        // ✅ Load the modal
        // ✅ Load the modal only once and preserve tab contents
        // ✅ Proper modal build ensuring both tabs exist before Bootstrap initialization
        const modalHtml = `
    <div id="addApplicantTabsContainer">
        ${buildModalBody()}
    </div>`;

        AppUtils.loadModal("addApplicantModal", {
            title: "Add Employee",
            body: modalHtml,
            buttons: {
                Save: {
                    Enabled: true,
                    text: '<i class="bi bi-save me-2"></i>Save',
                    btnClass: "btn btn-primary px-5",
                    action: function () {
                        if (AppUtils.validateForm("#frmCreateApplicant")) {
                            const data = {
                                hireType: $('#ddlHireTypeCreate').val(),
                                firstName: $('#txtFirstName').val(),
                                lastName: $('#txtLastName').val(),
                                phone: $('#txtPhoneNumber').val(),
                                email: $('#txtEmailAddress').val(),
                                address: {
                                    street: $('#txtStreet').val(),
                                    aptNumber: $('#txtAptNumber').val(),
                                    city: $('#txtCity').val(),
                                    state: $('#txtState').val(),
                                    postalCode: $('#txtPostalCode').val(),
                                    country: $('#txtCountry').val()
                                }
                            };
                            console.log("💾 Saving applicant:", data);
                            $('#addApplicantModal').modal('hide');
                        }
                    }
                },
                Cancel: {
                    Enabled: true,
                    text: "Cancel",
                    btnClass: "btn btn-light",
                    action: function () {
                        const modal = $('#addApplicantModal');
                        if (modal.length) {
                            modal.modal('hide'); // ✅ Bootstrap 4 & 5 compatible
                        } else {
                            // Fallback: find any open modal and close it
                            $('.modal.show').modal('hide');
                        }
                    }
                }
            },
            settings: "modal-dialog-centered modal-xl"
        });

        // ✅ Ensure Bootstrap tabs are properly initialized after modal appears
        $(document)
            .off('shown.bs.modal.addApplicant')
            .on('shown.bs.modal.addApplicant', '#addApplicantModal', function () {
                const modalBody = $(this).find('.modal-body');

                // If Create tab content still empty, inject it
                const createPane = modalBody.find('#pane-create');
                if (createPane.length && createPane.is(':empty')) {
                    createPane.html(getCreateTabTemplate());
                }

                // ✅ Initialize Bootstrap 4 tab behavior manually
                $('#addApplicantTabs button[data-bs-toggle="tab"]').each(function () {
                    $(this).tab();
                });

                // ✅ Attach click handler to switch visible tab content
                $(document)
                    .off('click.dynamicTabs')
                    .on('click.dynamicTabs', '#addApplicantTabs button[data-bs-toggle="tab"]', function (e) {
                        e.preventDefault();
                        const target = $(this).data('bs-target');

                        // Remove active/show from all panes
                        $('.tab-pane').removeClass('show active');
                        // Add it to the selected pane
                        $(target).addClass('show active');

                        // Toggle footer buttons
                        const footer = $('#addApplicantModal .modal-footer');
                        if (target === '#pane-create') {
                            footer.find('#addApplicantModal_Save, #addApplicantModal_Clear').fadeIn(150);
                        } else {
                            footer.find('#addApplicantModal_Save, #addApplicantModal_Clear').fadeOut(150);
                        }

                        // Update tab header active state
                        $('#addApplicantTabs .nav-link').removeClass('active');
                        $(this).addClass('active');
                    });

                // Default to Search tab
                $('#addApplicantTabs button[data-bs-target="#pane-search"]').tab('show');
                $('#pane-search').addClass('show active');
                $('#pane-create').removeClass('show active');

                const footer = $(this).find('.modal-footer');
                footer.find('#addApplicantModal_Save, #addApplicantModal_Clear').hide();
            });

        // ✅ Correct Bootstrap 4-compatible tab toggle that preserves DOM content
        // ✅ Handle tab switching and nested tab-pane issue
        $(document)
            .off('click.dynamicTabs')
            .on('click.dynamicTabs', '#addApplicantTabs button[data-bs-toggle="tab"]', function (e) {
                e.preventDefault();

                const $clickedTab = $(this);
                const targetSelector = $clickedTab.data('bs-target');
                const $targetPane = $(targetSelector);

                if (!$targetPane.length) {
                    console.warn('⚠️ Target tab-pane not found:', targetSelector);
                    return;
                }

                // Deactivate all tab headers
                $('#addApplicantTabs .nav-link').removeClass('active');
                $clickedTab.addClass('active');

                // Hide all tab panes
                $('#addApplicantTabContent .tab-pane').removeClass('show active');

                // Show selected pane
                $targetPane.addClass('show active');

                // ✅ Fix nested tab-pane (Search tab specific)
                if (targetSelector === '#pane-search') {
                    const nestedPane = $targetPane.find('.tab-pane');
                    if (nestedPane.length) {
                        nestedPane.addClass('show active'); // Ensure inner pane is visible
                    }
                }

                // Toggle footer buttons
                const footer = $('#addApplicantModal .modal-footer');
                if (targetSelector === '#pane-create') {
                    footer.find('#addApplicantModal_Save, #addApplicantModal_Clear').fadeIn(150);
                } else {
                    footer.find('#addApplicantModal_Save, #addApplicantModal_Clear').fadeOut(150);
                }
            });


        AppUtils.maskPhoneInput("#txtPhoneNumber");

        // ✅ When modal is shown, rebind Bootstrap tab logic
        $(document).off('click.bs.tab.dynamic', '[data-bs-toggle="tab"]').on('click.bs.tab.dynamic', '[data-bs-toggle="tab"]', function (e) {
            e.preventDefault();

            const tabTrigger = new bootstrap.Tab(this);
            tabTrigger.show(); // Manually activate the tab
        });
    });

    // 🔍 Handle search via Enter key
    $(document).on('keypress', '#txtSearchEmail', function (e) {
        if (e.which === 13) {
            e.preventDefault();

            const searchValue = $(this).val().trim();
            const hireType = $('#ddlHireType').val();

            if (!searchValue) return;

            console.log("🔍 Searching applicants:", searchValue, hireType);

            $.ajax({
                url: `/CandidateSearch/Search`,
                type: 'GET',
                data: { email: searchValue, hireType: hireType },
                success: function (data) {
                    const tbody = $('#tblSearchResults tbody');
                    tbody.empty();

                    if (data && data.length > 0) {
                        data.forEach(x => {
                            tbody.append(`
                                <tr>
                                    <td>${x.name}</td>
                                    <td>${x.email}</td>
                                    <td>${x.phone}</td>
                                    <td><button class="btn btn-sm btn-primary">Select</button></td>
                                </tr>
                            `);
                        });
                    } else {
                        tbody.append(`<tr><td colspan="4" class="text-center text-muted">No results found</td></tr>`);
                    }
                },
                error: function () {
                    alert('Error retrieving results.');
                }
            });
        }
    });

    // 💾 Save Applicant
    $(document).on('click', '#btnSaveApplicant', function () {
        const applicant = {
            hireType: $('#ddlHireTypeCreate').val(),
            firstName: $('#txtFirstName').val(),
            lastName: $('#txtLastName').val(),
            phone: $('#txtPhoneNumber').val(),
            email: $('#txtEmailAddress').val(),
            street: $('#txtStreet').val(),
            aptNumber: $('#txtAptNumber').val(),
            city: $('#txtCity').val(),
            state: $('#txtState').val(),
            postalCode: $('#txtPostalCode').val(),
            country: $('#txtCountry').val()
        };

        console.log("🧩 New Applicant:", applicant);

        // Example API call
        // $.post('/Applicant/Create', applicant, function(response) {
        //     console.log('✅ Applicant saved', response);
        // });
    });

    $('#exportExcel').on('click', function (e) {
        e.preventDefault();
        AppUtils.showToast('Exported to Excel!', 'success');
    });

    $('#exportCsv').on('click', function (e) {
        e.preventDefault();
        AppUtils.showToast('Exported to CSV!', 'success');
    });

    $('#exportPdf').on('click', function (e) {
        e.preventDefault();
        AppUtils.showToast('Exported to PDF!', 'success');
    });

    // Search filter example
    $('#searchInput').on('keyup', function () {
        const searchValue = $(this).val().toLowerCase();
        $("#kt_hiring_roster tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(searchValue) > -1);
        });
    });
})();
