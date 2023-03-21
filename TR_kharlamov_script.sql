-- Создание БД --
create database TR_Kharlamov;

create schema tr; 

-- Создание Таблиц для БД --

-- таблица building является ролительской для всех связанных с ней таблиц -- 
create table tr.building (
work_id serial primary key,
work_name varchar(50),
begin_date date not null,
end_date date check (end_date > begin_date));

--  Таблица workers --
create table tr.workers (
worker_id serial primary key,
FIO varchar(80),
profession varchar(50) default 'разнорабочий',
salary int check (salary > 0),
company varchar(50) default 'MyCompany',
work_id int );

-- таблица KPP --
create table tr.kpp (
worker_id int not null,
kpp_date date not null,
mat_id int default null,
mat_kol int default null,
inst_id int default null,
inst_kol int default null,
primary key (worker_id, kpp_date));

-- таблица storage materials --
create table tr.storage_mat(
mat_id serial primary key,
mat_kol int check (mat_kol >= 0),
mat_price int check (mat_price > 0),
mat_name varchar(50) not null);

-- таблица storage instruments --
create table tr.storage_inst(
inst_id serial primary key,
inst_kol int check (inst_kol >= 0),
inst_price int check (inst_price > 0),
inst_name varchar(50) not NULL);

-- таблица materials in use --
create table tr.mat_in_use(
id_miu serial primary key,
mat_id int not null,
mat_kol int check (mat_kol >= 0),
work_id int not null);

-- таблица instruments in use --
create table tr.inst_in_use(
id_iiu serial primary key,
inst_id int not null,
inst_kol int check (inst_kol >= 0),
work_id int not null);

-- таблица User type --
create table tr.user_type (
id_type serial primary key,
typename varchar(20));

-- таблица users --
create table tr.users(
user_id serial primary key,
type_id int,
login varchar(20),
pass varchar(8));


-- Создание связей между таблицами --

-- building родительская для workers, тк сотрудники могут выполнять работы только на описанных в building проектах --
alter table workers add foreign key (work_id) references building on delete cascade;

-- building родительская для mat_in_use тк материалы используются только на работах описаннах в building
alter table mat_in_use add foreign key (work_id) references building on delete cascade;

-- building родительская для inst_in_use тк инструменты используются только на работах описаннах в building
alter table inst_in_use add foreign key (work_id) references building on delete cascade;

-- storage_mat родительская для mat_in_use тк в работе используются только те материалы которые описаны в storage_mat
alter table mat_in_use add foreign key (mat_id) references storage_mat on delete cascade;

-- storage_inst родительская для mat_in_use тк в работе используются только те инструменты которые описаны в storage_mat
alter table inst_in_use add foreign key (inst_id) references storage_inst on delete cascade;

/* таблица KPP дочерняя для таблиц workers, storage_mat и storage_inst 
 тк в нееё можно внести данные только с теми сотрудниками, материалами и инструментами
 которые описаны в вышеперечисленных таблицах */

alter table kpp add foreign key (worker_id) references workers on delete cascade;
alter table kpp add foreign key (mat_id) references storage_mat on delete cascade;
alter table kpp add foreign key (inst_id) references storage_inst on delete cascade;

-- связь таблиц где хранятся пароли доступа --
alter table users add foreign key (type_id) references user_type on delete cascade; 


-- Заполнение таблиц данными --

insert into building (work_name, begin_date, end_date)
values
('Геодезические измерения', '2022-11-20', '2022-11-23'),
('Кровельные работы', '2022-10-30', '2022-12-12'),
('Электромонтажные работы', '2022-11-15', '2023-02-02');

