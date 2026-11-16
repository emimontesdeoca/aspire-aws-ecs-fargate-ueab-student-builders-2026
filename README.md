# See your app as a system with .NET Aspire

A small, beginner-friendly demo for the AWS Student Builder Group at the University of Eastern Africa, Baraton.

The goal is to explain .NET Aspire and the architecture of a distributed app. Everything runs locally; the talk does **not** deploy anything to AWS. AWS deployment choices are discussed at the end.

## The demo

```text
HTTP client -> API --cache miss--> Sub API --> PostgreSQL
                 |                                SELECT current_database(), CURRENT_TIMESTAMP
                 +------------------------------> Redis (cached response)
```

| Resource | Responsibility |
|---|---|
| `api` | Calls the Sub API on a cache miss, stores its string in Redis, and returns it. |
| `sub-api` | Returns the current database name and time as plain text. |
| `postgres` | PostgreSQL database; the demo creates no tables. |
| `cache` | Redis response cache with a one-minute expiration. |

## Run locally

Requirements:

- .NET 10 SDK
- Aspire CLI 13.6+
- Docker Desktop with Docker Compose v2, running

```powershell
cd src
aspire run
```

Open the dashboard URL printed by Aspire and wait for `postgres`, `cache`, `sub-api`, and `api` to become healthy. The demo endpoints use ports 8080 (API) and 8081 (Sub API).

Use the request files in [src/Api/Api.http](src/Api/Api.http) and [src/SubApi/SubApi.http](src/SubApi/SubApi.http), or try:

1. Call `GET http://localhost:8080/database-info`. On a cache miss, API calls Sub API, which runs a read-only query against PostgreSQL.
2. Call the same endpoint again. API returns the cached string from Redis.
3. Optionally call `GET http://localhost:8081/database-info` directly to see the plain-text PostgreSQL result.
4. In the dashboard, inspect the cache logs and the API-to-Sub-API trace.

No tables, migrations, or seed data are created.

## Generate Docker Compose

From `src`, publish the Aspire app model:

```powershell
aspire publish
```

Aspire generates the Compose file and environment template under `AppHost/aspire-output`. That folder is ignored by Git because the files are generated output. To run the generated Compose deployment on the **local** Docker host:

```powershell
aspire deploy
```

Stop it with:

```powershell
aspire destroy
```

The AppHost uses `Aspire.Hosting.Docker`; no AWS package or AWS credentials are required for local development or Compose generation.

## AWS options (discussion only)

This repository does not provision AWS resources. At a high level:

- **EC2 + Docker Compose**: easiest to explain and reuse for a small lab; you manage the VM and its operations.
- **ECS + Fargate**: managed container compute without managing EC2 hosts; add the network, load balancer, and managed data services the app needs.
- **ECS + EC2**: ECS schedules containers on EC2 capacity you control; more tuning, more infrastructure to operate.
- **EKS**: Kubernetes on AWS; appropriate when the team already needs Kubernetes, but the most operationally involved option here.

Aspire's app model does not make every deployment target interchangeable. Compose is useful as a local/container artifact; an AWS deployment needs a target and integrations that map the app resources to AWS infrastructure. Review the current [AWS integrations for .NET Aspire](https://github.com/aws/integrations-on-dotnet-aspire-for-aws) before planning a real deployment; support and maturity vary by resource.

## Export the slides

```powershell
cd slides
npx @marp-team/marp-cli deck.md -o deck.html
npx @marp-team/marp-cli deck.md --pdf -o deck.pdf
```

## References

- [Aspire app host](https://aspire.dev/fundamentals/app-host/)
- [Aspire dashboard](https://aspire.dev/dashboard/overview/)
- [Aspire Docker Compose deployment](https://aspire.dev/deployment/docker-compose/)
- [Aspire PostgreSQL integration](https://aspire.dev/integrations/databases/postgres/postgres-host/)
- [Aspire Redis integration](https://aspire.dev/integrations/caching/redis/)
- [AWS integrations for .NET Aspire](https://github.com/aws/integrations-on-dotnet-aspire-for-aws)
