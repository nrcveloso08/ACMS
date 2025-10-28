// wwwroot/js/wave-roster/wave-notes.js
$(document).ready(function () {
    console.log("✅ wave-notes.js initialized");

    // Delegated binding (for dynamic partials)
    $(document).on('click', '#btnAddWaveNote', function (e) {
        e.preventDefault();
        console.log("🟢 Add Wave Note clicked");

        const modalBody = `
            <form id="waveNoteForm">
                <div class="form-group mb-3">
                    <label for="txtWaveNote" class="form-label fw-bold">Note</label>
                    <textarea class="form-control" id="txtWaveNote" name="Note" 
                              placeholder="Insert note..." rows="4" required></textarea>
                </div>
            </form>
        `;

        // Load modal dynamically using your AppUtils helper
        AppUtils.loadModal('addWaveNoteModal', {
            title: "Add New Note",
            body: modalBody,
            buttons: {
                Cancel: {
                    Enabled: true,
                    text: "Cancel",
                    btnClass: "btn btn-light",
                    autoDismiss: true
                },
                Save: {
                    Enabled: true,
                    text: "Save Note",
                    btnClass: "btn btn-primary",
                    action: function () {
                        if (!AppUtils.validateForm('#waveNoteForm')) return;

                        const noteValue = $('#txtWaveNote').val().trim();
                        console.log("📝 Saving note:", noteValue);

                        AppUtils.ajaxCall({
                            url: '/WaveNotes/SaveNote',
                            type: 'POST',
                            data: { note: noteValue },
                            successMessage: "Note saved successfully!",
                            onSuccess: function () {
                                // ✅ Close modal after success
                                const modalEl = document.getElementById('addWaveNoteModal');
                                const modalInstance = bootstrap.Modal.getInstance(modalEl);
                                if (modalInstance) modalInstance.hide();

                                // Append or refresh table
                                $('#waveNotesTableBody').prepend(`
                                    <tr>
                                        <td>${new Date().toISOString().split('T')[0]}</td>
                                        <td>Current User</td>
                                        <td>${noteValue}</td>
                                    </tr>
                                `);
                            }
                        });
                    }
                }
            }
        });
    });

    $('#addWaveNoteModal_Cancel').click(function () {
        $('#addWaveNoteModal').modal('hide');
    });
});
