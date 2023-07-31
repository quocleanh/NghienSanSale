function readFile() {
    if (!this.files || !this.files[0]) return;
    const FR = new FileReader();
    FR.addEventListener("load", function (evt) {
        debugger;
        document.querySelector("#Images").textContent = "";
        document.querySelector("#imgPreview").src = evt.target.result;
        document.querySelector("#Images").value = evt.target.result;
    });
    FR.readAsDataURL(this.files[0]);
}
document.querySelector("input[type=file]").addEventListener("change", readFile);