insert into workers (fio, profession, salary, company, work_id)
values  
('Иванов Пётр Михайлович', 'Геодезист', 60000, 'СтройПро', 1),
('Михайлов Василий Юрьевич', 'Ассистент геодезиста', 30000, 'ССО', 1),
('Мамедов Порфирий Денисович', default, 25000worker_id int not null,
kpp_date, default, 1),
('Карпин Владимир Алексеевич', 'Кровельщик', 48000, 'Чистокров', 2),
('Васин Артём Витальевич', 'Кровельщик', 48000, 'Чистокров', 2),
('Перов Леонид Витальевич', 'Кровельщик', 45000, 'Чистокров', 2),
('Брежнев Александр Дмитриевич', default, 28000, default, 2),
('Романов Георгий Павлович',  'Электрик', 52000, 'Электромонтаж', 3),
('Муравьев Евгений Алексеевич',  'Электрик', 52000, 'Электромонтаж', 3),
('Телегин Данила Евгеньевич', default, 25000, default, 3);

insert into workers (fio, profession, salary, company)
values  
('Смирнов Пётр Михайлович', 'Водитель', 43000, 'Грузоперевозки'),
('Милов Василий Альбертович', 'Водитель', 43000, 'Грузоперевозки');


insert into storage_inst (inst_kol, inst_price, inst_name)
values 
(20, 3000, 'Мультиметр'),
(30, 8000, 'Шуруповерт'),
(25, 1500, 'Уровень'),
(10, 20000, 'Тахеометр'),
(22, 5600, 'Лазерная рулетка'),
(50, 6800, 'Набор инструментов');

-- Преобразуем тип данных c int на real 
alter table storage_mat alter column mat_kol type real;

insert into storage_mat (mat_kol, mat_price, mat_name)
values 
(5.5, 50000, 'Бетон'),
(3.0, 67000, 'Изоляционные материалы'),
(10, 47000, 'Металлочерепица'),
(8.2, 36000, 'Профнаcтил'),
(5, 24000, 'Кабели и комплектующие');

insert into kpp (worker_id, kpp_date, mat_id, mat_kol, inst_id, inst_kol)
values 
(11, '2022-08-08', 1, 5, 4, 4),
(12, '2022-11-08', 2, 4, 3, 5),
(11, '2022-09-29', 1, 5, 2, 1),
(11, '2022-11-01', DEFAULT, DEFAULT, 6, 2),
(11, '2022-10-30', 2, 3, 2, 4),
(12, '2022-11-03', 4, 10, DEFAULT, DEFAULT);

			-- Создание функций --

-- скалярная функция --
--drop function allinstprice_byid;

create function AllInstPrice_byID (id int) returns int
as $$
begin
	return(
	select inst_kol * inst_price from storage_inst
	where inst_id = id);
end;
$$ language plpgsql;

--select AllInstPrice_byId (4);

create function AllMatPrice_byID (id int) returns int
as $$
begin
	return(
	select mat_kol * mat_price from storage_mat
	where mat_id = id);
end;
$$ language plpgsql;

--select allmatprice_byid(2);

 -- Табличная функция --

create or replace function allkpp_lastmonth() returns table (f_date date, material varchar(50), 
amount_mat int, istrument varchar(50), amount_inst int)
as $$
begin
	return query
	select kpp_date as f_date, mat_name as material, kpp.mat_kol as amount_mat, inst_name as instrument, 
	kpp.inst_kol as amount_inst from kpp
	left join storage_mat on storage_mat.mat_id = kpp.mat_id
	left join storage_inst on storage_inst.inst_id  = kpp.inst_id
	where kpp_date > (current_date - interval '1 month');
end;
$$ language plpgsql;

--select * from allkpp_lastmonth();

create or replace function all_free_workers(bd date, ed date) returns table (profession varchar(50),
fio varchar(80))
as $$
begin
	return query
	select workers.profession as profession, workers.fio as fio from workers
	join building on building.work_id = workers.work_id
	where building.begin_date > ed or building.end_date < bd;
end;
$$ language plpgsql;

--select * from all_free_workers ('2022-11-28', '2022-12-20');

-- вывести все текцщие работы с занятыми сотрудниками

-- drop function get_all_works();
create or replace function get_all_works() returns table (work_name varchar(50), profession varchar(50),
fio varchar(80), bd date, ed date)
as $$
begin
	return query
	select "Work name", allworks_now."profession" as profession, "Worker"  as fio,  "Begin" as bd, 
	"End" as ed from allworks_now
	order by work_name; 
