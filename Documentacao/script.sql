use CONTROLE_AVE

	GO 
	IF OBJECT_ID('Pai', 'U') IS NOT NULL
	DROP TABLE Pai;

	GO 
	IF OBJECT_ID('Mae', 'U') IS NOT NULL
	DROP TABLE Mae;

	GO 
	IF OBJECT_ID('Filho', 'U') IS NOT NULL
	DROP TABLE Filho;

	GO 
	IF OBJECT_ID('Casamento', 'U') IS NOT NULL
	DROP TABLE Casamento;

	GO 
	IF OBJECT_ID('MutacaoAve', 'U') IS NOT NULL
	DROP TABLE MutacaoAve;

	GO 
	IF OBJECT_ID('Mutacao', 'U') IS NOT NULL
	DROP TABLE Mutacao;

	GO 
	IF OBJECT_ID('PortadorAve', 'U') IS NOT NULL
	DROP TABLE PortadorAve;

	GO 
	IF OBJECT_ID('Portador', 'U') IS NOT NULL
	DROP TABLE Portador;

	GO 
	IF OBJECT_ID('Caracteristica', 'U') IS NOT NULL
	DROP TABLE Caracteristica;

    GO 
	IF OBJECT_ID('Ave', 'U') IS NOT NULL
	DROP TABLE Ave;

	GO
	CREATE TABLE Ave (
    id INT IDENTITY NOT NULL,
	codigo VARCHAR(10) NOT NULL,
    nome VARCHAR(100) NULL,
    sexo int NOT NULL,
	cor VARCHAR(100) NOT NULL,
	dt_nascimento date NULL,
	ativo bit NOT NULL,
    PRIMARY KEY(id))

	GO
	CREATE TABLE Caracteristica (
    id INT IDENTITY not null,
    ds_caracteristica VARCHAR(100) NOT NULL,
	id_ave INT not null,
	FOREIGN KEY(id_ave) references Ave(id),
    PRIMARY KEY(id))

	GO
	CREATE TABLE Mutacao (
    id INT IDENTITY not null,
    ds_mutacao VARCHAR(100) NOT NULL,
    PRIMARY KEY(id))

	GO
	CREATE TABLE MutacaoAve (
    id INT IDENTITY not null,
    id_mutacao INT not null,
	id_ave INT not null,
	FOREIGN KEY(id_mutacao) references Mutacao(id),
	FOREIGN KEY(id_ave) references Ave(id),
    PRIMARY KEY(id))

	GO
	CREATE TABLE Portador (
    id INT IDENTITY not null,
    ds_portador VARCHAR(100) NOT NULL,
    PRIMARY KEY(id))

	GO
	CREATE TABLE PortadorAve (
    id INT IDENTITY not null,
    id_portador INT not null,
	id_ave INT not null,
	FOREIGN KEY(id_portador) references Portador(id),
	FOREIGN KEY(id_ave) references Ave(id),
    PRIMARY KEY(id))

	GO
	Create table Casamento (
	id INT IDENTITY not null,
	id_macho int not null,
	id_femea int not null,
	PRIMARY KEY(id),
	FOREIGN key(id_macho) references Ave(id),
	FOREIGN key(id_femea) references Ave(id))

	GO
	Create table Pai (
	id INT IDENTITY not null,
	id_ave int not null,
	id_pai int not null,
	PRIMARY KEY(id),
	FOREIGN key(id_ave) references Ave(id),
	FOREIGN key(id_pai) references Ave(id))

	GO
	Create table Mae (
	id INT IDENTITY not null,
	id_ave int not null,
	id_mae int not null,
	PRIMARY KEY(id),
	FOREIGN key(id_ave) references Ave(id),
	FOREIGN key(id_mae) references Ave(id))

	GO
	Create table Filho (
	id INT IDENTITY not null,
	id_pai int not null,
	id_mae int not null,
	id_filho int not null,
	PRIMARY KEY(id),
	FOREIGN key(id_pai) references Ave(id),
	FOREIGN key(id_mae) references Ave(id),
	FOREIGN key(id_filho) references Ave(id))





	

	/* 
	
		select * from Ave p 
		inner join Pai pai on p.id = pai.id_ave
		inner join Mae mae on p.id = mae.id_ave
		inner join Filho filho on p.id = filho.id_ave

		inner join Ave pessoaPai on pai.id_pai = pessoaPai.id 
		inner join Ave pessoaMae on mae.id_mae = pessoaMae.id 
		inner join Ave pessoaFilho on filho.id_filho = pessoaFilho.id 
		where p.id = 1
	
	*/

