function changerImage(animalId, imageUrl) {
    var animalImage = document.getElementById("image-" + animalId);
    if (animalImage) {
        animalImage.src = imageUrl;
        saveImageUrlToDatabase(animalId, imageUrl);
}
    function getImageUrlInputValue(elementId) {
        var inputElement = document.getElementById(elementId);
        if (inputElement) {
            return inputElement.value;
        }
        return null;
    }