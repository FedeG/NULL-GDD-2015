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

EXEC "NULL".spSetFechaSistema '2016-01-01 00:00:00.000'


/********************************** BORRADO DE TABLAS **************************************/
IF OBJECT_ID('NULL.Auditoria_Login', 'U') IS NOT NULL
  DROP TABLE "NULL".Auditoria_Login
GO

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

IF OBJECT_ID('NULL.Retiro', 'U') IS NOT NULL
  DROP TABLE "NULL".Retiro
GO

IF OBJECT_ID('NULL.Cheque', 'U') IS NOT NULL
  DROP TABLE "NULL".Cheque
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
  Usr_Borrado BIT NOT NULL DEFAULT 0,
  Usr_Estado NVARCHAR(255) DEFAULT 'Habilitado' NOT NULL CHECK (Usr_Estado IN('Habilitado', 'Deshabilitado'))
);

CREATE TABLE "NULL".Rol_Usuario
(
  Rol_Nombre NVARCHAR(255) REFERENCES "NULL".Rol(Rol_Nombre),
  Usr_Username NVARCHAR(255) REFERENCES "NULL".Usuario(Usr_Username),
  PRIMARY KEY(Rol_Nombre, Usr_Username)
);

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
  TipoCta_Nombre NVARCHAR(255) REFERENCES "NULL".TipoCuenta(TipoCta_Nombre),
  Cli_Cod NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cliente(Cli_Cod),
  Moneda_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
  Cuenta_Borrado BIT NOT NULL  DEFAULT 0
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
  Tarjeta_Borrado BIT NOT NULL DEFAULT 0,
  Tarjeta_Estado NVARCHAR(255) NOT NULL DEFAULT 'Asociada' CHECK (Tarjeta_Estado IN ('Asociada','Desasociada'))
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

CREATE TABLE "NULL".Banco
(
  Banco_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(10005,1),
  Banco_Nombre NVARCHAR(255) NOT NULL,
  Banco_Direccion NVARCHAR(255),
  Banco_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Cheque
(
  Cheque_Numero NUMERIC(18,0) NOT NULL PRIMARY KEY IDENTITY (151550275,1),
  Cheque_Fecha DATETIME NOT NULL,
  Cheque_Importe NUMERIC(18,2) NOT NULL,
  Cheque_Nombre NVARCHAR(255) NOT NULL,
  Banco_Codigo NUMERIC(18,0) NOT NULL REFERENCES "NULL".Banco(Banco_Codigo),
  Moneda_Nombre NVARCHAR(255) NOT NULL REFERENCES "NULL".Moneda(Moneda_Nombre),
  Cheque_Borrado BIT NOT NULL DEFAULT 0
);

CREATE TABLE "NULL".Retiro
(
  Retiro_Codigo NUMERIC(18,0) PRIMARY KEY IDENTITY(15315188277,1),
  Retiro_Importe NUMERIC(18,2) NOT NULL,
  Retiro_Fecha DATETIME NOT NULL,
  Cuenta_Numero NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cuenta(Cuenta_Numero),
  Retiro_Borrado BIT NOT NULL DEFAULT 0,
  Cheque_Numero NUMERIC(18,0) NOT NULL REFERENCES "NULL".Cheque(Cheque_Numero)
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
  Fact_Borrado BIT NOT NULL DEFAULT 0,
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
  F_Item_Borrado BIT NOT NULL DEFAULT 0,
  
  CONSTRAINT FK_Fact_Item_Cabecer FOREIGN KEY (Fact_Numero, Fact_Tipo)
  REFERENCES "NULL".Factura_Cabecera (Fact_Numero, Fact_Tipo),

  PRIMARY KEY(Fact_Numero, Fact_Tipo, Transacc_Codigo)
);

CREATE TABLE "NULL".Nacionalidad
(
  Nac_Pais_Cod NUMERIC(18,0) REFERENCES "NULL".Pais(Pais_Codigo),
  Nac_Nombre NVARCHAR(255)
);

CREATE TABLE "NULL".Auditoria_Login
(
  Log_Cod NUMERIC(18,0) PRIMARY KEY IDENTITY(1,1) NOT NULL,
  Usr_Username NVARCHAR(255) REFERENCES "NULL".Usuario(Usr_Username) NOT NULL,
  Log_Fecha DATETIME NOT NULL,
  Log_Estado NVARCHAR(20) NOT NULL CHECK (Log_Estado IN('Exitoso', 'No Exitoso')),
  Log_Nro_Fallo NUMERIC(1,0) DEFAULT NULL
);


/************************************ FN Y PRODCEDURES *********************************************/
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'fnValidarDeposito' 
)
   DROP FUNCTION "NULL".fnValidarDeposito
GO

CREATE FUNCTION "NULL".fnValidarDeposito(
  @Cuenta_Numero NUMERIC(18,0), 
  @Importe INT,
  @Fecha_Deposito DATETIME,
  @Moneda_Nombre NVARCHAR(255),
  @Tarjeta_Numero NVARCHAR(255))
  RETURNS INT
AS
BEGIN
  IF(@Importe <= 0)
  /*Importe menor igual a 0*/
  BEGIN
    RETURN(1)
  END
  
  IF((SELECT COUNT(*) FROM [GD1C2015].[NULL].[Tarjeta] WHERE Tarjeta_Numero = @Tarjeta_Numero AND @Fecha_Deposito > Tarjeta_Fecha_Vencimiento) = 1)
  BEGIN
    /*Tarjeta Vencida*/
    RETURN(2)
  END
  
  IF((SELECT COUNT(*) FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Numero) = 0)
  BEGIN
    /*Cuenta Inexistente*/
    RETURN(3)
  END
  
  IF((SELECT COUNT(*) FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Numero AND Cuenta_Estado = 'Habilitada') = 0)
  BEGIN
    /*Cuenta no habilitada*/
    RETURN(4)
  END

  RETURN(0)
END
GO


IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spRealizarDeposito' 
)
   DROP PROCEDURE "NULL".spRealizarDeposito
GO

CREATE PROCEDURE "NULL".spRealizarDeposito
  @Cuenta_Numero NUMERIC(18,0), 
  @Importe INT,
  @Fecha_Deposito DATETIME,
  @Moneda_Nombre NVARCHAR(255),
  @Tarjeta_Numero NVARCHAR(255)
AS
BEGIN
  DECLARE @Validacion int
  SET @Validacion = "NULL".fnValidarDeposito(@Cuenta_Numero, @Importe, @Fecha_Deposito, @Moneda_Nombre, @Tarjeta_Numero)
  
  IF(@Validacion = 0)
  BEGIN
    INSERT INTO [GD1C2015].[NULL].[Deposito](Deposito_Importe, Deposito_Fecha, Tarjeta_Numero, Cuenta_Numero)
    VALUES (@Importe, @Fecha_Deposito, @Tarjeta_Numero, @Cuenta_Numero)
  END
  RETURN(@Validacion)
END
GO


IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spRealizarLogin' 
)
   DROP PROCEDURE "NULL".spRealizarLogin
GO

CREATE PROCEDURE "NULL".spRealizarLogin 
  @Username NVARCHAR (255), 
  @Password NVARCHAR (255)
AS
BEGIN
  SET NOCOUNT ON;
  DECLARE @Nro_fallo int = 0
  DECLARE @EstadoLogin NVARCHAR(20)
  DECLARE @ValorRetorno int
  
  IF (SELECT COUNT(*) FROM [GD1C2015].[NULL].[Usuario] WHERE Usr_Username = @Username AND Usr_Password = @Password AND Usr_Estado = 'Habilitado') = 1
    BEGIN
       SET @Nro_fallo = 0
       SET @EstadoLogin = 'Exitoso'
       SET @ValorRetorno = 0
    END
  ELSE 
    BEGIN
      IF (SELECT COUNT(*) FROM [GD1C2015].[NULL].[Usuario] WHERE Usr_Username = @Username) = 1
      BEGIN
        IF (SELECT COUNT(*) FROM [GD1C2015].[NULL].[Usuario] WHERE Usr_Username = @Username AND Usr_Password = @Password AND Usr_Estado = 'Deshabilitado') = 1
        BEGIN
          SET @ValorRetorno = 3
        END
        ELSE
        BEGIN
          SET @Nro_fallo = (SELECT TOP 1 Usr_Intentos_Login
            FROM [GD1C2015].[NULL].[Usuario]
            WHERE Usr_Username = @Username) + 1
          SET @EstadoLogin = 'No Exitoso'
          SET @ValorRetorno = 1
        END
      END
      ELSE
      BEGIN
        SET @ValorRetorno = 2
      END
      
    END
  
  IF(@Nro_fallo = 3)
    BEGIN
      UPDATE [GD1C2015].[NULL].[Usuario] SET Usr_Intentos_Login = @Nro_fallo, Usr_Estado = 'Deshabilitado' WHERE Usr_Username = @Username
    END
  ELSE
    BEGIN
      UPDATE [GD1C2015].[NULL].[Usuario] SET Usr_Intentos_Login = @Nro_fallo WHERE Usr_Username = @Username
    END
  
  IF(@ValorRetorno = 0 OR @ValorRetorno = 1)
  BEGIN
    INSERT INTO [GD1C2015].[NULL].[Auditoria_Login](Usr_Username, Log_Fecha, Log_Estado, Log_Nro_Fallo) VALUES
    (@Username, "NULL".fnGetFechaSistema(), @EstadoLogin, @Nro_fallo)
  END

  RETURN(@ValorRetorno)
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'fnValidarRetiro' 
)
   DROP FUNCTION "NULL".fnValidarRetiro
GO

CREATE FUNCTION "NULL".fnValidarRetiro(
	@Username NVARCHAR(255), 
	@TipoDoc_Cod Numeric(18,0), 
	@Nro_Doc NVARCHAR(255), 
	@Cuenta_Numero Numeric(18,0), 
	@Importe Numeric(18,0)) 
	RETURNS INT
AS
	BEGIN
		DECLARE @Saldo int
		IF(SELECT TOP 1 COUNT(*) 
		FROM [GD1C2015].[NULL].[Cliente] 
		WHERE Usr_Username = @Username AND Cli_Nro_Doc = @Nro_Doc AND TipoDoc_Cod = @TipoDoc_Cod) = 1
		BEGIN
			IF(SELECT TOP 1 Cuenta_Estado FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Numero) = 'Habilitada'
			BEGIN
				SET @Saldo = (SELECT TOP 1 Cuenta_Saldo FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Numero)
				IF @Importe <= 0 
				BEGIN
					RETURN 2
				END
				ELSE
				BEGIN
					IF(@Saldo < @Importe)
					BEGIN
						RETURN 3
					END
					ELSE
					BEGIN
						RETURN 0
					END
				END
			END
			ELSE
			BEGIN
				RETURN 4
			END
		END
		ELSE
		BEGIN
			RETURN 1
		END
		
		RETURN 0
	END;
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spRealizarRetiro' 
)
   DROP PROCEDURE "NULL".spRealizarRetiro
GO

CREATE PROCEDURE "NULL".spRealizarRetiro 
	@Username NVARCHAR(255), 
	@TipoDoc_Cod NUMERIC(18,0), 
	@Nro_Doc NVARCHAR(255), 
	@Cuenta_Numero NUMERIC(18,0), 
	@Importe NUMERIC(18,0),
	@Fecha_Deposito DATETIME,
	@Banco_Cod NUMERIC(18,0),
	@Moneda_Nombre NVARCHAR(255)
AS
	DECLARE @Validacion int
	SET @Validacion = "NULL".fnValidarRetiro(@Username, @TipoDoc_Cod, @Nro_Doc, @Cuenta_Numero, @Importe)
	IF(@Validacion = 0)
	BEGIN
		DECLARE @InsertedCheques TABLE(
			Cheque_Numero Numeric(18,0)
		);
		DECLARE @Cheque_Nombre NVARCHAR(255)
		
		SET @Cheque_Nombre = (SELECT TOP 1 Cli_Apellido FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = @Username)
		SET @Cheque_Nombre = @Cheque_Nombre + ', ' + (SELECT TOP 1 Cli_Nombre FROM [GD1C2015].[NULL].[Cliente] WHERE Usr_Username = @Username)
		
		INSERT INTO [GD1C2015].[NULL].[Cheque](Cheque_Fecha, Cheque_Importe, Cheque_Nombre, Banco_Codigo, Moneda_Nombre)
		OUTPUT inserted.Cheque_Numero INTO @InsertedCheques
		VALUES(@Fecha_Deposito, @Importe, @Cheque_Nombre, @Banco_Cod, @Moneda_Nombre)
		
		INSERT INTO [GD1C2015].[NULL].[Retiro](Retiro_Importe, Retiro_Fecha, Cuenta_Numero, Cheque_Numero)
		SELECT @Importe, @Fecha_Deposito, @Cuenta_Numero, c.Cheque_Numero
		FROM @InsertedCheques as c
		
		RETURN(@Validacion)
	END
	ELSE
	BEGIN
		RETURN(@Validacion)
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
	IF (SELECT COUNT(*) FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Destino AND Cuenta_Estado = 'Habilitada' OR Cuenta_Estado ='Inhabilitada') = 0
	BEGIN
		RETURN(1)
	END
	
	IF @Importe < 0 
	BEGIN
		RETURN(2)
	END
	
	IF(SELECT TOP 1 Cuenta_Saldo FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Origen) < @Importe
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
			SET @Transferencia_Costo = (SELECT TOP 1 tc.TipoCta_Costo_Transf FROM [GD1C2015].[NULL].[TipoCuenta] as tc, [GD1C2015].[NULL].[Cuenta] as c WHERE c.TipoCta_Nombre = tc.TipoCta_Nombre AND c.Cuenta_Numero = @Cuenta_Origen)
		END
		
		INSERT INTO [GD1C2015].[NULL].[Transferencia](Cuenta_Origen_Numero, Cuenta_Destino_Numero, Transf_Fecha, Transf_Importe, Transf_Costo)
		VALUES(@Cuenta_Origen, @Cuenta_Destino, @Fecha_Transferencia, @Importe, @Transferencia_Costo)
	END
	
	RETURN @Validacion
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spCrearRol' 
)
   DROP PROCEDURE "NULL".spCrearRol
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spEditarRol' 
)
   DROP PROCEDURE "NULL".spEditarRol
GO


IF EXISTS (
	SELECT * 
	FROM sys.types 
	WHERE is_table_type = 1 AND name = 'ListaNumeric'
)
	DROP TYPE "NULL".ListaNumeric
GO

CREATE TYPE "NULL".ListaNumeric AS Table (
        number Numeric(18,0)
);
GO

CREATE PROCEDURE "NULL".spCrearRol
	@Rol_Nombre varchar(255),
	@Rol_Estado varchar(255),
	@Lista_Funcionalidades As ListaNumeric READONLY
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO [GD1C2015].[NULL].[Rol](Rol_Nombre, Rol_Estado)
	VALUES (@Rol_Nombre, @Rol_Estado)
	
	INSERT INTO [GD1C2015].[NULL].[Rol_Funcionalidad](Rol_Nombre, Func_Cod)
	SELECT @Rol_Nombre, number FROM @Lista_Funcionalidades
END
GO

