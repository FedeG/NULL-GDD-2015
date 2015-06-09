USE GD1C2015;
GO

SET LANGUAGE Spanish

/********************************** CREACIÓN DEL SCHEMA **************************************/
IF NOT EXISTS 
(
	SELECT  schema_name
	FROM    information_schema.schemata
	WHERE   schema_name = 'NULL'
)

BEGIN
	EXEC sp_executesql N'CREATE SCHEMA "NULL" AUTHORIZATION gd'
END


/********************************** FECHA DEL SISTEMA **************************************/
IF OBJECT_ID('NULL.Fecha_Sistema', 'U') IS NOT NULL
	DROP TABLE "NULL".Fecha_Sistema
GO

CREATE TABLE "NULL".Fecha_Sistema
(
	Fecha DATETIME PRIMARY KEY
);
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spSetFechaSistema' 
)
   DROP PROCEDURE "NULL".spSetFechaSistema
GO

CREATE PROCEDURE "NULL".spSetFechaSistema 
	@fecha DATETIME
AS
	BEGIN
		IF @fecha is NULL
				INSERT "NULL".Fecha_Sistema (Fecha) VALUES (GETDATE())
		ELSE
				INSERT "NULL".Fecha_Sistema (Fecha) VALUES (CONVERT(DATETIME, @fecha, 121)) --aaaa-mm-dd hh:mi:ss.mmm(24h)
	END;
GO

IF OBJECT_ID (N'NULL.fnGetFechaSistema') IS NOT NULL
   DROP FUNCTION "NULL".fnGetFechaSistema
GO

CREATE FUNCTION "NULL".fnGetFechaSistema()
RETURNS DATETIME
AS
	BEGIN
		DECLARE @fecha DATETIME
		SELECT TOP 1 @fecha = Fecha FROM "NULL".Fecha_Sistema
		RETURN @fecha
	END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'fnValidarTransferencia' 
)
   DROP FUNCTION "NULL".fnValidarTransferencia
GO

CREATE FUNCTION "NULL".fnValidarTransferencia(
	@Cuenta_Origen NUMERIC(18,0), 
	@Cuenta_Destino NUMERIC(18,0),
	@Importe INT
	)
	RETURNS INT
AS
BEGIN
	IF (SELECT COUNT(*) FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Destino AND Cuenta_Estado = 'Habilitada' AND Cuenta_Estado ='Inhabilitada') = 0
	BEGIN
		RETURN(1)
	END
	
	IF @Importe < 0 
	BEGIN
		RETURN(2)
	END
	
	IF(SELECT TOP 1 Cuenta_Saldo FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Origen) >= @Importe
	BEGIN
		RETURN(3)
	END
	
	RETURN(0)
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spRealizarTransferencia' 
)
   DROP PROCEDURE "NULL".spRealizarTransferencia
GO

CREATE PROCEDURE "NULL".spRealizarTransferencia
	@Cuenta_Origen NUMERIC(18,0), 
	@Cuenta_Destino NUMERIC(18,0),
	@Fecha_Transferencia DATETIME,
	@Importe INT
AS
BEGIN
	DECLARE @Validacion INT = "NULL".fnValidarTransferencia(@Cuenta_Origen, @Cuenta_Destino, @Importe)
	DECLARE @Transferencia_Costo INT = 0
	IF(@Validacion = 0)
	BEGIN
		IF(SELECT COUNT(*) FROM [GD1C2015].[NULL].[Cuenta] as c1, [GD1C2015].[NULL].[Cuenta] as c2 WHERE c1.Cuenta_Numero = @Cuenta_Origen AND c2.Cuenta_Numero = @Cuenta_Destino AND c1.Cli_Cod = c2.Cli_Cod) = 0
		BEGIN
			SET @Importe = (SELECT TOP 1 tc.TipoCta_Costo_Transf FROM [GD1C2015].[NULL].[TipoCuenta] as tc, [GD1C2015].[NULL].[Cuenta] as c WHERE c.TipoCta_Nombre = tc.TipoCta_Nombre AND c.Cuenta_Numero = @Cuenta_Origen)
		END
		
		INSERT INTO [GD1C2015].[NULL].[Transferencia](Cuenta_Origen_Numero, Cuenta_Destino_Numero, Transf_Fecha, Transf_Importe, Transf_Costo)
		VALUES(@Cuenta_Origen, @Cuenta_Destino, @Fecha_Transferencia, @Importe, @Transferencia_Costo)
	END
	
	RETURN @Validacion
