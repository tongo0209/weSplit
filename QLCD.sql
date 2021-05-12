USE master
GO

IF DB_ID('QLCD') IS NOT NULL
DROP DATABASE QLCD

CREATE DATABASE QLCD
GO

USE QLCD
GO

CREATE TABLE DD_DULICH
(
	MADD CHAR(5),
	TENDD NVARCHAR(60),
	DIACHI NVARCHAR(100),
	THONGTIN NVARCHAR(MAX)

	CONSTRAINT PK_DD_DULICH
	PRIMARY KEY(MADD)
)

CREATE TABLE CHUYENDI
(
	MACD CHAR(5),
	TENCD NVARCHAR(60),
	DIADANH CHAR(5),
	KHOANTHU FLOAT,
	TIENCHI FLOAT,
	NGAYDI DATE,
	NGAYVE DATE,

	CONSTRAINT PK_CHUYENDI
	PRIMARY KEY(MACD)
)

CREATE TABLE THANHVIEN
(
	MATV CHAR(5),
	TENTV NVARCHAR(60),

	CONSTRAINT PK_THANHVIEN
	PRIMARY KEY(MATV)
)

CREATE TABLE LOTRINH
(
	MA_LOTRINH CHAR(5),
	MA_DIADIEM CHAR(5),
	TEN_DD_LT NVARCHAR(50),
	DIACHI NVARCHAR(MAX),

	CONSTRAINT PK_LOTRINH
	PRIMARY KEY(MA_LOTRINH, MA_DIADIEM)
)

CREATE TABLE TT_CHUYENDI
(
	MA_DIADANH CHAR(5),
	MA_CHUYENDI CHAR(5),
	TRANGTHAI NVARCHAR(30)

	CONSTRAINT PK_TT_CHUYENDI
	PRIMARY KEY(MA_DIADANH, MA_CHUYENDI)
)

CREATE TABLE THAMGIA
(
	MACD CHAR(5),
	MATV CHAR(5),
	TIENTHU FLOAT,

	CONSTRAINT PK_THAMGIA
	PRIMARY KEY(MACD, MATV)
)

ALTER TABLE TT_CHUYENDI
ADD
	CONSTRAINT FK_TTCD_CHUYENDI
	FOREIGN KEY(MA_CHUYENDI)
	REFERENCES CHUYENDI,
	CONSTRAINT FK_TTCD_DD_DL
	FOREIGN KEY (MA_DIADANH)
	REFERENCES DD_DULICH;


ALTER TABLE THAMGIA
ADD
	CONSTRAINT FK_TG_CHUYENDI
	FOREIGN KEY(MACD)
	REFERENCES CHUYENDI,
	CONSTRAINT FK_TG_THANHVIEN
	FOREIGN KEY(MATV)
	REFERENCES THANHVIEN

ALTER TABLE CHUYENDI
ADD
	CONSTRAINT FK_CHUYENDI_DIADANH
	FOREIGN KEY(DIADANH)
	REFERENCES DD_DULICH

INSERT INTO CHUYENDI (MACD, TENCD, DIADANH, KHOANTHU, TIENCHI, NGAYDI, NGAYVE)
VALUES 
	('CD001', N'Khám phá Hồ Xuân Hương', NULL, 500000, 5000000, '2020-01-01', '2020-05-30'),
	('CD002', N'Hang yến Phú Quốc', NULL, 7500000, 7500000, '2020-01-01', '2020-06-30'),
	('CD003', N'Hành trình đất Mũi', NULL, 5000000, 4500000, '2020-01-01', '2020-04-01'),
	('CD004', N'Tham quan chùa Minh', NULL, 3000000, 1000000, '2020-01-01', '2020-03-02'), --Gia Lai
	('CD005', N'Lang than An Giang', NULL, 6000000, 5230000, '2020-01-01', '2020-01-01');

INSERT INTO THANHVIEN (MATV, TENTV)
VALUES 
	('TV001', N'Phạm Phong Phú Cường'),
	('TV002', N'Nguyễn Trong Quyết'),
	('TV003', N'Ngô Tất Tố'),
	('TV004', N'Huy Cận'),
	('TV005', N'Tố Hữu'),
	('TV006', N'Hàn Mạc Tử'),
	('TV007', N'Nguyễn Bé Đạt'),
	('TV008', N'Huỳnh Công Lý');

