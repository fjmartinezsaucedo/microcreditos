--Creaci�n de la base de datos.

Create database microcreditos

--Elecci�n de la previamente creada.

use microcreditos

--Creaci�n de la tabla prestamos.

Create table prestamos (
	Id int identity primary key,
	Nombre varchar(150) not null,
	Apaterno varchar(50) not null,
	Amaterno varchar(50) not null,
	Cantidad decimal(6,2) not null,
	Telefono varchar(32),
	Email varchar(200) not null,
	Fecha varchar(25),
	Dia smallint,
	Meses int,
	Intereses smallint,
	Id_deudores int,
	Id_historial int,	
	--Llaves foraneas para relacionar las tablas de informaci�n.

	Constraint fk_deudores Foreign key (Id_deudores) references deudores (Id),
	Constraint fk_historial Foreign key (Id_historial) references historial (Id)

	--Llaves foraneas para relacionar las tablas de informaci�n.
)

--Creaci�n de la tabla deudores.

Create table deudores(
	Id int identity primary key,
	Nombre varchar(150) not null,
	Email varchar(200),
	MontoD decimal(6,2) not null,
	MontoP decimal(6,2) not null,
	MontoF decimal(6,2) not null,

)

--Creaci�n de la tabla historial.

Create table historial(
	Id int identity primary key,
	Nombre varchar(150) not null,
	Periodo int not null,
	Fecha varchar(25),
	Estatus varchar(10)
)