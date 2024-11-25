create database EduTel2024;
use EduTel2024;

create table Docente (
IdDocente int primary key, 
Nombres varchar(30), 
ApellidoPaterno varchar(30) ,
ApellidoMaterno Varchar(30) ,
NroDocumento Varchar(8) ,
FechaNacimiento Date ,
Direccion varchar(100) ,
Email varchar(70) ,
Celular varchar(12)
)

ALTER procedure usp_Docente_insert(
@nombres varchar(30), 
@apellidoPaterno varchar(30) ,
@apellidoMaterno Varchar(30) ,
@nroDocumento Varchar(8) ,
@fechaNacimiento Date ,
@direccion varchar(100) ,
@email varchar(70) ,
@celular varchar(12)
)
as
begin
	declare @idDocente int = (select ISNULL(MAX(IdDocente),0) from Docente ) +1;

	insert into Docente(IdDocente, Nombres, ApellidoPaterno, ApellidoMaterno, NroDocumento, FechaNacimiento, Direccion, Email, Celular)
		values (@idDocente, @nombres, @apellidoPaterno, @apellidoMaterno, @nroDocumento, @fechaNacimiento, @direccion, @email, @celular)
end

alter procedure usp_Docente_List(
	@buscar varchar(100)
)
as 
begin
	Select
			IdDocente, Nombres, ApellidoPaterno, ApellidoMaterno, NroDocumento, FechaNacimiento, Direccion, Email, Celular
		from Docente	
		where Nombres like('%'+@buscar+'%')
end