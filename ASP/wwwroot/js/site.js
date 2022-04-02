//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap");

$("#button").click(() => {
    let target = window.location.protocol + "//" + window.location.host;

    Naam = $("#input-naam").val();
    WerknemerNum = $('#input-werknemernum').val();
    TelefoonNum = $('#input-telefoonnum').val();

    alert(Naam + ", " + WerknemerNum + ", " + TelefoonNum)

    $.post(target + "/Werknemer/WerknemerToevoegen", { naam: Naam, werknemerNum: WerknemerNum, telefoonNum: TelefoonNum });
})
    //.done(() => { alert("toegevoegd") })
    //.fail(() => { alert("Werknemer Niet Toegevoegd.") });