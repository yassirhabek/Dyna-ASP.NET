//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap");

$("#button").click(() => {
    let target = window.location.protocol + "//" + window.location.host;

    Naam = $("#input-naam").val();
    WerknemerNum = $('#input-werknemernum').val();

    alert(Naam + WerknemerNum);

    $.post(target + "/Test/WerknemerToevoegen", { naam: Naam, werknemerNum: WerknemerNum });
    })
        .fail(() => { alert("Werknemer Niet Toegevoegd.") });