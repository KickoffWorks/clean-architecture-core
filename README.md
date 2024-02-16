# clean-architecture-core
Repository with inner core libraries. Infrastructure and Domain libraries alongside Architectural, Integration and Unit tests.

## Objective

This aims to solve the issue of not having a common Domain / Infrastructure layer throughout multiple projects/repositories. This also implements clean architecture, having an infrastructure layer that communicates with external services like database, message queues, etc. And a Domain layer where you can setup your entities' logic and models.

## Setup

### Architecture

We'll create a NuGet package and upload it to the Github Package registry in order to use it on our Sample.Core.Api project, which is in another solution.

- Sample.Core
	- Sample.Core.Infrastructure (depends on Domain)
	- Sample.Core.Domain
	
This way we can update, manage and share our core libraries with multiple projects/repositories.

- Sample.Core.Tests
	- Sample.Core.Tests.Architecture
	- Sample.Core.Tests.Domain
	- Sample.Core.Tests.Infrastructure
	- Sample.Core.Tests.Infrastructure.Integration
	
### Installation

To install / use make sure you either clone, fork or use this repository as a template and set up your CI/CD pipeline to build and pack the libraries separately and to push them to your Package Registry of preference.

Setup a repository actions secret in Github with the name NUGET_GITHUB_TOKEN and with your write/read permission personal access token so you can authenticate to your NuGet source. Also make sure to update the username in the source, in the github workflow file.

In the case of future updates, make sure you set some kind of automatic versioning for your NuGet packages or update/add the line below to the libraries' .csproj

```
<Version>1.0.0</Version>
```

### Next Step

The next step will be to import this NuGet package in our Sample.Users.Api project, which is another repository.

You can find the repo here.

https://github.com/KickoffWorks/clean-architecture-api