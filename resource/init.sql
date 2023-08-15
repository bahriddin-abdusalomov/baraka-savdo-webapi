create table categories
(
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	description text,
	image_path text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table companies
(
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	description text,
	image_path text not null,
	phone_number varchar(13),
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table products
(
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	description text,
	image_path text not null,
	unit_price double PRECISION not null,
	category_id bigint references categories (id),
	company_id bigint references companies (id),
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table discounts
(
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_discounts
(
	id bigint generated always as identity primary key,
	product_id bigint references products (id),
	discount_id bigint references discounts (id),
	description text,
	percentage smallint,
	start_at timestamp without time zone not null,
	end_at timestamp without time zone not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table suppliers
(
	id bigint generated always as identity primary key,
	company_name varchar(50) not null,
	description text,
	fax_phone_number varchar(13),
	contact_phone_number varchar(13) not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_suppliers
(
	id bigint generated always as identity primary key,
	product_id bigint references products (id),
	supplier_id bigint references suppliers (id),
	quantity integer not null,
	unit_price double PRECISION not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table users
(
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	phone_number varchar(13) not null,
	phone_number_confirmed bool default false,
	passport_seria_number varchar(9),
	is_male bool not null,
	birth_date date,
	country text,
	region text,
	password_hash text not null,
	salt text not null,
	image_path text not null,
	last_activity timestamp without time zone default now(),
	identity_role text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_comments
(
	id bigint generated always as identity primary key,
	product_id bigint references products (id),
	user_id bigint references users (id),
	comment text not null,
	created_at timestamp without time zone default now(),
	is_edited bool default false,
	updated_at timestamp without time zone default now()
);

create table deliveries
(
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	phone_number varchar(13) not null,
	phone_number_confirmed bool default false,
	passport_seria_number varchar(9),
	is_male bool not null,
	birth_date date,
	country text,
	region text,
	password_hash text not null,
	salt text not null,
	image_path text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table orders
(
	id bigint generated always as identity primary key,
	user_id bigint references users (id),
	delivery_id bigint references deliveries (id),
	status text not null,
	products_price double PRECISION not null, -- summ all items of order details -> result_price
	delivery_price double PRECISION not null,
	result_price double PRECISION not null, -- add total_price + delivery price
	latitude double PRECISION not null,
	longitude double PRECISION not null,
	payment_type text not null,
	is_paid bool not null,
	is_contracted bool not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table order_details
(
	id bigint generated always as identity primary key,
	order_id bigint references orders (id),
	product_id bigint references products (id),
	quantity integer not null,
	total_price double PRECISION not null, -- product prise * quantity
	discount_price double PRECISION not null, -- discount -> start_at & end_at (product_id, current_datetime)
	result_price double PRECISION not null, -- total_price - discount_price
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);
SET TIMEZONE TO 'UTC';