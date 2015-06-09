IF OBJECT_ID (N'NULL.spCrearCliente') IS NOT NULL
   DROP FUNCTION "NULL".spCrearCliente
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
	VALUES (@Usr_Username, @Usr_Password, GETDATE(), GETDATE(), @Usr_Pregunta_Secreta, @Usr_Respuesta_Secreta, 0, 0, "Habilitado")

	INSERT INTO [GD1C2015].[NULL].[Cliente] (Usr_Username, Cli_Nombre, Cli_Apellido, TipoDoc_Cod, Cli_Nro_Doc, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto, Cli_Localidad, Cli_Fecha_Nac, Cli_Mail, Cli_Nacionalidad, Pais_Codigo, Cli_Borrado)
	VALUES (@Usr_Username, @Cli_Nombre, @Cli_Apellido, @TipoDoc_Cod, @Cli_Nro_Doc, @Cli_Dom_Calle, @Cli_Dom_Nro, @Cli_Dom_Piso, @Cli_Dom_Depto, @Cli_Localidad, @Cli_Fecha_Nac, @Cli_Mail, @Cli_Nacionalidad, @Pais_Codigo, 0)

END
GO

IF OBJECT_ID (N'NULL.spEditarCliente') IS NOT NULL
   DROP FUNCTION "NULL".spEditarCliente
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
   DROP FUNCTION "NULL".spHabilitarUsuario
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

IF OBJECT_ID (N'NULL.spDarDeBajaCliente') IS NOT NULL
   DROP FUNCTION "NULL".spDarDeBajaCliente
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