CREATE PROCEDURE "NULL".spEditarRol
	@Rol_Pk     varchar(255),
	@Rol_Nombre varchar(255),
	@Rol_Estado varchar(255),
	@Lista_Funcionalidades As ListaNumeric READONLY
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE [GD1C2015].[NULL].[Rol_Funcionalidad]
	WHERE Rol_Nombre = @Rol_Pk
	
	UPDATE [GD1C2015].[NULL].[Rol]
	SET Rol_Nombre = @Rol_Nombre, Rol_Estado = @Rol_Estado
	WHERE Rol_Nombre = @Rol_Pk
	
	INSERT INTO [GD1C2015].[NULL].[Rol_Funcionalidad](Rol_Nombre, Func_Cod)
	SELECT @Rol_Nombre, number FROM @Lista_Funcionalidades
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spDeshabilitarRol' 
)
   DROP PROCEDURE "NULL".spDeshabilitarRol
GO

CREATE PROCEDURE "NULL".spDeshabilitarRol
	@Rol_Pk     varchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [GD1C2015].[NULL].[Rol]
	SET Rol_Estado = 'Deshabilitado'
	WHERE Rol_Nombre = @Rol_Pk
END
GO

IF OBJECT_ID (N'NULL.spCrearCliente') IS NOT NULL
   DROP PROCEDURE "NULL".spCrearCliente
GO

CREATE PROCEDURE "NULL".spCrearCliente

  ---- Usuario ----
  @Usr_Username varchar(255),
	@Usr_Password varchar(255),
	@Usr_Pregunta_Secreta varchar(255),
	@Usr_Respuesta_Secreta varchar(255),

  ---- Cliente ----
	@Cli_Nombre varchar(255),
	@Cli_Apellido varchar(255),
	@Cli_Nro_Doc varchar(255),
	@Cli_Dom_Calle varchar(255),
	@Cli_Localidad varchar(255),
	@Cli_Mail varchar(255),
	@Cli_Nacionalidad varchar(255),
	@Cli_Dom_Nro varchar(20),
	@Cli_Dom_Piso varchar(10),
	@Cli_Dom_Depto varchar(10),
	@TipoDoc_Cod Numeric(18,0),
	@Pais_Codigo Numeric(18,0),
	@Cli_Fecha_Nac DATETIME

AS
BEGIN
	SET NOCOUNT ON;

  INSERT INTO [GD1C2015].[NULL].[Usuario] (Usr_Username, Usr_Password, Usr_Fecha_Creacion, Usr_Fecha_Ultima_Modificacion, Usr_Pregunta_Secreta, Usr_Respuesta_Secreta, Usr_Intentos_Login, Usr_Borrado, Usr_Estado)
	VALUES (@Usr_Username, @Usr_Password, GETDATE(), GETDATE(), @Usr_Pregunta_Secreta, @Usr_Respuesta_Secreta, 0, 0, 'Habilitado')

	INSERT INTO [GD1C2015].[NULL].[Cliente] (Usr_Username, Cli_Nombre, Cli_Apellido, TipoDoc_Cod, Cli_Nro_Doc, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto, Cli_Localidad, Cli_Fecha_Nac, Cli_Mail, Cli_Nacionalidad, Pais_Codigo, Cli_Borrado)
	VALUES (@Usr_Username, @Cli_Nombre, @Cli_Apellido, @TipoDoc_Cod, @Cli_Nro_Doc, @Cli_Dom_Calle, @Cli_Dom_Nro, @Cli_Dom_Piso, @Cli_Dom_Depto, @Cli_Localidad, @Cli_Fecha_Nac, @Cli_Mail, @Cli_Nacionalidad, @Pais_Codigo, 0)

END
GO

IF OBJECT_ID (N'NULL.spEditarCliente') IS NOT NULL
   DROP PROCEDURE "NULL".spEditarCliente
GO

CREATE PROCEDURE "NULL".spEditarCliente

  ---- Usuario ----
  @Usr_Username varchar(255),
	@Usr_Password varchar(255),
	@Usr_Pregunta_Secreta varchar(255),
	@Usr_Respuesta_Secreta varchar(255),

  ---- Cliente ----
	@Cli_Nombre varchar(255),
	@Cli_Apellido varchar(255),
	@Cli_Nro_Doc varchar(255),
	@Cli_Dom_Calle varchar(255),
	@Cli_Localidad varchar(255),
	@Cli_Mail varchar(255),
	@Cli_Nacionalidad varchar(255),
	@Cli_Dom_Nro varchar(20),
	@Cli_Dom_Piso varchar(10),
	@Cli_Dom_Depto varchar(10),
	@TipoDoc_Cod Numeric(18,0),
	@Pais_Codigo Numeric(18,0),
	@Cli_Fecha_Nac DATETIME

AS
BEGIN
	SET NOCOUNT ON;

  UPDATE [GD1C2015].[NULL].[Usuario]
	SET Usr_Password = @Usr_Password, Usr_Pregunta_Secreta = @Usr_Pregunta_Secreta, Usr_Respuesta_Secreta = @Usr_Respuesta_Secreta
	WHERE Usr_Username = @Usr_Username

  UPDATE [GD1C2015].[NULL].[Cliente]
	SET Cli_Nombre = @Cli_Nombre, Cli_Apellido = @Cli_Apellido, TipoDoc_Cod = @TipoDoc_Cod, Cli_Nro_Doc = @Cli_Nro_Doc, Cli_Dom_Calle = @Cli_Dom_Calle, Cli_Dom_Nro = @Cli_Dom_Nro, Cli_Dom_Piso = @Cli_Dom_Piso, Cli_Dom_Depto = @Cli_Dom_Depto, Cli_Localidad = @Cli_Localidad, Cli_Fecha_Nac = @Cli_Fecha_Nac, Cli_Mail = @Cli_Mail, Cli_Nacionalidad = @Cli_Nacionalidad, Pais_Codigo = @Pais_Codigo
	WHERE Usr_Username = @Usr_Username

END
GO

IF OBJECT_ID (N'NULL.spHabilitarUsuario') IS NOT NULL
   DROP PROCEDURE "NULL".spHabilitarUsuario
GO

CREATE PROCEDURE "NULL".spHabilitarUsuario
	@Usr_Username varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Usuario]
	SET Usr_Estado = 'Habilitado'
	WHERE Usr_Username = @Usr_Username

END
GO

IF OBJECT_ID (N'NULL.spDeshabilitarUsuario') IS NOT NULL
   DROP PROCEDURE "NULL".spDeshabilitarUsuario
GO

CREATE PROCEDURE "NULL".spDeshabilitarUsuario
	@Usr_Username varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Usuario]
	SET Usr_Estado = 'Deshabilitado'
	WHERE Usr_Username = @Usr_Username

END
GO

IF OBJECT_ID (N'NULL.spDarDeBajaCliente') IS NOT NULL
   DROP PROCEDURE "NULL".spDarDeBajaCliente
GO

CREATE PROCEDURE "NULL".spDarDeBajaCliente
	@Usr_Username varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cliente]
	SET Cli_Borrado = 1
	WHERE Usr_Username = @Usr_Username

END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spRealizarDeposito' 
)
   DROP PROCEDURE "NULL".spRealizarDeposito
GO

CREATE PROCEDURE "NULL".spRealizarDeposito
  @Cuenta_Numero NUMERIC(18,0), 
  @Importe INT,
  @Fecha_Deposito DATETIME,
  @Moneda_Nombre NVARCHAR(255),
  @Tarjeta_Numero NVARCHAR(255)
AS
BEGIN
  DECLARE @Validacion int
  SET @Validacion = "NULL".fnValidarDeposito(@Cuenta_Numero, @Importe, @Fecha_Deposito, @Moneda_Nombre, @Tarjeta_Numero)
  
  IF(@Validacion = 0)
  BEGIN
    INSERT INTO [GD1C2015].[NULL].[Deposito](Deposito_Importe, Deposito_Fecha, Tarjeta_Numero, Cuenta_Numero)
    VALUES (@Importe, @Fecha_Deposito, @Tarjeta_Numero, @Cuenta_Numero)
  END
  RETURN(@Validacion)
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spCrearTarjeta' 
)
   DROP PROCEDURE "NULL".spCrearTarjeta
GO

CREATE PROCEDURE "NULL".spCrearTarjeta
  @Tarjeta_Numero NVARCHAR(255), 
  @Tarjeta_Codigo_Seg NVARCHAR(255),
  @Tarjeta_Fecha_Emision DATETIME,
  @Tarjeta_Fecha_Vencimiento DATETIME,
  @Tarjeta_Numero_Visible NVARCHAR(255),
  @Emisor_Cod NUMERIC(18,0),
  @Cli_Cod NUMERIC(18,0)
AS
BEGIN
	IF(SELECT COUNT(*) FROM [GD1C2015].[NULL].[Tarjeta] WHERE Tarjeta_Numero = @Tarjeta_Numero) >= 1
	BEGIN
		RETURN(1)
	END

	INSERT INTO [GD1C2015].[NULL].[Tarjeta]
	(Tarjeta_Numero, Tarjeta_Numero_Visible, Tarjeta_Codigo_Seg, Emisor_Cod, Cli_Cod, Tarjeta_Fecha_Emision, Tarjeta_Fecha_Vencimiento)
	VALUES(@Tarjeta_Numero, @Tarjeta_Numero_Visible, @Tarjeta_Codigo_Seg, @Emisor_Cod, @Cli_Cod, @Tarjeta_Fecha_Emision, @Tarjeta_Fecha_Vencimiento)
	
	RETURN(0)
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spEditarTarjeta' 
)
   DROP PROCEDURE "NULL".spEditarTarjeta
GO

CREATE PROCEDURE "NULL".spEditarTarjeta
  @Tarjeta_Pk NVARCHAR(255), 
  @Tarjeta_Numero NVARCHAR(255), 
  @Tarjeta_Codigo_Seg NVARCHAR(255),
  @Tarjeta_Fecha_Emision DATETIME,
  @Tarjeta_Fecha_Vencimiento DATETIME,
  @Tarjeta_Numero_Visible NVARCHAR(255),
  @Emisor_Cod NUMERIC(18,0),
  @Cambio_Pk BIT
AS
BEGIN
	IF @Cambio_Pk = 1
	BEGIN
		IF(SELECT COUNT(*) FROM [GD1C2015].[NULL].[Tarjeta] WHERE Tarjeta_Numero = @Tarjeta_Numero) >= 1
		BEGIN
			RETURN(1)
		END
	END

	UPDATE [GD1C2015].[NULL].[Tarjeta]
	SET Tarjeta_Numero = @Tarjeta_Numero, Tarjeta_Numero_Visible = @Tarjeta_Numero_Visible, Tarjeta_Codigo_Seg = @Tarjeta_Codigo_Seg,
	Emisor_Cod = @Emisor_Cod, Tarjeta_Fecha_Emision = @Tarjeta_Fecha_Emision, Tarjeta_Fecha_Vencimiento = @Tarjeta_Fecha_Vencimiento
	WHERE Tarjeta_Numero = @Tarjeta_Pk
	
	RETURN 0
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spAsociarTarjeta' 
)
   DROP PROCEDURE "NULL".spAsociarTarjeta
GO

CREATE PROCEDURE "NULL".spAsociarTarjeta
  @Tarjeta_Pk NVARCHAR(255)
AS
BEGIN
	UPDATE [GD1C2015].[NULL].[Tarjeta]
	SET Tarjeta_Estado = 'Asociada'
	WHERE Tarjeta_Numero = @Tarjeta_Pk
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spDesasociarTarjeta' 
)
   DROP PROCEDURE "NULL".spDesasociarTarjeta
GO

CREATE PROCEDURE "NULL".spDesasociarTarjeta
  @Tarjeta_Pk NVARCHAR(255)
AS
BEGIN
	UPDATE [GD1C2015].[NULL].[Tarjeta]
	SET Tarjeta_Estado = 'Desasociada'
	WHERE Tarjeta_Numero = @Tarjeta_Pk
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spEliminarTarjeta' 
)
   DROP PROCEDURE "NULL".spEliminarTarjeta
GO

CREATE PROCEDURE "NULL".spEliminarTarjeta
  @Tarjeta_Pk NVARCHAR(255)
AS
BEGIN
	UPDATE [GD1C2015].[NULL].[Tarjeta]
	SET Tarjeta_Borrado = 1
	WHERE Tarjeta_Numero = @Tarjeta_Pk
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

INSERT INTO "NULL".Rol(Rol_Nombre, Rol_Estado) VALUES
  ('Administrador', 'Habilitado'),
  ('Cliente', 'Habilitado');

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

INSERT INTO "NULL".Pais(Pais_Codigo, Pais_Desc, Pais_Borrado)
SELECT DISTINCT master.Cuenta_Pais_Codigo, master.Cuenta_Pais_Desc, 0
FROM GD1C2015.gd_esquema.Maestra as master
WHERE master.Cuenta_Pais_Codigo NOT IN (SELECT Pais_Codigo FROM GD1C2015."NULL".Pais);


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
  (177, 'de las Islas Georgias del Sur y Sandwich del Sur'),
  (196, 'de las Islas Marianas del Norte'),
  (216, 'Marshalés'),
  (231, 'de las Islas Pitcairn'),
  (143, 'Salomonense'),
  (185, 'de las Islas Turcas y Caicos'),
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


/********************************************* TARJETAS ******************************/
IF OBJECT_ID('NULL.Tarjetas_SHA_Temp', 'U') IS NOT NULL
  DROP TABLE "NULL".Tarjetas_SHA_Temp
GO

CREATE TABLE "NULL".Tarjetas_SHA_Temp
(
  Tarjeta_Numero NVARCHAR(16) PRIMARY KEY,
  Tarjeta_SHA NVARCHAR(255)
);

