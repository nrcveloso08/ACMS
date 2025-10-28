// ======================================
// Candidate Search Page Script
// Handles Applicant Table Rendering and Modals
// ======================================

"use strict";

var CandidateSearchPage = (function () {

    // ======================================
    // 🔹 1. Private Variables
    // ======================================
    let table;

    const dummyApplicants = [
        {
            name: "(Testing) Alejandra Martina Perez Leon",
            email: "alemar@gmail.com",
            phone: "+(502) 4455-7788",
            language: "No Assigned Language",
            employmentType: "No Assigned Employment Type",
            dayforce: "null"
        },
        {
            name: "Testing Firstname Testing Lastname",
            email: "email@testmail.com",
            phone: "+639062151603",
            language: "No Assigned Language",
            employmentType: "No Assigned Employment Type",
            dayforce: "null"
        },
        {
            name: "Test .",
            email: "123456@789.com",
            phone: "123123",
            language: "English",
            employmentType: "Fulltime",
            dayforce: "null"
        }
    ];

    // ======================================
    // 🔹 2. Table Initialization
    // ======================================
    const initApplicantsTable = function () {
        table = $("#applicantsTable").DataTable({
            data: dummyApplicants,
            columns: [                
                { data: "name" },
                { data: "email" },
                {
                    data: "phone",
                    render: (data) => `<i class="fa fa-star text-danger mr-1"></i> ${data} <i class="fa fa-copy text-muted ml-1"></i>`
                },
               
                { data: "language" },
                { data: "employmentType" },
                {
                    data: "dayforce",
                    render: (data) => `${data} <i class="fa fa-copy text-muted ml-1"></i>`
                },
                {
                    data: null,
                    defaultContent: "",
                    orderable: false,
                    render: function (row) {
                        return `
                        <div class="dropdown">
                            <button class="btn btn-sm btn-light btn-icon" data-toggle="dropdown">
                                <i class="fa fa-ellipsis-v"></i>
                            </button>
                            <div class="dropdown-menu dropdown-menu-right shadow-sm">
                                <a class="dropdown-item btnApplicantDetails" href="#" data-name="${row.name}">
                                    <i class="fa fa-info-circle mr-2 text-primary"></i> Applicant Details
                                </a>
                                <a class="dropdown-item btnEmailApplicant" href="#" data-email="${row.email}">
                                    <i class="fa fa-envelope mr-2 text-info"></i> Email
                                </a>
                                <a class="dropdown-item btnSkillTest" href="#" data-name="${row.name}">
                                    <i class="fa fa-pencil-alt mr-2 text-warning"></i> Skill Test Result
                                </a>
                                <a class="dropdown-item btnUpdateWage" href="#" data-name="${row.name}">
                                    <i class="fa fa-edit mr-2 text-success"></i> Update Wave/Wage Info
                                </a>
                                <a class="dropdown-item btnEmploymentApp" href="#">
                                    <i class="fa fa-eye mr-2 text-secondary"></i> Employment Application
                                </a>
                            </div>
                        </div>`
                    }
                }
            ],
            responsive: true,
            autoWidth: false,
            ordering: true,
            pageLength: 10,
            dom: "<'table-responsive'tr>" + "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"
        });
    };

    // ======================================
    // 🔹 3. Event Handlers
    // ======================================
    const bindEvents = function () {

        // Applicant Details redirect
        $(document).on("click", ".btnApplicantDetails", function () {
            window.location.href = "/Dashboard/Edit";
        });

        // Email Modal
        $(document).on("click", ".btnEmailApplicant", function () {
            const email = $(this).data("email");
            AppUtils.loadModal("emailApplicantModal", {
                title: `<i class="fa fa-envelope text-info mr-2"></i> Send Email`,
                body: `
                    <form id="frmEmailApplicant" class="container-fluid">
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <label>To</label>
                                <input type="email" class="form-control" value="${email}" required />
                            </div>
                            <div class="form-group col-md-6">
                                <label>Subject</label>
                                <input type="text" class="form-control" placeholder="Enter subject" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label>Body</label>
                            <textarea class="form-control" rows="6" placeholder="Write your message..."></textarea>
                        </div>
                    </form>
                `,
                settings: "modal-dialog-centered modal-lg",
                buttons: {
                    Send: {
                        Enabled: true,
                        text: "Send",
                        btnClass: "btn btn-primary",
                        action: () => {
                            AppUtils.showToast("Email sent successfully!", "success");
                            $("#emailApplicantModal").modal("hide");
                        }
                    },
                    Cancel: {
                        Enabled: true,
                        text: "Cancel",
                        btnClass: "btn btn-secondary",
                        autoDismiss: true
                    }
                }
            });
        });

        // Update Wave/Wage Info Modal
        $(document).on("click", ".btnUpdateWage", function () {
            const name = $(this).data("name");
            AppUtils.loadModal("updateWageModal", {
                title: `${name} - Update Wage Info`,
                body: `
                    <form id="frmUpdateWage" class="p-2">
                        <div class="form-group">
                            <label>Location</label>
                            <select class="form-control">
                                <option selected>Guatemala</option>
                                <option>Philippines</option>
                                <option>Canada</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label>Requisition Request</label>
                            <select class="form-control">
                                <option>Select a Requisition Request</option>
                                <option>Request 1</option>
                                <option>Request 2</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label>Wage</label>
                            <input type="number" class="form-control" placeholder="0.00" />
                        </div>
                        <div class="form-group">
                            <label>Hire Type</label>
                            <select class="form-control">
                                <option>Select Hire Type</option>
                                <option>Full Time</option>
                                <option>Part Time</option>
                            </select>
                        </div>
                        <div class="form-group d-flex justify-content-between align-items-center">
                            <div>
                                <label>Orientation Date</label>
                                <p class="mb-0 text-muted">Not Booked</p>
                            </div>
                            <button class="btn btn-success btn-sm">
                                <i class="fa fa-calendar-alt mr-2"></i> Book Orientation
                            </button>
                        </div>
                    </form>
                `,
                settings: "modal-dialog-centered modal-md",
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "Save",
                        btnClass: "btn btn-primary",
                        action: () => {
                            AppUtils.showToast("Wage information saved!", "success");
                            $("#updateWageModal").modal("hide");
                        }
                    },
                    Cancel: {
                        Enabled: true,
                        text: "Cancel",
                        btnClass: "btn btn-secondary",
                        autoDismiss: true
                    }
                }
            });
        });

        // Skill Test Result Modal
        $(document).on("click", ".btnSkillTest", function () {
            const name = $(this).data("name");
            AppUtils.loadModal("skillTestModal", {
                title: `${name} - Skill Test Result`,
                body: `
                    <form id="frmSkillTest" class="p-3">
                        <div class="form-group">
                            <label>Typing</label>
                            <input type="text" class="form-control" placeholder="Enter Typing Result" />
                        </div>
                        <div class="form-group">
                            <label>Grammar</label>
                            <input type="text" class="form-control" placeholder="Enter Grammar Result" />
                        </div>
                        <div class="form-group">
                            <label>Spelling</label>
                            <input type="text" class="form-control" placeholder="Enter Spelling Result" />
                        </div>
                    </form>
                `,
                settings: "modal-dialog-centered modal-sm",
                buttons: {
                    Save: {
                        Enabled: true,
                        text: "Save",
                        btnClass: "btn btn-primary",
                        action: () => {
                            AppUtils.showToast("Skill test result saved!", "success");
                            $("#skillTestModal").modal("hide");
                        }
                    },
                    Cancel: {
                        Enabled: true,
                        text: "Cancel",
                        btnClass: "btn btn-secondary",
                        autoDismiss: true
                    }
                }
            });
        });
    };

    // ======================================
    // 🔹 4. Public Init
    // ======================================
    const init = function () {
        initApplicantsTable();
        bindEvents();
    };

    return { init };

})();

// ======================================
// 🔹 5. Initialize
// ======================================
$(document).ready(function () {
    CandidateSearchPage.init();
});
