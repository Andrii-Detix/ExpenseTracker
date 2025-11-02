# Expense Tracker

**Expense Tracker** is a project developed as part of the laboratory assignments for the **"Back-end"** course. 

The application is written in **C#** using the **ASP.NET Core** platform. 

## Content Table
- [Installation and Run](#installation)
- [Endpoints](#endpoints)
- [Individual Task](#individual-task)
- [Author](#author)

<span id="installation"></span>
## Installation and Run

The project uses **Docker** and **Docker Compose** for local deployment.  
Follow the instruction below to run the application locally:

1. Install **Docker** from the [website](https://www.docker.com/get-started/).
   
2. Follow the [official guide](https://docs.docker.com/compose/install/) to install **Docker Compose**.
  
3. Clone the repository:
    ```bash
    git clone https://github.com/Andrii-Detix/ExpenseTracker.git
    ```

4. Navigate to the project directory:
    ```bash
    cd ExpenseTracker
    ```

5. Run project with Docker Compose:
   ```bash
   docker compose up --build
   ```
   If you are outside the project folder where the `docker-compose.yml` file is located or you want to explicitly specify the path to it, use the `-f` option:
   ```bash
   docker compose -f <file-path> up --build
   ```

After these steps the application will be available at [`http://localhost:8080`](http://localhost:8080).

To check if the service is running correctly, send a simple **HTTP GET** request to the healthcheck endpoint [`http://localhost:8080/healthcheck`](http://localhost:8080/healthcheck).

<span id="endpoints"></span>
## Endpoints

### General

| HTTP Method | URL                      | Auth Required | Description                              |
|-------------|--------------------------|---------------|------------------------------------------|
| GET         | `/healthcheck`           | Yes           | Return service status and current UTC time |

### Auth
| HTTP Method | URL                      | Auth Required | Description                              |
|-------------|--------------------------|---------------|------------------------------------------|
| POST        | `/register`              | No            | Register a new user with the specified `login`, `name`, `password` and `defaultCurrencyId` |
| POST        | `/login`                 | No            | Authenticate a user using the provided `login` and `password`. Return a JWT token if the credentials are valid |

### User

| HTTP Method | URL                      | Auth Required | Description                              |
|-------------|--------------------------|---------------|------------------------------------------|
| PUT         | `/users/{id}/currency`   | Yes           | Set specified default currency for the user |
| DELETE      | `/users/{id}`            | Yes           | Delete a user by the unique id           |
| GET         | `/users/{id}`            | Yes           | Return a user by the unique id           |
| GET         | `/users`                 | Yes           | Return a list of users                   |

### Currency

| HTTP Method | URL                      | Auth Required | Description                              |
|-------------|--------------------------|---------------|------------------------------------------|
| POST        | `/currencies`            | Yes           | Create a new currency with the specified `code` and `name` |
| DELETE      | `/currencies/{id}`       | Yes           | Delete a currency by the unique id       |
| GET         | `/currencies/{id}`       | No            | Return a currency by the unique id       |
| GET         | `/currencies`            | No            | Return a list of currencies              |

### Category

| HTTP Method | URL                      | Auth Required | Description                              |
|-------------|--------------------------|---------------|------------------------------------------|
| POST        | `/categories`            | Yes           | Create a new category with the specified `name` |
| DELETE      | `/categories/{id}`       | Yes           | Delete a category by the unique id       |
| GET         | `/categories/{id}`       | Yes           | Return a category by the unique id       |
| GET         | `/categories`            | Yes           | Return a list of categories              |

### Record

| HTTP Method | URL                      | Auth Required | Description                              |
|-------------|--------------------------|---------------|------------------------------------------|
| POST        | `/records`               | Yes           | Create a new record with the specified `userId`, `categoryId`, `currencyId` and `amount` |
| DELETE      | `/records/{id}`          | Yes           | Delete a record by the unique id       |
| GET         | `/records/{id}`          | Yes           | Return a record by the unique id       |
| GET         | `/records?UserId={id}&CategoryId={id}` | Yes           | Return a list of records filtered by `UserId` and `CategoryId`. At least one parameter must be provided |

<span id="individual-task"></span>
## Individual Task
### Determining the Variant Number  
University group: IM-31.  
Number of variants: 3.  
Variant number: 31 mod 3 = 1.  

### Description of Variant 1
- Currency support. It is required to create a currency entity.  
- Each user has a default currency.  
- When creating an expense record, it is possible to specify the associated currency.  
- If no currency is provided for the expense record, the user's default currency will be used.

<span id="author"></span>
## Author

Developed by [Andrii Ivanchyshyn](https://github.com/Andrii-Detix).
