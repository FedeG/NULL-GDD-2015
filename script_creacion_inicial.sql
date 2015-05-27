USE GD1C2015;
GO

SET LANGUAGE Spanish

IF NOT EXISTS 
(
	SELECT  schema_name
	FROM    information_schema.schemata
	WHERE   schema_name = 'NULL'
)

BEGIN
	EXEC sp_executesql N'CREATE SCHEMA "NULL" AUTHORIZATION gd'
END

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


CREATE TABLE "NULL".Funcionalidad
(
	Func_Cod NUMERIC(18,0) PRIMARY KEY IDENTITY(12,1),
	Func_Nombre NVARCHAR(255), 
	Func_Borrado BIT NOT NULL DEFAULT 0
)
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
	Usr_INTentos_Login INT DEFAULT 0,
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
	Tarjeta_Codig_Seg NVARCHAR(255) NOT NULL,
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
