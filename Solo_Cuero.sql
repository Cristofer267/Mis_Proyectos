CREATE DATABASE Solo_Cueros
GO 

USE Solo_Cueros
GO

CREATE TABLE Users (
UserName VARCHAR (50) not null,
Password VARCHAR (50) not null
)
GO

CREATE TABLE UsersLogin (
	UserName VARCHAR (50) not null,
	Password VARCHAR (50) not null
)
GO

CREATE TABLE CategoryP (
	ID INT PRIMARY KEY IDENTITY (1,1),
	NameP VARCHAR (50),
)
GO

CREATE TABLE Products (
ID INT IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR (100),
Descripcion NVARCHAR(100),
Stock INT CHECK (Stock>=0),
Categoria NVARCHAR(100),
Precio FLOAT,
Size NVARCHAR (20),
Color NVARCHAR (50),

)
GO

CREATE TABLE Customer (
	ID INT PRIMARY KEY IDENTITY (1,1),
	NameC VARCHAR (50),
	Sex CHAR (1)
)
GO

CREATE TABLE Bill
(
	ID INT PRIMARY KEY IDENTITY,
	CustomerID VARCHAR (50) NOT NULL, 
	Fecha DATETIME DEFAULT GETDATE(),
	Total FLOAT,
)
GO

CREATE TABLE Sales_detail
(
	 ID INT PRIMARY KEY IDENTITY (1,1),
	 BillID INT NOT NULL,
	 ProducID NVARCHAR (50)  NOT NULL,
	 Amount INT NOT NULL,
	 Price FLOAT NOT NULL,
 )
 GO
----------------------------------------------------------------------------------------------------

insert into Users values ( 'Admin','Admin1234')
insert into Users values ( 'Josue.Calero','1234')
insert into Users values ( 'Test','1234')
GO

INSERT INTO CategoryP VALUES ('Carteras y Bolsos'), ('Calzado')
GO

insert into Bill values ('Jose',CURRENT_TIMESTAMP,5200)
GO

insert into Products Values ('Zapatilla','con escamas','12', 'alta',120, 'XXL','Negro')
GO

INSERT INTO Products
VALUES 
('CARTERA DE CUERO 100% ','','25','1','1200','0','NEGRO'),
('CARTERA DE CUERO 75%','','55','1','850','0','NEGRO'),
('CARTERA DE CUERO 50%','','32','1','640','0','NEGRO'),
('ZAPATOS DOWNTOWN','','58','2','2500','45','NEGRO'),
('BOTÍN NEW LOOK','','56','2','1850','49','NEGRO'),
('ZAPATOS DE VESTIR EN CUERO','','2','2','1750','48','NEGRO'),
('ZAPATOS DE VESTIR ESTILO DERBY ','','32','2','1900','40','NEGRO'),
('ZAPATOS DE ESTILO CASUAL','','48','2','1650','42','NEGRO'),
('BOTÍN MULTITEXTURA','','53','2','2100','42','NEGRO'),
('SNEAKERS EN CUERO','','10','2','2200','44','NEGRO'),
('PORTA LENTES DE CUERO','','55','1','450','0','NEGRO'),
('BILLETERA DE CUERO','','55','1','800','0','NEGRO')

----------------Procedimientos de almacenado -------------------------------------------------------------

CREATE PROC sp_MostraVentas
AS 
SELECT ProducID AS "Producto", price AS "Precio", Amount AS "Cantidad", BillID AS "N° Factura" FROM Sales_detail
GO


	CREATE PROC sp_MostrarProductos
	AS 
	SELECT * FROM Products ORDER BY Stock DESC
	GO

--EXEC sp_MostrarProductos

CREATE PROC sp_InsertProducts

@Nombre NVARCHAR (100),
@Descripcion NVARCHAR(100),
@Stock INT,
@Categoria NVARCHAR(100),
@Precio FLOAT,
@Size NVARCHAR (20),
@Color NVARCHAR (50)
AS
INSERT INTO Products VALUES 
(@Nombre,@Descripcion,@Stock,@Categoria,@Precio,@Size,@Color)
GO

--exec sp_MostrarProductos
--exec sp_InsertProducts  'Mochila','','12', 'alta',120, 'Grande','Negro'

CREATE PROC SP_Sales_detail
	@Producto NVARCHAR (50),
	@Amount INT ,
	@Price FLOAT
AS
BEGIN 
	DECLARE @num_fac INT  = (SELECT MAX(ID) AS ID FROM Bill)
	INSERT INTO Sales_detail VALUES
	(@num_fac,@Producto,@Amount,@Price)
END

--EXECUTE  SP_Insertar_Factura


CREATE PROC sp_EditarProducts
@Id INT,
@Nombre NVARCHAR(100),
@Descripcion NVARCHAR(100),
@Stock INT,
@Categoria NVARCHAR(100),
@Precio FLOAT,
@Size NVARCHAR (20),
@Color NVARCHAR (50)
AS
UPDATE Products 
SET Nombre = @Nombre,
	Descripcion = @Descripcion,
	Stock = @Stock,
	Categoria = @Categoria,
	Precio = @Precio,
	Size = @Size,
	Color = @Color
WHERE Id =@Id
GO

CREATE PROC sp_EliminarProducts
@Id INT 
AS
DELETE FROM Products 
WHERE Id =@Id
GO

--exec SP_Insertar_Factura
CREATE PROC sp_AlterarProducts
@Id INT,
@Stock INT
AS
UPDATE Products
SET
Stock = Stock - @Stock
WHERE Id =@Id
GO

CREATE PROC sp_ValidaProducts
@Id INT,
@Stock INT
AS
select Stock from Products
where id = @Id and Stock >= @stock
GO

CREATE PROC sp_CrearFactura

	@CustomerID VARCHAR (50),
	@Total FLOAT
AS
INSERT INTO Bill VALUES 
(@CustomerID,GETDATE(),@Total)
GO

CREATE PROC sp_UltimaFactura

AS
SELECT MAX(ID) AS ID FROM Bill
GO
