create database prueba

use prueba

create table clientes(
idcliente int identity(1,1) primary key,
nombre nvarchar(150),
apellidop nvarchar(50),
apellidom nvarchar(50),
cantidadp decimal(6,2),
telefono nvarchar(32),
email nvarchar(200),
fechap datetime,
diacobro smallint,
mesesprestamo int,
intereses smallint,
montodebe as ((cantidadp/mesesprestamo)+((cantidadp*intereses)/100))*mesesprestamo,
montopagado as (0),
montofinal as ((cantidadp/mesesprestamo)+((cantidadp*intereses)/100))*mesesprestamo
)
drop database prueba
drop trigger TR_clientes_insert
drop table clientes
drop table deudores
select * from clientes
select * from deudores
insert into clientes values('prueba','prueba','prueba',1000,'4491736813','eduardoavalos399@gmail.com','2023-03-27 23:18:00.000',5,10,5)

create table deudores(
iddeudor int not null identity primary key,
idcliente int,
nombre nvarchar(150),
email nvarchar(200),
montodebe decimal,
montopagado decimal,
montofinal decimal
CONSTRAINT fk_clientes FOREIGN KEY (idcliente) REFERENCES clientes (idcliente),
)

create trigger TR_clientes_insert
on clientes
for insert
as
declare @nombre nvarchar(150)
select @nombre = nombre from inserted
declare @email nvarchar(200)
select @email = email from inserted
declare @montofinal decimal
select @montofinal = montofinal from inserted
declare @montodebe decimal
select @montodebe = montodebe from inserted
declare @montopagado decimal
select @montopagado = montopagado from inserted
declare @idcliente int
select @idcliente = idcliente from inserted
insert into deudores values(@idcliente,@nombre,@email,@montodebe,@montopagado,@montofinal)

create table historial(
idmonto int not null identity primary key,
monto decimal,
periodopago int,
fechadepago date,
status varchar(20),
idcliente int, 
CONSTRAINT fk_clientes2 FOREIGN KEY (idcliente) REFERENCES clientes (idcliente),
)
insert into historial values(80,1,'2023-03-27 23:18:00.000','x',2)


create trigger TR_Historial_Update
on clientes
for update
as
declare @cantidadp decimal(6,2)
select @cantidadp = cantidadp from inserted
in