INSERT INTO DD_DULICH (MADD, TENDD, DIACHI, THONGTIN)
VALUES 
	('DD001', N'Hồ Xuân Hương', N'Xuan Huong Lake, Phường 1, Thành phố Đà Lạt, Lâm Đồng', N'Hồ mang tên Xuân Hương với nghĩa là hương của mùa Xuân.'),
	('DD002', N'Phú Quốc', N'Phú Quốc, Phú Quốc, Kiên Giang', N'Phú Quốc là một hòn đảo nhỏ thuộc Việt Nam, nhưng tại đảo Phú Quốc có đầy đủ mọi điều kiện tự nhiên như đất liền, trong đó phải kể đến những hang động tại hòn đảo này.'),
	('DD003', N'Cà Mau', N'Đất Mũi, Cà Mau', N'Đất Mũi là vùng đất cuối trời của tổ quốc.'),
	('DD004', N'Gia Lai', N'348 đường Nguyễn Viết Xuân, phường Hội Phú, thành phố Pleiku, tỉnh Gia Lai', N'Chùa Minh Thành chính là công trình mang đậm bản sắc dân tộc, lưu giữ những giá trị văn hóa đặc sắc, bao gồm nét văn hóa tâm linh và sự gìn giữ, truyền bá lại những kỹ thuật kiến trúc cổ xưa vô cùng quý giá của dân tộc đang dần bị mai một theo thời gian.'),
	('DD005', N'An Giang', N'An Giang Việt Nam', N'An Giang là một tỉnh thuộc vùng đồng bằng sông Cửu Long, Việt Nam. An Giang là tỉnh có dân số đông nhất đồng bằng sông Cửu Long, thuộc miền Nam Việt Nam.'),
	('DD006', N'Bình Phước', N'Bình Phước -  Việt Nam', N'Bình Phước là một tỉnh thuộc vùng Đông Nam Bộ, Việt Nam. Đây cũng là tỉnh có diện tích lớn nhất miền nam. ');

INSERT INTO LOTRINH (MA_LOTRINH, MA_DIADIEM, TEN_DD_LT, DIACHI)
VALUES 
	('LT001', 'DD001', N'Hồ Chí Minh', N'Hồ Chí Minh - Việt Nam'),
	('LT001', 'DD002', N'Long An', N'Long An - Việt Nam'),
	('LT001', 'DD003', N'Kiên Giang', N'An Giang - Việt Nam'),
	('LT002', 'DD001', N'Hồ Chí Minh', N'Hồ Chí Minh - Việt Nam'),
	('LT002', 'DD002', N'Bình Dương', N'Bình Dương - Việt Nam'),
	('LT002', 'DD003', N'Đắk Lắk', N'Đắk Lắk - Việt Nam'),
	('LT002', 'DD004', N'Nghệ An', N'Nghệ An - Việt Nam');

INSERT INTO THAMGIA (MACD, MATV, TIENTHU)
VALUES 
	('CD001', 'TV001', 1200000),
	('CD001', 'TV002', 2500000),
	('CD002', 'TV003', 1330000),
	('CD003', 'TV004', 1230000),
	('CD002', 'TV002', 1200000),
	('CD001', 'TV004', 1300000),
	('CD003', 'TV003', 1400000);

INSERT INTO TT_CHUYENDI (MA_CHUYENDI, MA_DIADANH, TRANGTHAI)
VALUES
	('CD001', 'DD002', N'Đã đi'),
	('CD001', 'DD001', N'Chưa đi'),
	('CD002', 'DD004', N'Đã đi'),
	('CD003', 'DD001', N'Đã đi'),
	('CD002', 'DD002', N'Chưa đi'),
	('CD004', 'DD002', N'Đã đi');

UPDATE CHUYENDI SET DIADANH = 'DD001' WHERE MACD = 'CD001';
UPDATE CHUYENDI SET DIADANH = 'DD002' WHERE MACD = 'CD002';
UPDATE CHUYENDI SET DIADANH = 'DD003' WHERE MACD = 'CD003';
UPDATE CHUYENDI SET DIADANH = 'DD004' WHERE MACD = 'CD004';
UPDATE CHUYENDI SET DIADANH = 'DD005' WHERE MACD = 'CD005';

SELECT * FROM CHUYENDI
SELECT * FROM DD_DULICH
SELECT * FROM THANHVIEN
SELECT * FROM TT_CHUYENDI
SELECT * FROM LOTRINH
SELECT * FROM THAMGIA