INSERT INTO "NULL".Tarjetas_SHA_Temp(Tarjeta_Numero, Tarjeta_SHA) VALUES
('9657847441893932', '65a64ed655c13421f72b9a385a46abe1c221adf878cf6df7a2eef08baf7ccb7b'),
('9323136914503491', '23bf22a063d8f31ba1c69cca58da409ae48bdb18579b20f34f770fb969630c86'),
('5289065044060860', '31fa37a8e2bc794b577bf1c7e3fe7ce2a9a0b0296fc34feff403b819abe68c8c'),
('0546752832205027', 'd9110af4bd82cda2df43f21383e11e31a6e70f3deb743fb2827692e31841618a'),
('5968753848160967', 'b3572c31fc2130a3f4255a19bf69880daeab808cbe1b877829f3442a94872786'),
('3711160072587833', '1b5e7465c18e0ec671580f4ba56469654f11ec67a4821c8f5d46eb87c7bd6272'),
('3892092427040733', '953f4b7ba22f87a1d358a0c66e47693ccb47a2e993f6ccbd1198929ab2a6132b'),
('5287854373330742', '462de4efb6fe79921b2e37ce4a7b16c5a9fe780249d695b63707268d3a4b2080'),
('8350774133395369', 'f572cd65826a2eeb908d8197fa9bfb321e382f2666d7287b1db521d7d887d22e'),
('4891620084094919', '20ea3768ffbb71ee80209803038cbd10d314ca8ab8673b4b6b3fc059b71cd41e'),
('3044189130754291', '1c493de940bdbf5e6e105798ce66b2be98aaf35ba00cf3f4dacbbd01d74f5a1d'),
('6874757428626518', '975b489f053a68065be1f4091da788c465d812987ee4a98fcb2b55f88141e441'),
('0770413970756489', '13b8bd5b8b3de4dc1ed88f22f7036a9677f85054f0648f5ab2a944f6ff986e03'),
('2218635869438291', '80a0ebe2526863839b0f40589ee9b79700d6936a06aac4a1f7a7586f87f2f071'),
('8678621846238418', '9ae3036222048fd3dda6eb929762ac916653e02fa108af3f708c8fbc03d393a9'),
('7494092950917786', 'ce9b31e3c257ae3c89c542bc29f562c075826f8772ae85dde86dd8c7fd9745ba'),
('6919350895316866', '2b40ceafef5494804d5532e9315cb4384981ff7c3c3630af99dad2959ab7e42f'),
('3032768398261975', '42c945aaa548001b92100ebe9e2ac749c1c07be2d503d176ba3edad0aad66683'),
('2920561476267018', 'd3827ec72c38aa08ed3de7757fa55b34be334d48cda9ca9b63f4398df7f1d632'),
('4041934865251778', '6adc05deef1b3a2d66a0646e983b1f131ea2d02f5902d47af122b72287b65f45'),
('4006118035223396', 'fbd4c0601e8a32d274c6d3e507ee7d5d90bcf90f29fb1907f02e2943e3ad4c92'),
('7705163435578441', 'f1dfe202f68abe7a8b2901fdd542d592a9e879de1f62a7f193b6f63eee593574'),
('6506529390735709', 'e0bffb9a4b899e63c6d9694dacf7c1cbdfc6892287b1223b6c1b03a75f9a0278'),
('1579714523535654', '07516d167fd70878fa50658aafe9699c7edd82fe1c9720d48792bdac93e5bb9a'),
('7107816717439498', 'fde58c5558aa3ad3edd5357273b52aa5a9aa0da51be70badd9fd98ba4696c3c7'),
('6075323406591059', 'f1534058a51f1f4df22fc0de073885568546d241d7b1d1b02a9d680b90f914a1'),
('0034496259269328', 'ea723e84af2c9b91acb6a47e319974e48002acc79d3f24d453f40e48f6a17625'),
('2550113545565237', '5d6f52deea53962d3935a8a386558aa91158911526eade032aa708e8f9595232'),
('2385212828171029', '3b268e44bba4f75419181462260a505f6cf137b298deb860bed3a85a451698b7'),
('8125839474375981', 'a84a1ed9153e4cbc93deadf14323cb2155169d99bfac40f0b43df5b397f9d4eb'),
('6138293599799741', '5b5cf80d5ab94671f50a9de36c50499b7a907f2ed0d0218b0173b3480bef72e2'),
('8345659054025066', 'bb62da2588f091df1e78ae98a2b3298cf9aebb938b034c56d1b71e433ddfa42d'),
('4778457062470489', 'c3480c7f933783e4b9c141f3206fb28a5ea6f0c446f5d6beaa51428004a08571'),
('5291779571311674', '529ddadc2ccfc486bccbf30a67efc37c0c50dfae721f113ea130014efee6a70e'),
('3323895607651174', 'ea095a70b4929892ba7a8e11297f1e6c07ba7676bf5a9c1e892dcbd071f20dce'),
('8422872248132544', 'a331e54d3211210f3e11db89091cd284975037a1963d3a14135afda0a843e4ae'),
('5045196974006263', 'a26ab3386af746026d9e215955edaa177dedc3929997f888bda0259e791da86d'),
('9403385153870653', '7ccd869aa855eb019f761633c7b227a023000633b4cece69bdde1b25c89c2d97'),
('9231502315126193', '4e2d70008fae9396af3639c40a53e0b294e0440955486dc351e0c0108cbbe7ad'),
('5162171930839348', 'aa04abeb326b224fd9683dce27944eb426e6dc1e8f46a7ab000c62351e315b98'),
('4867936634357460', 'b32503b2d9eb53e43cae18363b093e40b307086b5e945caea2a19a43548396c0'),
('9534769146269066', '1352dc9860c54fbabe22414384f6cab1f99b53be24ce9d1a58daa5b8d1138950'),
('7634528668843561', '266b858cf7bbb0175d1e1528b45275ea6e6793095477c3c67f853a4c2f6e3f6e'),
('3869440595326906', '8add90b22400f6e68f85ef49443f682cbb721df9c39c748886c491aaa5308f3d'),
('3623452829365124', 'e1246ef1710b843502ea0f4c4cce74a1cef922931616680d031c20c994565957'),
('6274226157966933', '37876b3ff94d4c778993ae2604a0af1c40cb9084ee2d53863081e4f7cf974e3a'),
('1366822405942588', '91ef9f63029b6fd44d39b5dc7f6ff57a093dad1e5928606a2edd958888664f5d'),
('6105363682812421', '17f4141b6823c7c84f3221e4716bd16ed6e170ba4b5ec5261371cd3cffda237d'),
('1672136567570320', '5aea6afdcaeb29b67494aa856aeeef2bc930fd00661ba736c495be3e7fba80c3'),
('3151434641567514', 'd77271fb2a46eec299cefc738499a63f7d418b6bcb28fb34c53a3efeacce52d7'),
('9482351853356681', 'b0f2aae93e7398d60309591fd5fb593a66a3e136ec6c10b265412a03ae4ee4eb'),
('1128378055922937', '8d71cc46fbf23b244db1dc95175f30edfc5e8ed75be81c94cc7d245ec4fa2b53'),
('2080396843804516', 'ecec81d5eac792d20a86b198b1903bc88d76918bda55824850cd6bcc075eff3b'),
('9782085756603220', '533bb203764a1a19558089b666ef473d18d01111ac5407324dd26b0dd9545fd7'),
('0261666407257839', '4a7b4a90b3100339a34608d93d950667ca42843b60f95ea4353a042fc1a5b5e2'),
('6794715656082992', 'c4f3ba9f51ff5ca62fb880b44ec2cf2ff14ae2f82fbf379b466d50b98232db76'),
('7775430233793460', 'a71608cd627d750bd7bab96179a26484f236e5e6e62cb31c03cd8a6701158cf0'),
('1753173385607599', 'db6ba124326a7260a901a5373b2cc5e14f5dc5cad376ed57c3502ac21a137c4d'),
('0934568883020410', 'b6c360aaf7d87416d7001a30553bb8840fec02aab77af409cbc4a969ae20913f'),
('7541466524457269', 'eaf642ed9b6a06cf69632cecf5cc3d9321a886144de3716604a3099d4604bd56'),
('8817861807996093', 'd99e0b2e749010c353e11043fa16ec2ea897edb6f48e7cb475d70acc3e9d5d89'),
('4413326629367835', '25a17a81e7337a15a7234325aa9e7a77aedc4c3d229e23b82694965fbdb92e41'),
('5631115914176206', 'cc40047507da15ec1c731a79b6fe96cfc0c20b335cb37de04005a3849541b085'),
('7319798058657367', '679c7cb8e473ddff214a3116a0bf707d4465c520f92631373011177ab0f02b64'),
('3801770096541352', '1d7a5de92fc96f58f3adfb061f9bdb03eb6ee20a99885333955bee5aa58d821a'),
('7538017867673033', 'ca9f4bf5182e0a14607d7946d4dc86c1d340ec62044e4f601ec928119858db84'),
('0876449316839406', '4548fd6f1c84a93d7171f30223f82e4d0e0a3fd2ef7f440afab71331ed7519d1'),
('8884887553269688', '889556d58d7654e1c0956b5ca139676bc0fa3c00eab410e75f56605222686eef'),
('9968239846608049', '3367997791531371e3bc31a3517e108f589459e621925986b2c8941d0ebc2856'),
('2006402089373860', '59be25c318046b62a4492f937b8e5b2862da738783ec0624acf847fc989c6834'),
('9610904717985290', 'a07302a0997e4b5d96eca596a74ec7167ac8cae3ba75bc9da93977a91f65cd14'),
('0025811537648494', '7d8e69e59588d801fed00c79771938501f1710d0e30d60ad09cb7b63922c225f'),
('4340001470633632', '4176b87a09b9bdd7e77d0f643c182d590568f29cf93176938b94a05fa02ed3d7'),
('4384243221566370', 'b2a26f5a8e0cab55585f66812306a1e46e8c6375e773b7243cf8ca2730e3756a'),
('2694193483474095', 'b6323e78f3d1ca61162550ed8426c3fc89e154f302e47f0db8d75b81ac9762c2'),
('4748132838155131', '59db6540d59ed079e1a65ccddcbf07770ef84bf5d161f6b003f3033c457848a8'),
('0575702838885428', '16e39fc42384a9221479f415c3591d266f76e4d5e01c1e660a53c1478744bf02'),
('0851232866211984', 'd3b2dfa0bf3cb996d1acf5b0a0d24581b213ecd24bd4c968fb44433544fe2216'),
('4256699943049950', 'c781bc1fa7ff8ea6f394b2ad4c5fda24e64717f236819c5c59f5d95eedc0152d'),
('9124822178652408', 'c9b0e54d0731d2dca21425e81b718a08b1b38c67361851af4b6608e9104699e4'),
('2429692211108775', 'a71ad61afaa66a12a0c41c65402c96a29ea4c5fbdd9c9c7b6c5b9bcc620e5896'),
('6052138070057141', '78578f19e54334fcb7191987358cc653f2f27569ac7a8d3d2ee1929763f03b44'),
('9927876439495651', '4f28b1ab2fb843955ae7596886f4702511e3d765efa1013b409e8911cb4d6076'),
('6699385853188780', 'df95af125ded3d0d19f383809b60278a03cef5b89c79926d980f0029a6add0f1'),
('4996334165366536', 'ab0afe17b26013158286b19b874d73c3003aa854eb29d34cad044202c5ac8f46'),
('3220069786780553', '19196bc7feff9f6f6cdd3f7607a98b09642ac5349106d805b13ebd083f491483'),
('9974967463292032', 'fdc9e3bfea0c9c4059cfc0167ec7440ac980085591f52191e1bdf3381777dd1f'),
('7745565704053774', 'a8a665d1d08f9d99c15459f07fdf835ef744803928e0b4015b502125220c0fd9'),
('1244856032737948', '57f6e02f6284d91cef556b5b6c64a5955df21256611c9049e2ad6cca5ac99bc9'),
('9648473612304264', '4dd44c9091e02b18f5223adf236ddefe69b70a38682c8f471b63fceff024f5dd'),
('5760180435487091', 'b9ac04da8d5d4bd758abec1a069e614973528bdd1f5ca6314fa0f0d1b640bcca'),
('9804869229253831', '2aecb0586a93b0a00ccaee5e13b0ff2adc021f3b85ec95081924b6d9cc0ea239'),
('0604446015461459', '990e81969f434c68702db44f686a916c100cc78b78eca74065effcc1570c045a'),
('6069303127793794', '06a61fa4022b334fd4660b7595d137a19321a8ce5c3ebb40416a83e398340689'),
('9077689406001729', '8dc8a2affb0d7f595104e5ec0da060ac6a37a1eef7028603a1736e63520ef8ed'),
('7115823070514035', 'daa86d0e453ff9e2625a4af7c47b910d367c37a3a5a174213913e2a522ea84b0'),
('1735644505714847', 'c6391e0558763011e943548b761ddcb506146125a34d8d7a784670f33ba28ad1'),
('0660522099882064', '77594dd6ddf55965dcfd0eae88d6780d5a447c959f8e21be213c82dbd661f6cf'),
('3341514889317681', '23a9cf39142276af6aa79a3fc5a4a327fe596a6c4fa8613c6356394ecb2e6b43'),
('0974654824394742', '7a827e6a6b88a93d9cf8fb6e6a042d1564c4a8272bd2851c79655e6231a576ba'),
('4587862237035994', '2266ded8fc6e10b742ee6b5301fedfae148f5d026032f0f8f778245123716428'),
('9599561831309540', '2beaa5577fdc815de79f0ec066422cedec69fc9592851177a1ea23a4bf165697'),
('8215302823323799', '3c3e35bf0013c29bdc23bef80f20445f72df3039a31f92228adfc8310684c955'),
('0599932068278849', '3c82ca818c248d9d9fc2abb3afeb13b15567909bec96f98b3ffaf3c760ea0568'),
('8014344413922449', 'a7a1f9f513ab46309e4af659b673f3aa5d5b00bcd9a913baa1c2310c6c2ed6f5'),
('3117163737259254', 'dfb50b62be0db2ee678f2b05cae7d5a5f10e6a28ac43457a834bd7ff09cb52d9'),
('4154878861083102', '9f378272bcc614ea9956ef66223035606d57b693f4dfc4ab8cd3f46483fa67c2'),
('1216224720808610', '4d536654370bd7bdc9dcd5e268fc35ecdb4b6b5096d78f1813024c0282a185d6'),
('6052866716940393', 'dd024a0f8bcc001b6585cd307214d6f08116f052e17f2adc41b5327260c3db43'),
('5535596574696862', '2eb60c29e47c51065f424ff191949b69557961cb2fd77c0d5d44f57db886ce70'),
('1601322845116032', '09266bb06684a5df2591162a9fd6d770f6a752266cb2561d8a08e106b8e6d89d'),
('2753995799573164', 'eb56475281d9d29449fcb29690e248c3385fa6f61bacec4537f3cd18fa5351b2'),
('8189180408551131', 'fc2836c8e4b064264e531bef281c88d6a1d47032969ceb76afcf33c2abed68dd'),
('8549088606443118', 'e780403e04dc3e85a500da9d6ee1c88ed6339193ed73264650f6968fb53b56b8'),
('5604743462876138', 'd5468461a435076e0ad9e3b99bf14924a58eb26dedfaa0a17ed36f594f4fc920'),
('3959981714867150', 'f1228787a6b237c0a0d099f839dcc4feae626f9909355476eea6457414aa101e'),
('8474032713225962', '5d28b358e767a63494ffe4a3a346d5f4341db738b4f0387671f9226089266fa4'),
('9876425207468457', '4339b515be17f16474173da0b127c79066fc0d65ac90bee3b21a2247a1aeca63'),
('4901954407084865', '740c972ac5af3bba886dc3c5aea8d0cb8d7ee59f52828f6b45e10acea59f58f2'),
('8462678007793039', '5f48ab69ae260bf18799c807f36a4f7f74f18aaadd25a37bb0af7797c7e7bff1'),
('2336881333774282', '9210227a01931041ce4693a595683dc3a12860553082d2de368b2153dee5afb3'),
('6243426087452082', 'c9180def03fe8b2253cdedeb46fb31d58ef6764a82c28b990b3df6d25dd027db'),
('3952630552671095', '76b7b8e372241b7c3dd2bd20e049195f3eef2cc7ae0a4c5d5b07a31a01a5e942'),
('4489040541337597', '37d228820522557208ea155b7faed05d6a0f12ca5f4130a3c72ec7e6c46e02dc'),
('1364711352198505', 'f0e3a39c8b036651f79c110bfe9459aceb89d01bfd4906dfe44250f1ebd780dd'),
('5333301334956520', '338901f26e5efea4392f2fd8d8a369b1fbb90169e565a5be5c8a58253e7bbb7e'),
('7416993946698761', 'ee12d7462891e768837f897665259195d6a48a738f70b83749c50a36f2fb1eec'),
('3176457661382502', 'a97d8655bf77bcff35fe9ef92f6ac0095213a24d51d3635a930b3a7b3004a0d1'),
('1550840754929619', '2bf001c1b2504af100ff668286fd14978e05988b871506e7c03ecadca09f2bd5'),
('9598522198034814', '7f0fd53b190723377de2ef23e94f569b4bdfd70953601fcd648ea1d075fdc1a4'),
('1730906136457371', '313c5869bc4fbd2ee9679ca6abd2c3ffa521495c24b55b7198ee9012c445976b'),
('8563602116378343', '3097840a964f3c808f569a3cbe9f1b4422f8e8ea1d8b47bdab64648e1c99a70e'),
('5141356133871067', '00ab23034256f8cae43e8ce9a2dadddcd45077be09dd2347d5f24644aee00626'),
('5996483574982248', 'a0e51824f5be2ac8c1e79a0239da1f8ad29476e6569fb03e7af034ff1b423e84'),
('5317306338861063', '255c0a39085de3d7d635c7732af0e15fecf1d88ddb4485e1e74a39706e10a8e9'),
('4249100207581897', '9890013ddb5877e97f1ab046b8f98ada385118024767d8de0b4253e48e917434'),
('7371657586120309', '41066b8904a060e6a1d88b3d9199ee86276c61ba78a3195b3b342bfc14be4592'),
('7191708331763923', 'f2e00e0074cf4500655610e580c2dac4210637a3d36a920f99ddf2b02253bde5'),
('5176863191853893', 'f1a4f19037c72b107675b6fb7f0ce3dcd071dbabc6c0b35aee3229db79f44b3c'),
('8559296630402537', '5dd4189623051f67287832ac3b8a261b0d2a2532f8823be242ca659224a6771f'),
('8468739843066188', '56516cec16b0340d6bbbf9497317ec25ebe3847ff0199ac4a06d10dcb5220ef2'),
('0906181135791863', '0a27a10bb99f430a70bf383ccbba8bbe434b98d4e90f007bbf9bfdcba5cd9d3d'),
('8469812841638809', '7a97c4fab38bc7187b0221512d21fb48d45a61d211cb429df4e85a78daf613b4'),
('1532174321636467', '45afed734c1d5da3a0ad8444ea52c717b82228c2096f7e8a36c85ea222cb4c9f'),
('5112185149435235', '0ac899b4cd978b06cce33b2514c684cf87c3ad89a6c58ec36f933731808b0b81'),
('0554708077404294', '29d3c5a0321bafad8aa5000873449eccd4a78449ffaa1bc1c791ff897c936dba'),
('7037390163506147', '3d605d9f291e1680f885068fbb69484d12556f83d653d9d912214a49150db95f'),
('7585954365083815', 'b70b867dde4b94eaf4295ba32fb58db86366649c75415ee749ccf3eeb13eda36'),
('6576357273248971', '1440c03a27fba204b676e91279fe14546b0840b139d2340c398db2b6de0f0805'),
('9006963899127010', '144a2df2b7648f26f8557960e130e81e21d688712dcfbaaae0435f11ead58a79'),
('8244461109417031', '641c48eeaabab450de60e1db922e68d871cf9e7162cbdaa894359338b62b3404'),
('6927112181162554', '888aad030760b2575145125b44c05c169fad56501b269b877295e2b6d05dd17b'),
('5083143517722863', 'c906d70dea12766ab41e8d879e2c321968e810e176acd56e307e3ff27fffb34c'),
('5513465728231853', 'fc42929801e090c09fc63345392c88f189bd3463417008a1655021d3f2c73d7c'),
('8839689544483746', '81bfacc3e2e8e41ed9ca058b898c3d428bec7c59cfa498a678c5516cafbc2661'),
('7173667699251948', '68dec1149e612e23b46a4cbfdc9f0dc3427f582a131899d12165ff310bc9a773'),
('4006416666497275', 'a2051673fa56131772cff5f80f9ec47413e7305cd18a100a248330bfb6a6ee6d'),
('4656093741347799', '6008780da949dc8d363bd42f9687ddb648767bb582eb74ab351d7b09af422a43'),
('0119067020852773', '57d19fe335a535a3e8cc21943eb99cf96d646922865f36e1c816a431efcca2b5'),
('3153979645719631', 'c31c72ee4aea1b1b218b95b7263fd73755f7f465ad79bb172e37b36cb0ca58b7'),
('9096754452088794', '4b8d2ddbc8b7424320ccffeebeb6bad01bf33a508bcdfe4596937d98429a8242'),
('7028813430790568', '4eeb3df660566f162ef29f04733fd1aab1bed2966aeedcf2fe5904c4e4fe28a1'),
('6030307647878330', '5eecee7ff71fc645e228d0a0732885b0afcd65e6cfd62e7dbe721e5f70cb71be'),
('5195524681768308', '302589e25f7966e72dd125dad8d8516c07035627c3bd194fb6b372bef5d5ebe6'),
('7338946277813536', 'f8154f94ed083ea03808e223f73ddd566b2ab6908e548fc46b577f0d46522d5f'),
('3711916761038293', 'a4cb7ec5b48afe396a64b03699d90b628315a4a73a1bd51ba8f40f6d55d5ae2e'),
('3240964130765132', 'a6f8a0092bc0f79dae4759d85f99fbf8d98f7262a73b0e8df328a806e8d293d6'),
('9857522501241519', '87af9a26f8b8adb7d7e9a0b0955e7563c2a88d3cf881dd27e3277f34864914e6'),
('4716498666949863', '7e200b6dadcb53dc48309045e26596f7849309c7df212e1ff0215e3db6a07cfa'),
('2303308838627143', '7da569146124e2ed0b4ef651bcb01e005abf70b65c00ae3aaf7d820f6cf9d219'),
('8183384404570975', 'eb063dce4ad5881f291f09022b382446bc3f7ce16b2c86e7651970c2b8feb668'),
('4800582469513172', 'd2aadf960722c72315d26063ef64775dbc36bfffc73b91a8ef20cdf1954e3310'),
('5276335670671422', '82e3b0065668f780359e97f35c5d355d85a82dc90fd53256b1ef3c4628f9a702'),
('6288857917538020', 'fbd8ce43eafaa9ac130618aa174393ac4014d5b8b8e96b483fd9a2584321450e'),
('1538043370509743', '9aa363647ab56318fe5b8ca0382701e923e4017635d9ee9dd41dafe7590afa5b'),
('9985089849461852', '6c7f188c95518a34c385679479ffb67e89a3622e1122e07223d00ec5f7c2245a'),
('9416414327204597', '59e4a06523f3ef6b0138489f6f7a1526e39744236821910cc76d07dbfb50da4d'),
('7023971511996155', 'ce23ea880b9187fb871e8f4afcb85755c7f1474c9e32be87cf8d6eb4b1f3d771'),
('9664075869332583', 'd5792095158b3e34af1d9e802310eabfd4749f65927c720265e184098591fbae'),
('0004766936888849', '536e572b522db7cb37c20bd8f0857adc1bc088180afed6177c9a0eac8072b6da'),
('4412614236473415', 'e76e7224af433337da3690939140c736a60e817bd063b118eb2ee0e0b4550e4b'),
('7918088880328043', '3216580354a3408a010977f068faf28c2f79301ac1881d26256470235171ea43'),
('8485434093214899', '2eb78b54fab2816da57c1032540b06c1db17f3c97ffe49d52737a7126306ad96'),
('3648525489909548', '00cac9a9e9ed7ed533b83f04fbad26eef64e8e48c51cdb2b6863bbf094044f8b'),
('1510836651216565', '78708e54b67235576f32f903484e87d0a75f1915cbe40235f19cfd25eb8f1d70'),
('3578009893799886', '5ce564a0850155f39af123a1875059918539159119b2741f3ef212e172140e18'),
('0855174181980504', '867d7297c6460e8c34b64fb87fb901cccfb93207c3ae27658d52f47d2232f04a'),
('0414182371612934', 'd894e62822bc98cf94ea8738317872b40ef79f8bc247650c08a2dcaa3f6840d9'),
('8226629091541056', 'b0bbb7b55718acc29a840d2c648f243d337d6d2f8ae44d934a771970633b8ae8'),
('8151073929676965', '0a254b40c6eb5c7503888a5b1cc808f2bf74dfcdf12461f18a6d3b9f01a84e4a'),
('7225069699417088', 'f0d858b96ade38e623546a1a5a413d44ca4ec15b9488e7baa6aefcf451548b99'),
('2996196760603707', '8183c041c2ab78d3c7df9613cdd0a7823a4130de3170d9a7dbc9fce29abba873'),
('2393660639992542', '6abe1c30f1f218c7d9c03a75cfd34a0bf233261f6e3b7cd985284db7322ecc4c'),
('4884589311143037', '0eb9dc83e6f50b38c6b537d006f3ab9015a5977db90e6022a3d0b7ffec0695f6'),
('5525372788878402', '649202d1474b3c0de179677149b7885be378b1aeb941465a6da0c2c64f3aa84a'),
('1725939251559745', '5c7cedb6d727a67bbfe7cd642e02f0adb9d1ffce66a1e35cc7402c8db92e7cda'),
('5835598749793820', '872cc7ca5ba386f3071c9c40e1d72f8e19749a6c81f13cee1e819c27d2707c64'),
('2798549110084975', 'a5d83b807d045d708407b74fd91da5b04d0a185e04e9a7827e42d5efcafe3e82'),
('5287690357936871', '67b00c8d7af6aaf7fe79e733c53a23cb1d7ada4054bf2cadc262657203e392c1'),
('1656766282421972', '87bb1e0bfb452b6c1599c3e0c8d32150f83835c57a8d0c24149b92e53b9445ba'),
('7866276450062898', '2c2cc0d12b2d5780097ffa417b4406c4461b413bd742d639c9c43528d9ade342'),
('2780579014607693', 'afe4b7340f73e5c70bea44f6d852cd23c6b977e49f0b8bddf692f7bd9dde7fa9'),
('5370388125408204', '827ec2572591678774ec751ff85742314f92a276fe42334290b02d4f5d7c61f4'),
('7219948162158998', '1f30947cc250c986249abe5d8bba1b10171a542ad754d8a69574608001d170ab'),
('5245857865194730', 'aa165a83d269551249ea84b8d8e76693db3304603ab35665c77720d6e590cc68'),
('9334586850558778', 'd424aae1466eb9eb6a695cea3cd050a1cad0328f67c3ede88e27cc5ed9fb9de3'),
('4587049378040666', 'de3a1bc27d70fffae6dc4d8b956d981ad8be2e6e326e83e584b4ab1b0f9c6861'),
('0846678175004166', 'cd69e567a0f9cdaef8cf51902f94b43aa5aed4eb8e164e23ad46747f914b3707'),
('8001181949969778', '8dae4f200812485211c04d982d4aea59b47d19b27ff34f2c4652b65e645becd3'),
('9219970259787880', 'e448f6ce8a3e68b956976f50cba66e512f37b4c677cff389bed56ccac9e9c56f'),
('9739078962115556', '3715858fed90c92dcd6231bfd8cbae835656890adbdf589249ef184347411e6f'),
('9956488823298631', 'bc21345f7380fa115e74b45bea332eea6a4e5e3a3b7c9a6863cba95d400a5a9e'),
('8176608526128751', '646023ee216486665e75b0cfaeb22ec0ee00f60a7f171d623a08404d055508ca'),
('5629327243507548', '48077671cc4c4d77bc2d748b1b60404b950e2d31e886de2467f1012c117bd375'),
('4006456314480171', '5067f1263b97f3eee6bc64c0e4caf6ae8e0464390d6d6ac8ea23326321b2c46e'),
('8982468847031938', '357eef56825417cfd53140c3a158b7ccf21074c0c87ad33bc2df4c72bf149cea'),
('8073721196888201', '6f30143e00c9aaf2ded59069ad0e8c5fed7c788a41433e4750e1e749e4d4e403'),
('6564577995479615', 'a9bb9f63ae1052422a6b0df90f4ca84216a5c812180cb7e9d3475529145ebacc'),
('0924340782169594', '188a52de7054d1cad74f6b355db6b827c03d628049539f276a747d041ef016fe'),
('2543659648188269', 'fa77f136fb1e0dfa179a2e7b5b6270a6b1937fb140ea40b0cd1ee70d6a36a3f2'),
('5795107873627920', 'fc9ba2f4690192914b853b0d8ac3027b0d349a0bcb5e1b77961a4f7d410cb7d4'),
('3048623412685595', 'dcb82b04e862433f95e91669a0f0ad1c587bd7ceacbdf07e25046b883c1a87d8'),
('6726090836640060', 'c855256a596c35d9e53a7a1b14e8a47810b3d140eb4115ad4b89b3602a82e8e5'),
('4268078940469110', 'e8a4973ef5caae8c3fd7f60804eea1e1801d489c336854c272d57b8989971746'),
('7407120366967323', 'cd277d074ba93745d49b199ffc9df2dc0bbc94dde0e8aa56db9216e3df7fd3fb'),
('0623138149942355', 'a92802a6a738458996e1df561b82c10dab48d2bab53e77d347c85f85434f6b8c'),
('5187632683507150', '2c2c0946cd3f25caf9d3114dacb5663022afaee7346f4e94b16cab000a3b409b'),
('8841118709254907', '2a060073c834b2bdddd4da4b4b7faa983aa9ec83efbaa75ea2f98dffc05a018f'),
('0577587219724373', '528f6ed52dc1f19f6ec2f36182a821e85cde6afade466d43f5af448be61d3bd4'),
('1227829753835924', 'accdf4013dc830488bc3cc3be3336406c530ba7136f7500a92600a0d5e7e540a'),
('2277662483989455', '192c92d31a04e650e5c56539329a3cb713a58cfd9f3344dff68578e3cd872de3'),
('6051938042694452', '8683514aea42a41f02a0eaadcb488d0b0b968a39f33d214fa0ac0e52d8b5a817'),
('4164434597113125', 'ed6d306abc0add025a5ec56dca0d4bed6e193d21a7f97a712a80730f3005536c'),
('6499105225058525', '9ffd9649b8d2104fe8e8787d65fa7c635bac9ef36ccc22aae2cea89a1b4a9d8a'),
('7032294232778367', 'd17de0de2d1b9e4b690ae4d675daea90bece361b8e9d22593e0d7d508767c946'),
('9876749583827398', 'fec77beacbdc85d496249db60c873c595acecbb0469884bcd5169d33987fa643'),
('5382710884884485', 'e9e0e68f5fa0dff21b4c6b0eae1247be7b95d26de947fcc43fb5e38f6cf5bd8e'),
('6742599602954431', 'aaf3c978863f9eb738dc6d64504d9e8777210e6ca6db3a4dc76a92de27fab4e7'),
('6567974504956200', '8828df625dde8a508660c4bd1e0a9bc8ef7ba9247c0404f456c8b314776b5977'),
('4735361745664903', 'd5977404a69ace42a593c5444652b9ff4b7ab03b8f5266c1deda7ccf9d36344d'),
('0534796481133557', '44ceff58b5515910539a7394ab349d6393b8c0ddaa061b96212f933373af6000'),
('2924190478048168', 'f39ffc5fabbafa44f47efb0145d6975c1a3bd434ce39aa43ffdaca29f173d917'),
('8718251973518189', '38522bf350b41a63dbc2225b21b53313db3f7ce02f2ddb95ea6f0a0916db6688'),
('0598995556948821', '13f1eba2e1400eebd98ebcfdaf7dfd4aaa2581d37434e54560606b2a41e52e5e'),
('8786087234024529', '91750be2087da12e7e5e1e2fdea531f32383a5dbee9ce8f727edb76fd6827bba'),
('4384457221685948', 'c14c410df5190774edce9ce058491545225cd93f4c989e1fff4c44817e949a8d'),
('4908145428098358', '4ed166655fae061905543159c7c7b5ad1c7783a5ec62b9c04a05c4136aa1b6eb'),
('5549426061606283', '40998e81860ad423d7bf7e789cbcff2274788dbaeb7026be7d85d60fedf0828b'),
('7874414750590481', 'fca39a46736095857183e9a227821e85726cab0ba78e1e9ae41d4e3c35fcc689'),
('8655162299111245', '58a738b6c2ab4f2c39709b0ce21d0c47abc720456f8a420d893a268463a12857'),
('5314984259321925', '4e14a9225baeb4b373394a861b3e3e1e9ccf50ebd4c33d5ed91066a1e635e99d'),
('4880830085970600', '2d3112d74d4bdd80a5f298a09f967e72535cbd051ad3c489592abd48d8af58bc'),
('6369917613826367', '7905f003363e2ad4738e9518c68514fd8c24a9b51e4e3016ffd4f431fd5840c5'),
('6107584469993211', '5a549bad8d34848affa87a7279bf126751771ba019aef96e432ff3f439ea3591'),
('1784803025929199', 'd43189d665541c7bb73b5c794cd679c41a503fd957201293e414cd1ae620addb'),
('1853412071457963', '21c291272d0c8bd9776967cfa5863567bac729894b3d08703c5abcaa67a4d92f'),
('9604246366514531', '20a38f71be204c2b06a11f1764e68766ffa5557860881d6b66f4d7c1ddf1b8e9'),
('8200832575825835', 'c13b989f4bbbb45ef245c477102399568328ac2957d52ed609a4e6ef9d1c0883'),
('3956400823556507', '7af8dfd025ab7f2936fce41189cb05dcc8ee0be0b88fd423eae217eb13dc8bb7'),
('0609609436549819', '998b0dc135c7d20730b04e1d3da8bf3501a3dd88a324abb7df99139cacf4b319'),
('3854187942619785', '3e3493425e533217afc7f670eaf395f3eff9815dc72ccb5344bd74d34090d5a6'),
('7828764466903727', 'dcd250e9c0f5de5c76a116b41708532c4eb5c32108b9e444f836bfdcf0e49596'),
('8251349398057335', '26c4de1c23be9defb8c6d825eb21cf51a20b8ec3273bd20366227931bcfb7992'),
('8736362410213664', 'bf220ff174b67d6bd3d726e5a93448e1f82ab1f46edf8f1dabac06ec347af5c2'),
('2783988196988859', '2b74d3d7921bd8b78b8ebbe0d689c70a92e99ec9199bedc9a7045c228a345598'),
('2519208828472807', 'a76ca27f5f9b4533bf8b3d9c4387b5e8abb9fd0e5e75296b109a47ab3f62225a'),
('8065774689641239', '3f8409081feb7348f244ecd5135fa46b4f77609979f85dda40429c798c75d1f3'),
('6845676996826012', 'bc1e8924973329600b472015a325f196d3a8b44f736304287eac19fc3933384f'),
('6559542550169803', 'f54778dfc934006d457c07e702395c6344a1e9097007d4fd44f28c7f78cfd238'),
('3782835647987106', '931be42645cfb322e759a0e703a7e4a1760651310c9ec0c59eaaeada8b5f4b6d'),
('1577711961897971', 'cf290dcd8ab195c4cf222f6c1d7126eeba5d01d8f8643fa7406c2d68b19c0809'),
('4732648705405943', '2faa9592bad0159a6f3bb2dc449bb897bfeef330e382de8feea4a3f98607be38'),
('1415663009381526', '8ac381cfea52891572cb0b63386e75d77b57f1227941b67d6e68ed10e3535df0'),
('0642018552773219', '7e4b59e86d8aea2410e7be6ad45d0a0debbcd8eb01143d78a59365b4773f4b67'),
('8818676672091518', 'c23dbeb30c083f80411b5af4d0e8060510a407a5c60612ee7374cdd390ca6be9'),
('6430305590283719', '000cacbfefcf283d70ba23f8304199acfd47ad19f8a719a0f773a1a352d91e89'),
('7887258845988482', '2db0e382f46e7343f9f48dccbf5b3e221a823e039c7c280f73be83ae9f59642a'),
('4163480229094336', 'bc81e27e3e26cb1b4e34f0a469c98c6acab5ef242dfe6de3677273a19783f721'),
('8896140020617424', 'bc2ad89abf18e506b2b5d06ddd01a6bf93b187d2421f39285ae228d59a0d4257'),
('6077176049434727', '569b9b4a7d904f90d7d249cc4e94d37a0d67a2e88ab29c88007cfa1f3bfacfd6'),
('5735011439077640', 'ae8e50d050c33f2678ed5f884bace8d8e8b1eceeecd91d4277e5870dc8611ca2'),
('4698528361238026', '85f51bc44198eb891fab1e9b90b7592a7b80dfa074628f1a98cecfc0be1f07ee'),
('6385078940527128', '33af15a21e48e86ecb1422ff4a0afb8667a50323f57be73e292e9024b8f343a2'),
('6965359460545045', '9669965e5052870271aceb032f857a1443e4baa1ff3955110e89f3078cc0c591'),
('3433157172798535', 'a44fa47c6707bfdcf273f92b4f8170de461f96edb81d4725fb38612a5730114c'),
('7661727388196664', '072604ba33ab987859361bafa291470589bb0102fd3d50be780aa86ebb658aac'),
('9664207341882049', '85be48acfd9b9dd025cc7eb4c691cc6fb44ab6dec711974998edc8b6b86f97fa'),
('9359650134471913', 'e3afc0f80420d8d4c0af2996389b2cc17d433d8f1a3d099c267d08fe78ba7c89'),
('8686321721965826', '06985817737e4e356fd500be77042bdbd3975870ba9381805b8fbe440330ff8b'),
('9108097700260286', '286c0c9f0e60fa085333ffc72e39e4dd5f0028a154a0872c3fdc2c9c214247b5'),
('5113215642625893', 'fd48826a4338f8ba2f42b4ea6e0f62d37aa21b36fea22b02c66c637adf34f369'),
('8389921471512449', 'f3b692f4899ae1cb63dcc73bf516363ccf4f4c165a9213bc3d989f99aa58a471'),
('5506001896744918', 'eb23f4939a85967164b96264f6ce98715df886f1948eb3cb1900b67d9e62f2dd'),
('1749175787614938', '28bff27a3a77d5196084fc51b4cb820ae798f615625d316ea2c1b865dfb90750'),
('1049905716976476', '7b46a28d737ec6f627885203e32b91abbf7747e034bb0b0999960717f871d292'),
('5807576401046590', '8b09fc73dc4d80afcb9d53ba93ef63523fe800086f1ef435e187e871c41f8706'),
('3468925103647025', 'c03b78d133681b89408ddcf3bc30b4e84cd18091f0f466775efb237c9cd41295'),
('2437707064747637', '1370fa87caec004671942cedf9e5a597053a968f72eaf4b6b5847a9faf83f817'),
('4299194013833222', '5347ceaa0be26f46ccaff5e4398f3d6e80b5d1a4c7a3d3a8d3d43cef36d8fe52'),
('5674905167386112', '84f92557e437cdb86423649be953ae961ffb3586f087c7dd2c0b477c6f7b4460'),
('3461705186668427', 'ac75b0b7fea8fbc5b7e437aeb6e534c6f6cee0d7768f805d49eabdcaaab03673'),
('8844851398456014', 'e0b417a9f6e691960eb0380ee2b12e67bb366a5a249cdb12f9ba871e5734bff6'),
('9470687034651837', '23a377196b8061a6ab94c91f32fc98030e24f87574ac4a5ced942d804b48a67c'),
('4060134993605485', '5d1b2e102fd50648b829d676858635939647dc98755a7b114143272ab80355cd'),
('1909248605268781', '897eb8fc374f69c343e35c4c08aadb9279b245f504b1670603d014523166e0d4'),
('4170015911578859', 'af7117fa14454dd13fb8b08b56c356185e2f8c51c96f13d25f61861c971470b3'),
('6931252297650180', '4642d62e7bef062289585015e89180172110bc0150e2f9397c904da75c037e28'),
('0603675265716407', '6d5b7b4bccdc2583ef3a0ce265c73eb9842dedc462073e9a861ab56bef3374b6'),
('5444467387859333', 'bcd4c9210b6ed30e3c3376f9afef06cee33b66df4aa3a34c5fbf13cc2bde9f96'),
('3319051453884304', '14031c041187de2f276a10596abf292e67cf551d97f68ebc0d7c5488983b40a0'),
('9412227590672237', 'b09a52df3e2c8c170a4dbff80a762ee2f6e851bdf2f67a8829bdfcb473a7e311'),
('6709359634174816', '59f91e259391828a740ad48974fca31e60e85f0f4d367b43f42531d8b8863be8'),
('5422780530199181', '589c428e4235d7c26c51333768cb6018398046480750d2482f305dd27c91a132'),
('2399570133695654', 'f8f65912305a8cca1be1ea694c686e2db61cada692cb4aea86f771742685bad9'),
('3079912453355831', '4eaaa2174c96d3d0de3eb988310e475924a8706d73f53f614ccc44b12de4afa3'),
('3061509144569717', '6ffa6acd9ce28a0c750b5eb9d5f38400c378ebfbb64e63973f0d142cb4aa0902'),
('7937723384235277', '043d4bd55776783934b404c6211363843ca771565441240713983b1d9d12a2ba'),
('2597709356888222', '205f2d53c3b7709d117e1ac841ef430a7af165d2a362dc8f73b0af3f1aaeaf83'),
('7680712507049367', 'b7e60a4280d2a5c0fc41777ff8767ff0ed870dd132b2cd8aa54712cf0c2b9c59'),
('1935708471165838', '5b43578167c85ecb8a837ef69f75282988c2e1c57eeadfa402348a7308470d7a'),
('2416769371399148', '46288af78985f513b7679046697ec05253b26c0f2fb7d79d32c523dfe59285b1'),
('1014040093189556', '5c46a66061cc2c459aa2eb34941ba49f74d98e6de46e7807434f7171d57baf03'),
('3469973979041089', '33ae13ab516d5f14013b29a40816466282c43c2081673c34451f8cde7922de02');


