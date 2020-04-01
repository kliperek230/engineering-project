CREATE TABLE users (
user_id int identity(1,1) primary key,
first_name nvarchar(20) not null,
last_name nvarchar(20) not null,
sex nvarchar(1) not null,
birth_date date not null,
u_height int not null,
u_weight int not null,
email nvarchar not null,
pswd nvarchar not null,
nickname nvarchar(20) not null,
kcal int,
protein int,
carbs int,
fats int,
bench int,
ohp int,
squat int,
deadlift int,
measurements int
);



alter table users
add foreign key(deadlift) references lifts(lift_id);

CREATE TABLE products (
product_id int identity(1,1) primary key,
category nvarchar(20),
product_name varchar(50),
kcal int,
protein int,
carbs int,
fats int,
);




CREATE TABLE measurements (
measurement_id int identity(1,1) primary key,
m_date date,
user_id int not null,
l_calve int,
r_calve int,
l_thigh int,
r_thigh int,
butt int,
waist int,
chest int,
l_arm int,
r_arm int,
l_forearm int,
r_forearm int,
u_weight int,
foreign key(user_id) references users(user_id)
);

create table meals (
meal_id int identity(1,1) primary key,
user_id int not null,
m_date date,
product_id int,
foreign key(user_id) references users(user_id),
foreign key(product_id) references products(product_id)
);

alter table meals
add unit nvarchar(20)

create table lifts (
lift_id int identity(1,1) primary key,
user_id int not null,
m_date date not null,
lift_name nvarchar(8) not null,
value int not null,
foreign key(user_id) references users(user_id)
);



set identity_insert p_categories off;

delete from p_categories

insert into p_categories
values ('drinks', 'gram');

delete from products where product_name = 'strawberry';

sp_rename 'products.product_name', 'product_name_eng', 'column';

update users
set 
u_type = 'user'
where first_name = 'Test';

alter table users
add u_type nvarchar(20);


insert into products (category, product_name_eng, product_name_pl, kcal, protein, carbs, fats)
values ('fruit','orange', 'pomarañcza', 37, 1.1, 8.5, 0.1);

insert into users (first_name, last_name, sex, birth_date, u_height, u_weight, email, pswd, nickname, u_type)
values ('Test2', 'Teœciñski2', 'M', '1980-05-05', 181, 92, 'kliperek230@gmail.com', 'kacper', 'test_tescinski2', 'operator');