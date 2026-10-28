# Ship a .NET app to AWS without suffering

Beginner-friendly demo for the AWS Student Builder Group at the University of Eastern Africa, Baraton.

Build a tiny shop with an API, Redis, and a background worker. Use Aspire to run it locally and follow an order in the dashboard. Aspire also generates Docker Compose artifacts, which we deploy to a single AWS EC2 machine.

This demo uses **EC2, not ECS or Fargate**. EC2 is a Linux computer; Docker Compose runs the containers on it directly. That is the simplest way to explain the deployment without AWS-specific Aspire packages or infrastructure code.

## The app

| Service | What it does |
|---|---|
| `api` | Serves the shop page and accepts orders |
| `cache` | Redis stores orders and holds the queue |
| `worker` | Reads the queue and marks orders processed |

The order flow is `shop -> api -> cache -> worker`.

## Requirements

- .NET 10 SDK
- Aspire CLI 13.6 or newer
- Docker Desktop with Docker Compose v2, running
- For AWS: an Ubuntu EC2 instance with Docker Engine and Compose v2, reachable over SSH

## Run locally

```powershell
cd src
aspire run
```

Open the dashboard URL printed by Aspire, open the `api` endpoint, and place an order. The dashboard shows the resources, logs, and trace.

## Generate and run Docker Compose

```powershell
cd src
aspire publish
```

Aspire writes `docker-compose.yaml` and an environment template under `AppHost/aspire-output`. To build and start the Compose deployment on the current Docker host:

```powershell
aspire deploy
```

Stop and remove that deployment with:

```powershell
aspire destroy
```

The AppHost uses `Aspire.Hosting.Docker` for Compose generation. It does not reference an AWS package.

## Deploy to AWS EC2

Create an Ubuntu EC2 instance and install Docker Engine and the Docker Compose v2 plugin. In its security group, allow:

- SSH (`22`) from your IP only
- The shop (`8080`) for demo traffic

Do not open Redis (`6379`) to the internet. Make sure `ssh ubuntu@<EC2-PUBLIC-DNS>` works and the SSH user can run Docker commands. The key must be available to your local SSH client.

Create a Docker context on your laptop. It tells Docker to connect to the EC2 machine over SSH:

```powershell
docker context create cacao-ec2 --docker "host=ssh://ubuntu@<EC2-PUBLIC-DNS>"
docker context use cacao-ec2
```

From `src`, deploy the Compose app to that Docker host:

```powershell
aspire deploy --environment production
```

Open `http://<EC2-PUBLIC-IP>:8080` and place an order. To see the running containers:

```powershell
docker --context cacao-ec2 ps
```

The context is remote, so `aspire deploy` builds and runs the Compose deployment on EC2. Switch back to the local Docker host when finished:

```powershell
docker context use default
```

### Clean up

Use the same Docker context and environment to remove the Compose deployment:

```powershell
docker context use cacao-ec2
cd src
aspire destroy --environment production
docker context use default
```

Then **terminate the EC2 instance in AWS**. Removing containers does not stop EC2 or storage charges. Add an AWS budget alert before the first deployment.

## Scope

This is a short learning demo, not a production design: one machine, HTTP on port 8080, and a Redis container. There is no autoscaling, load balancer, managed Redis, or high availability. Use ECS/Fargate or other managed services when those are the goal.

## Slides and preparation

- [Slides](slides/deck.md)
- [Speaker preparation](PREPARACION.md)
- [Talk proposal](CFP.md)

Export the presenter HTML and PDF with Marp:

```powershell
cd slides
npx @marp-team/marp-cli deck.md -o deck.html
npx @marp-team/marp-cli deck.md --pdf -o deck.pdf
```

## References

- [Aspire Docker Compose deployment](https://aspire.dev/deployment/docker-compose/)
- [Aspire Docker hosting integration](https://aspire.dev/integrations/compute/docker/)
- [Amazon EC2 getting started](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/EC2_GetStarted.html)
- [Docker contexts](https://docs.docker.com/engine/manage-resources/contexts/)