IF OBJECT_ID('NULL.Tarjetas_Codigos_SHA_Temp', 'U') IS NOT NULL
  DROP TABLE "NULL".Tarjetas_Codigos_SHA_Temp
GO

CREATE TABLE "NULL".Tarjetas_Codigos_SHA_Temp
(
  Tarjeta_Codigo_Seg NVARCHAR(3) PRIMARY KEY,
  Codigo_SHA NVARCHAR(255)
);

INSERT INTO "NULL".Tarjetas_Codigos_SHA_Temp(Tarjeta_Codigo_Seg, Codigo_SHA) VALUES
('600', '284b7e6d788f363f910f7beb1910473e23ce9d6c871f1ce0f31f22a982d48ad4'),
('000', '2ac9a6746aca543af8dff39894cfe8173afba21eb01c6fae33d52947222855ef'),
('343', '3c15285c04fff40024bb8714b93e58178bf8d3bebe6943178e1c5412957b7aa1'),
('158', '7ed8f0f3b707956d9fb1e889e11153e0aa0a854983081d262fbe5eede32da7ca'),
('312', '865736a1c30a82dc67aba820360a01b1d9d0da5643234cd07c4d60b06eb530c5'),
('122', '1be00341082e25c4e251ca6713e767f7131a2823b0052caf9c9b006ec512f6cb'),
('901', 'fa88d374b9cf5e059fad4a2fe406feae4c49cbf4803083ec521d3c75ee22557c'),
('555', '91a73fd806ab2c005c13b4dc19130a884e909dea3f72d46e30266fe1a1f588d8'),
('271', '3635a91e3da857f7847f68185a116a5260d2593f3913f6b1b66cc2d75b0d6ec0'),
('541', '5de664ef205f95d4a68b69b148eecb04f110ac95ef77f4e5158ae315a76ddd8a'),
('110', '9bdb2af6799204a299c603994b8e400e4b1fd625efdb74066cc869fee42c9df3'),
('649', '5480ab857f30bc9abdc0d88179b66cb30b6a294029f8bed71e3b606a19941359'),
('039', 'e1981523bc0228de6f7cea5a874721c52f2d6b6e4c09193e689bebf67db9f6ab'),
('051', 'd2f4eb77ffb11b01e93fcff593b65ed60346f16feae59c17b0bea079c87b82ff'),
('010', '13715f6c8b48ed1b00f509ca29bc826bd04fd6f1ce8d8ebe27fb286312ce3ba1'),
('108', '9537f32ec7599e1ae953af6c9f929fe747ff9dadf79a9beff1f304c550173011'),
('903', 'a2cc73deb383356e2c51d5616631e0071bdf5faba44812156af3526ebc6fba69'),
('357', '2ab0ce7632a611e907a40710ff46da13c5ba832f5a402c6f51e15f53d6e8fa0e'),
('005', '5a96acc64c72c5b2b890cf2855d036cacc054d61a360be4da12c1fa47dc0b480'),
('551', '1f09802c4beac758321ae8a9f94d752b0976c7d54baa6e511bba8a7374107bef'),
('083', '9c60ff016521ee3f8d5aa49ce3b0fae56d11b8b51654fc9c3ec8f4885aaa8517'),
('193', '684fe39f03758de6a882ae61fa62312b67e5b1e665928cbf3dc3d8f4f53e3562'),
('739', '40962624bfc236888ff8a68a74b0c30166b7245423520bb28196b67f57d5e332'),
('595', 'a3aaf5a0e9ad2901ab35ce73910be7fbbe1731a3ed1ff947a6ac395c5024a8b3'),
('924', '1a5659493256d9eb296edea686b14dfd94116d21c8ab25ec0ca46a46f617067e'),
('588', 'a917ca757ac59f9d568616140c2f72362fc2722ab277e7b5019008f280f17beb'),
('139', '8d27ba37c5d810106b55f3fd6cdb35842007e88754184bfc0e6035f9bcede633'),
('366', '600b4cdf20cc06a7b5a5cca5f7464296861815519af6d8a14604201b13965ab8'),
('409', '480f5a496560ae4228bb7977ecf29b2c589d7a7aa6b609534566af8cbc229a9e'),
('184', '52f11620e397f867b7d9f19e48caeb64658356a6b5d17138c00dd9feaf5d7ad6'),
('290', '09895de0407bcb0386733daa14bdb5dfa544505530c634334a05a60f161b71fc'),
('237', 'f0bc318fb8965cad8d73d578cd03c63b7987dc6a79b906aada091e1b6a13443f'),
('497', 'dcb5d6e69e4ded78464ae2843f509daf65c9ca09dfdc9b5ad69166341963a877'),
('976', '3cb65cad26f0a517866e06dcba59afa4a5eadf832404e0e17c707b928825c144'),
('925', '51054b8a03281fd02034378a5570ae0c970fb1d5d64246e0eb981481c228c108'),
('552', 'cc6bb91d4a9aec9fe2e20ae49fd18166f522a7918a2ff2ecd1c2c35b5d4649e1'),
('004', '9c1850fcaa632f2189deac5e9b66e02fa85be92a920b6cae7696c9b691e4bacb'),
('097', '23f9194bae2863c6d91656260a3f7a637a65db287fa21a3ce75129d98c19b842'),
('930', 'bff3992c1d4f9aa5a7945023e32989d83c60ae06b21fd37904de6dbfd67694a3'),
('145', 'be47addbcb8f60566a3d7fd5a36f8195798e2848b368195d9a5d20e007c59a0c'),
('585', 'c403741c4121989ac12c0829be88b8bec6f27b270f3cf8a7be3fe72cba473897'),
('398', '188c1fdca79d927f6e812133173fc41d3a4e57074de521020274caa9bb29af7d'),
('291', '33512007840ced1bb0aab68f47cb5f702abd494a15f26bcbe26a1e47af03d841'),
('183', 'b8aed072d29403ece56ae9641638ddd50d420f950bde0eefc092ee8879554141'),
('518', '8952115444bab6de66aab97501f75fee64be3448203a91b47818e5e8943e0dfb'),
('732', '81defd9e2e8f85c7f09874bbe5b8d9a9a5503c6d915a3afe4b65758f28d71fb7'),
('825', '10e4e7caf8b078429bb1c80b1a10118ac6f963eff098fd25a66c78862ae5ebce'),
('492', '23e8b0175874e1bb3b4799e13a6634a8eddb456c1b8675b871e07ec09abc0c07'),
('159', 'ff2ccb6ba423d356bd549ed4bfb76e96976a0dcde05a09996a1cdb9f83422ec4'),
('862', 'ee9d527a0a6108477fc5c98cf2a00f65d38c8e8508c4d17c1c11b2441c78a2ec'),
('062', 'ffdb47b70fde8d226cdcfe3c8fc75577a35b6a5bd797c3afb6c9a1259ffd630e'),
('727', '30e4c02268d49ca010e3c62fcc2615da2fad4cf0c359eb8fedc0366739b34205'),
('478', '200dd69b70a88134b3a939de5f0b10c44a1675344329b9d9a5ad6b7342f978b2'),
('306', '38b83caefa1ef26940f1d07bd4ec94c60809b0f88f2118e82ef8ec2d98938a84'),
('642', '68fcd1eb684859a314bbf7f7c99037cead480f5bb209ccd4725bd319423e832f'),
('853', '7fc81a57656ec055615121454cb5343aaf3db93c762fe310d976e5fe8d05e66d'),
('533', 'fb8a0d2da8683cec6cc64542f95ae11e085c72d56c744b2be5be335295976610'),
('205', 'f8809aff4d69bece79dabe35be0c708b890d7eafb841f121330667b77d2e2590'),
('797', 'f7abf2a084c3668c7b90654bf01205085e5d0219ffad0564904e5c923af11523'),
('847', '19e68d9fe08f7c4ac18948bf437400f955359b1cf21a86544342427695c3c938'),
('715', '35c71bd7eaf4607047bb7c186d17251942204229b897e033923b13dc8ce2d109'),
('621', '90b0ce469fbd8e30a2862bb24d562dc641c534a9b43c7c33c25cfaefe25e5e47'),
('445', '0e12831a7047f759733b21f028525039607350b1b1b4fe904595427e72ea0d9b'),
('968', 'd6420a4ee44bc345c7bf3a2efbab98e08a4727016df8e8d6bb8375d0a23a8c72'),
('003', '88c0413bfef1d0570a8a6f9c780a8d2c9e90c4d107551d62bf3cec9ff1f5b634'),
('255', '9556b82499cc0aaf86aee7f0d253e17c61b7ef73d48a295f37d98f08b04ffa7f'),
('356', '03a3d955b8799a90f1ff5a39479fde8e618f8ca3282d5b187186f2cf361abd32'),
('059', '33de253288ec0ae86ccb537636875ea4da12a51ba1b98b336cf2298d8cc1861b'),
('964', '835cc509d6d86a3a287de70581b43415aee21309f2dfd9533df5f563f37e3b22'),
('700', '99ee50221221864d50c60baea6f14d8ac2e235cc6e78be6088cd40cc97fca394'),
('846', '6cb6d4b2fa122bf8bd63280061e4a230565fdec3ce03268caa2f48ccd931c691'),
('611', '97623535a9ed79620c0c749a7c0a785de0f8a895807195daf2b0e58893db160e'),
('721', '74de057f768beb42de17ffc4b8a56100f0bed85947ecacaef111e3d3ec997950'),
('127', '922c7954216ccfe7a61def609305ce1dc7c67e225f873f256d30d7a8ee4f404c'),
('016', '8449c20ed663992df29144b6634547fde54fa73ceaa67f70dc211acb1052e29a'),
('387', '25dac95b8f595046bc435139636b0e2f1ff6e0ea31a54f3c19e7e726fb98738b'),
('370', 'f1607c19a0f910ca1b8dce18843bc34e46a533c87e3524ea75798949f7a352d5'),
('812', '313c938e0103b56b43632b702e3e63447fab1f90a4fe890ca5abb7a6cf8830ee'),
('363', 'a43231c2216f23db8d65bbd57e0ce6573654f9a102365cd4b345723f1437ab2b'),
('768', 'f7b856c054de7ccced087ad4f9413380ec494e40abc818b840aaad990ca3c5bc'),
('865', '8b6cd7c429e83373dbd412f43d7422c0c4a127d93d0f2ad15909f0c2a3e7b320'),
('713', '40f8d6d22b99ea3388538fd60bbf532256434b0eac401df1d9a2bdbb29354ae8'),
('080', '613854558120137287d7edd940f2614df8626eefc055bf3c2ed882c115038faa'),
('457', '353767b239099863e13ca954e20a66c9d75f777baf239f56e399958de49bf79d'),
('198', 'a4e00d7e6aa82111575438c5e5d3e63269d4c475c718b2389f6d02932c47f8a6'),
('229', '08490295488a1189099751ebeddb5992313dd2a831e07a92e66d196ddc261777'),
('615', '3de8392541ace28284aca7f2724273739fcf4cf73de276a8ddd3547c0011323c'),
('705', 'bd94717d91260895035088525e817ea10375454f03aa3bd8b28b355a4cee22c5'),
('665', '9ae8f17cfc8ba7fd8fb34b2a194ef965a3b36a40839a46eeab1350e916692ac9'),
('959', 'e6d3cee8c029277da8d978deb058e43540a640414845b2f1c9ffe75f64f8d8be'),
('640', '3f1bb7c0da3c01e685edd592f3a3ca0b149a399d25b97c0da47118c24a39f59a'),
('202', 'c17edaae86e4016a583e098582f6dbf3eccade8ef83747df9ba617ded9d31309'),
('431', '0a1f1256f9bac68e806442aa76455bb761af5414855efa23c1b3fd54477c0ba1'),
('828', '43d244581aa23a744de9d775979165eb226a80e2cce6c0d0885412c9b6a0dbdf'),
('174', '41e521adf8ae7a0f419ee06e1d9fb794162369237b46f64bf5b2b9969b0bcd2e'),
('864', '8f97d9164b8fa131f0361abbe49fe706d3abfd77663ed7939ee20d361a0c6a67'),
('703', '769e881d85fc5d27cb4cbc8382200d95b179cfdeb56e0b439da737069eaf8a5a'),
('348', '06b2d82840e43ed8432b3f444de18b57dbe60637c99379c708aa8e66de83dbc1'),
('275', '3a1dfb05d7257530e6349233688c3e121945c5de50f1273a7620537755d61e45'),
('207', '968076be2e38cf897d4d6cea3faca9c037e1a4e3b4b7744fb2533e07751bd30a'),
('575', 'fb84a9739699e1a2c6c56b5baa0a16047a4d845a5c6615ab9e18bafe688f45d6'),
('249', '9f484139a27415ae2e8612bf6c65a8101a18eb5e9b7809e74ca63a45a65f17f4'),
('261', 'e888a676e1926d0c08b5f11fb9116df58b62604b05846f39f8d6fc4dd0ba31f1'),
('230', 'a0eaec5a55dc2f5b2ba523018adc485ff620b9d83509b9f37186a7716e438d21'),
('087', '8ebced264174028f8c5169d8787f7ff1ea2dc6d6c5a7a840b6a2c2454ef88e79'),
('050', '1dd68a2d273df991618f7d4a02d8fe2b79ac131ca6eb0791d5042b99e247918e'),
('214', '802b906a18591ead8a6dd809b262ace4c65c16e89764c40ae326cfcff811e10c'),
('257', '4c970004b0678d439f177e77d3cabdb7e9a44df770948ddc2467cbc76b7211c3'),
('784', 'ff108b68b0e9bc1e5a744f80f9ef1b8575c7d041eeb3e8d2eae300347de6e7fc'),
('546', '6fc8f95bc6465849249d974d53eecc56c00ffda0fc3c7024bfa5b8e4d794b072'),
('663', '4b8ba4b13094beaef100d3eb7d4c8e23600c30be4420c47e0d6b4e88dbd70abb'),
('902', '6a97982dccf77dd3dafa27fcbdf75c017301f730ba186b1d9e8ea212eee73f54'),
('601', '36c1cc2f9d7022bf6beacb6248a89e7e677b3bf9a91e6457a5ffdbade55b76da'),
('946', 'fb335e8fd0f8aed3eb6ffedd7fca08259d3f25bb14066536c978fbc96c4f75fd'),
('816', '96bb293aaa330ef307ee004448b92b75ffdc25ade2831ed23fc60ffa97fffb7f'),
('360', '838f461c2fa673cec73e6eecdafa88b127802d6cb0a61c53175197a122cb645a'),
('758', 'f15223dcc0da90206acdce51c6a9e24938b18665165a819f1abb69233c068cae'),
('724', '68c6c6e9ad314d1a5c4d647cfb6ed84265e47cbc2a05a54fb58ae74c0085ef29'),
('834', '5c344ba7044815dd03c3448028a43e5b9c16074cb5a6a19c7ae86165c149735f'),
('287', 'd7cdaa5ca0582076c8e772cce739e32c5077cfd24f2ea33f04bb754594989a56'),
('345', 'da70dfa4d9f95ac979f921e8e623358236313f334afcd06cddf8a5621cf6a1e9'),
('403', 'd26eae87829adde551bf4b852f9da6b8c3c2db9b65b8b68870632a2db5f53e00'),
('509', 'a05198938c6ca8cd56289c6dba6bb8aaa68dfe8e0d7a37df2fb76e48eeba4244'),
('796', '724213d95916de041564e5d39c2373585dc15855743a42a5841d849b9f3716de'),
('729', '509694b0a010c6431900e71b8210521af57d39ce8e64deb365f0a5c6c9a2ef6d'),
('269', 'f747870ae666c39b589f577856a0f7198b3b81269cb0326de86d8046f2cf72db'),
('435', '5f2703a5211db19a9020f7443f6a440fbc95cda90b7c2d53912f5ce47d050056'),
('187', '38b2d03f3256502b1e9db02b2d12aa27a46033ffe6d8c0ef0f2cf6b1530be9d8'),
('024', '66a3c6db23417d04441d223f67bdf73a5f195a03db472cd55c14388756ceb506'),
('686', '162753c27c8b32975a0edf5e89ab4ed8e2f06f02a182e0f181481cc050fdcc72'),
('084', '26d9f77aa7b5e82108bf19e112939fbd28872a5a870ce63f2299d2ecdbec5a10'),
('484', 'a42e815c58f3977fe531a80ffd4659121c3b9f876a89869042816c369ed80776'),
('194', '7559ca4a957c8c82ba04781cd66a68d6022229fca0e8e88d8e487c96ee4446d0'),
('414', '8111eb1556229541d7d2720a51203037e78ee57fb2e407e0da4a805473dab7af'),
('372', '62f77e7d6197863ac98d9e0cfa76bea0c8e05379ed5281afbe72f7fc206fe37b'),
('009', '5371e1a064f3cfff62decbc181fdfa0fe8e6fc078afad0e36e438a960d38a4e3'),
('199', '5a39cadd1b007093db50744797c7a04a34f73b35ed444704206705b02597d6fd'),
('550', 'f89f8d0e735a91c5269ab08d72fa27670d000e7561698d6e664e7b603f5c4e40'),
('773', 'd15e7843961ed4bfa3e08a80b882c74670e9e9347ea55325cbc1be93c7f54edc'),
('273', '303c8bd55875dda240897db158acf70afe4226f300757f3518b86e6817c00022'),
('396', '3c1b7053f0edd447b778edbc0ad8359b0fa892d69857d9bd5e6b19007bb3f01e'),
('053', '9b23c0760f95b2c9234e032124d75a0ef265a1c5a1543227b60e0e3ee6a8f493'),
('393', '99a0b871c9047c4f5555fcf062e0623174bae38746fece6efdf032d80fb2221a'),
('978', '759b87b87ba0c0c701d14eb2e6e31560929e93591537818856d079634ee1bb68'),
('972', '3658d7fa3c43456f3c9c87db0490e872039516e6375336254560167cc3db2ea2'),
('969', '16b30490a644117a249c2018f31f7d29d2848c8bd43956a895f9bcd649f3ff9c'),
('950', '5538e771949ffec150f6e8260b2e3801236c7373ed62c22a3f82dc0071265cc4'),
('380', '2af4dd48399a5cf64c23fc7933e11aaf6171d80001b4b1377498ae6056b1acbf'),
('572', '5e74cb2ad4e2c9e2d3f59a1e6c8a5d4999df48e5dd69871d2798e0a146b91ee9'),
('444', '3538a1ef2e113da64249eea7bd068b585ec7ce5df73b2d1e319d8c9bf47eb314'),
('164', '3f9807cb9ae9fb6c30942af6139909d27753a5e03fe5a5c6e93b014f5b17366f'),
('473', '3a8f6d79cd434dc10588606993976b7b2bc038ff4a2481e857ac0168fc29a683'),
('116', 'e5b861a6d8a966dfca7e7341cd3eb6be9901688d547a72ebed0b1f5e14f3d08d'),
('781', '28955b1fb53203e2ff246fd2d4c3e148d4666a617469cdcc86060985682ab4bc'),
('131', 'eeca91fd439b6d5e827e8fda7fee35046f2def93508637483f6be8a2df7a4392'),
('873', '46f9d22816179479bd27b0036854788327eedf3f6f5d8dcb866b976e17cc9715'),
('543', '18beb4813723e788a1d79bcbf80802538ec813aa19ded2e9c21cbf08bed6bee3'),
('212', 'fa2b7af0a811b9acde602aacb78e3638e8506dfead5fe6c3425b10b526f94bdd'),
('456', 'b3a8e0e1f9ab1bfe3a36f231f676f78bb30a519d2b21e6c530c0eee8ebb4a5d0'),
('639', '2cfd4b162e427e8e59a2fedf7d5d138eb696d08b98ad9765da0af1690c77b280'),
('654', '92a6a32f99def322d70ea1167a99c6859ab4e8bbc593b997ec5994d244a82475'),
('997', '864995ea35b82212a9a2d456a3f89833f24651c4e5ebc21c18476a9afb065035'),
('708', '1706be6c293444756e72b05e4afa9eb1038e552ac6ce058309451ef7ddad7748'),
('418', '4c8d5b6c695d265fb63dd73f275a21043a5887b37cb4fea0552ecc7b417c8f88'),
('167', '73d3f1ba062585bce51f77d70a26be88c44b55d70f81b8bd7e2ded030ca4454a'),
('365', '4e47eb5525df25f94da777993dafa41d9ab2bfa80a89e28f76d42cd46ab082e7'),
('565', '236b565af6b512826fd89dbbde2e88b94465f780985c134e58b62dea6ee258b2'),
('323', '3949ac1596ec77106a709a618bf5adcb19b77537ce8bcbdf54ff830169cdd084'),
('079', '9c8a49e5cd96b62294f22d7cb42fba85b83c88881b7724694e49697c17ab3b73'),
('562', '4eef24c6b8248c2271f6663f44ec0de3c2535ca396a22cf60051137d71721309'),
('761', 'c78961d3d782d8a85d9344eedae027f43ce6b9fd35c8f355861a39e0d0ddecc5'),
('121', '89aa1e580023722db67646e8149eb246c748e180e34a1cf679ab0b41a416d904'),
('744', 'a15faf6f6c7e4c11d7956175f4a1c01edffff6e114684eee28c255a86a8888f8'),
('336', 'eaa0689a095d4394a05fb51b84b0175a47f68221261377e4829444cbfcae23ca'),
('042', '19158c4a7252e121f7367e7e86a3baabbf93fded76110076cfab2b31eb7df887'),
('979', '8590ac062555493444893ec5871609dffedf8cf684d93f7533bc52ffc5611dc8'),
('780', '0e78437805639c14d6413de94c031fd1babdb561b7728d31ae06bfc5ff1766d4'),
('674', '8ef532f440c91b5dfa24570e53d6bded96c4064a45e6d18a61c5e08b172b9814'),
('382', 'f65ccfbfec288565c1d414275985547799fde0ed286c85a50bd0ec5faa01d1ac'),
('437', '0ef962215cc055786d516355238a80dacc204ecf9b160d0a252190bf5c0cc370'),
('961', 'eef79a6a04f78a54832c8891ed83f4863850ec0a7d9807e9b6ac208d23a1e59c'),
('774', '089ee14b926fabea6dd95890032d1a37e69c1011c710977af774ec3a7b5b39a6'),
('798', '4d5e5deb0353d3a6c0b5cf97de0a23087a56796a3474ee500edbe4676c3b9716'),
('027', '79f918029276a9945ab1751873e7c5fc76ad909ad902b814f5b787271df33b5f'),
('476', 'e73cb135243c08ab2c2adc333b150b9237093315f6b38e3361f07caf2bfb4d6b'),
('181', '580811fa95269f3ecd4f22d176e079d36093573680b6ef66fa341e687a15b5da'),
('378', '21ef779311a43f0e067d0f4f600bb5451a8a7e093662086a1fe6a75d27d7892a'),
('908', '5e61b431f3823da05836b2139f9a811c3cc078153ba1853b44519879b7d64af4'),
('498', 'f138665c5aa6600801452ebb40df70c46e73f2c51f4cb72f66b438139c5ec3f6'),
('352', '9a72c24f2fd76561729110d804c69f38a7088f2ec41fdf8fbfea20d07e8bcff8'),
('115', '28dae7c8bde2f3ca608f86d0e16a214dee74c74bee011cdfdd46bc04b655bc14'),
('540', '84f01dd97c687fb28a296bcc2ef1801446ea7405860595924eb2b5bb634718d1'),
('574', '8e28c5eb829e92abf7a5a921f42364cbb8b255d7c9861a68a3814a9de95d9d67'),
('684', '10ba045e9ee40807e57f6093280b9fa9eaf640ba4955e340ae4c749382ad96fc'),
('219', '314f04b30f62e0056bd059354a5536fb2e302107eed143b5fa2aa0bbba07f608'),
('689', 'fc4fb94d36f45aa9d13358022455e55db4b6f0eb536a1b2897c90dfd3df9eb9b'),
('608', '1de4d95a81eb1780d5c21a880a8be6595306670af426e40872b2a03c5cfb9996'),
('652', '83eaf4dc5e19bcbeb23801e2c3e08c4a89cc82d0a42a903767f9c938d1deac4f'),
('427', '42f25adecf47629878e89e31b2073d1af009c9c76f4140a06313af5e5950eabc'),
('547', 'fadb19bfbddde11ed6828a22e742cc97f5589ce48ac8ec8f94a6510ad5f16b8b'),
('926', '85e36899399df701301f6741ffab57962a14326584b6f082ae0e87d90e492fd4'),
('460', '841a05fd378a2c067058585e3691c2a3f5399206fded7a580fdbbc281003168e'),
('770', 'ca0cec7f60085f0289aaea5cbfbdd84ad2ba05148de121075dab1c636682a566'),
('371', '9b15fed64ef16980f625aeed46ab4cd2c498690551d3a2d1e5254d551d7d6ddf'),
('735', '1a42d5267aba37d7057cadd672fefef04771be2476eeee231d6f56a8e1f57733'),
('390', '48a1a756f2d83f1dc57bbf14052b70a6f40d0fceed6662812e34903a9fe90924'),
('923', 'b5a9ede9a93528be3e12c5665c179c2dc0e2648aa6f1b1650f3715e56dad8bec'),
('301', 'c3ea99f86b2f8a74ef4145bb245155ff5f91cd856f287523481c15a1959d5fd1'),
('224', '84a5092e4a5b6fe968fd523fb2fc917dbffae44105f82b6b94c8ed5b9a800223'),
('599', '182dc6b90f1c9cd913c39a6b5506f582caba9ddeadafe32f5bdbac25efd705ac'),
('699', 'c9a5da075f9e5c3e7a916570946fed4826e181656382e13696fbe0aaf1412bf5'),
('342', '023849c38925e2af028a2eb4e1dc41afd7dc7a238195c1c2ae00438d1dae00e1'),
('586', '219de1387a6743e583e805aad3bf0ffc69dc2107e6d233d43ee8ab62434729e9'),
('113', '6c658ee83fb7e812482494f3e416a876f63f418a0b8a1f5e76d47ee4177035cb'),
('673', 'f4466a4b51d21014b34f621813a1ed75f1c750ec328d908d9edc989c64778962'),
('638', 'f4dd301311d96b70a2ee62a6bccfe21bb0d94a89ca2805333cf352c1a2381c13'),
('197', '8bcbb4c131df56f7c79066016241cc4bdf4e58db55c4f674e88b22365bd2e2ad'),
('057', '0230a7f0e3ccb489d22c25507b3976a65addd02f3e44cd6e8f2b4e46cd7bce8e'),
('385', '131b0c35e2d7edef9dd63f48eff39341ef0a5f770538aa4e0017f41b9cdb135d'),
('858', '8e46760943785a93c7bbfd1b0e733299b05f4d9fe575cf23087da310db486f7a'),
('305', '090d3859ff6840b2280f4708cf08cdaed873d967183a4d1deedc1a7964a21eee'),
('101', '16dc368a89b428b2485484313ba67a3912ca03f2b2b42429174a4f8b3dc84e44'),
('486', '86b700fab5db37977a73700b53a0654b21bdad0896914cc19ad70dee5f5fb3f6'),
('120', '2abaca4911e68fa9bfbf3482ee797fd5b9045b841fdff7253557c5fe15de6477'),
('017', '9309f67fd6480663a48cf90236ec14c1db61583825ac210112e2abf19e47d95c'),
('827', 'ab16ce326c754df41ed00df6f64f7073dcac3e2986bbf8b2a1ce4549b189a0fb'),
('471', '064c3e311ef63912b0cc91db9681ce2d301c3e76c447febf8faa303de38cc005'),
('218', '5966abd0cbfc86f98a186531b2b4ee5f6e910120ce13222f98207203dfc9a9a2'),
('436', '155d1cf609cedded2fbc27a4646de87ce7f7de2913b1e5a1bbf148a6df483e19'),
('730', '61182f39851829ca78c919a83ecbfa045fc0686bff16d0cfa3e643988d9dfecd'),
('534', '5ef6514ed3304cf62b950982541114ac352c52729dbf80747775a9d1a733af93'),
('490', 'cbd02d97b0731d88c78d30c20d90492b2d4c3f2f983931c38fef2dedc7ce48d4'),
('072', 'bc7d001b725557c76b0557936003c32bd3f2fc49f60fb9a29aaa342d25fdce4d'),
('491', '227445a988500528d7826c6921d2e3b4a79ccf3a94cc3bcf7b667e3ae4990b36'),
('944', '6d094db49a6e224278dd87d28835b66af6d2db257522e22c105b829cf368af98'),
('714', 'c66bbe9d118f554bfdba35a609848b9ab2d9c22e6bed77be6f8a55e96c295549'),
('065', 'b64706e7a27319da731ff50ec5312ba6cb194d9f29a85e0f6ad835f15558fe0d'),
('209', '83f814f7a92e365cbd79f9addceed185761a8d38a06a2d4350bb1fe4b7632b34'),
('033', '842d0b270c035fb2f5a9097b78949d9fb38438afae3ccc9c40495e36ba96345f'),
('155', '210e3b160c355818509425b9d9e9fd3ea2e287f2c43a13e5be8817140db0b9e6'),
('118', '85daaf6f7055cd5736287faed9603d712920092c4f8fd0097ec3b650bf27530e'),
('211', '093434a3ee9e0a010bb2c2aae06c2614dd24894062a1caf26718a01e175569b8'),
('619', '86a3f9b13a5b652f93cb17e3f4d212d84cf25c52a595f13fff9f3c5810afff1f'),
('132', 'dbb1ded63bc70732626c5dfe6c7f50ced3d560e970f30b15335ac290358748f6'),
('660', 'fc9e91cc78e1817d80b4ba8c2dc9a638d0c57959825ee34f5e3d7688ad80dfb9'),
('286', '00328ce57bbc14b33bd6695bc8eb32cdf2fb5f3a7d89ec14a42825e15d39df60'),
('288', '23c657f2efda7731a3c1990b25f318fa2eb1332208f97ab9cc2a7eac70ab5a76'),
('144', '5ec1a0c99d428601ce42b407ae9c675e0836a8ba591c8ca6e2a2cf5563d97ff0'),
('508', 'ecac903ea62dc1d5446a88330af0a17ce89c7787e5aaf450113a4a426813e3cc'),
('795', 'c032851ed192d8ac0a3ad04b0ef3060b44d1f6d62f8c17414006702787c5d88b'),
('419', 'cc6aed2709b80e146bebc151f1cf1dec5e323b58148535a433529155030e3a52'),
('529', '8920a14a7f6469b955b114111564cb9736440238d220fc9fd525efdb9a056d3e'),
('607', '67eab6db6703cdf9acf656bbb09640fcde2ff197786adbd9ae9c14936fc8d159'),
('210', 'd29d53701d3c859e29e1b90028eec1ca8e2f29439198b6e036c60951fb458aa1'),
('981', 'aaf5060b9517ba4f550ee34a7f3ed7b05b6e5100a523d3e0aae05bf4f8f7ec34'),
('464', '88b54564b232405ab2165996517fece1149259cf1ea262a375db0f66039294d0'),
('081', '8daea4d2b044cd2b5e634f1ecf141633c74d141aae20fc426945317f4f5f8177'),
('942', '68e1e435db6ab43fd38ae5df6c6a03b50a5c9c6290f4691e1b670a786c0ebe12'),
('804', 'dccb3c52e7c79f7033e1ea06eadf92fd90bfd7b0b5737dc0c2511a0e163872f5'),
('251', 'c75d3f1f5bcd6914d0331ce5ec17c0db8f2070a2d4285f8e3ff11c6ca19168ff'),
('833', '130790feced08212eed7d1490dd4d7abf138543be61a4744a03f69ecb9609764'),
('454', '48f89b630677c2cbb70e2ba05bf7a3633294e368a45bdc2c7df9d832f9e0c941'),
('948', 'b1e92fe0fa7edab2161fa5090a65e065425f6ead93e1152013b1ba77b471494c'),
('874', '3d734d729009b74c011651eb24b06a74151fb99b8da5110295da8bb77ec3f92d'),
('190', '2397346b45823e070f6fc72ac94c0a999d234c472479f0e26b30cdf5942db854'),
('643', '62e66f3e9936906923febd26f9d2536edf38936998c4e5d678b925d848aaa89d'),
('030', '78b4c871b95d95f5e348ae05a194c7c3519520387f9da0d06a5f19ea4df73efb'),
('303', '8bd9c0d453533757387ed019c45617cdc440ba680a67b1a101c85b998ef715c0'),
('609', '1f594da9b409f7f4b9dc5015a81761b2fc2dd60eec773f74539bdfd30c552c89'),
('364', 'b3dfdc6efe322a6feccb0d081e88ffac20b0f28e8495efa76188c8dc3ada6181'),
('971', '98964da49d0a98402ed3d2d37b3350cf0aa346f522f8f1feb6b01cd680bc9455'),
('439', '050a010ce24d0896056e9a36a1940738d38f469d644b3682cfcc47569739c525'),
('899', '91d95f436356bc3df44d44406a139351debd062823258c8cdc67e8dadb9df256'),
('527', 'e1bb74a7794720edf4935a8813538e8113491318168b1fa61a0ac3528e7b0440'),
('510', '5e5c743a015ff8d81e2374d5bca1bdf8ed87ce18484fce8cf4062183dde08493'),
('531', '891d46993a36d78392247c642138cede01d9841daab1d945709755b5194597c4'),
('028', '76a00ed73f38046ec631b719ec46144b6c13f558c884eb5afb38fd5d8aa6e639'),
('602', 'aee4848a8580f31102073d34012cb3700fee3e61f9fcfd725fcfe5fa4a220ec3'),
('706', '35254aa9a21444e50349cebb5465b9b42cb4a625ebcbffe24504b178c35bcb85'),
('049', '3c241d2674142a5f160c29ede81c822e8a834b88c254dff24c0ed4eb313970a7'),
('934', 'a8443b1426652157edc23a7c54fb7ad2c7643b5d8c431ceb54bc29341faf7e7c');


