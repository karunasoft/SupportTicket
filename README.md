SupportTicket
=============


This is a project to demonstrate some of the technologies that I am working on, using DevOps best practices (broadly based upon the [Twelve Factor App approach](https://12factor.net/)) and [Systems as Code](https://jeanpierrefouche.wordpress.com/2019/01/29/systems-as-code/).  This project uses React, .Net Core, VSTS and Kubernetes, and runs on AWS and Azure.  Please contact me for a hands-on demonstration of the end-to-end pipeline. 

My general DevOps picture can be viewed in the first part of a presentation I delivered (in the context of Azure Service Fabric): [Deployment for Devops](https://www.slideshare.net/jeanpierrefouche/deployment-for-dev-ops-with-service-fabric-127250670).
See particularly my concept of ["Systems as Code"](https://www.slideshare.net/jeanpierrefouche/clipboards/systems-as-code-a-model-for-devops-automation)

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

The application supports storing security information in a separate database from the application data.  (Two connection strings)

## Versioning
The deployed version appears at the foot of each of the pages of the application e.g. **Docker: virasana/streact:2.0.20190103.3**  This makes it easy for developers to identify the source build (and code) of any issues in production.

## Technologies and architecture
It is a React front end, with a .Net Core REST API. There is also a legacy .Net Core front end. 
There are three Docker containers (all Linux): 

* React front end
* .Net Core Web API with Entity Framework Core
* SQL Server

All dev and test is done using the Microsoft Linux SQL Server image.  (Containers facilitate parity between dev and test - we see a great reduction in configuration drift!)

Production uses a managed SQL Server database, hosted on AWS using their Relational Database Service (RDS).

## Responsive Design
Responsive design supports mobile phone displays - see the React hamburger menu (tested on my Android).  

## Testing 
One of the challenges of testing has been to run the tests on the production environment (i.e. the container).  When it comes to the build server, we want to run the tests that were buily from the new source code, on the container.  This project demonstrates how to run the tests on the container, and to extract the results back to the host, for publication to VSTS/Azure Devops Services.

## Devops Practices
### Configuration as Code
The application is automated from top to bottom (I term this "Systems As Code" - see [here](https://www.slideshare.net/jeanpierrefouche/clipboards/systems-as-code-a-model-for-devops-automation)).  

### Build Once, Deploy Many
Build once on a build server.  This produces a set of build artifacts, sometimes called the "Drop".   You can then take this "Drop", configure it, and deploy it to all of your environments.  You should not need to rebuild the application in order to reconfigure it, and nor should you tinker with a build package after it has been built.  Configuration is "orthogonal" to the build.  Always build and test the same artifact - don't rebuild to push the code to QA!

### "F5" Developer Experience
* The developer should be able to download the code and be up and running with the minimum of fuss (i.e. if using Visual Studio, you would hit F5 and go).
* The developer machine should be configured as close to Production as possible.  Containers offer a big leap forward in the right direction.  (They do not eliminate configuration differences though!)

### Zero-Downtime Upgrades
Also known as "rolling upgrades".  You should be able to roll out a new version of the application withouth having to pull down the system!  Kubernetes is the winner here!  The database changes need to be carefully thought-through, and you will need to be able to support at least two schemas at the same time, as you roll a new version.

### Ability to deploy "n" number of environments 
With the container-based kubernetes orchestration in place, it is well possible to provision any number of new environments in the cloud. 

### Systems as Code
The aim of DevOps automation is to convert the entire software stack into code (insofar as this is feasible and beneficial to the business - note that in some cases it is better to continue with a manual approach!): ["Systems as Code"](https://www.slideshare.net/jeanpierrefouche/clipboards/systems-as-code-a-model-for-devops-automation). The modern system stack (consisting of the Infrastructure layer of Networks, Virtual Machines and Firewalls, the "Platform" or Operating System and Supporting Applications, the automated Deployment, Build and the Application Services should all be viewed as software artifacts, and can be version-controlled and evolved as code. We therefore have an entire system (including traditional hardware layers) built, configured, deployed and executed through code











