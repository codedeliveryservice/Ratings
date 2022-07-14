(function (app) {
    imageInput.onchange = (event) => {
        const file = imageInput.files[0];
        if (file) {
            imageContainer.style.backgroundImage = `url(${URL.createObjectURL(file)})`;
            imageContainer.innerText = '';
        }
    }
})(window.app = window.app || {});