let intentos = 10;
let letrasUsadas = [];

function ArriesgarLetra() {

    const inputLetra = document.getElementById("letra");
    const inputPalabra = document.getElementById("palabra");
    const divPalabra = document.getElementById("palabraOculta");
    const divChances = document.getElementById("ChancesRestantes");
    const divMensaje = document.getElementById("mensaje");

    let letra = inputLetra.value.toUpperCase();
    let palabra = inputPalabra.value.toUpperCase();

    if (letra == "") {
        return;
    }

    if (letrasUsadas.includes(letra)) {
        divMensaje.innerHTML = "Ya usaste esa letra.";
        inputLetra.value = "";
        return;
    }

    letrasUsadas.push(letra);

    if (!palabra.includes(letra)) {
        intentos--;
    }

    let palabraMostrada = "";
    let gano = true;

    for (let i = 0; i < palabra.length; i++) {

        if (letrasUsadas.includes(palabra[i])) {
            palabraMostrada += palabra[i] + " ";
        }
        else {
            palabraMostrada += "_ ";
            gano = false;
        }
    }

    divPalabra.innerHTML = palabraMostrada;

    divChances.innerHTML =
        "Intentos restantes: " + intentos;

    if (gano) {
        divMensaje.innerHTML =
            "<strong>Descubriste la palabra. Revisá el diario... A dejó algo escondido ahí.</strong>";

        inputLetra.disabled = true;
    }

    if (intentos == 0) {

        divMensaje.innerHTML =
            "<strong>Te quedaste sin intentos. Perdiste una vida.</strong>";

        inputLetra.disabled = true;

        document.getElementById("formPerderVida").submit();
    }

    inputLetra.value = "";
}