--Devuelve los tipo de documento y descripcion
SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc 
FROM GD1C2015.gd_esquema.Maestra
WHERE Cli_Tipo_Doc_Cod IS NOT NULL

--Devuelve los bancos
SELECT DISTINCT Banco_Cogido, Banco_Direccion, Banco_Nombre
FROM GD1C2015.gd_esquema.Maestra
WHERE Banco_Cogido IS NOT NULL

--Devuelve los paises
SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc 
FROM GD1C2015.gd_esquema.Maestra
WHERE Cli_Pais_Codigo IS NOT NULL

--Devuelve los emisores de tarjeta
SELECT DISTINCT Tarjeta_Emisor_Descripcion
FROM GD1C2015.gd_esquema.Maestra
WHERE Tarjeta_Emisor_Descripcion IS NOT NULL

--Devuelve los clientes del sistema
SELECT DISTINCT Cli_Nombre, Cli_Apellido, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto, Cli_Fecha_Nac, Cli_Mail, Cli_Tipo_Doc_Cod, Cli_Nro_Doc, Cli_Pais_Codigo
FROM GD1C2015.gd_esquema.Maestra
WHERE Cli_Nombre IS NOT NULL

--Devuelve las tarjetas. Necesita tener cargados los clientes y emisores
SELECT DISTINCT master.Tarjeta_Numero, master.Tarjeta_Fecha_Emision, master.Tarjeta_Fecha_Vencimiento, master.Tarjeta_Codigo_Seg, em.Emisor_Cod, cli.Cli_Cod
FROM GD1C2015.gd_esquema.Maestra as master, GD1C2015.gd_esquema.Emisor as em, GD1C2015.gd_esquema.Cliente as cli
WHERE master.Tarjeta_Emisor_Descripcion IS NOT NULL AND master.Tarjeta_Emisor_Descripcion = em.Emisor_Desc AND master.Cli_Tipo_Doc_Cod = cli.Tipo_Doc_Cod AND master.Cli_Nro_Doc AND cli.Cli_Nro_Doc

--Devuelve las cuentas en el sistema. Necesita tener cargados los clientes.
SELECT DISTINCT master.Cuenta_Numero, master.Cuenta_Estado, master.Cuenta_Fecha_Cierre, master.Cuenta_Fecha_Creacion, master.Cuenta_Pais_Codigo, cli.Cli_Cod
FROM GD1C2015.gd_esquema.Maestra as master, GD1C2015.gd_esquema.Cliente as cli
WHERE master.Cli_Dom_Nro = cli.Cli_Domm_Nro AND master.Cli_Tipo_Doc_Cod = cli.Cli_Tipo_Doc_Cod

--Devuelve los depositos.
SELECT DISTINCT Deposito_Codigo, Deposito_Importe, Deposito_Fecha, Tarjeta_Numero, Cuenta_Numero
FROM GD1C2015.gd_esquema.Maestra
WHERE Deposito_Codigo IS NOT NULL AND Tarjeta_Numero IS NOT NULL AND Cuenta_Numero IS NOT NULL

--Devuelve las transferencias
SELECT DISTINCT Transf_Fecha, Trans_Importe, Trans_Costo_Trans, Cuenta_Numero, Cuenta_Dest_Numero
FROM GD1C2015.gd_esquema.Maestra 

--Devuelve los cheques
SELECT DISTINCT Cheque_Numero, Cheque_Fecha, Cheque_Importe, Banco_Cogido
FROM GD1C2015.gd_esquema.Maestra

--Devuelve los retiros.
SELECT DISTINCT Retiro_Codigo, Retiro_Importe, Retiro_Fecha, Cuenta_Numero, Cheque_Numero 
FROM GD1C2015.gd_esquema.Maestra