INSERT INTO GD1C2015."NULL".Tarjeta(Tarjeta_Numero, Tarjeta_Numero_Visible, Tarjeta_Fecha_Emision,
  Tarjeta_Fecha_Vencimiento, Tarjeta_Codigo_Seg, Cli_Cod, Emisor_Cod, Tarjeta_Borrado)
SELECT DISTINCT tarjetas_shas.Tarjeta_SHA, SUBSTRING(master.Tarjeta_Numero, 13, 4), 
  CONVERT(DATETIME, master.Tarjeta_Fecha_Emision, 121), 
  CONVERT(DATETIME, master.Tarjeta_Fecha_Vencimiento, 121), cod_shas.Codigo_SHA, cli.Cli_Cod, 
  em.Emisor_Cod, 0
FROM GD1C2015.gd_esquema.Maestra as master, GD1C2015."NULL".Emisor as em, GD1C2015."NULL".Cliente as cli, 
  GD1C2015."NULL".Tarjetas_SHA_Temp as tarjetas_shas, GD1C2015."NULL".Tarjetas_Codigos_SHA_Temp as cod_shas
WHERE master.Tarjeta_Numero = tarjetas_shas.Tarjeta_Numero AND 
  master.Tarjeta_Emisor_Descripcion IS NOT NULL AND master.Tarjeta_Emisor_Descripcion = em.Emisor_Desc AND 
  master.Cli_Tipo_Doc_Cod = cli.TipoDoc_Cod AND master.Cli_Nro_Doc = cli.Cli_Nro_Doc AND 
  cod_shas.Tarjeta_Codigo_Seg = master.Tarjeta_Codigo_Seg;

