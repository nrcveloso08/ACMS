$(document).ready(function () {
    $('#btnProfileModal').on('click', function () {
        console.log('🟢 Bubble clicked');

        const modalBody = `
            <div class="text-center py-4">
                <div class="symbol symbol-100px mb-3">
                    <span class="symbol-label fs-2x fw-bold bg-dark text-success">EV</span>
                </div>
                <h4 class="fw-bold mb-1">Enrico Veloso</h4>
                <p class="text-muted mb-4">Software Developer</p>
                <a href="#" class="btn btn-light-primary w-100 mb-2">View Profile</a>
            </div>
        `;

        AppUtils.loadModal("profileModal", {
            title: "Profile",
            body: modalBody,
            buttons: {
                SignOut: {
                    Enabled: true,
                    text: "Sign Out",
                    btnClass: "btn btn-danger",
                    autoDismiss: true,
                    action: function () {
                        Swal.fire({
                            title: 'Are you sure you want to sign out?',
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'Yes, sign out',
                            cancelButtonText: 'Cancel',
                            reverseButtons: true
                        }).then(result => {
                            if (result.isConfirmed) {
                                window.location.href = '/Account/Logout';
                            }
                        });
                    }
                },
                Close: {
                    Enabled: true,
                    text: "Close",
                    btnClass: "btn btn-light",
                    autoDismiss: true
                }
            },
            settings: "modal-dialog-centered modal-md"
        });
    });
});
