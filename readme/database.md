### Database

#### Connection informations
The credentials to the database server are the same as the one given during the course.

#### Tables
##### cities
This table stores the different cities where customers and deliverers live, and where restaurants are located.

| Column        | Datatype         | Description  |
| ------------- |-------------| -----|
| id           | int + PK | technical id |
| zip_code      | int | city zip code |
| name      | varchar(50) | city name |

##### customer
This table stores the information about the customers.

| Column        | Datatype         | Description  |
| ------------- |-------------| -----|
| id           | int + PK | technical id |
| username      | varchar(50) | customer username |
| password      | varchar(50) | customer password |
| full_name      | varchar(50) | customer firstname and lastname |
| fk_cities      | int + FK to cities.id | foreign key to customer's city's id |
| created_at      | datetime | customer creation date |
| address      | varchar(100) | customer address |

##### deliverers
This table stores the information about the deliverers.

| Column        | Datatype         | Description  |
| ------------- |-------------| -----|
| id           | int + PK | technical id |
| username      | varchar(50) | deliverer username |
| password      | varchar(50) | deliverer password |
| full_name      | varchar(50) | deliverer firstname and lastname |
| fk_cities      | int + FK to cities.id | foreign key to deliverer's city's id |
| created_at      | datetime | deliverer creation date |

##### dishes
This table stores the information about the dishes. 

| Column        | Datatype         | Description  |
| ------------- |-------------| -----|
| id           | int + PK | technical id |
| fk_restaurants      | int + FK to restaurants.id | foreign key to restaurant's id |
| name      | varchar(50) | dish name |
| price      | int | dish price |
| status      | bit | dish status (active or not) |
| created_at      | datetime | dish creation date |

##### order_dishes
This table makes the link between an order and a dish. 

##### orders
This table stores the order informations.

##### order_status
This table stores the order statuses : "_En cours de livraison_", "_Annulée_", ...

##### order_status_history
This tables stores the status' history of an order

##### restaurants
This table stores the information about the restaurants.