/******************************************* TIPO CUENTA ***************************************************/

INSERT INTO "NULL".TipoCuenta(TipoCta_Nombre, TipoCta_Costo_Apertura, TipoCta_Duracion, TipoCta_Costo_Dia,
                TipoCta_Costo_Transf, TipoCta_Borrado) VALUES
  ('ORO', 300, 30, 10, 5, 0),
  ('PLATA', 200, 40, 5, 10, 0),
  ('BRONCE', 100, 20, 5, 15, 0),
  ('GRATUITA', 0, 9999, 0, 0, 0); /*Tendríamos que ver si la duración la ponemos así o la ignoramos por código*/


/************************************************ CUENTAS ***************************************************/

IF object_id(N'NULL.TipoCuentaRandom', 'V') IS NOT NULL
  DROP VIEW "NULL".TipoCuentaRandom
GO

CREATE VIEW "NULL".TipoCuentaRandom AS
  SELECT TOP 1 TipoCta_Nombre FROM "NULL".TipoCuenta ORDER BY NEWID()
GO

IF OBJECT_ID(N'NULL.fnGetTipoCuentaRandom') IS NOT NULL
   DROP FUNCTION "NULL".fnGetTipoCuentaRandom
GO

CREATE FUNCTION "NULL".fnGetTipoCuentaRandom()
RETURNS NVARCHAR(255)
AS
  BEGIN
    DECLARE @TipoCta_Nombre NVARCHAR(255);
    SELECT @TipoCta_Nombre = TipoCta_Nombre FROM "NULL".TipoCuentaRandom;
    RETURN @TipoCta_Nombre;
  END
