

// função para criar a preview do arquivo de imagem selecionado
// input, são os arquivos recebidos pelo input do tipo file
// elementId, id da tag img que irá conter a preview do arquivo selecionado
function imagePreview(input, elementId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(elementId)
                .attr('src', e.target.result)
                .width(200)
                .height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}