---
marp: true
theme: default
paginate: true
size: 16:9
lang: en
title: "Ship a .NET app to AWS without suffering"
description: "A beginner-friendly .NET Aspire, dashboard, and Docker Compose demo on AWS."
header: "AWS Student Builder Group<br>University of Eastern Africa"
footer: "Emiliano Montesdeoca · AWS Student Builder Group UEAB"
style: |
  :root {
    --ink: #151b23;
    --panel: #202a35;
    --green: #00e676;
    --white: #f5f7fa;
    --muted: #aab4bf;
  }
  section {
    position: relative;
    box-sizing: border-box;
    padding: 82px 86px 64px;
    background-color: var(--ink);
    background-image:
      linear-gradient(rgba(255,255,255,.08) 1px, transparent 1px),
      linear-gradient(90deg, rgba(255,255,255,.08) 1px, transparent 1px);
    background-size: 64px 64px;
    color: var(--white);
    font-family: "Segoe UI", "Inter", system-ui, sans-serif;
    font-size: 28px;
    line-height: 1.28;
  }
  section::before {
    content: "";
    position: absolute;
    top: 0;
    right: 0;
    width: 52px;
    height: 52px;
    background: var(--green);
  }
  section h1 {
    color: var(--green);
    font-size: 1.8em;
    line-height: 1.1;
    letter-spacing: -.025em;
  }
  section h2 {
    margin-bottom: .55em;
    color: var(--green);
    font-size: 1.3em;
    line-height: 1.15;
  }
  section h3 { color: var(--green); }
  section strong, section a { color: var(--green); }
  section li { margin: .35em 0; }
  section code {
    padding: .08em .25em;
    border-radius: 4px;
    background: #2a3541;
    color: var(--white);
    font-size: .82em;
  }
  section pre {
    padding: .75em .9em;
    border-left: 5px solid var(--green);
    background: #1c242d;
    font-size: .68em;
    line-height: 1.3;
  }
  section pre code { padding: 0; background: transparent; }
  section pre .hljs-string { color: #9ef0b2 !important; }
  section pre .hljs-number { color: #75d7ff !important; }
  section pre .hljs-keyword { color: #ff8296 !important; }
  section header {
    top: 28px;
    right: 82px;
    left: auto;
    width: 250px;
    color: var(--green);
    font-size: 13px;
    line-height: 1.4;
    text-align: right;
  }
  section footer {
    bottom: 17px;
    left: 72px;
    color: var(--muted);
    font-size: 12px;
  }
  section::after {
    right: 72px;
    bottom: 17px;
    color: var(--muted);
    font-size: 12px;
  }
  section.cover {
    padding: 48px 80px 222px;
  }
  section.cover::before { display: none; }
  section.cover header, section.cover footer, section.cover::after { display: none; }
  .cover-layout {
    display: grid;
    grid-template-columns: minmax(0, 1.2fr) minmax(0, .8fr);
    align-items: center;
    gap: 1.2em;
    height: 100%;
  }
  .cover-copy { align-self: center; }
  .cover-copy .eyebrow {
    color: var(--green);
    font-size: .65em;
    font-weight: 700;
    letter-spacing: .12em;
  }
  section.cover h1 {
    margin: .25em 0;
    color: var(--white);
    font-size: 1.55em;
    line-height: 1.08;
  }
  .cover-copy .subtitle { color: #d3dae1; }
  .cover-copy .date { color: var(--muted); font-size: .75em; }
  .cover-visual {
    display: block;
    width: 100%;
    max-height: 330px;
    border-radius: 18px;
    object-fit: contain;
  }
  .brand-strip {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    height: auto;
  }
  section.section {
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: var(--green);
    background-image:
      linear-gradient(rgba(21,27,35,.12) 1px, transparent 1px),
      linear-gradient(90deg, rgba(21,27,35,.12) 1px, transparent 1px);
    color: var(--ink);
  }
  section.section::before { background: var(--ink); }
  section.section h1, section.section strong { color: var(--ink); }
  section.section p { font-size: 1.05em; }
  section.demo { background-color: #10151b; }
  section.demo h2::before {
    content: "LIVE DEMO / ";
    color: var(--green);
    font-size: .5em;
    letter-spacing: .1em;
  }
  .cols {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 1.2em;
  }
  .cards {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: .8em;
  }
  .cards.four { grid-template-columns: repeat(4, 1fr); }
  .cards.four .card { padding: .65em; }
  .cards.four h3 { font-size: .88em; }
  .cards.four .card p { font-size: .72em; line-height: 1.25; }
  .card {
    padding: .8em;
    border: 1px solid #53606d;
    border-top: 5px solid var(--green);
    background: rgba(32,42,53,.9);
  }
  .card p { margin: .3em 0; }
  .flow {
    display: grid;
    grid-template-columns: 1fr auto 1fr auto 1fr auto 1fr;
    align-items: center;
    gap: .25em;
    margin: 1.2em 0;
  }
  .node {
    padding: .75em .45em;
    border: 2px solid var(--green);
    background: var(--panel);
    text-align: center;
  }
  .arrow { color: var(--green); font-size: 1.25em; }
  .big {
    color: var(--green);
    font-size: 1.35em;
    font-weight: 700;
    line-height: 1.2;
  }
  .small { color: var(--muted); font-size: .72em; }
  .muted { color: var(--muted); }
  .callout {
    padding: .5em .8em;
    border-left: 5px solid var(--green);
    background: var(--panel);
  }
  .center { text-align: center; }
  section.bio { padding: 76px 82px 64px; }
  .bio-layout {
    display: grid;
    grid-template-columns: minmax(0, .72fr) minmax(0, 1.28fr);
    align-items: center;
    gap: 1.15em;
    height: 100%;
  }
  .bio-photo {
    display: block;
    width: 100%;
    height: 390px;
    border: 8px solid var(--green);
    border-radius: 24px;
    object-fit: cover;
    object-position: center 32%;
  }
  .bio-copy .eyebrow {
    color: var(--green);
    font-size: .62em;
    font-weight: 700;
    letter-spacing: .12em;
  }
  section.bio h1 {
    margin: .15em 0;
    font-size: 1.45em;
  }
  .bio-role { color: #d3dae1; }
  .bio-facts {
    margin: .65em 0;
    padding-left: 1.1em;
    font-size: .82em;
  }
  .bio-facts li { margin: .25em 0; }
  .bio-badges {
    display: flex;
    align-items: center;
    gap: .45em;
    margin-top: .65em;
  }
  .bio-badges img {
    display: block;
    width: auto;
    height: 56px;
    object-fit: contain;
  }
  .bio-badges img.mvp { height: 42px; }
---

<!-- _class: cover -->
<!-- _paginate: false -->

<div class="cover-layout">
  <div class="cover-copy">
    <p class="eyebrow">AWS STUDENT BUILDER GROUP · UEAB</p>
    <h1>Ship a .NET app<br>to AWS without suffering</h1>
    <p class="subtitle">Build a shop. Read one trace.<br>Deploy with .NET Aspire and Docker Compose.</p>
    <p class="date">16 November 2026 · 17:00 UTC</p>
  </div>
  <img class="cover-visual" src="assets/image slide 1.png" alt="Microsoft and AWS connected for a .NET app">
</div>
<img class="brand-strip" src="assets/image.png" alt="AWS Student Builder Group at University of Eastern Africa, Baraton">

<!--
TIME: 0:00 – 0:45

Welcome everyone. I am joining from the Canary Islands. Thank you to the AWS Student Builder Group for having me.
Today we will build a tiny shop, use the Aspire dashboard, and ship the same app to AWS without a complicated cloud setup.
-->

---

<!-- _class: bio -->

## About the speaker

<div class="bio-layout">
  <img class="bio-photo" src="https://avatars.githubusercontent.com/u/13499480?v=4" alt="Emiliano Montesdeoca">
  <div class="bio-copy">
    <p class="eyebrow">YOUR HOST</p>
    <h1>Emiliano Montesdeoca</h1>
    <p class="bio-role">Cloud Development Team Lead · Intelequia</p>
    <ul class="bio-facts">
      <li>Microsoft MVP · Developer Technologies since 2022</li>
      <li>Sessionize speaker · .NET and cloud community events</li>
      <li>Most Active Speaker · 2023, 2024, 2025</li>
    </ul>
    <div class="bio-badges">
      <img src="assets/most-active-speaker-2023.svg" alt="Most Active Speaker 2023">
      <img src="assets/most-active-speaker-2024.svg" alt="Most Active Speaker 2024">
      <img src="assets/most-active-speaker-2025.svg" alt="Most Active Speaker 2025">
      <img class="mvp" src="assets/microsoft-mvp.png" alt="Microsoft MVP">
    </div>
  </div>
</div>

<!--
TIME: 0:45 – 2:15

Introduce yourself briefly: you started as a .NET developer and now lead cloud development at Intelequia.
Mention the Microsoft MVP award and community speaking, then move on.
-->

---

## The promise

<p class="big">No AWS experience needed.</p>

<div class="cards">
  <div class="card">
    <h3>Build</h3>
    <p>A tiny online shop with three services.</p>
  </div>
  <div class="card">
    <h3>Understand</h3>
    <p>Use the Aspire dashboard to follow one order.</p>
  </div>
  <div class="card">
    <h3>Deploy</h3>
    <p>Run the same app on AWS with Docker Compose.</p>
  </div>
</div>

<!--
TIME: 2:15 – 3:15

Set the level: a web app is enough background. Explain any cloud word when it first appears.
The goal is not to learn all of AWS today. It is to see the same small system run in two places.
-->

---

## Our tiny app

<div class="flow">
  <div class="node"><strong>Shop</strong><br><span class="small">place an order</span></div>
  <div class="arrow">-&gt;</div>
  <div class="node"><strong>API</strong><br><span class="small">accepts it</span></div>
  <div class="arrow">-&gt;</div>
  <div class="node"><strong>Redis</strong><br><span class="small">holds the order</span></div>
  <div class="arrow">-&gt;</div>
  <div class="node"><strong>Worker</strong><br><span class="small">processes it</span></div>
</div>

<p class="callout">One order. Three services. One thing to watch in the dashboard.</p>

<!--
TIME: 3:15 – 5:00

The shop is just a web page. The API receives the order. Redis is a small in-memory service that holds it.
The worker is a separate process that picks it up and marks it processed. No database lecture today.
-->

---

## What is .NET Aspire?

<p class="big">Open-source tooling for building, running, and observing distributed .NET apps.</p>

<div class="cols">
  <div class="card">
    <h3>Distributed app</h3>
    <p>An app made of parts such as an API, a worker, and a cache.</p>
  </div>
  <div class="card">
    <h3>Developer workflow</h3>
    <p>A consistent way to start the whole system and understand what it is doing.</p>
  </div>
</div>

<p class="small">Aspire is not a web framework and it does not replace Docker or AWS.</p>

<!--
TIME: 5:00 – 7:30

Explain Aspire as tooling around the app, not a new way to write the API.
It helps coordinate the pieces and makes the whole system easier to run and observe.
-->

---

## What Aspire gives you

<div class="cards four">
  <div class="card">
    <h3>One app model</h3>
    <p>Describe services and how they depend on each other.</p>
  </div>
  <div class="card">
    <h3>Local orchestration</h3>
    <p>Start the API, Redis, and worker together.</p>
  </div>
  <div class="card">
    <h3>Useful defaults</h3>
    <p>Service discovery, health checks, and telemetry.</p>
  </div>
  <div class="card">
    <h3>Deployment targets</h3>
    <p>Publish this demo as Docker Compose.</p>
  </div>
</div>

<p class="callout">One workflow for the whole app, from local development to deployment artifacts.</p>

<!--
TIME: 7:30 – 10:00

Introduce the features at a high level. Aspire coordinates services, supplies shared defaults,
and can publish the app model to a deployment target. No code walkthrough on this slide.
-->

---

## The Aspire dashboard

<p class="big">One local window into the whole application.</p>

<div class="cards four">
  <div class="card">
    <h3>Resources</h3>
    <p>Running state, health, dependencies, and endpoints.</p>
  </div>
  <div class="card">
    <h3>Logs</h3>
    <p>Search and filter messages across services.</p>
  </div>
  <div class="card">
    <h3>Traces</h3>
    <p>Follow one request through the API, cache, and worker.</p>
  </div>
  <div class="card">
    <h3>Metrics</h3>
    <p>See runtime and application measurements over time.</p>
  </div>
</div>

<p class="callout">The same dashboard gives you a view of the whole system, not just one project.</p>

<!--
TIME: 10:00 – 12:00

Introduce the four areas: resources, logs, traces, and metrics.
The live demo will use the resource list and one trace.
-->

---

<!-- _class: demo -->

## Start the shop

```powershell
cd src
aspire run
```

<p class="big">Open the dashboard. Wait for the resources to turn green.</p>

<p class="muted">Then open the API link and place one order.</p>

<!--
TIME: 12:00 – 16:00

Start the app. In the dashboard show cache, api, and worker.
Open the API endpoint and place a cacao order. The first run may pull images, so pre-run it before the talk.
If Docker is not running or the demo stalls, switch to the recording and keep going.
-->

## Follow one order

<div class="flow">
  <div class="node"><strong>POST</strong><br><span class="small">/orders</span></div>
  <div class="arrow">-&gt;</div>
  <div class="node"><strong>API</strong><br><span class="small">saves it</span></div>
  <div class="arrow">-&gt;</div>
  <div class="node"><strong>Redis</strong><br><span class="small">queues it</span></div>
  <div class="arrow">-&gt;</div>
  <div class="node"><strong>Worker</strong><br><span class="small">marks it done</span></div>
</div>

<p class="callout">The trace answers one simple question: what happened to my order?</p>

<!--
TIME: 16:00 – 18:30

Show one trace, not every span. Start at POST /orders and point to the worker finishing the order.
The important lesson is that traces connect work across processes.
-->

---

## Aspire gives us Docker Compose

```powershell
aspire publish
```

<div class="cols">
  <div class="card">
    <h3>Publish</h3>
    <p>Creates the Compose file and its environment template.</p>
  </div>
  <div class="card">
    <h3>Deploy</h3>
    <p>Builds the images and starts the Compose services.</p>
  </div>
</div>

<p class="small">Generated output: <code>src/AppHost/aspire-output/docker-compose.yaml</code></p>

<!--
TIME: 18:30 – 21:00

Aspire has a Docker Compose deployment target. Publish writes the Compose artifacts.
Deploy prepares the values, builds the images, and runs docker compose.
No AWS-specific Aspire package and no hand-written CloudFormation.
-->

---

## AWS, the simple version

<div class="cols">
  <div class="card">
    <h3>EC2</h3>
    <p>A Linux computer rented from AWS.</p>
    <p>Docker and Compose run on that computer.</p>
  </div>
  <div class="card">
    <h3>Our app</h3>
    <p>Compose starts the API, Redis, and worker together.</p>
    <p>The browser reaches the API on port 8080.</p>
  </div>
</div>

<p class="big">One machine. One Compose deployment.</p>

<!--
TIME: 21:00 – 23:00

Explain EC2 as a rented Linux computer. This is the simplest way to run a Compose file in AWS.
We are intentionally not using ECS or Fargate in this beginner demo.
-->

---

## Keep the network simple

<div class="cards">
  <div class="card">
    <h3>SSH · 22</h3>
    <p>Allow only your own IP.</p>
  </div>
  <div class="card">
    <h3>Shop · 8080</h3>
    <p>Allow browser traffic for the demo.</p>
  </div>
  <div class="card">
    <h3>Redis</h3>
    <p>Keep it inside the Compose network.</p>
  </div>
</div>

<p class="small">This is a demo setup, not a production security design.</p>

<!--
TIME: 23:00 – 25:00

The AWS security group is the virtual firewall. Open SSH only to the presenter's IP and the shop port for viewers.
Do not expose Redis or the worker.
-->

---

## Point Aspire at the EC2 machine

```powershell
docker context create cacao-ec2 --docker "host=ssh://ubuntu@<EC2-PUBLIC-DNS>"
docker context use cacao-ec2

cd src
aspire deploy --environment production
```

<p class="callout">A Docker context tells Docker to use the EC2 machine over SSH.</p>

<!--
TIME: 25:00 – 28:30

The EC2 instance is prepared before the talk. The key must already work with ssh ubuntu@host.
Create the Docker context once, select it, then Aspire deploys the Compose app to that Docker host.
The deployment can take a few minutes. Start it live and have a recording ready.
-->

---

<!-- _class: demo -->

## The same shop, now on AWS

1. Open <code>http://&lt;EC2-PUBLIC-IP&gt;:8080</code>.
2. Place an order.
3. Check that the order becomes <code>processed</code>.
4. See the containers with <code>docker --context cacao-ec2 ps</code>.

<p class="big">Same app. New machine.</p>

<!--
TIME: 28:30 – 33:00

Show the endpoint, submit one order, and confirm the worker processed it.
Use docker --context cacao-ec2 ps to show the three containers. Do not spend time navigating the AWS console.
-->

---

## Simple on purpose

<div class="cols">
  <div class="card">
    <h3>What we get</h3>
    <p>A clear local workflow and a small AWS demo.</p>
  </div>
  <div class="card">
    <h3>What we do not get</h3>
    <p>No autoscaling, managed Redis, load balancer, or high availability.</p>
  </div>
</div>

<p class="callout">For production, choose a managed deployment target. This talk is about understanding the pieces first.</p>

<!--
TIME: 33:00 – 36:00

Be honest: a single EC2 instance is not Fargate and is not highly available.
It is the simplest place to run this Compose file and learn how the pieces connect.
-->

---

## Clean up the demo

```powershell
docker context use cacao-ec2
cd src
aspire destroy --environment production

docker context use default
```

<p class="big">Then terminate the EC2 instance.</p>
<p class="small">Deleting containers does not stop the EC2 bill.</p>

<!--
TIME: 36:00 – 37:30

Aspire destroy removes the Compose deployment. Switch back to the local Docker context.
Then terminate the EC2 instance in AWS. Set a budget alert before the first deployment.
-->

---

## Three things to remember

1. **Aspire** helps describe and run a multi-service app locally.
2. **The dashboard** helps us see what happened to one request.
3. **Docker Compose** can run the same services on an AWS EC2 machine.

<p class="big">Start small. Make the whole system work.</p>

<!--
TIME: 37:30 – 40:00

Recap the three steps. Invite questions. If asked about Fargate, explain that it needs an ECS deployment model;
we chose EC2 because it runs Compose directly and keeps today's demo beginner-friendly.
-->

---

<!-- _class: section -->
<!-- _paginate: false -->

# Questions?

<p>Thank you · 17:00 UTC · AWS Student Builder Group UEAB</p>

<!--
TIME: 40:00 – 45:00

Questions. If needed, use the backup slides and links in the README.
-->