end;
$$ language plpgsql;

select * from get_all_works();

-- функция для получения группы пользователя (авторизация)-- 
create or replace function get_typename(l varchar(20), p varchar(8))
returns varchar(20)
as $$
begin
	return 
	(select typename from user_type
	join users on users.type_id = user_type.id_type  
	where l = login and p = pass);
end;
$$ language plpgsql;

-- функции для получения данных о материлах на складе --
create or replace function get_stor_mat_info() returns table  (material varchar(50), kol real)
as $$
begin
	return query
	select mat_name, mat_kol from storage_mat
    order by mat_name ;
end;
$$ language plpgsql;

--select * from get_stor_mat_info();

-- функции для получения данных об инструментах на складе --
create or replace function get_stor_inst_info() returns table  (instrument varchar(50), kol int)
as $$
begin
	return query
	select inst_name, inst_kol from storage_inst
    order by inst_name ;
end;
$$ language plpgsql;

select * from get_stor_inst_info();

-- Представление (все ведущиеся в данный момент работы, участвующие в них сотрудники)--

create view allworks_now as
select work_name as "Work name", begin_date as "Begin", end_date as "End",  
fio as "Worker", profession, company from building
join workers on workers.work_id = building.work_id 
where current_date between begin_date and end_date
group by work_name, company, profession, fio, end_date, begin_date
order by end_date;

select * from allworks_now;

create view workers_view as
select work_name as "Work name", fio as FIO, profession, company, salary  from workers
join building on workers.work_id = building.work_id; 

create or replace function select_workers_data (wname varchar(80)) returns table ("work name" varchar(50), 
FIO varchar(80), profession varchar(50), company varchar(50), salary int)
as $$
begin
	return query
	select * from workers_view
	where wname = workers_view.FIO;
end;
$$ language plpgsql; 

select select_workers_data('Мамедов Порфирий Денисович');

-- процедуры для манипуляций с данными в БД --

-- процедура для загрузки данных в building --
create or replace procedure add_to_building(wn varchar(50),bd date, ed date)
language sql 
as $$
insert into building (work_name, begin_date, end_date)
values (wn, bd, ed);
$$

call add_to_building ('малярные работы', '2022-11-15', '2022-11-28');

-- процедура для загрузки нового материала в storage_mat --
create or replace procedure add_new_to_storage_mat (mn varchar(50), mp int)
language sql 
as $$
insert into storage_mat (mat_kol, mat_price, mat_name)
values (0, mp, mn);
$$

call add_new_to_storage_mat ('Краски малярные', 185000);

-- процедура для загрузки новых инструментов в storage_inst --
create or replace procedure add_new_to_storage_inst (inn varchar(50), ip int)
language sql 
as $$
insert into storage_inst (inst_kol, inst_price, inst_name)
values (0, ip, inn);
$$

call add_new_to_storage_inst ('Кисти малярные', 800);

-- процедура добавляющая нового сотрудника --
create or replace procedure add_new_worker(f varchar(80), p varchar(50), s int, c varchar(50), w int)
language sql 
as $$
insert into workers (fio, profession, salary, company, work_id)
values (f, p, s, c, w);
$$

call add_new_worker ('Козлов Иван Даниилович', 'маляр',  33000, 'МалярПрофи', 4);

-- Добавление записи в KPP --
create or replace procedure new_kpp (w int, kd date, mi int, mk real, ii int, ik int)
language sql 
as $$
insert into kpp (worker_id, kpp_date, mat_id, mat_kol, inst_id, inst_kol)
values (w, kd, mi, mk, ii, ik);
$$

call new_kpp (11, '2022-11-13', 6, 70, 7, 100);

-- Добавление записи в mat_in_use --
create or replace procedure new_miu (mi int, mk int, wi int)
language sql 
as $$
insert into mat_in_use(mat_id, mat_kol, work_id)
values (mi, mk, wi)
$$

call new_miu (5, 2, 3);

-- Добавление записи в inst_in_use --
create or replace procedure new_iiu (ii int, ik int, wi int)
language sql 
as $$
insert into inst_in_use(inst_id, inst_kol, work_id)
values (ii, ik, wi)
$$

