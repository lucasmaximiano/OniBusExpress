IF DB_ID('OnibusExpressDb') IS NULL
BEGIN
    CREATE DATABASE OnibusExpressDb;
END;
GO

USE OnibusExpressDb;
GO

IF OBJECT_ID('dbo.Rotas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Rotas
    (
        Id UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT PK_Rotas PRIMARY KEY,

        Origem NVARCHAR(150) NOT NULL,
        Destino NVARCHAR(150) NOT NULL,
        DistanciaKm DECIMAL(10, 2) NULL,
        Ativo BIT NOT NULL
            CONSTRAINT DF_Rotas_Ativo DEFAULT 1,

        DataCriacao DATETIME2 NOT NULL
            CONSTRAINT DF_Rotas_DataCriacao DEFAULT SYSUTCDATETIME()
    );
END;
GO

IF OBJECT_ID('dbo.Viagens', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Viagens
    (
        Id UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT PK_Viagens PRIMARY KEY,

        RotaId UNIQUEIDENTIFIER NOT NULL,
        DataSaida DATETIME2 NOT NULL,
        DataChegada DATETIME2 NULL,
        Valor DECIMAL(18, 2) NOT NULL,
        QuantidadeAssentos INT NOT NULL,
        AssentosDisponiveis INT NOT NULL,

        DataCriacao DATETIME2 NOT NULL
            CONSTRAINT DF_Viagens_DataCriacao DEFAULT SYSUTCDATETIME(),

        CONSTRAINT FK_Viagens_Rotas
            FOREIGN KEY (RotaId)
            REFERENCES dbo.Rotas(Id)
    );
END;
GO

IF OBJECT_ID('dbo.Passageiros', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Passageiros
    (
        Id UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT PK_Passageiros PRIMARY KEY,

        Nome NVARCHAR(150) NOT NULL,
        Cpf VARCHAR(11) NOT NULL,
        Email NVARCHAR(200) NULL,
        Telefone VARCHAR(20) NULL,

        DataCriacao DATETIME2 NOT NULL
            CONSTRAINT DF_Passageiros_DataCriacao DEFAULT SYSUTCDATETIME(),

        CONSTRAINT UQ_Passageiros_Cpf UNIQUE (Cpf)
    );
END;
GO

IF OBJECT_ID('dbo.Reservas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Reservas
    (
        Id UNIQUEIDENTIFIER NOT NULL
            CONSTRAINT PK_Reservas PRIMARY KEY,

        ViagemId UNIQUEIDENTIFIER NOT NULL,
        PassageiroId UNIQUEIDENTIFIER NOT NULL,
        NumeroAssento INT NOT NULL,
        Status INT NOT NULL,

        DataReserva DATETIME2 NOT NULL
            CONSTRAINT DF_Reservas_DataReserva DEFAULT SYSUTCDATETIME(),

        CONSTRAINT FK_Reservas_Viagens
            FOREIGN KEY (ViagemId)
            REFERENCES dbo.Viagens(Id),

        CONSTRAINT FK_Reservas_Passageiros
            FOREIGN KEY (PassageiroId)
            REFERENCES dbo.Passageiros(Id),

        CONSTRAINT UQ_Reservas_Viagem_Assento
            UNIQUE (ViagemId, NumeroAssento)
    );
END;
GO