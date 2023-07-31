function myConfirm(e) {
    e.preventDefault();
    Swal.fire({
        title: 'Are you sure?',
        icon: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancel',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, remove it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Removed!',
                'Your book has been removed from your shopping cart.',
                'success'
            ).then((resultDeleted) => {
                e.target.parentElement.parentElement.submit();
            })
        }
    })
}
