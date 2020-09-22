create database	Paie
use Paie

create table cibel_liste(
value nvarchar(max) not null,
matricule nvarchar(max) not null,
name varchar(max) not null,
persnl_time nvarchar(max) not null
);

create table archiv_liste(
value nvarchar(max) not null,
matricule nvarchar(max) not null,
name varchar(max) not null,
persnl_time nvarchar(max) not null
);

insert into cibel_liste values ('s2026','006414FSOS','EL FARISSI IBTISSAM','20:18'), ('006163FSOS','EL KOSSIR GHITA',22), ('006077FSOS','AZZEDDINE FATIHA',44), ('005966FSOS','EL MAHBOUBY SOUMIA',40), ('005480FSOS','EL GHASSANY ZAHIRA',18), ('005291FSOS','AGOURAM KHADIJA',44), ('003361FSOS','BAHTAR ZAHRA',42), ('002554FSOS','EL MEAALEM FADMA',38), ('006468FSOS','IGLIOU FATIHA',35);

insert into archiv_liste values ('006163FSOS','EL KOSSIR GHITA',02), ('006077FSOS','AZZEDDINE FATIHA',14), ('005966FSOS','EL MAHBOUBY SOUMIA',20), ('005291FSOS','AGOURAM KHADIJA',15), ('003361FSOS','BAHTAR ZAHRA',07), ('002554FSOS','EL MEAALEM FADMA',28), ('006468FSOS','IGLIOU FATIHA',17);

/*select the same matricule in both tables*/
select cibel_liste.matricule, cibel_liste.name, cibel_liste.persnl_time, archiv_liste.matricule, archiv_liste.name, right(archiv_liste.persnl_time, 8) [persnl_time] from archiv_liste, cibel_liste where archiv_liste.matricule = cibel_liste.matricule and cibel_liste.value='F206' and archiv_liste.value='F206'

/*select the matricules that exist only on the first table*/
select * from cibel_liste where cibel_liste.matricule not in (select archiv_liste.matricule from archiv_liste where archiv_liste.value='F206') and cibel_liste.value='F206' 

/*select first 11 char of a column*/
SELECT convert(char(11), persnl_time, 10) [time] FROM archiv_liste

/*select last 8 char of a column*/
SELECT right(archiv_liste.persnl_time, 8) [time] FROM archiv_liste


select * from archiv_liste WHERE value='F206' order by matricule

select archiv_liste.matricule, archiv_liste.name, right(persnl_time, 8) as Time from archiv_liste WHERE value='TEST'

delete cibel_liste

select distinct archiv_liste.value from archiv_liste

select cibel_liste.matricule, cibel_liste.name, right(cibel_liste.persnl_time, 8) [time], archiv_liste.matricule, archiv_liste.name, right(archiv_liste.persnl_time, 8) [time] from archiv_liste, cibel_liste where archiv_liste.matricule = cibel_liste.matricule and cibel_liste.value='mr1' and archiv_liste.value='mr1'

select cibel_liste.matricule, cibel_liste.name, right(cibel_liste.persnl_time, 8) [time] from cibel_liste where cibel_liste.matricule not in (select archiv_liste.matricule from archiv_liste where archiv_liste.value='mr1') and cibel_liste.value='mr1'












