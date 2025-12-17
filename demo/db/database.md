# Database design


## Database schema of E-commerce system in Postgresql
```
create table customers (
  id bigint primary key generated always as identity,
  name text not null,
  email text unique not null,
  phone text,
  address text
);

create table products (
  id bigint primary key generated always as identity,
  name text not null,
  description text,
  price numeric(10, 2) not null,
  stock int not null
);

create table orders (
  id bigint primary key generated always as identity,
  customer_id bigint not null,
  order_date timestamp with time zone default now(),
  status text not null,
  total_amount numeric(10, 2) not null,
  foreign key (customer_id) references customers (id)
);

create table order_items (
  id bigint primary key generated always as identity,
  order_id bigint not null,
  product_id bigint not null,
  quantity int not null,
  price numeric(10, 2) not null,
  foreign key (order_id) references orders (id),
  foreign key (product_id) references products (id)
);
```

## Description of tables in database

The tables for an order management system in an e-commerce platform have been created:

* Customers: Stores customer information.
  * id: Primary key
  * name: Customer's name
  * email: Unique email address
  * phone: Contact number
  * address: Shipping address

* Products: Contains product details.
  * id: Primary key
  * name: Product name
  * description: Product description
  * price: Product price
  * stock: Available stock
  
* Orders: Records customer orders.
  * id: Primary key
  * customer_id: References customers(id)
  * order_date: Date of order
  * status: Order status
  * total_amount: Total order amount


* Order Items: Details of products in each order.
  * id: Primary key
  * order_id: References orders(id)
  * product_id: References products(id)
  * quantity: Quantity ordered
  * price: Price per unit
```