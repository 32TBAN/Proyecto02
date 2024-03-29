CREATE TABLE USUARIOS
(ID_USU INT PRIMARY KEY IDENTITY,
CED_USU NVARCHAR(10) UNIQUE NOT NULL,
NICKNAME VARCHAR(10) UNIQUE NOT NULL,
NOM_USU NVARCHAR(30) NOT NULL,
EMAIL NVARCHAR(30) NOT NULL);

CREATE TABLE PRODUCTOS
(COD_PRO INT PRIMARY KEY IDENTITY,
NOM_PRO NVARCHAR(10) NOT NULL,
PRE_PRO DECIMAL NOT NULL);

CREATE TABLE PRODUCTORES
(ID_PRO INT PRIMARY KEY IDENTITY,
CED_PRO NVARCHAR(10) UNIQUE NOT NULL,
NOM_PRO NVARCHAR(30) NOT NULL,
TEL_PRO NVARCHAR(10),
TEL_PRO_ADI NVARCHAR(10),
EMAIL NVARCHAR(30));

CREATE TABLE RECEPCION_PRO
(NUM_REC INT PRIMARY KEY IDENTITY,
ID_USU_REC INT FOREIGN KEY REFERENCES USUARIOS(ID_USU),
ID_PRO_REC INT FOREIGN KEY REFERENCES PRODUCTORES(ID_PRO),
TOTAL DECIMAL NOT NULL);

CREATE TABLE DETALLE_RECEPCION
(NUM_REC_REC INT FOREIGN KEY REFERENCES RECEPCION_PRO(NUM_REC),
COD_PRO_REC INT FOREIGN KEY REFERENCES PRODUCTOS(COD_PRO),
CANTIDAD INT NOT NULL,
SUBTOTAL DECIMAL);

CREATE TABLE CLIENTES
(ID_CLI INT PRIMARY KEY IDENTITY,
CED_CLI NVARCHAR(10) UNIQUE NOT NULL,
NOM_CLI NVARCHAR(30) NOT NULL,
CEL_CLI NVARCHAR(10),
EMAIL NVARCHAR(30));

CREATE TABLE VENTAS
(NUM_VEN INT PRIMARY KEY IDENTITY,
ID_USU_VEN INT FOREIGN KEY REFERENCES USUARIOS(ID_USU),
ID_CLI_VEN INT FOREIGN KEY REFERENCES CLIENTES(ID_CLI),
TOTAL DECIMAL);

CREATE TABLE DETALLE_VENTAS
(NUM_VEN_PER INT FOREIGN KEY REFERENCES VENTAS(NUM_VEN),
COD_PRO_VEN INT FOREIGN KEY REFERENCES PRODUCTOS(COD_PRO),
CANTIDAD INT NOT NULL,
SUBTOTAL DECIMAL);