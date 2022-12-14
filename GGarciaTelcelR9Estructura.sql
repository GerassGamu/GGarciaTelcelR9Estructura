CREATE DATABASE GGarciaEstructura

USE GGarciaEstructura



CREATE TABLE Departamento
(

DepartamentoID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Descripcion VARCHAR(100)
);
INSERT INTO Departamento VALUES
('Soporte Técnico'),
('Administración'),
('Compras'),
('Ventas'),
('Recursos Humanos')


Select* from Departamento



CREATE TABLE Puesto 
(
PuestoID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Descripcion VARCHAR(100)

);



INSERT INTO Puesto VALUES
('Gerente'),
('Jefe'),
('Supervisor'),
('Analista'),
('Secretaría')

SELECT*FROM Puesto

CREATE TABLE Empleado
(

EmpleadoID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
Nombre VARCHAR(200) NOT NULL,

DepartamentoID INT,
CONSTRAINT FK_EmpleadoDepartamento FOREIGN KEY (DepartamentoID)
    REFERENCES Departamento(DepartamentoID),

PuestoID INT,
CONSTRAINT FK_EmpleadoPuesto FOREIGN KEY (PuestoID)
    REFERENCES Puesto(PuestoID)



)

INSERT INTO Empleado VALUES
('Alfredo Pérez Torres',1,2),
('Fernando Gonzalez Jiménez',1,2),
('Luis Martinez Pérez',1,3),
('José Robles Torres',2,2),
('Karla Garcia Higareda',2,3),
('Lorena Duran Castro',2,4),
('Jessica Ramírez Estrada',3,3),
('Jimena Zarate Vélez',3,4),
('Daniel Garcia Buendía',3,5),
('Rubén Núñez López',4,1),
('Jorge Franco Quiroz',4,3),
('Rubén Núñez López',4,1),
('Gloria Velazquez Pérez',4,5),
('Wendy Salgado Olguín',5,2),
('Arturo Frías Robles',5,4),
('Francisco Torres Duran',5,1)

SELECT*FROM Empleado

/*
5.	Crear una vista que permita unir las tablas Empleado, Departamento y Puesto que devuelva los siguientes campos
a.	EmpleadoID
b.	Nombre
c.	PuestoID
d.	DescripcionPuesto
e.	DepartamentoID
f.	DescripcionDepartamento
*/
SELECT 
	[EmpleadoID],
    [Nombre],
    [Empleado].[PuestoID],
	[Puesto].[Descripcion] AS DescripcionPuesto,
	[Empleado].[DepartamentoID],
	[Departamento].[Descripcion] AS DescripcionDepartamento

  FROM [Empleado]

INNER JOIN Puesto 
ON Empleado.PuestoID=Puesto.PuestoID
INNER JOIN Departamento 
ON Empleado.DepartamentoID=Departamento.DepartamentoID

/*
6.	Crear los stored procedure que cumplan con las siguientes funciones
a.	Permitir consultar todos los empleados con sus puestos y departamentos
b.	Permitir consultar todos los puestos
c.	Permitir consultar todos los departamentos
d.	 Permitir insertar empleados recibiendo como parámetros 
	i.	Nombre
	ii.	PuestoID
	iii.	DepartamentoID
e.	Permitir eliminar empleados por medio del ID

f.	Consultar empleados por coincidencia del nombre

*/
--Creacion Store--
CREATE PROCEDURE EmpleadoGetAll
AS
SELECT 
	[EmpleadoID],
    [Nombre],
    [Empleado].[PuestoID],
	[Puesto].[Descripcion] AS DescripcionPuesto,
	[Empleado].[DepartamentoID],
	[Departamento].[Descripcion] AS DescripcionDepartamento

  FROM [Empleado]

INNER JOIN Puesto 
ON Empleado.PuestoID=Puesto.PuestoID
INNER JOIN Departamento 
ON Empleado.DepartamentoID=Departamento.DepartamentoID
GO

---Llamar a SP--
EmpleadoGetAll

CREATE PROCEDURE EmpleadoGetById 
@EmpleadoID INT
AS
SELECT 
	[EmpleadoID],
    [Nombre],
    [Empleado].[PuestoID],
	[Puesto].[Descripcion] AS DescripcionPuesto,
	[Empleado].[DepartamentoID],
	[Departamento].[Descripcion] AS DescripcionDepartamento

  FROM [Empleado]

INNER JOIN Puesto 
ON Empleado.PuestoID=Puesto.PuestoID
INNER JOIN Departamento 
ON Empleado.DepartamentoID=Departamento.DepartamentoID

WHERE EmpleadoID=@EmpleadoID
GO


---Llamar a SP--
EmpleadoGetById 1

CREATE PROCEDURE PuestoGetAll
AS
SELECT
[PuestoID]
,[Descripcion]
  FROM [Puesto]
GO

--Llamado a SP---
PuestoGetAll

CREATE PROCEDURE DepartamentoGetAll
AS
SELECT 
[DepartamentoID]
,[Descripcion]

  FROM [Departamento]
GO

---Llamado a SP---
DepartamentoGetAll



CREATE PROCEDURE EmpleadoAdd
@Nombre VARCHAR(200),
@DepartamentoID INT,
@PuestoID INT
AS
INSERT INTO Empleado (Nombre,DepartamentoID,PuestoID) VALUES (@Nombre,@DepartamentoID,@PuestoID)
GO

----Llamado a SP---
EmpleadoAdd 'Carlos Rivera Lara',1,1
EmpleadoGetAll


CREATE PROCEDURE EmpleadoUpdate
@EmpleadoID INT,
@Nombre VARCHAR(200),
@DepartamentoID INT,
@PuestoID INT
AS
UPDATE Empleado SET Nombre=@Nombre,DepartamentoID=@DepartamentoID,PuestoID=@PuestoID

WHERE EmpleadoID=@EmpleadoID
GO
---Llamado a SP---
EmpleadoUpdate 17,'Carlos Luna Lara',1,1
EmpleadoGetAll


CREATE PROCEDURE EmpleadoDelete
@EmpleadoID INT
AS
DELETE FROM Empleado
WHERE EmpleadoID=@EmpleadoID
GO


---Llamado a SP---
EmpleadoDelete 17
EmpleadoGetAll