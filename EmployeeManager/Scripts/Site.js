//this method does 2 things:
//1) store the PK of the employee to be deleted in a hidden input.  (this is the only way to get this value server-side)
//2) prompt the user for confirmation
function ConfirmDelete(employeePk) {
    var hiddenInput = document.getElementById("selEmployeePk");
    hiddenInput.setAttribute("value", employeePk)
    return confirm("Are you sure you want to delete this record?")
}