call new_iiu (1, 3, 3);

-- Добавление данных в таблицу user_type --

create or replace procedure add_to_ut (tn varchar(20))
language sql 
as $$
insert into user_type(typename)
values (tn)
$$

call add_to_ut ('директор');

-- Добавление данных в таблицу users --
create or replace procedure add_to_users (ti int, lg varchar(20), p varchar(8))
language sql 
as $$
insert into users(type_id, login, pass)
values (ti, lg, p)
$$

call add_to_users (1, 'superuser1', '00001111');

-- Удаление записи из таблицы building --
create or replace procedure delete_work(id int)
language sql 
as $$
delete from building
where work_id = id
returning *;
$$

call delete_work(4);

-- update данных в таблице storage_mat--
create or replace procedure upd_sm (mi int, mk real)
language sql 
as $$
update storage_mat set mat_kol = mk where mat_id = mi;
$$

call upd_sm (1, 7.5);

-- update данных в таблице storage_mat--
create or replace procedure upd_si (ii int, ik real)
language sql 
as $$
update storage_inst set inst_kol = ik where inst_id = ii;
$$

call upd_si (4, 11);

-- назначить существующего работника на работу
create or replace procedure give_work_to_worker(wid int, job_id int)
language sql 
as $$
update workers set work_id = job_id where worker_id = wid;
$$

			-- Триггеры --
create or replace function new_mi_kol() returns trigger 
as $tr_kpp$	
declare 
stor_kol_m real;
new_kol_m real;
kol_m real;
stor_kol_i int;
new_kol_i int;
kol_i int;
begin 
if new.mat_id is not null then -- если привезли новые материалы
stor_kol_m = (select mat_kol from storage_mat where mat_id = new.mat_id);
new_kol_m = new.mat_kol;
kol_m = stor_kol_m + new_kol_m;
call upd_sm (new.mat_id, kol_m);
end if;
if new.inst_id is not null then  -- если привезли новые инструменты
stor_kol_i = (select inst_kol from storage_inst where inst_id = new.inst_id);
new_kol_i = new.inst_kol;
kol_i = stor_kol_i + new_kol_i;
call upd_si (new.inst_id, kol_i);
end if;
return new;
end;	
$tr_kpp$ language plpgsql;

create trigger tr_kpp after insert on kpp
for each row execute procedure new_mi_kol(); 

call new_kpp (12, '2022-11-19', 6, 1, 7, 20);

--DROP TRIGGER tr_kpp ON tr.kpp;

-- подсчитать цену всех материалов на складе -- 
create or replace function all_mat_price() returns int
as $$
begin 
	return (
	select sum(mat_price * mat_kol) from storage_mat);
end;
$$ language plpgsql;
end;

select all_mat_price();

-- подсчитать цену всех инструментов на складе --
create or replace function all_inst_price() returns int
as $$
begin 
	return (
	select sum(inst_price * inst_kol) from storage_inst);
end;
$$ language plpgsql;
end;

select all_inst_price();

-- возвращает все используемые в работах материалы --
create or replace function get_all_miu() returns table (material varchar(50), mat_kol real, work_name varchar(50))
as $$
begin
	return query 
	select mat_name, mat_in_use.mat_kol, building.work_name from mat_in_use
	join building on building.work_id = mat_in_use.work_id 
	join storage_mat on storage_mat.mat_id = mat_in_use.mat_id
	order by material;
end
$$ language plpgsql;

select get_all_miu();

-- возвращает все используемые в работах инструменты --
--drop function get_all_iiu();
create or replace function get_all_iiu() returns table (instrument varchar(50), inst_kol int, work_name varchar(50))
as $$
begin
	return query 
	select inst_name, inst_in_use.inst_kol, building.work_name from inst_in_use
	join building on building.work_id = inst_in_use.work_id 
	join storage_inst on storage_inst.inst_id = inst_in_use.inst_id
	order by instrument;
end
$$ language plpgsql;

select get_all_iiu();

