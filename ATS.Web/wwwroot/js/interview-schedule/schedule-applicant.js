
$(document).ready(function () {
    $('#btnScheduleApplicant').on('click', function () {
        const modalBody = `
                    <div class="mb-4">
                        <div class="input-group">
                            <span class="input-group-text bg-light">
                                <i class="ki-outline ki-magnifier fs-3 text-muted"></i>
                            </span>
                            <input type="text" id="candidateSearch" class="form-control" placeholder="Search..." />
                        </div>
                    </div>

                    <div class="table-responsive">
                        <table id="scheduleCandidateTable" class="table table-striped table-bordered align-middle w-100">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Email Address</th>
                                    <th>Phone Number</th>
                                </tr>
                            </thead>
                            <tbody>
                                <!-- Will be dynamically loaded -->
                            </tbody>
                        </table>
                    </div>
                `;

        AppUtils.loadModal("scheduleApplicantModal", {
            title: "Schedule Candidate",
            body: modalBody,
            buttons: {
                Close: {
                    Enabled: true,
                    text: "Close",
                    btnClass: "btn btn-light-danger",
                    autoDismiss: true
                },
                Schedule: {
                    Enabled: true,
                    text: "Schedule",
                    btnClass: "btn btn-primary",
                    autoDismiss: false,
                    action: function () {
                        // Example: handle scheduling logic
                        alert("Schedule button clicked!");
                    }
                }
            },
            settings: "modal-dialog-centered modal-xl"
        });

        // Initialize DataTable after modal loads
        setTimeout(() => {
            $('#scheduleCandidateTable').DataTable({
                paging: true,
                searching: true,
                responsive: true,
                language: { searchPlaceholder: "Search candidate..." }
            });
        }, 300);
    });
});
