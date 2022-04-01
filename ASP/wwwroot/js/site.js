//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap");

$("#button").click(() => {
    let target = window.location.protocol + "//" + window.location.host;

    Naam = $("#input-naam").val();
    WerknemerNum = $('#input-werknemernum').val();
    TelefoonNum = $('#input-telefoonnum').val();

    $.post(target + "/Test/WerknemerToevoegen", { naam: Naam, werknemerNum: WerknemerNum, telefoonNum = TelefoonNum });
    })
    .fail(() => { alert("Werknemer Niet Toegevoegd.") });