﻿function setToastr() {
    toastr.options = {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-center",
        "showDuration": "300",
        "hideDuration": "1500",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
    }
}

function exibirModalDetalhesCliente() {
    $("#modalDetalheUsuario").modal("show");
}

function refresh() {
    window.location.reload();
}
function showModalLogin() {
    $("#modalLogin").modal("show");
}
function exibirModalUploadImagem(){
    $("#modalAddImagem").modal("show");
}
function showModalPesquisa() {
    $("#modalPesquisa").modal("show");
}
function pesquisaGravadoSucesso(data) {
    $("#modalPesquisa").modal("hide");
    $('body').removeClass('modal-open');
    $('.modal.backdrop').remove();

    var doc = new DOMParser().parseFromString(data, "text/html");

    var elemento = doc.getElementById("nomeUsuario").innerHTML + "!";
    var nome = elemento.split(" ");
    var title = "Obrigado ".concat(nome[0]);
    title.concat("!");

    setToastr();
    toastr["info"]("Você ganhou 10 pontos!", title);
}
function falhaGravarPesquisa() {
    $("#modalPesquisa").modal("hide");
    $('body').removeClass('modal-open');
    $('.modal.backdrop').remove();

    setToastr();
    toastr["fail"]("Houve um erro ao gravar a pesquisa!", "Ops!");
}
function closeModalLogin() {
    $("#modalLogin").modal("hide");
    $('body').removeClass('modal-open');
    $('.modal.backdrop').remove();
}
function showModalCreateUser() {
    $("#modalCreateUser").modal("show");
}
function failCadastroProduto() {
    $("#modalCadastroProduto").modal("hide");

    setToastr();
    toastr["fail"]("Erro!", "Atenção");
}
function falhaGravarCategoria() {
    $("#modalCadastroCategoria").modal("hide");

    setToastr();
    toastr["fail"]("Erro ao gravar categoria!", "Atenção");
}
function categoriaGravadoSucesso() {
    $("#modalCadastroCategoria").modal("hide");

    setToastr();
    toastr["success"]("Categoria inserida com sucesso!", "Adicionado");
}
function showCadastroProduto() {
    $("#modalCadastroProduto").modal("show");
}
function showCadastroCategoria() {
    $("#modalCadastroCategoria").modal("show");
}
function cadastroClienteSucesso(data) {

    $("#modalCreateUser").modal("hide");
    $('body').removeClass('modal-open');
    
    var doc = new DOMParser().parseFromString(data, "text/html");

    var elemento = doc.getElementById("nomeUsuario").innerHTML + "!";
    var nome = elemento.split(" ");
    var title = "Olá ".concat(nome[0]);

    setToastr();
    toastr["success"]("Seu cadastro foi realizado com sucesso!", title);
    $('.modal.backdrop').remove();
}
function loginSucesso(data) {
    var doc = new DOMParser().parseFromString(data, "text/html");

    $("#modalLogin").modal("hide");
    $('body').removeClass('modal-open');
    $('.modal.backdrop').remove();
    
    var elemento = doc.getElementById("nomeUsuario").innerHTML + "!";
    var nome = elemento.split(" ");
    var title = "Olá ".concat(nome[0]);
    
    toastr.options =
    {
        "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-center",
        "showDuration": "300",
        "hideDuration": "1500",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "onHidden": refresh(),
    };
    toastr["success"]("Seja bem vindo!", title);
}
function cadastroSucesso(data) {
    $("#btnCadastroPrimeiroAdmin").hide();
    $("#modalLogin").modal("hide");
    $("#modalCreateUser").modal("show");
}
function falhaCadastroUsuario(data) {
    closeModalLogin();

    setToastr();
    toastr["error"]("Ocorreu um erro durante o cadastro", "Atenção");
    console.log();
}
function falhaLogin() {
    closeModalLogin();

    setToastr();
    toastr["error"]("Ocorreu um erro durante o login", "Atenção");
    console.log();
}
function fail(data) {
    setToastr();
    toastr["error"]("Ocorreu um erro desconhecido.", "Atenção");
    console.log(data);
}