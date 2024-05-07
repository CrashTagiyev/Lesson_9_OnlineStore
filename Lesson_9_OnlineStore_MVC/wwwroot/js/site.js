
var addTagForm = document.getElementById("Add-Tag-Form")
var showAddTagButton = document.getElementById("Show-Add-Tag")

addTagForm.style.display = "None";

showAddTagButton.addEventListener("click", () => {
    addTagForm.style.display = addTagForm.style.display === "none" ? "block" : "none"
    showAddTagButton.innerText = showAddTagButton.innerText === "Add Tag" ? "Close" : "Add Tag"
});