END
GO

EXEC "NULL".spSetFechaSistema '2016-01-01 00:00:00.000'

/********************************** BORRADO DE TABLAS **************************************/
IF OBJECT_ID('NULL.Rol_Funcionalidad', 'U') IS NOT NULL
	DROP TABLE "NULL".Rol_Funcionalidad
GO

IF OBJECT_ID('NULL.Funcionalidad', 'U') IS NOT NULL
	DROP TABLE "NULL".Funcionalidad
GO

IF OBJECT_ID('NULL.Rol_Usuario', 'U') IS NOT NULL
	DROP TABLE "NULL".Rol_Usuario
GO

IF OBJECT_ID('NULL.Rol', 'U') IS NOT NULL
	DROP TABLE "NULL".Rol
GO

IF OBJECT_ID('NULL.Deposito', 'U') IS NOT NULL
	DROP TABLE "NULL".Deposito
GO

IF OBJECT_ID('NULL.Transferencia', 'U') IS NOT NULL
	DROP TABLE "NULL".Transferencia
GO

IF OBJECT_ID('NULL.Cheque', 'U') IS NOT NULL
	DROP TABLE "NULL".Cheque
GO

IF OBJECT_ID('NULL.Retiro', 'U') IS NOT NULL
	DROP TABLE "NULL".Retiro
GO

IF OBJECT_ID('NULL.Banco', 'U') IS NOT NULL
	DROP TABLE "NULL".Banco
GO

IF OBJECT_ID('NULL.Cuenta', 'U') IS NOT NULL
	DROP TABLE "NULL".Cuenta
GO

IF OBJECT_ID('NULL.Tarjeta', 'U') IS NOT NULL
	DROP TABLE "NULL".Tarjeta
GO

IF OBJECT_ID('NULL.Emisor', 'U') IS NOT NULL
	DROP TABLE "NULL".Emisor
GO

IF OBJECT_ID('NULL.Factura_Item', 'U') IS NOT NULL
	DROP TABLE "NULL".Factura_Item
GO

IF OBJECT_ID('NULL.Factura_Cabecera', 'U') IS NOT NULL
	DROP TABLE "NULL".Factura_Cabecera
GO

IF OBJECT_ID('NULL.Transaccion', 'U') IS NOT NULL
	DROP TABLE "NULL".Transaccion
GO

IF OBJECT_ID('NULL.Cliente', 'U') IS NOT NULL
	DROP TABLE "NULL".Cliente
GO

IF OBJECT_ID('NULL.TipoDoc', 'U') IS NOT NULL
	DROP TABLE "NULL".TipoDoc
GO

IF OBJECT_ID('NULL.Nacionalidad', 'U') IS NOT NULL
	DROP TABLE "NULL".Nacionalidad
GO

IF OBJECT_ID('NULL.Pais', 'U') IS NOT NULL
	DROP TABLE "NULL".Pais
GO

IF OBJECT_ID('NULL.Usuario', 'U') IS NOT NULL
	DROP TABLE "NULL".Usuario
GO

IF OBJECT_ID('NULL.TipoCuenta', 'U') IS NOT NULL
	DROP TABLE "NULL".TipoCuenta
GO

IF OBJECT_ID('NULL.Moneda', 'U') IS NOT NULL
	DROP TABLE "NULL".Moneda
GO


/********************************** CREACIÓN DE TABLAS **************************************/

