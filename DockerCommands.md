For running a local instance of PostgreSQL in Docker, use the following command:
 - docker run --name rhs_dev.db -e POSTGRES_DB=rhs_dev -e POSTGRES_USER=rhs_dev -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres
   - PostgreSQL connectionstring: Server=localhost,5432;Database=rhs_dev;Username=rhs_dev;Password=postgres;Include Error Detail=true
 - docker run --name rhs_dev.db -e ACCEPT_EULA=Y -e MSSQL_SA_PASSWORD=yourStrong(!)Password -e MSSQL_PID=Developer -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest 
   - MSSQL connectionstring: Data Source=localhost,1433;Database=rhs_dev.db;Application Name=RHS;Integrated Security=false;User ID=SA;Password=yourStrong(!)Password;TrustServerCertificate=True;

For running the whole application stack with Docker Compose, use:
 - docker compose up

dotnet ef database update --connection "" --project ./src/RHS.API/RHS.API.csproj