GO

SET IDENTITY_INSERT "NULL".Cuenta ON

INSERT INTO "NULL".Cuenta(Cuenta_Numero, Cuenta_Estado, Cuenta_Fecha_Vencimiento, Cuenta_Fecha_Cierre,
  Cuenta_Fecha_Creacion, Cuenta_Saldo, Pais_Codigo, TipoCta_Nombre, Cli_Cod, Moneda_Nombre, Cuenta_Borrado)
SELECT DISTINCT master.Cuenta_Numero, 'Habilitada' Cuenta_Estado, NULL Cuenta_Fecha_Vencimiento, 
  master.Cuenta_Fecha_Cierre, CONVERT(DATETIME, master.Cuenta_Fecha_Creacion, 121) Cuenta_Fecha_Creacion, 
  0 Cuenta_Saldo, master.Cuenta_Pais_Codigo, NULL, cli.Cli_Cod, 
  'Dólares Estadounidenses' Moneda_Nombre, 0 Cuenta_Borrado
FROM GD1C2015.gd_esquema.Maestra as master, GD1C2015."NULL".Cliente as cli
WHERE master.Cli_Nro_Doc = cli.Cli_Nro_Doc AND master.Cli_Tipo_Doc_Cod = cli.TipoDoc_Cod

SET IDENTITY_INSERT "NULL".Cuenta OFF

