CREATE TABLE countries
(
    id_country int NOT NULL AUTO_INCREMENT,
    ccn3 varchar(10) NOT NULL default '',
    name varchar(200) NOT NULL ,
    name_ua varchar(200) null ,
    capital varchar(200) not null default '',
    flag varchar(1000) NULL,
    PRIMARY KEY (id_country)
);

CREATE TABLE languages
(
    id int NOT NULL AUTO_INCREMENT,
    id_country int not null,
    name varchar(100) not null ,
    small_name varchar(20) not null,
    PRIMARY KEY (id),
    FOREIGN KEY (id_country) REFERENCES countries(id_country)
);

CREATE TABLE currencies
(
    id int NOT NULL AUTO_INCREMENT,
    id_country int not null,
    name varchar(100) not null ,
    small_name varchar(20) not null,
    symbol varchar(10) null ,
    PRIMARY KEY (id),
    FOREIGN KEY (id_country) REFERENCES countries(id_country)
);

CREATE TABLE map_type
(
    id int NOT NULL,
    name varchar(50),
    PRIMARY KEY (id)
);

INSERT INTO map_type(id, name)
VALUES (1, 'openStreetMaps'), (2, 'googleMaps');

CREATE TABLE maps
(
    id int NOT NULL AUTO_INCREMENT,
    id_country int not null,
    id_map_type int NOT NULL,
    url varchar(1000) NOT NULL ,
    PRIMARY KEY (id),
    FOREIGN KEY (id_map_type) REFERENCES map_type(id),
    FOREIGN KEY (id_country) REFERENCES countries(id_country)
);

ALTER TABLE countries CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE languages CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE currencies CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;