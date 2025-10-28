// wwwroot/js/pages/_csi-schedule.js

$(document).ready(function () {

    // Create New CSI Schedule button
    $(document).on("click", "#btnCreateCsiSchedule", function () {
        AppUtils.loadModal("csiScheduleModal", {
            title: "Scheduler",
            body: getCsiModalBody(),
            buttons: {
                Save: {
                    Enabled: true,
                    text: "Save",
                    btnClass: "btn btn-primary",
                    action: saveCsiSchedule
                },
                Cancel: {
                    Enabled: true,
                    text: "Cancel",
                    btnClass: "btn btn-secondary",
                    autoDismiss: true
                }
            },
            settings: "modal-dialog-centered modal-md"
        });
    });

    // Build modal body content
    function getCsiModalBody() {
        return `
            <form id="frmCsiSchedule">
                <div class="form-group">
                    <label>Location</label>
                    <select id="LocationId" name="LocationId" class="form-control" required>
                        <option value="">Select a Location</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Program</label>
                    <select id="ProgramId" name="ProgramId" class="form-control" required>
                        <option value="">Select a Program</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Schedule</label>
                    <div class="d-flex">
                        <input type="datetime-local" id="StartDateTime" name="StartDateTime" class="form-control mr-2" required />
                        <input type="datetime-local" id="EndDateTime" name="EndDateTime" class="form-control" required />
                    </div>
                </div>
                <div class="form-group">
                    <label>Capacity</label>
                    <input type="number" id="Capacity" name="Capacity" class="form-control" value="0" required min="1" />
                </div>
            </form>
        `;
    }

    // Handle Save action
    function saveCsiSchedule() {
        if (!AppUtils.validateForm("#frmCsiSchedule")) {
            return;
        }

        const data = {
            LocationId: $("#LocationId").val(),
            ProgramId: $("#ProgramId").val(),
            StartDateTime: $("#StartDateTime").val(),
            EndDateTime: $("#EndDateTime").val(),
            Capacity: $("#Capacity").val()
        };

        AppUtils.ajaxCall({
            url: "/CsiSchedule/Create",
            type: "POST",
            data: data,
            successMessage: "CSI Schedule created successfully!",
            onSuccess: function () {
                $("#csiScheduleModal").modal("hide");
                loadCsiScheduleList();
            }
        });
    }

    // Reload schedule list (placeholder)
    function loadCsiScheduleList() {
        console.log("Reload CSI schedule table...");
        // You can later integrate DataTables or reload via AJAX
    }

});
