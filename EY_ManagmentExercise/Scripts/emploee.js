function validateId(model){
    if (model == null || model.value == null)
        alert("ID is required!")
    else {
        if (model.value.length != 9) {
            alert("ID must contains 9 digits.")
            model.value = null;
        }
    }
}