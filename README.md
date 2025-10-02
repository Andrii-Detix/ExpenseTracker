# Expense Tracker

**Expense Tracker** is a project developed as part of the laboratory assignments for the **"Back-end"** course. 

The application is written in **C#** using the **ASP.NET Core** platform. 

## Content Table
- [Installation and Run](#installation)
- [Endpoints](#endpoints)
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

| HTTP Method | URL                      | Description                              |
|-------------|--------------------------|------------------------------------------|
| GET         | `/healthcheck`           | Returns service status and current UTC time |

<span id="author"></span>
## Author

Developed by [Andrii Ivanchyshyn](https://github.com/Andrii-Detix).
