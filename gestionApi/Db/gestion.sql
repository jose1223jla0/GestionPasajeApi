CREATE DATABASE VentaPasaje;
USE VentaPasaje;

-- DROP DATABASE VentaPasaje;

CREATE TABLE PASAJERO
(
    IdPasajero            INT         NOT NULL AUTO_INCREMENT,
    NombrePasajero        VARCHAR(30) NOT NULL,
    ApellidoPasajero      VARCHAR(30) NOT NULL,
    DniPasajero           CHAR(8)     NOT NULL,
    TelefonoPasajero      CHAR(9)     NOT NULL,
    FechaCreacionPasajero DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdPasajero)
);


CREATE TABLE CONDUCTOR
(
    IdConductor            INT         NOT NULL AUTO_INCREMENT,
    NombreConductor        VARCHAR(30) NOT NULL,
    ApellidoConductor      VARCHAR(30) NOT NULL,
    TelefonoConductor      CHAR(9)     NOT NULL,
    EstadoConductor        BOOLEAN     NOT NULL,
    FechaCreacionConductor DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdConductor)
);

CREATE TABLE RUTA
(
    IdRuta            INT         NOT NULL AUTO_INCREMENT,
    OrigenRuta        VARCHAR(20) NOT NULL,
    DestinoRuta       VARCHAR(20) NOT NULL,
    DuracionRuta      TIME        NOT NULL,
    EstadoRuta        BOOLEAN     NOT NULL,
    FechaCreacionRuta DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdRuta)
);


CREATE TABLE VEHICULO
(
    IdVehiculo            INT         NOT NULL AUTO_INCREMENT,
    PlacaVehiculo         VARCHAR(7)  NOT NULL,
    MarcaVehiculo         VARCHAR(20) NOT NULL,
    ModeloVehiculo        VARCHAR(20) NOT NULL,
    CapacidadVehiculo     INT         NOT NULL,
    EstadoVehiculo        BOOLEAN     NOT NULL,
    IdConductor           INT         NOT NULL,
    FechaCreacionVehiculo DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdVehiculo),
    FOREIGN KEY (IdConductor) REFERENCES CONDUCTOR (IdConductor)
);


CREATE TABLE HORARIO
(
    IdHorario            INT           NOT NULL AUTO_INCREMENT,
    FechaSalida          DATE          NOT NULL,
    HoraSalida           TIME          NOT NULL,
    Precio               DECIMAL(5, 2) NOT NULL,
    EstadoHorario        BOOLEAN       NOT NULL,
    IdVehiculo           INT           NOT NULL,
    IdRuta               INT           NOT NULL,
    FechaCreacionHorario DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdHorario),
    FOREIGN KEY (IdVehiculo) REFERENCES VEHICULO (IdVehiculo),
    FOREIGN KEY (IdRuta) REFERENCES RUTA (IdRuta)
);

CREATE TABLE PAGO
(
    IdPago            INT            NOT NULL AUTO_INCREMENT,
    MontoPago         DECIMAL(10, 2) NOT NULL,
    MetodoPago        VARCHAR(20)    NOT NULL,
    EstadoPago        BOOLEAN        NOT NULL,
    FechaCreacionPago DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdPago)
);


CREATE TABLE USUARIO
(
    IdUsuario            INT          NOT NULL AUTO_INCREMENT,
    NombreUsuario        VARCHAR(8)   NOT NULL UNIQUE,
    Contrasena           VARCHAR(255) NOT NULL,
    EstadoUsuario        BOOLEAN      NOT NULL DEFAULT TRUE,
    FechaCreacionUsuario DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdUsuario)
);

CREATE TABLE PASAJE
(
    IdPasaje            INT      NOT NULL AUTO_INCREMENT,
    FechaventaPasaje    DATE     NOT NULL,
    EstadoPasaje        BOOLEAN  NOT NULL,
    IdPasajero          INT      NOT NULL,
    IdHorario           INT      NOT NULL,
    IdPago              INT      NOT NULL,
    FechaCreacionPasaje DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IdUsuario           INT      NOT NULL,
    PRIMARY KEY (IdPasaje),
    FOREIGN KEY (IdPasajero) REFERENCES PASAJERO (IdPasajero),
    FOREIGN KEY (IdHorario) REFERENCES HORARIO (IdHorario),
    FOREIGN KEY (IdPago) REFERENCES PAGO (IdPago),
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO (IdUsuario)
);


CREATE TABLE ASIENTO
(
    IdAsiento     INT     NOT NULL AUTO_INCREMENT,
    EstadoAsiento BOOLEAN NOT NULL,
    IdHorario     INT     NOT NULL,
    PRIMARY KEY (IdAsiento),
    FOREIGN KEY (IdHorario) REFERENCES HORARIO (IdHorario)
);


-- ===================================================================
-- CONDUCTOR
-- ===================================================================
INSERT INTO CONDUCTOR (NombreConductor, ApellidoConductor, TelefonoConductor, EstadoConductor)
VALUES ('Carlos Alberto', 'Ramírez Bravo', '987654321', true);

INSERT INTO CONDUCTOR (NombreConductor, ApellidoConductor, TelefonoConductor, EstadoConductor)
VALUES ('Lucía', 'Pérez Gómez', '912345678', true);

SELECT *
FROM CONDUCTOR;

-- ===================================================================
-- RUTA
-- ===================================================================
INSERT INTO RUTA (OrigenRuta, DestinoRuta, DuracionRuta, EstadoRuta)
VALUES ('Andahuaylas', 'Abancay', '04:30:00', TRUE);

-- ===================================================================
-- VEHICULO
-- ===================================================================
INSERT INTO VEHICULO (PlacaVehiculo, MarcaVehiculo, ModeloVehiculo, CapacidadVehiculo, EstadoVehiculo, IdConductor)
VALUES ('ABC1234', 'Toyota', 'Hiace', 18, TRUE, 1);

-- ===================================================================
-- HORARIO
-- ===================================================================
INSERT INTO HORARIO (FechaSalida, HoraSalida, Precio, EstadoHorario, IdVehiculo, IdRuta)
VALUES ('2025-05-10', '08:00:00', 45.00, TRUE, 1, 1);

SELECT h.IdHorario,
       h.FechaSalida,
       h.HoraSalida,
       h.Precio,
       h.EstadoHorario,
       v.IdVehiculo,
       v.PlacaVehiculo,
       v.MarcaVehiculo,
       v.ModeloVehiculo,
       v.CapacidadVehiculo,
       v.EstadoVehiculo
FROM HORARIO h
         INNER JOIN VEHICULO v on h.IdVehiculo = v.IdVehiculo;

SELECT *
FROM CONDUCTOR;