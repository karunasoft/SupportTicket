SupportTicket
=============


This is a project to demonstrate some of the technologies that I am working on, using DevOps best practices (broadly based upon the [Twelve Factor App approach](https://12factor.net/)).  

## Running Deployments

### AWS
* http://react.ticket-track.com
* http://webapi.ticket-track.com

### Azure
* http://dev.react.ticket-track.com
* http://dev.webapi.ticket-track.com

## Overview
Support Ticket is a simple application which allows you to capture support tickets into a database. You can add, view and edit tickets. 

## Authentication
You need to register, and sign in order to capture tickets.  The authentication is implemented on the browser (using JWT tokens) as well as in the back end, using .Net Core "Authorize" attributes and JWT integration.

## Kubernetes
The application demonstrates full DevOps automation from end to end, allowing you to deploy to multiple Kubernetes cloud hosts (AWS and Azure AKS are supported).

## Versioning
The deployed version appears at the foot of each of the pages of the application e.g. **Docker: virasana/streact:2.0.20190103.3**

## Technologies and architecture
It is a React front end, with a .Net Core REST API. There is also a legacy .Net Core front end. 
There are three Docker containers (all Linux): 

* React front end
* .Net Core Web API with Entity Framework Core
* SQL Server

All dev and test is done using the Microsoft Linux SQL Server image.  (Containers facilitate parity between dev and test - we see a great reduction in configuration drift!)

Production uses a managed SQL Server database, hosted on AWS using their Relational Database Service (RDS).

### Mobile Phones
responsive design supports mobile phone displays - see the React hamburger menu (tested on my Android).  

## Devops Practices
### Configuration as Code
The application is automated from top to bottom (I term this "Systems As Code" - see [here](https://www.slideshare.net/virasana/clipboards/systems-as-code-a-model-for-devops-automation?rftp=success_toast)).  

### Build Once, Deploy Many
Build once on a build server.  This produces a set of build artifacts, sometimes called the "Drop".   You can then take this "Drop", configure it, and deploy it to all of your environments.  You should not need to rebuild the application in order to reconfigure it, and nor should you tinker with a build package after it has been built.  Configuration is "orthogonal" to the build.  Always build and test the same artifact - don't rebuild to push the code to QA!

### "F5" Developer Experience
* The developer should be able to download the code and be up and running with the minimum of fuss (i.e. if using Visual Studio, you would hit F5 and go).
* The developer machine should be configured as close to Production as possible.  Containers offer a big leap forward in the right direction.  (They do not eliminate configuration differences though!)

### Zero-Downtime Upgrades
Also known as "rolling upgrades".  You should be able to roll out a new version of the application withouth having to pull down the system!  Kubernetes is the winner here!  The database changes need to be carefully thought-through, and you will need to be able to support at least two schemas at the same time, as you roll a new version.

### Ability to deploy "n" number of environments 
With the container-based kubernetes orchestration in place, it is well possible to provision new environments in the cloud.  Self service for developers is becoming a reality.


See the whole presentation (in the context of Azure Service Fabric) [Deployment for Devops](https://www.slideshare.net/virasana/deployment-for-dev-ops-with-service-fabric-127250670).








