For running a local instance of PostgreSQL in Docker, use the following command:
 - docker run --name rhs_dev.db -e POSTGRES_DB=rhs_dev -e POSTGRES_USER=rhs_dev -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres

For running the whole application stack with Docker Compose, use:
 - docker compose up