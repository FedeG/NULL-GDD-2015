IF OBJECT_ID (N'NULL.spCrearCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spCrearCuenta
GO

CREATE PROCEDURE "NULL".spCrearCuenta

  @Cuenta_Numero numeric(18,0),
  @Cuenta_Fecha_Creacion DATETIME,
  @Pais_Codigo varchar(255),
  @TipoCta_Nombre varchar(255),
  @Cli_Cod varchar(255),
  @Moneda_Nombre varchar(255)

AS
BEGIN
  SET NOCOUNT ON;
  SET IDENTITY_INSERT [GD1C2015]."NULL".Cuenta ON

  INSERT INTO [GD1C2015].[NULL].[Cuenta] (Cuenta_Numero, Cuenta_Estado, Cuenta_Fecha_Vencimiento, Cuenta_Fecha_Cierre, Cuenta_Fecha_Creacion, Cuenta_Saldo, Pais_Codigo, TipoCta_Nombre, Cli_Cod, Moneda_Nombre, Cuenta_Borrado)
  VALUES (@Cuenta_Numero, 'Pendiente de Activación', NULL, NULL, @Cuenta_Fecha_Creacion, 0, @Pais_Codigo, @TipoCta_Nombre, @Cli_Cod, @Moneda_Nombre, 0)

  SET IDENTITY_INSERT "NULL".Cuenta OFF
END
GO

IF OBJECT_ID (N'NULL.spEditarCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spEditarCuenta
GO

CREATE PROCEDURE "NULL".spEditarCuenta

  @Cuenta_Numero numeric(18,0),
  @Cuenta_Fecha_Creacion DATETIME,
  @Pais_Codigo varchar(255),
  @TipoCta_Nombre varchar(255),
  @Cli_Cod varchar(255),
  @Moneda_Nombre varchar(255)

AS
BEGIN
	SET NOCOUNT ON;

  UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Pais_Codigo = @Pais_Codigo, TipoCta_Nombre = @TipoCta_Nombre, Moneda_Nombre = @Moneda_Nombre
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF OBJECT_ID (N'NULL.spHabilitarCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spHabilitarCuenta
GO

CREATE PROCEDURE "NULL".spHabilitarCuenta
	@Cuenta_Numero varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Estado = 'Habilitada'
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF OBJECT_ID (N'NULL.spDeshabilitarCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spDeshabilitarCuenta
GO

CREATE PROCEDURE "NULL".spDeshabilitarCuenta
	@Cuenta_Numero varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Estado = 'Deshabilitada'
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF OBJECT_ID (N'NULL.spDarDeBajaCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spDarDeBajaCuenta
GO

CREATE PROCEDURE "NULL".spDarDeBajaCuenta
	@Cuenta_Numero varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Borrado = 1
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO
IF OBJECT_ID (N'NULL.spCrearCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spCrearCuenta
GO

CREATE PROCEDURE "NULL".spCrearCuenta

  @Cuenta_Numero numeric(18,0),
  @Cuenta_Fecha_Creacion DATETIME,
  @Pais_Codigo varchar(255),
  @TipoCta_Nombre varchar(255),
  @Cli_Cod varchar(255),
  @Moneda_Nombre varchar(255)

AS
BEGIN
	SET NOCOUNT ON;

  INSERT INTO [GD1C2015].[NULL].[Cuenta] (Cuenta_Numero, Cuenta_Estado, Cuenta_Fecha_Vencimiento, Cuenta_Fecha_Cierre, Cuenta_Fecha_Creacion, Cuenta_Saldo, Pais_Codigo, TipoCta_Nombre, Cli_Cod, Moneda_Nombre, Cuenta_Borrado)
	VALUES (@Cuenta_Numero, 'Pendiente de Activación', NULL, NULL, @Cuenta_Fecha_Creacion, 0, @Pais_Codigo, @TipoCta_Nombre, @Cli_Cod, @Moneda_Nombre, 0)

END
GO

IF OBJECT_ID (N'NULL.spEditarCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spEditarCuenta
GO

CREATE PROCEDURE "NULL".spEditarCuenta

  @Cuenta_Numero numeric(18,0),
  @Cuenta_Fecha_Creacion DATETIME,
  @Pais_Codigo varchar(255),
  @TipoCta_Nombre varchar(255),
  @Cli_Cod varchar(255),
  @Moneda_Nombre varchar(255)

AS
BEGIN
	SET NOCOUNT ON;

  UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Pais_Codigo = @Pais_Codigo, TipoCta_Nombre = @TipoCta_Nombre, Moneda_Nombre = @Moneda_Nombre
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF OBJECT_ID (N'NULL.spHabilitarCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spHabilitarCuenta
GO

CREATE PROCEDURE "NULL".spHabilitarCuenta
	@Cuenta_Numero varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Estado = 'Habilitada'
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF OBJECT_ID (N'NULL.spDeshabilitarCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spDeshabilitarCuenta
GO

CREATE PROCEDURE "NULL".spDeshabilitarCuenta
	@Cuenta_Numero varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Estado = 'Inhabilitada'
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF OBJECT_ID (N'NULL.spDarDeBajaCuenta') IS NOT NULL
   DROP PROCEDURE "NULL".spDarDeBajaCuenta
GO

CREATE PROCEDURE "NULL".spDarDeBajaCuenta
	@Cuenta_Numero varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET Cuenta_Borrado = 1
	WHERE Cuenta_Numero = @Cuenta_Numero

END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spConsultaCambioTipoCuenta' 
)
   DROP PROCEDURE "NULL".spConsultaCambioTipoCuenta
GO

CREATE PROCEDURE "NULL".spConsultaCambioTipoCuenta
  @Cuenta_Numero Numeric(18,0),
  @Hoy DATETIME		
AS
BEGIN
	IF(SELECT COUNT(*) FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Numero AND Cuenta_Estado = 'Habilitada') = 0
	BEGIN
		RETURN(-1)
	END
	
	DECLARE @Importe INT  = (SELECT TOP 1 t.TipoCta_Costo_Dia 
	FROM [GD1C2015].[NULL].[Cuenta] as c, [GD1C2015].[NULL].[TipoCuenta] as t
	WHERE c.Cuenta_Numero = @Cuenta_Numero AND t.TipoCta_Nombre = c.TipoCta_Nombre)
	DECLARE @Fecha_Vencimiento DATETIME = (SELECT TOP 1 Cuenta_Fecha_Vencimiento 
	FROM [GD1C2015].[NULL].[Cuenta] 
	WHERE Cuenta_Numero = @Cuenta_Numero) 
	RETURN(@Importe *  DATEDIFF(DAY, @Hoy, @Fecha_Vencimiento))
END
GO

IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_NAME = N'spCambiarTipoCuenta' 
)
   DROP PROCEDURE "NULL".spCambiarTipoCuenta
GO

CREATE PROCEDURE "NULL".spCambiarTipoCuenta
  @Cuenta_Numero Numeric(18,0),
  @TipoCta_Nombre NVARCHAR(255),
  @Hoy DATETIME		
AS
BEGIN
	IF(SELECT COUNT(Cuenta_Numero) FROM [GD1C2015].[NULL].[Cuenta] WHERE Cuenta_Numero = @Cuenta_Numero AND Cuenta_Estado = 'Habilitada') = 0
	BEGIN
		RETURN(-1)
	END
	
	DECLARE @Importe INT  = (SELECT TOP 1 t.TipoCta_Costo_Dia 
	FROM [GD1C2015].[NULL].[Cuenta] as c, [GD1C2015].[NULL].[TipoCuenta] as t
	WHERE c.Cuenta_Numero = @Cuenta_Numero AND t.TipoCta_Nombre = c.TipoCta_Nombre)
	
	DECLARE @Fecha_Vencimiento DATETIME = (SELECT TOP 1 Cuenta_Fecha_Vencimiento 
	FROM [GD1C2015].[NULL].[Cuenta] 
	WHERE Cuenta_Numero = @Cuenta_Numero)
	
	SET @Importe = (@Importe *  DATEDIFF(DAY, @Hoy, @Fecha_Vencimiento))
	
	UPDATE [GD1C2015].[NULL].[Cuenta]
	SET TipoCta_Nombre = @TipoCta_Nombre, Cuenta_Saldo = Cuenta_Saldo + @Importe, Cuenta_Fecha_Vencimiento = @Hoy
	WHERE Cuenta_Numero = @Cuenta_Numero

	/*Generar factura*/	
END
GO
