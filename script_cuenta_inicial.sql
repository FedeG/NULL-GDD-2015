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
