# Team Prime - Project Bank

This is a vertical slice of the Team Prime Project Bank. This project is run in a browser, with at database that needs to be containerized on a local machine using Docker. This is a university project and meant for demonstation purposes only.

## HOW TO RUN THE APPLICATIION:

1. Clone repository to local machine from https://github.com/poppyjones/BDSA-Project.git

2. Navigate into Server folder.

3. Run these command in powershell:
  - `$password = New-Guid`
  - `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest`
  - `$database = "PrimeSlice"`
  - `$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True"`
  - `dotnet user-secrets set "ConnectionStrings:PrimeSlice" "$connectionString"`

4. Run following commands:
  - `dotnet restore`
  - `dotnet ef database update`
  - `dotnet run`

5. Open browser and navigate to [Team Primes Project Bank](https://localhost:7081) - (https://localhost:7081)