UPDATE "NULL".Cuenta
SET TipoCta_Nombre = [NULL].fnGetTipoCuentaRandom()

ALTER TABLE "NULL".Cuenta
  ALTER COLUMN TipoCta_Nombre NVARCHAR(255) NOT NULL
GO

UPDATE "NULL".Cuenta
SET Cuenta_Fecha_Vencimiento = DATEADD(day, tipo.TipoCta_Duracion, cta.Cuenta_Fecha_Creacion)
FROM
  "NULL".Cuenta as cta, "NULL".TipoCuenta as tipo
WHERE
  cta.TipoCta_Nombre = tipo.TipoCta_Nombre


IF object_id(N'NULL.TipoCuentaRandom', 'V') IS NOT NULL
  DROP VIEW "NULL".TipoCuentaRandom
GO

IF OBJECT_ID(N'NULL.fnGetTipoCuentaRandom') IS NOT NULL
   DROP FUNCTION "NULL".fnGetTipoCuentaRandom
GO


/********************************************* DEPOSITOS ********************************************/
IF OBJECT_ID ('NULL.trUpdateSaldoDeposito','TR') IS NOT NULL
   DROP TRIGGER "NULL".trUpdateSaldoDeposito
GO

CREATE TRIGGER "NULL".trUpdateSaldoDeposito ON "NULL".Deposito AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	IF (SELECT COUNT(*) FROM inserted) > 0
	BEGIN
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo + (SELECT SUM(Deposito_Importe) FROM inserted WHERE Cuenta_Numero = c.Cuenta_Numero)
		FROM inserted as depo, "NULL".Cuenta as c
		WHERE depo.Cuenta_Numero = c.Cuenta_Numero
	END

	IF (SELECT COUNT(*) FROM deleted) > 0
	BEGIN
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo - (SELECT SUM(Deposito_Importe) FROM deleted WHERE Cuenta_Numero = c.Cuenta_Numero)
		FROM deleted as depo, "NULL".Cuenta as c
		WHERE depo.Cuenta_Numero = c.Cuenta_Numero
	END
END
GO


SET IDENTITY_INSERT "NULL".Deposito ON

INSERT INTO "NULL".Deposito(Deposito_Codigo, Deposito_Importe, Deposito_Fecha, Tarjeta_Numero, Cuenta_Numero, Deposito_Borrado)
SELECT DISTINCT m.Deposito_Codigo, m.Deposito_Importe, CONVERT(DATETIME, m.Deposito_Fecha, 121), t.Tarjeta_SHA, m.Cuenta_Numero, 0 Deposito_Borrado
FROM GD1C2015.gd_esquema.Maestra as m, "NULL".Tarjetas_SHA_Temp as t
WHERE m.Deposito_Codigo IS NOT NULL AND m.Tarjeta_Numero = t.Tarjeta_Numero

SET IDENTITY_INSERT "NULL".Deposito OFF

IF OBJECT_ID('NULL.Tarjetas_SHA_Temp', 'U') IS NOT NULL
  DROP TABLE "NULL".Tarjetas_SHA_Temp
GO

IF OBJECT_ID('NULL.Tarjetas_Codigos_SHA_Temp', 'U') IS NOT NULL
  DROP TABLE "NULL".Tarjetas_Codigos_SHA_Temp
GO

/******************************************* TRANSFERENCIAS ****************************************/
IF OBJECT_ID ('NULL.trUpdateSaldoTransferencia','TR') IS NOT NULL
   DROP TRIGGER "NULL".trUpdateSaldoTransferencia
GO

CREATE TRIGGER "NULL".trUpdateSaldoTransferencia ON "NULL".Transferencia AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	IF (SELECT COUNT(*) FROM inserted) > 0
	BEGIN
		
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo - (SELECT SUM(Transf_Importe) FROM inserted WHERE Cuenta_Origen_Numero = c.Cuenta_Numero)
		FROM inserted as trans, "NULL".Cuenta as c
		WHERE trans.Cuenta_Origen_Numero = c.Cuenta_Numero
		
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo + (SELECT SUM(Transf_Importe) FROM inserted WHERE Cuenta_Destino_Numero = c.Cuenta_Numero)
		FROM inserted as trans, "NULL".Cuenta as c
		WHERE trans.Cuenta_Destino_Numero = c.Cuenta_Numero
	END

	IF (SELECT COUNT(*) FROM deleted) > 0
	BEGIN
		
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo + (SELECT SUM(Transf_Importe) FROM deleted WHERE Cuenta_Origen_Numero = c.Cuenta_Numero)
		FROM deleted as trans, "NULL".Cuenta as c
		WHERE trans.Cuenta_Origen_Numero = c.Cuenta_Numero
		
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo - (SELECT SUM(Transf_Importe) FROM deleted WHERE Cuenta_Destino_Numero = c.Cuenta_Numero)
		FROM deleted as trans, "NULL".Cuenta as c
		WHERE trans.Cuenta_Destino_Numero = c.Cuenta_Numero
		
	END
END
GO


INSERT INTO "NULL".Transferencia(Transf_Fecha, Transf_Importe, Transf_Costo, Cuenta_Origen_Numero,
								Cuenta_Destino_Numero, Transf_Borrado)
SELECT DISTINCT CONVERT(DATETIME, Transf_Fecha, 121), Trans_Importe, Trans_Costo_Trans, Cuenta_Numero, Cuenta_Dest_Numero, 0
FROM GD1C2015.gd_esquema.Maestra WHERE Transf_Fecha IS NOT NULL


/*************************************** CHEQUES Y RETIROS *****************************************/
IF OBJECT_ID ('NULL.trUpdateSaldoRetiro','TR') IS NOT NULL
   DROP TRIGGER "NULL".trUpdateSaldoRetiro
GO

CREATE TRIGGER "NULL".trUpdateSaldoRetiro ON "NULL".Retiro AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	IF (SELECT COUNT(*) FROM inserted) > 0
	BEGIN
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo - (SELECT SUM(Retiro_Importe) FROM inserted WHERE Cuenta_Numero = c.Cuenta_Numero)
		FROM inserted as retiro, "NULL".Cuenta as c
		WHERE retiro.Cuenta_Numero = c.Cuenta_Numero
	END

	IF (SELECT COUNT(*) FROM deleted) > 0
	BEGIN
		UPDATE "NULL".Cuenta
		SET Cuenta_Saldo = c.Cuenta_Saldo + (SELECT SUM(Retiro_Importe) FROM deleted WHERE Cuenta_Numero = c.Cuenta_Numero)
		FROM deleted as retiro, "NULL".Cuenta as c
		WHERE retiro.Cuenta_Numero = c.Cuenta_Numero
	END
END
GO


SET IDENTITY_INSERT "NULL".Cheque ON

INSERT INTO "NULL".Cheque(Cheque_Nombre, Cheque_Numero, Cheque_Fecha, Cheque_Importe, Banco_Codigo, Moneda_Nombre, Cheque_Borrado)
SELECT DISTINCT Cli_Nombre + ' ' + Cli_Apellido, Cheque_Numero, CONVERT(DATETIME, Cheque_Fecha, 121), Cheque_Importe, Banco_Cogido, 'Dólares Estadounidenses', 0
FROM GD1C2015.gd_esquema.Maestra WHERE Cheque_Numero IS NOT NULL

SET IDENTITY_INSERT "NULL".Cheque OFF


SET IDENTITY_INSERT "NULL".Retiro ON

INSERT INTO "NULL".Retiro(Retiro_Codigo, Retiro_Fecha, Retiro_Importe, Cuenta_Numero, Cheque_Numero, Retiro_Borrado)
SELECT DISTINCT Retiro_Codigo, CONVERT(DATETIME, Retiro_Fecha, 121), Retiro_Importe, Cuenta_Numero, Cheque_Numero, 0
FROM GD1C2015.gd_esquema.Maestra WHERE Retiro_Codigo IS NOT NULL

SET IDENTITY_INSERT "NULL".Retiro OFF


/*********************************** FACTURAS Y TRANSACCIONES **************************/


SET IDENTITY_INSERT "NULL".Factura_Cabecera ON

INSERT INTO "NULL".Factura_Cabecera(Fact_Numero, Fact_Tipo,Fact_Fecha, Fact_Total, Cli_Cod, Fact_Borrado)
SELECT DISTINCT Factura_Numero, 'A', CONVERT(DATETIME, Factura_Fecha, 121), 0, Cli_Cod, 0
FROM GD1C2015.gd_esquema.Maestra as m, "NULL".Cliente as cli
WHERE m.Cli_Nombre = cli.Cli_Nombre AND m.Cli_Apellido = cli.Cli_Apellido AND m.Factura_Numero IS NOT NULL

SET IDENTITY_INSERT "NULL".Factura_Cabecera OFF


IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spMigrarFacturas' 
)
   DROP PROCEDURE "NULL".spMigrarFacturas
GO

CREATE PROCEDURE "NULL".spMigrarFacturas
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Factura_Numero NUMERIC(18,0),
			@Factura_Fecha DATETIME,
			@Cli_Cod NUMERIC(18,0),
			@Importe NUMERIC(18,2),
			@Descripcion NVARCHAR(255),
			@InsertedTransaccion NUMERIC(18,0)
	
	DECLARE facturas CURSOR FOR
		SELECT Item_Factura_Importe,Item_Factura_Descr,Cli_Cod,Factura_Numero
		FROM GD1C2015.gd_esquema.Maestra as m, "NULL".Cliente as cli
		WHERE m.Cli_Nombre = cli.Cli_Nombre AND m.Cli_Apellido = cli.Cli_Apellido AND m.Item_Factura_Importe IS NOT NULL
	
	OPEN facturas
	FETCH NEXT FROM facturas INTO @Importe, @Descripcion, @Cli_Cod, @Factura_Numero
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		INSERT INTO "NULL".Transaccion(Cli_Cod, Moneda_Nombre, Transacc_Cantidad, Transacc_Detalle, Transacc_Facturada, Transacc_Importe, Transacc_Borrado) VALUES
			(@Cli_Cod, 'Dólares Estadounidenses', 1, @Descripcion, 1, @Importe, 0);
		
		SELECT @InsertedTransaccion = SCOPE_IDENTITY()
		
		INSERT INTO "NULL".Factura_Item(F_Item_Cantidad,F_Item_Desc,F_Item_Precio_Unitario,Fact_Numero,Fact_Tipo,Moneda_Nombre,Transacc_Codigo,F_Item_Borrado) VALUES
			(1, @Descripcion, @Importe, @Factura_Numero, 'A', 'Dólares Estadounidenses',@InsertedTransaccion, 0);
		
		FETCH NEXT FROM facturas INTO @Importe, @Descripcion, @Cli_Cod, @Factura_Numero
	END
	CLOSE facturas
	DEALLOCATE facturas
END
GO

EXEC [NULL].[spMigrarFacturas]
GO
