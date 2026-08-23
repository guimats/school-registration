-- Script de criacao do banco para o teste pratico
-- SQL Server (qualquer edicao)

IF DB_ID('TesteEscola') IS NULL
    CREATE DATABASE TesteEscola;
GO

USE TesteEscola;
GO

IF OBJECT_ID('dbo.Matricula') IS NOT NULL DROP TABLE dbo.Matricula;
IF OBJECT_ID('dbo.Aluno') IS NOT NULL DROP TABLE dbo.Aluno;
IF OBJECT_ID('dbo.Turma') IS NOT NULL DROP TABLE dbo.Turma;
GO

CREATE TABLE dbo.Aluno (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(120) NOT NULL,
    Email VARCHAR(120) NOT NULL,
    DataNascimento DATE NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE dbo.Turma (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(80) NOT NULL,
    Periodo VARCHAR(20) NOT NULL,        -- Manha, Tarde ou Noite
    VagasTotal INT NOT NULL,
    VagasDisponiveis INT NOT NULL
);

CREATE TABLE dbo.Matricula (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AlunoId INT NOT NULL FOREIGN KEY REFERENCES dbo.Aluno(Id),
    TurmaId INT NOT NULL FOREIGN KEY REFERENCES dbo.Turma(Id),
    DataMatricula DATETIME NOT NULL DEFAULT GETDATE()
);
GO

INSERT INTO dbo.Aluno (Nome, Email, DataNascimento, Ativo) VALUES
('Ana Souza',       'ana.souza@email.com',      '2006-03-14', 1),
('Bruno Lima',      'bruno.lima@email.com',     '2005-11-02', 1),
('Carla Mendes',    'carla.mendes@email.com',   '2006-07-25', 1),
('Diego Ferreira',  'diego.ferreira@email.com', '2005-01-30', 0),
('Elisa Rocha',     'elisa.rocha@email.com',    '2006-09-18', 1),
('Felipe Andrade',  'felipe.andrade@email.com', '2005-05-08', 1),
('Gabriela Nunes',  'gabriela.nunes@email.com', '2006-12-01', 1),
('Henrique Alves',  'henrique.alves@email.com', '2005-08-21', 1);

INSERT INTO dbo.Turma (Nome, Periodo, VagasTotal, VagasDisponiveis) VALUES
('3A - Ensino Medio', 'Manha', 30, 28),
('3B - Ensino Medio', 'Tarde', 30, 30),
('Turma Intensiva',   'Noite', 5,  1),
('Turma Lotada',      'Manha', 2,  0);

INSERT INTO dbo.Matricula (AlunoId, TurmaId) VALUES
(1, 1),
(2, 1),
(3, 3),
(5, 3),
(6, 3),
(7, 3),
(2, 4),
(8, 4);
GO