CREATE TABLE "NULL".Funcionalidad
(
	Func_Cod NUMERIC(18,0) PRIMARY KEY IDENTITY(12,1),
	Func_Nombre NVARCHAR(255), 
	Func_Borrado BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE "NULL".Rol
(
	Rol_Nombre NVARCHAR(255) PRIMARY KEY,
	Rol_Estado NVARCHAR(255) CHECK (Rol_Estado IN('Habilitado', 'Deshabilitado')),
	Rol_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Rol_Funcionalidad
(
	Rol_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Rol(Rol_Nombre),
	Func_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Funcionalidad(Func_Cod),
	PRIMARY KEY(Rol_Nombre, Func_Cod)
);

CREATE TABLE "NULL".Usuario
(
	Usr_Username NVARCHAR(255) PRIMARY KEY,
	Usr_Password NVARCHAR(255) NOT NULL,
	Usr_Fecha_Creacion DATETIME NOT NULL,
	Usr_Fecha_Ultima_Modificacion DATETIME NOT NULL,
	Usr_Pregunta_Secreta NVARCHAR(255) NOT NULL,
	Usr_Respuesta_Secreta NVARCHAR(255) NOT NULL,
	Usr_Intentos_Login INT DEFAULT 0,
	Usr_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Rol_Usuario
(
	Rol_Nombre NVARCHAR(255) REFERENCES "NULL".Rol(Rol_Nombre),
	Usr_Username NVARCHAR(255) REFERENCES "NULL".Usuario(Usr_Username),
	PRIMARY KEY(Rol_Nombre, Usr_Username)
);

/**
Para después insertar valores específicos para un identity:

SET IDENTITY_INSERT Yaks ON

INSERT INTO dbo.Yaks (YakID, YakName) Values(1, 'Mac the Yak')

SET IDENTITY_INSERT Yaks OFF

 - See more at: http://www.sqlteam.com/article/understanding-identity-columns#sthash.VdvGfvjn.dpuf
*/

CREATE TABLE "NULL".TipoDoc
(
	TipoDoc_Cod NUMERIC(18,0) PRIMARY KEY IDENTITY(10003,1),
	TipoDoc_Desc NVARCHAR(255) NOT NULL,
	TipoDoc_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Pais
(
	Pais_Codigo NUMERIC(18,0) PRIMARY KEY,
	Pais_Desc NVARCHAR(250) NOT NULL,
	Pais_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cliente
(
	Cli_Cod NUMERIC(18,0) PRIMARY KEY IDENTITY(100000,1),
	Usr_Username NVARCHAR(255) NOT NULL REFERENCES "NULL".Usuario(Usr_Username),
	Cli_Nombre NVARCHAR(255),
	Cli_Apellido NVARCHAR(255),
	TipoDoc_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".TipoDoc(TipoDoc_Cod),
	Cli_Nro_Doc NVARCHAR(255) NOT NULL,
	Cli_Dom_Calle NVARCHAR(255),
	Cli_Dom_Nro NVARCHAR(20),
	Cli_Dom_Piso NVARCHAR(10),
	Cli_Dom_Depto NVARCHAR(10),
	Cli_Localidad NVARCHAR(255),
	Cli_Fecha_Nac DATETIME,
	Cli_Mail NVARCHAR(255),
	Cli_Nacionalidad NVARCHAR(255),
	Pais_Codigo NUMERIC(18,0) NOT NULL REFERENCES "NULL".Pais(Pais_Codigo),
	Cli_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".TipoCuenta
(
	TipoCta_Nombre NVARCHAR(255) PRIMARY KEY,
	TipoCta_Costo_Apertura NUMERIC(18,2) NOT NULL,
	TipoCta_Duracion NUMERIC(18,0) NOT NULL,
	TipoCta_Costo_Dia NUMERIC(18,2) NOT NULL,
	TipoCta_Costo_Transf NUMERIC(18,2) NOT NULL,
	TipoCta_Borrado BIT NOT NULL
);

CREATE TABLE "NULL".Moneda
(
	Moneda_Nombre NVARCHAR(255) PRIMARY KEY,
	Moneda_Simbolo NVARCHAR(5) NOT NULL, 
	Moneda_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cuenta
(
	Cuenta_Numero NUMERIC(18,0) PRIMARY KEY IDENTITY(1111111111111452, 1),
	Cuenta_Estado NVARCHAR(255) NOT NULL DEFAULT 'Pendiente de Activación' 
		CHECK (Cuenta_Estado IN('Pendiente de Activación', 'Cerrada', 'Inhabilitada', 'Habilitada')),
	Cuenta_Fecha_Vencimiento DATETIME,
	Cuenta_Fecha_Cierre DATETIME,
	Cuenta_Fecha_Creacion DATETIME NOT NULL,
	Cuenta_Saldo NUMERIC(18,2) NOT NULL DEFAULT 0,
	Pais_Codigo NUMERIC(18,0) NOT NULL REFERENCES "NULL".Pais(Pais_Codigo),
	TipoCta_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".TipoCuenta(TipoCta_Nombre),
	Cli_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	Moneda_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	Cuenta_Borrado BIT NOT NULL	 DEFAULT 0
);

CREATE TABLE "NULL".Emisor
(
	Emisor_Cod NUMERIC(18,0) PRIMARY KEY IDENTITY(100001,1),
	Emisor_Desc NVARCHAR(255) NOT NULL,
	Emisor_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Tarjeta
(
	Tarjeta_Numero NVARCHAR(255) PRIMARY KEY,
	Tarjeta_Numero_Visible NVARCHAR(255) NOT NULL,
	Tarjeta_Fecha_Emision DATETIME NOT NULL,
	Tarjeta_Fecha_Vencimiento DATETIME NOT NULL,
	Tarjeta_Codigo_Seg NVARCHAR(255) NOT NULL,
	Cli_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	Emisor_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Emisor(Emisor_Cod),
	Tarjeta_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Deposito
(
	Deposito_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(35135160440,1),
	Deposito_Importe NUMERIC(18,2) NOT NULL,
	Deposito_Fecha DATETIME NOT NULL,
	Tarjeta_Numero NVARCHAR(255) NOT NULL REFERENCES "NULL".Tarjeta(Tarjeta_Numero),
	Cuenta_Numero NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Deposito_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Transferencia
(
	Transf_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(10000000001,1),
	Transf_Fecha DATETIME NOT NULL,
	Transf_Importe NUMERIC(18,2) NOT NULL,
	Transf_Costo NUMERIC(18,2) NOT NULL,
	Cuenta_Origen_Numero NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Cuenta_Destino_Numero NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Transf_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Retiro
(
	Retiro_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(15315188277,1),
	Retiro_Importe NUMERIC(18,2) NOT NULL,
	Retiro_Fecha DATETIME NOT NULL,
	Cuenta_Numero NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Retiro_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Banco
(
	Banco_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(10005,1),
	Banco_Nombre NVARCHAR(255) NOT NULL,
	Banco_Direccion NVARCHAR(255),
	Banco_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cheque
(
	Retiro_Codigo NUMERIC(18,0) PRIMARY KEY REFERENCES "NULL".Retiro(Retiro_Codigo),
	Cheque_Numero NUMERIC(18,0) NOT NULL IDENTITY (151550275,1),
	Cheque_Fecha DATETIME NOT NULL,
	Cheque_Importe NUMERIC(18,2) NOT NULL,
	Cheque_Nombre NVARCHAR(255) NOT NULL,
	Banco_Codigo NUMERIC(18,0) NOT NULL REFERENCES "NULL".Banco(Banco_Codigo),
	Moneda_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	Cheque_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Transaccion
(
	Transacc_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(1,1),
	Transacc_Cantidad NUMERIC(18,0) NOT NULL,
	Transacc_Importe NUMERIC(18,2) NOT NULL,
	Transacc_Facturada BIT NOT NULL DEFAULT 0,
	Transacc_Detalle NVARCHAR(255) NOT NULL,
	Moneda_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	Cli_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	Transacc_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Factura_Cabecera
(
	Fact_Numero NUMERIC(18,0) IDENTITY(15372426,1),
	Fact_Tipo NVARCHAR(10) NOT NULL,
	Fact_Fecha DATETIME NOT NULL,
	Fact_Total NUMERIC(18,2),
	Cli_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	PRIMARY KEY(Fact_Numero, Fact_Tipo)
);

CREATE TABLE "NULL".Factura_Item
(
	Fact_Numero NUMERIC(18,0) NOT NULL,
	Fact_Tipo NVARCHAR(10) NOT NULL,
	Transacc_Codigo NUMERIC(18,0) NOT NULL REFERENCES "NULL".Transaccion(Transacc_Codigo),
	F_Item_Desc NVARCHAR(255) NOT NULL,
	F_Item_Cantidad NUMERIC(18,0) NOT NULL,
	F_Item_Precio_Unitario NUMERIC(18,2) NOT NULL,
	Moneda_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	
	CONSTRAINT FK_Fact_Item_Cabecer FOREIGN KEY (Fact_Numero, Fact_Tipo)
	REFERENCES "NULL".Factura_Cabecera (Fact_Numero, Fact_Tipo),

	PRIMARY KEY(Fact_Numero, Fact_Tipo, Transacc_Codigo)
);

CREATE TABLE "NULL".Nacionalidad
(
	Nac_Pais_Cod NUMERIC(18,0) REFERENCES "NULL".Pais(Pais_Codigo),
	Nac_Nombre NVARCHAR(255)
);

/*Triggers*/

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'trTransferencia' 
)
   DROP TRIGGER "NULL".trTransferencia
GO

CREATE TRIGGER "NULL".trTransferencia ON "NULL".Transferencia AFTER INSERT
AS
BEGIN
	DECLARE @Importe INT
	DECLARE @Cuenta_Destino_Numero NUMERIC(18,0)
	
	SET @Importe = (SELECT TOP 1 Transf_Importe FROM inserted)
	SET @Cuenta_Destino_Numero = (SELECT TOP 1 Cuenta_Destino_Numero FROM inserted)
	
	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Saldo = Cuenta_Saldo + @Importe
	WHERE Cuenta_Numero = @Cuenta_Destino_Numero
END
GO

/******************************* MIGRACION *********************************************/

SET IDENTITY_INSERT "NULL".Funcionalidad ON

INSERT INTO "NULL".Funcionalidad(Func_Cod, Func_Nombre) VALUES 
	(1, 'ABM de Rol'),
	(2, 'ABM de Usuario'),
	(3, 'ABM de Clientes'),
	(4, 'ABM de Cuenta'),
	(5, 'Asociar/Desasociar Tarjetas de Credito'),
	(6, 'Depósitos'),
	(7, 'Retiros'),
	(8, 'Transferencias'),
	(9, 'Facturación'),
	(10, 'Consulta Saldos'),
	(11, 'Listados Estadísticos');

SET IDENTITY_INSERT "NULL".Funcionalidad OFF

INSERT INTO "NULL".Rol(Rol_Nombre) VALUES
	('Administrador'),
	('Cliente');

INSERT INTO "NULL".Rol_Funcionalidad(Func_Cod, Rol_Nombre) VALUES
	(1, 'Administrador'),
	(2, 'Administrador'),
	(3, 'Administrador'),
	(4, 'Administrador'),
	(5, 'Cliente'),
	(6, 'Cliente'),
	(7, 'Cliente'),
	(8, 'Cliente'),
	(9, 'Cliente'),
	(9, 'Administrador'),
	(10, 'Cliente'),
	(10, 'Administrador'),
	(11, 'Administrador');


/*********** INSERTO 3 USUARIOS ADMIN, ADMIN2 Y ADMIN3 CON PASSWORD w23e **********************/
INSERT INTO "NULL".Usuario(Usr_Username, Usr_Password, Usr_Fecha_Creacion, 
							Usr_Fecha_Ultima_Modificacion, Usr_Pregunta_Secreta,
							Usr_Respuesta_Secreta, Usr_Intentos_Login, Usr_Borrado) VALUES
	('admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',
	[NULL].fnGetFechaSistema(), [NULL].fnGetFechaSistema(), 'Pregunta?',
	'9853528a46d3fc973096dc43528e4a2a660496fb5a24739d9788d5891a49121d', 0, 0),
	('admin2', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',
	[NULL].fnGetFechaSistema(), [NULL].fnGetFechaSistema(), 'Pregunta?',
	'9853528a46d3fc973096dc43528e4a2a660496fb5a24739d9788d5891a49121d', 0, 0),
	('admin3', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7',
	[NULL].fnGetFechaSistema(), [NULL].fnGetFechaSistema(), 'Pregunta?',
	'9853528a46d3fc973096dc43528e4a2a660496fb5a24739d9788d5891a49121d', 0, 0);

INSERT INTO "NULL".Rol_Usuario(Rol_Nombre, Usr_Username) VALUES
	('Administrador', 'admin'),
	('Administrador', 'admin2'),
	('Administrador', 'admin3');


/****************************** MIGRACIÓN DEL RESTO DE LOS USUARIOS ***************************/
INSERT INTO "NULL".Usuario(Usr_Username, Usr_Password, Usr_Fecha_Creacion, 
			Usr_Fecha_Ultima_Modificacion, Usr_Pregunta_Secreta, Usr_Respuesta_Secreta, 
			Usr_Intentos_Login, Usr_Borrado)
SELECT DISTINCT LOWER(Cli_Nombre +'.'+ Cli_Apellido), 
				'37a8eec1ce19687d132fe29051dca629d164e2c4958ba141d5f4133a33f0688f', 
				"NULL".fnGetFechaSistema(), "NULL".fnGetFechaSistema(), 'pregunta?', 
				'9853528a46d3fc973096dc43528e4a2a660496fb5a24739d9788d5891a49121d', 0, 0 
FROM GD1C2015.gd_esquema.Maestra
WHERE Cli_Nombre IS NOT NULL;


INSERT INTO "NULL".Rol_Usuario(Rol_Nombre,Usr_Username)
SELECT 'Cliente', Usr_Username
FROM "NULL".Usuario
WHERE Usr_Username NOT IN ('admin', 'admin2', 'admin3');

/*********************************** MAS MIGRACIONES ******************************************/
INSERT INTO "NULL".Moneda(Moneda_Nombre, Moneda_Simbolo, Moneda_Borrado) VALUES
	('Dólares Estadounidenses', 'U$S', 0);

INSERT INTO "NULL".Pais(Pais_Codigo, Pais_Desc, Pais_Borrado)
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc, 0
FROM GD1C2015.gd_esquema.Maestra
WHERE Cli_Pais_Codigo IS NOT NULL;


SET IDENTITY_INSERT "NULL".TipoDoc ON

INSERT INTO "NULL".TipoDoc(TipoDoc_Cod, TipoDoc_Desc, TipoDoc_Borrado)
SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc, 0 
FROM GD1C2015.gd_esquema.Maestra
WHERE Cli_Tipo_Doc_Cod IS NOT NULL;

SET IDENTITY_INSERT "NULL".TipoDoc OFF


SET IDENTITY_INSERT "NULL".Banco ON

INSERT INTO "NULL".Banco(Banco_Codigo, Banco_Direccion, Banco_Nombre, Banco_Borrado)
SELECT DISTINCT Banco_Cogido, Banco_Direccion, Banco_Nombre, 0
FROM GD1C2015.gd_esquema.Maestra
WHERE Banco_Cogido IS NOT NULL;

SET IDENTITY_INSERT "NULL".Banco OFF


INSERT INTO "NULL".Emisor(Emisor_Desc, Emisor_Borrado)
SELECT DISTINCT Tarjeta_Emisor_Descripcion, 0
FROM GD1C2015.gd_esquema.Maestra
WHERE Tarjeta_Emisor_Descripcion IS NOT NULL;


INSERT INTO "NULL".Nacionalidad(Nac_Pais_Cod, Nac_Nombre) VALUES
	(212, 'Acrotirense y dhekeliano'),
	(195, 'Andorrano'),
	(23, 'Angolano'),
	(224, 'Anguilense'),
	(8, 'Argentino'),
	(142, 'Armenio'),
	(6, 'Australiano'),
	(113, 'Azerbaiyano'),
	(201, 'Barbadense'),
	(102, 'Beninés'),
	(229, 'Bermudeño'),
	(40, 'Birmano'),
	(128, 'Bosnio'),
	(48, 'Botsuano'),
	(173, 'Bruneano'),
	(105, 'Búlgaro'),
	(54, 'Camerunés'),
	(164, 'Catarí'),
	(21, 'Chadiano'),
	(169, 'Chipriota'),
	(246, 'Vaticano'),
	(99, 'Coreano'),
	(109, 'Coreano'),
	(69, 'Marfileño'),
	(129, 'Costarricense'),
	(133, 'Danés'),
	(187, 'Dominiqués'),
	(74, 'Ecuatoriano'),
	(153, 'Salvadoreño'),
	(154, 'Eslavo'),
	(52, 'Español'),
	(132, 'Estonio'),
	(73, 'Filipino'),
	(156, 'Fiyiano'),
	(77, 'Gabonés'),
	(121, 'Georgiano'),
	(82, 'Ghanés'),
	(206, 'Granadino'),
	(97, 'Griego'),
	(12, 'Groenlandés'),
	(194, 'Guameño'),
	(107, 'Guatemalteco'),
	(145, 'Ecuatoguineano'),
	(137, 'Guineano'),
	(147, 'Haitiano'),
	(183, 'Hongkonés'),
	(18, 'Iraní'),
	(108, 'Islandés'),
	(209, 'Caimanés'),
	(182, 'Feroés'),
	(177, 'de las Islas Georgias del Sur y Sandwich del Sur'),
	(196, 'de las Islas Marianas del Norte'),
	(216, 'Marshalés'),
	(231, 'de las Islas Pitcairn'),
	(143, 'Salomonense'),
	(185, 'de las Islas Turcas y Caicos'),
	(219, 'de las Islas Vírgenes Británicas'),
	(152, 'Israelí'),
	(166, 'Jamaiquino'),
	(204, 'Janmayense'),
	(222, 'Jersiais'),
	(112, 'Jordano'),
	(49, 'Keniata'),
	(157, 'Kuwaití'),
	(168, 'Libanés'),
	(104, 'Liberio'),
	(218, 'Liechtensteiniano'),
	(235, ''),
	(67, 'Malayo'),
	(24, 'Maliense'),
	(207, 'Maltés'),
	(14, 'Mexicano'),
	(245, 'Monaqués'),
	(161, 'Montenegrino'),
	(223, NULL),
	(35, 'Mozambiqueño'),
	(94, 'Nepalí'),
	(98, 'Nicaragüeño'),
	(32, 'Nigeriano'),
	(68, 'Noruego'),
	(155, 'Neocaledonio'),
	(71, 'Omanés'),
	(175, 'Francopolinesio'),
	(70, 'Polaco'),
	(138, 'Chino'),
	(149, 'Macedonio'),
	(64, 'Congoleño'),
	(11, 'Congoleño'),
	(148, 'Ruandés'),
	(83, 'Rumano'),
	(1, 'Ruso'),
	(215, 'Samoano'),
	(238, 'Francés'),
	(226, 'San Marinés'),
	(228, 'Sanmartinense'),
	(184, 'Santotomense'),
	(119, 'Sierraleonés'),
	(89, 'Sirio'),
	(43, 'Somalí'),
	(122, 'Ceilandés'),
	(25, 'Sudafricano'),
	(56, 'Sueco'),
	(135, 'Suizo'),
	(92, 'Surinamés'),
	(125, 'Svalbarense'),
	(51, 'Tailandés'),
	(96, 'tayiko'),
	(159, 'Timorense'),
	(126, 'Togolés'),
	(240, 'Tokelauano'),
	(188, 'Tongano'),
	(236, 'Tuvaluano'),
	(91, 'Uruguayo'),
	(33, 'Venezolano'),
	(220, 'Walisiano'),
	(50, 'Jemetí'),
	(150, 'yibutiano'),
	(39, 'Zambiano'),
	(242, 'de la Isla Clipperton'),
	(233, 'Estadounidense');



INSERT INTO "NULL".Cliente(Usr_Username, Cli_Nombre, Cli_Apellido, TipoDoc_Cod, Cli_Nro_Doc,
						Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto, Cli_Localidad,
						Cli_Fecha_Nac, Cli_Mail, Cli_Nacionalidad, Pais_Codigo, Cli_Borrado)

SELECT DISTINCT LOWER(Cli_Nombre +'.'+ Cli_Apellido), Cli_Nombre, Cli_Apellido, Cli_Tipo_Doc_Cod,
	Cli_Nro_Doc, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto, NULL,
	Cli_Fecha_Nac, Cli_mail, Nac_Nombre, Cli_Pais_Codigo, 0
FROM GD1C2015.gd_esquema.Maestra, "NULL".Nacionalidad
WHERE Cli_Nombre IS NOT NULL AND Cli_Pais_Codigo = Nac_Pais_Cod;