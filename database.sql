create table users(
	id int not null auto_increment,
	user varchar(100) unique,
	pwd varchar(100),
	primary key(id)
);

create table processes(
	id int not null auto_increment,
	name varchar(100) unique,
	url text,
	timeout_url int,
	updated_at datetime,
	primary key(id)
);

create table tasks(
	id int not null auto_increment,
	name varchar(100),
	directory text,
	command text,
	window_style int,
	uac int,
	id_process int,
	primary key(id),
	foreign key (id_process) references processes(id)
);

create table logs(
	id int not null auto_increment,
	details text,
	pid int,
	id_task int,
	primary key(id),
	foreign key (id_task) references tasks(id)
);

insert into users(user,pwd) 
	values('supervisor','$2a$11$n3wJp8J589XUawMW2tdk4eghkRqMys.NA7YfaoMq6.jLzOHF8QjBa');