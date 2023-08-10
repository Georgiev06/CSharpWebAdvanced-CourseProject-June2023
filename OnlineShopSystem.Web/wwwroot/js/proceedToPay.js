function proceedToPay(e) {
    e.preventDefault();
    Swal.fire({
        title: 'Order Submitted!',
        icon: 'success',
        showCancelButton: false,
        cancelButtonText: 'Cancel',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
    })
}

