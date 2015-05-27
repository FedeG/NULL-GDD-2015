USE GD1C2015;
GO

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
	Func_Nombre varchar(255) PRIMARY KEY, 
	Func_Borrado bit NOT NULL DEFAULT 0
)
GO

CREATE TABLE "NULL".Rol
(
	Rol_Nombre varchar(255) PRIMARY KEY,
	Rol_Estado varchar(255) CHECK (Rol_Estado IN('Habilitado', 'Deshabilitado')),
	Rol_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Rol_Funcionalidad
(
	Rol_Nombre varchar(255) NOT NULL REFERENCES "NULL".Rol(Rol_Nombre),
	Func_Nombre varchar(255) NOT NULL REFERENCES "NULL".Funcionalidad(Func_Nombre),
	PRIMARY KEY(Rol_Nombre, Func_Nombre)
);

CREATE TABLE "NULL".Usuario
(
	Usr_Username varchar(255) PRIMARY KEY,
	Usr_Password varchar(255) NOT NULL,
	Usr_Fecha_Creacion datetime NOT NULL,
	Usr_Fecha_Ultima_Modificacion datetime NOT NULL,
	Usr_Pregunta_Secreta varchar(255) NOT NULL,
	Usr_Respuesta_Secreta varchar(255) NOT NULL,
	Usr_Intentos_Login int DEFAULT 0,
	Usr_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Rol_Usuario
(
	Rol_Nombre varchar(255) REFERENCES "NULL".Rol(Rol_Nombre),
	Usr_Username varchar(255) REFERENCES "NULL".Usuario(Usr_Username),
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
	TipoDoc_Cod numeric(18,0) PRIMARY KEY IDENTITY(10003,1),
	TipoDoc_Desc varchar(255) NOT NULL,
	TipoDoc_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Pais
(
	Pais_Codigo numeric(18,0) PRIMARY KEY,
	Pais_Desc varchar(250) NOT NULL,
	Pais_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cliente
(
	Cli_Cod numeric(18,0) PRIMARY KEY IDENTITY(100000,1),
	Usr_Username varchar(255) NOT NULL REFERENCES "NULL".Usuario(Usr_Username),
	Cli_Nombre varchar(255),
	Cli_Apellido varchar(255),
	TipoDoc_Cod numeric(18,0) NOT NULL REFERENCES "NULL".TipoDoc(TipoDoc_Cod),
	Cli_Nro_Doc varchar(255) NOT NULL,
	Cli_Dom_Calle varchar(255),
	Cli_Dom_Nro varchar(20),
	Cli_Dom_Piso varchar(10),
	Cli_Dom_Depto varchar(10),
	Cli_Localidad varchar(255),
	Cli_Fecha_Nac datetime,
	Cli_Mail varchar(255),
	Cli_Nacionalidad varchar(255),
	Pais_Codigo numeric(18,0) NOT NULL REFERENCES "NULL".Pais(Pais_Codigo),
	Cli_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".TipoCuenta
(
	TipoCta_Nombre varchar(255) PRIMARY KEY,
	TipoCta_Costo_Apertura numeric(18,2) NOT NULL,
	TipoCta_Duracion numeric(18,0) NOT NULL,
	TipoCta_Costo_Dia numeric(18,2) NOT NULL,
	TipoCta_Costo_Transf numeric(18,2) NOT NULL,
	TipoCta_Borrado bit NOT NULL
);

CREATE TABLE "NULL".Moneda
(
	Moneda_Nombre varchar(255) PRIMARY KEY,
	Moneda_Simbolo varchar(5) NOT NULL, 
	Moneda_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cuenta
(
	Cuenta_Numero numeric(18,0) PRIMARY KEY IDENTITY(1111111111111452, 1),
	Cuenta_Estado varchar(255) NOT NULL DEFAULT 'Pendiente de Activación' 
		CHECK (Cuenta_Estado IN('Pendiente de Activación', 'Cerrada', 'Inhabilitada', 'Habilitada')),
	Cuenta_Fecha_Vencimiento datetime,
	Cuenta_Fecha_Cierre datetime,
	Cuenta_Fecha_Creacion datetime NOT NULL,
	Cuenta_Saldo numeric(18,2) NOT NULL DEFAULT 0,
	Pais_Codigo numeric(18,0) NOT NULL REFERENCES "NULL".Pais(Pais_Codigo),
	TipoCta_Nombre varchar(255) NOT NULL REFERENCES "NULL".TipoCuenta(TipoCta_Nombre),
	Cli_Cod numeric(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	Moneda_Nombre varchar(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	Cuenta_Borrado bit NOT NULL	 DEFAULT 0
);

CREATE TABLE "NULL".Emisor
(
	Emisor_Cod numeric(18,0) PRIMARY KEY IDENTITY(100001,1),
	Emisor_Desc varchar(255) NOT NULL,
	Emisor_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Tarjeta
(
	Tarjeta_Numero varchar(255) PRIMARY KEY,
	Tarjeta_Numero_Visible varchar(255) NOT NULL,
	Tarjeta_Fecha_Emision datetime NOT NULL,
	Tarjeta_Fecha_Vencimiento datetime NOT NULL,
	Tarjeta_Codig_Seg varchar(255) NOT NULL,
	Cli_Cod numeric(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	Emisor_Cod numeric(18,0) NOT NULL REFERENCES "NULL".Emisor(Emisor_Cod),
	Tarjeta_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Deposito
(
	Deposito_Codigo numeric(18,0) PRIMARY KEY IDENTITY(35135160440,1),
	Deposito_Importe numeric(18,2) NOT NULL,
	Deposito_Fecha datetime NOT NULL,
	Tarjeta_Numero varchar(255) NOT NULL REFERENCES "NULL".Tarjeta(Tarjeta_Numero),
	Cuenta_Numero numeric(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Deposito_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Transferencia
(
	Transf_Codigo numeric(18,0) PRIMARY KEY IDENTITY(10000000001,1),
	Transf_Fecha datetime NOT NULL,
	Transf_Importe numeric(18,2) NOT NULL,
	Transf_Costo numeric(18,2) NOT NULL,
	Cuenta_Origen_Numero numeric(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Cuenta_Destino_Numero numeric(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Transf_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Retiro
(
	Retiro_Codigo numeric(18,0) PRIMARY KEY IDENTITY(15315188277,1),
	Retiro_Importe numeric(18,2) NOT NULL,
	Retiro_Fecha datetime NOT NULL,
	Cuenta_Numero numeric(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
	Retiro_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Banco
(
	Banco_Codigo numeric(18,0) PRIMARY KEY IDENTITY(10005,1),
	Banco_Nombre varchar(255) NOT NULL,
	Banco_Direccion varchar(255),
	Banco_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cheque
(
	Retiro_Codigo numeric(18,0) PRIMARY KEY REFERENCES "NULL".Retiro(Retiro_Codigo),
	Cheque_Numero numeric(18,0) NOT NULL IDENTITY (151550275,1),
	Cheque_Fecha datetime NOT NULL,
	Cheque_Importe numeric(18,2) NOT NULL,
	Cheque_Nombre varchar(255) NOT NULL,
	Banco_Codigo numeric(18,0) NOT NULL REFERENCES "NULL".Banco(Banco_Codigo),
	Moneda_Nombre varchar(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	Cheque_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Transaccion
(
	Transacc_Codigo numeric(18,0) PRIMARY KEY IDENTITY(1,1),
	Transacc_Cantidad numeric(18,0) NOT NULL,
	Transacc_Importe numeric(18,2) NOT NULL,
	Transacc_Facturada bit NOT NULL DEFAULT 0,
	Transacc_Detalle varchar(255) NOT NULL,
	Moneda_Nombre varchar(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	Cli_Cod numeric(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	Transacc_Borrado bit NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Factura_Cabecera
(
	Fact_Numero numeric(18,0) IDENTITY(15372426,1),
	Fact_Tipo varchar(10) NOT NULL,
	Fact_Fecha datetime NOT NULL,
	Fact_Total numeric(18,2),
	Cli_Cod numeric(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
	PRIMARY KEY(Fact_Numero, Fact_Tipo)
);

CREATE TABLE "NULL".Factura_Item
(
	Fact_Numero numeric(18,0) NOT NULL,
	Fact_Tipo varchar(10) NOT NULL,
	Transacc_Codigo numeric(18,0) NOT NULL REFERENCES "NULL".Transaccion(Transacc_Codigo),
	F_Item_Desc varchar(255) NOT NULL,
	F_Item_Cantidad numeric(18,0) NOT NULL,
	F_Item_Precio_Unitario numeric(18,2) NOT NULL,
	Moneda_Nombre varchar(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
	
	CONSTRAINT FK_Fact_Item_Cabecer FOREIGN KEY (Fact_Numero, Fact_Tipo)
	REFERENCES "NULL".Factura_Cabecera (Fact_Numero, Fact_Tipo),

	PRIMARY KEY(Fact_Numero, Fact_Tipo, Transacc_Codigo)
);
