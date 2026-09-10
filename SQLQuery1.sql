USE Sala;
GO

INSERT INTO salas
(idSala, nombreSala, orden, descripcion, respuestaEsperada, imagenes)
VALUES
(
    1,
    'The Beginning',
    1,
    'Te despertás en una habitación oscura. Hay un teléfono, fotos, un cuaderno, un candado y una vela. A dejó un mensaje y tenés que investigar los objetos para descubrir la palabra que abre la puerta.',
    'ROSE',
    'sala1.jpg'
);

INSERT INTO salas
(idSala, nombreSala, orden, descripcion, respuestaEsperada, imagenes)
VALUES
(
    2,
    'A''s Messages',
    2,
    'Entrás a una habitación donde un celular empieza a recibir mensajes. Entre los textos hay información escondida que tenés que encontrar y ordenar para descubrir el código.',
    '7429',
    'sala2.jpg'
);

INSERT INTO salas
(idSala, nombreSala, orden, descripcion, respuestaEsperada, imagenes)
VALUES
(
    3,
    'Alison''s Room',
    3,
    'La llave abre la habitación de Alison. Hay un diario, fotos, una muñeca, un espejo y una caja cerrada. Tenés que investigar cada objeto y combinar las pistas.',
    '0627',
    'sala3.jpg'
);

INSERT INTO salas
(idSala, nombreSala, orden, descripcion, respuestaEsperada, imagenes)
VALUES
(
    4,
    'Who is A?',
    4,
    'Entrás a una habitación llena de fotografías, fechas y lugares. Tenés que relacionar las pistas correctamente para descubrir la siguiente clave.',
    'A',
    'sala4.jpg'
);

INSERT INTO salas
(idSala, nombreSala, orden, descripcion, respuestaEsperada, imagenes)
VALUES
(
    5,
    'The Final Game',
    5,
    'Llegaste a la última habitación. Frente a vos hay un teclado. Tenés que usar todo lo descubierto en las salas anteriores para ingresar el código final y escapar.',
    'ROSE74290627A',
    'sala5.jpg'
);

SELECT * FROM salas;