# SigmaTest

# SigmaTest

SigmaTest is a .NET project for managing candidate information. This project uses ASP.NET Core with Entity Framework Core to handle CRUD operations for candidate records.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Database Migration](#database-migration)
- [License](#license)

## Installation

To get started with SigmaTest, follow these steps:

1. Clone the repository:
    ```sh
    git clone https://github.com/adrianmarginean/SigmaTest.git
    ```

2. Navigate to the project directory:
    ```sh
    cd SigmaTest
    ```

3. Restore the dependencies:
    ```sh
    dotnet restore
    ```

4. Update the database connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SigmaTestDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

## Usage

1. Run the application:
    ```sh
    dotnet run
    ```

2. Open your browser and navigate to `https://localhost:5001/swagger` to explore the API endpoints using Swagger UI.

## API Endpoints

The following API endpoints are available:

- `POST /api/Candidates/UpsertCandidate`: Upsert candidate.


## Database Migration

To apply the latest database migrations, use the following commands:

1. Add a new migration:
    ```sh
    dotnet ef migrations add InitialCreate
    ```

2. Update the database:
    ```sh
    dotnet ef database update
    ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
