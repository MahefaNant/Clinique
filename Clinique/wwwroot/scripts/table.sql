delete from depense;
delete from budgedepense;
delete from typedepense;
delete from acte;
delete from budgetacte;
delete from typeacte;
delete from facture_patient;
delete from patient;

alter sequence depense_id_seq RESTART ;
alter sequence budgedepense_id_seq RESTART ;
alter sequence typedepense_id_seq RESTART ;
alter sequence acte_id_seq RESTART ;
alter sequence budgetacte_id_seq RESTART ;
alter sequence typeacte_id_seq RESTART;
alter sequence facture_patient_id_seq RESTART;
alter sequence patient_id_seq RESTART ;

create table utilisateur(
    id serial primary key ,
    nom varchar(20),
    prenom varchar(20),
    mail varchar(50) unique ,
    code varchar(100) ,
    type varchar(10)
);

create table patient(
    id serial primary key ,
    nom varchar(50),
    naissance date not null ,
    genre int check ( genre=0 or genre=1 ),
    remboursement boolean default false
);

create table facture_patient(
    id serial primary key , 
    id_patient int not null ,
    date timestamp not null 
);

alter table facture_patient add foreign key (id_patient) references patient(id);

create table typeacte(
    id serial primary key ,
    nom varchar(30),
    code varchar(15)
);

create table budgetacte(
    id serial primary key ,
    id_typeacte int not null ,
    annee int not null ,
    budget decimal(18,2)
);

alter table budgetacte add foreign key (id_typeacte) references typeacte(id);

create table acte(
    id serial primary key ,
    id_typeacte int not null ,
    id_facture int not null ,
    montant decimal(18,2) not null
);

alter table acte add foreign key (id_typeacte) references typeacte(id);
alter table acte add foreign key (id_facture) references facture_patient(id);

create table typedepense(
    id serial primary key ,
    nom varchar(30),
    code varchar(15)
);

create table budgedepense(
    id serial primary key ,
    id_typedepense int not null ,
    annee int not null ,
    budget decimal(18,2)
);

alter table budgedepense add foreign key (id_typedepense) references typedepense(id);

create table depense(
    id serial primary key ,
    id_typedepense int not null ,
    date timestamp default null,
    montant decimal(18,2) not null
);

alter table depense add foreign key (id_typedepense) references typedepense(id);

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------








