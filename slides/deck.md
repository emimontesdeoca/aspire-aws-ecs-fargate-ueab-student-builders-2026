---
marp: true
theme: default
paginate: true
size: 16:9
lang: en
title: "See your app as a system with .NET Aspire"
description: "A local .NET Aspire demo with two APIs, PostgreSQL, Redis, Docker Compose, and AWS deployment choices."
header: "AWS Student Builder Group<br>University of Eastern Africa"
footer: "Emiliano Montesdeoca · .NET Aspire"
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
  .cover-copy .eyebrow, .eyebrow {
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
    grid-template-columns: repeat(2, 1fr);
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
  .big {
    color: var(--green);
    font-size: 1.25em;
    font-weight: 700;
    line-height: 1.2;
  }
  .small { color: var(--muted); font-size: .72em; }
  .callout {
    padding: .5em .8em;
    border-left: 5px solid var(--green);
    background: var(--panel);
  }
  .center { text-align: center; }
  section.code-title {
    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
  }
  section.code-title h1 { margin: 0; font-size: 2.8em; }
  section.code-title::before,
  section.code-title::after,
  section.code-title header,
  section.code-title footer { display: none; }
  .aws-options .card { min-height: 5.2em; }
  .aws-options .card p { font-size: .78em; line-height: 1.25; }
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
    <h1>See your app<br>as a system</h1>
    <p class="subtitle">Two APIs, PostgreSQL, Redis —<br>one local .NET Aspire demo.</p>
    <p class="date">16 November 2026 · 17:00 UTC · No AWS deployment</p>
  </div>
  <img class="cover-visual" src="assets/image slide 1.png" alt="Microsoft and AWS connected for a .NET app">
</div>
<img class="brand-strip" src="assets/image.png" alt="AWS Student Builder Group at University of Eastern Africa, Baraton">

<!--
TIEMPO: 0:00 – 0:35

Agradecer la invitación. Aclarar desde el principio: hoy no desplegaremos nada en AWS; primero
entenderemos la app en local y al final compararemos destinos posibles.
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
TIEMPO: 0:35 – 1:15

Presentación breve: .NET, cloud y comunidades. No alargarse; el objetivo es que la audiencia salga
entendiendo las piezas de una aplicación distribuida.
-->

---

<!-- _class: lead center -->

# What makes up an application?

<!--
TIEMPO: 1:15 – 1:30

Preguntar y esperar un par de respuestas. No contestar todavía.
-->

---

## An application is more than code

<div class="cards four">
  <div class="card"><h3>APIs</h3><p>Services that call each other.</p></div>
  <div class="card"><h3>Database</h3><p>Where durable data lives.</p></div>
  <div class="card"><h3>Cache</h3><p>Fast access to repeated reads.</p></div>
  <div class="card"><h3>Configuration</h3><p>URLs, credentials, and settings.</p></div>
  <div class="card"><h3>Startup</h3><p>What must be ready first?</p></div>
  <div class="card"><h3>Networking</h3><p>How does one service find another?</p></div>
  <div class="card"><h3>Observability</h3><p>Logs, traces, and health.</p></div>
  <div class="card"><h3>Deployment</h3><p>Where will the pieces run?</p></div>
</div>
<br>

<p class="callout">The hard part is often wiring the pieces together.</p>

<!--
TIEMPO: 1:30 – 3:00

No todas las aplicaciones necesitan todo esto. Pero en cuanto hay más de un proceso, aparecen
dependencias, configuración y preguntas de diagnóstico además del código de negocio.
-->

---

## The problem: too much glue

<div class="cols">
  <div>
    <ul>
      <li>Start the database and cache in the right order.</li>
      <li>Give each API the right connection details.</li>
      <li>Make one service discover another.</li>
      <li>Find one request across multiple logs.</li>
    </ul>
  </div>
  <div class="card">
    <h3>Without a shared app model</h3>
    <p>Every developer reconstructs the system from scripts, environment files, and tribal knowledge.</p>
    <p class="big">The app is small.<br>The glue multiplies.</p>
  </div>
</div>

<!--
TIEMPO: 3:00 – 4:30

Conectar con la experiencia de la audiencia: arrancar servicios manualmente, copiar connection
strings, no saber cuál API falló. Aspire ayuda a describir y ejecutar el conjunto.
-->

---

## What is .NET Aspire?

<p class="big">Open-source tooling to build, run, observe, and deploy distributed applications.</p>

<div class="cards">
  <div class="card">
    <h3>One app model</h3>
    <p>Describe projects, containers, and dependencies in an AppHost.</p>
  </div>
  <div class="card">
    <h3>One local workflow</h3>
    <p>Start the system together and inspect it in the dashboard.</p>
  </div>
</div>
<br>

<p class="small">Aspire is not a web framework, a cloud provider, or a replacement for Docker.</p>

<!--
TIEMPO: 4:30 – 6:00

Aspire no cambia cómo escribimos los endpoints. Es tooling alrededor de la aplicación para
coordinar recursos, configuración, service discovery y observabilidad.
-->

---

<!-- _class: code-title -->

# Show me the code

<!--
TIEMPO: 6:00 – 24:00

Esta es la única diapositiva para la demo. No explicar aquí la arquitectura paso a paso: abrir el
código, ejecutar `cd src; aspire run` y hacer la demo en vivo.
-->

---

## Generate Docker Compose artifacts

<div class="cols">
  <div class="card">
    <h3>What you get</h3>
    <p>A Compose file and environment template under <code>AppHost/aspire-output</code>.</p>
  </div>
  <div class="card">
    <h3>What it means</h3>
    <p>A deployment artifact for the same set of services — not an AWS deployment.</p>
  </div>
</div>
<br>

<p class="small">Optional local run: <code>aspire deploy</code> uses the configured local Docker Compose environment.</p>

<!--
TIEMPO: 24:00 – 27:00

Mostrar el compose generado y qué servicios aparecen: api, sub-api, PostgreSQL y Redis.
No ejecutar comandos contra AWS.
-->

---

## AWS deployment choices

<div class="cards aws-options">
  <div class="card">
    <h3>EC2 + Docker Compose</h3>
    <p>Reuse Compose on one virtual machine. Easiest to explain; you own the machine and operations.</p>
  </div>
  <div class="card">
    <h3>ECS + Fargate</h3>
    <p>Run containers without managing EC2 hosts. A good managed-container path; add networking and load balancing.</p>
  </div>
  <div class="card">
    <h3>ECS + EC2</h3>
    <p>ECS schedules containers on instances you control. More capacity and cost choices, more operations.</p>
  </div>
  <div class="card">
    <h3>EKS</h3>
    <p>Kubernetes on AWS. Choose it when the team already needs the Kubernetes ecosystem.</p>
  </div>
</div>
<br>
<p class="small">Aspire describes the app; a deployment target maps it to AWS infrastructure. Compose is not Fargate.</p>

<!--
TIEMPO: 27:00 – 31:00

La opción más sencilla para enseñar el compose es EC2. Para una app gestionada, ECS/Fargate evita
administrar nodos. ECS sobre EC2 y EKS añaden control pero también operación. Aspire no transforma
mágicamente cualquier recurso: el target y la integración determinan el mapeo disponible.
-->

---

## Three things to remember

1. **Aspire** makes the relationships in a distributed app explicit.
2. **The dashboard** helps us follow resources, logs, and requests locally.
3. **Deployment is a separate choice**: generate Compose, choose AWS tooling, and map each resource.

<p class="big">Understand the system first. Then choose where it runs.</p>

<!--
TIEMPO: 31:00 – 33:00

Cerrar el hilo narrativo: problema de wiring, modelo en AppHost, demo local y opciones de despliegue.
-->

---

<!-- _class: section -->
<!-- _paginate: false -->

# Questions?

<p>Thank you · AWS Student Builder Group UEAB</p>

<!--
TIEMPO: 33:00 – 45:00

Preguntas. Si preguntan por despliegue AWS específico, aclarar que no se hizo hoy; hablar de EC2,
ECS/Fargate, ECS/EC2 o EKS según el nivel de operación que quieran asumir.
-->
