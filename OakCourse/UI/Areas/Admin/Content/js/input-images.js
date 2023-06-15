

function loadImages(images) {
    console.clear();
    console.log(images.length);

    for (var ind = 0; ind < images.length; ind++) {
        if (ind > 0) {
            htmlInput = `<input type="file" name="PostImage" accept="image/gif, image/jpg, image/jpeg, image/png" multiple onchange="$('#PostImage')[${ind}].src=window.URL.createObjectURL(this.files[${ind}])" />`;
            $("#inputImages").append(htmlInput);
        }
    }

}