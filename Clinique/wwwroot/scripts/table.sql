alter sequence utilisateur_id_seq RESTART ;

create table utilisateur(
    id serial primary key ,
    nom varchar(20),
    prenom varchar(20),
    mail varchar(50) unique ,
    code varchar(100) ,
    type varchar(10)
);