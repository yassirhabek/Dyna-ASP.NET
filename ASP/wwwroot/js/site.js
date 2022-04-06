//const { Alert } = require("../lib/bootstrap/dist/js/bootstrap");
//const { data } = require("jquery");


$("#buttontoevoegen").click(() => {
    WerknemerToevoegen();
})

$("#buttonaanpassen").click(() => {
    WerknemerAanpassen();
})

$('#buttonverwijderen').click(() => {
    WerknemerVerwijderen();
})

function WerknemerToevoegen() {
    let target = window.location.protocol + "//" + window.location.host;

    Naam = $("#input-naam").val();
    WerknemerNum = $('#input-werknemernum').val();
    TelefoonNum = $('#input-telefoonnum').val();

    alert(Naam + ", " + WerknemerNum + ", " + TelefoonNum)

    $.post(target + "/Werknemer/WerknemerToevoegen", { naam: Naam, werknemerNum: WerknemerNum, telefoonNum: TelefoonNum });
}

function WerknemerAanpassen() {
    let target = window.location.protocol + "//" + window.location.host;

    WerknemerID = $("#drop-aanpassen").val();
    Naam = $("#changed-naam").val(); 
    WerknemerNum = $("#changed-werknemernum").val();
    TelefoonNum = $("#changed-telefoonnummer").val();

    alert(Naam + ", " + WerknemerNum + ", " + TelefoonNum)

    $.post(target + "/Werknemer/WerknemerAanpassen", { newNaam: Naam, newWerknemerNum: WerknemerNum, newTelefoonNum: TelefoonNum, oldWerknemerID: WerknemerID });
}

function WerknemerVerwijderen() {
    let target = window.location.protocol + "//" + window.location.host;

    WerknemerID = $("#drop-verwijderen").val();

    alert(WerknemerID)

    $.post(target + "/Werknemer/WerknemerVerwijderen/" + WerknemerID, null);
}

