function proceedToPay(e) {
    e.preventDefault();
    Swal.fire({
        title: 'Are you sure you want to submit your order?',
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancel',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Order Submitted!',
                'We\'ve sent you an email with more details.',
                'success'
            ).then((resultDeleted) => {
                e.target.parentElement.parentElement.submit();
            })
        }
    })
}
