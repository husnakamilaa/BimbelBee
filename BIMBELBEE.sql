CREATE DATABASE BIMBELBEE;
GO


CREATE TABLE siswa (
    nisn CHAR(10) PRIMARY KEY,
	CONSTRAINT CHK_nisn CHECK (LEN(nisn) = 10),
    nama VARCHAR(50)NOT NULL,
    notelp VARCHAR(15) NOT NULL,
	check (notelp like '08%' and len(notelp) between 10 and 13),
    alamat VARCHAR(200)
);


CREATE TABLE mapel(
	id_mapel char(4) PRIMARY KEY,
	nama_mapel VARCHAR(100) NOT NULL,
	mapel varchar(25) check (mapel in ('kimia', 'biologi', 'fisika', 'matematika', 'geografi', 'sejarah', 'ekonomi','sosiologi')),
	tingkat varchar(2) check (tingkat in ('10', '11', '12')),
	ruangan char(1) check (ruangan in ('a', 'b', 'c', 'd', 'e')),
	durasi varchar(10) NOT NULL,
	hari_kursus varchar(10) check (hari_kursus in ('senin', 'selasa', 'rabu', 'kamis', 'jumat')),
	waktu_kursus time, -- time gk bisa make constraint like
	harga int,
	id_tutor char(4) foreign key references tutor(id_tutor)
);

create table tutor (
	id_tutor char(4) primary key,
	nama_tutor varchar(20), --bisa jadi instrukturnya trus keluar
	notelp VARCHAR(15) NOT NULL,
	check (notelp like '08%' and len(notelp) between 10 and 13),
);

create table pendaftaran (
	id_pendaftaran char(6) primary key,
	nisn char(10) foreign key references siswa(nisn),
	id_mapel char(4) foreign key references mapel(id_mapel),
	total_pembayaran int,
	tgl_daftar date check (tgl_daftar like '[0-9][0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9]')
);