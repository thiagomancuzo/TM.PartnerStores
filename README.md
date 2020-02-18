# Partner Stores

This is an WebAPI application that implements

- Creation of partners;
- Retrieve a partner by Id
- Search a partner by location (longitude / latitude)

## Application design

### Performance

This project delivers a great performance once it was built over aspnet core platform, that is a pretty fast one and also follow the best performance pratices, e.g. singleton DI, over string eviction and over enumeration eviction.
In a extreme performance scenario, I could separate the partner search responsability of this platform and include into another one built over GoLang (like a nano-service). The aspnet core one should be responsible for manage the partners in a relational database with mutch more fields and the GoLang nano-service one should separatelly serve the search feature with independent scalling and its own non-relational database that should be filled by a repplication triggered by a CQRS pattern in the aspnet core app. This approach will be considered in another sprint :)

### Concepts 

The objective of this implementation is to establish a pattern to turn the features able to grow up easily and consistently.  
For this, the follow concepts was applied:  

- SOLID
- DDD (a little bit part of that whole universe)
- Hexagonal Architecture

I tried to keep the domain objects integrity as well as possible. They have theirs own validations, value objects, business-based structures without any link with technologies. E.g. MultiPolygon was abstracted to don't keep any dirty from tech models. It was necessary to write parsers in the upper and lower layers because of that, which made the hexagonal architecture possible.

### Deploying

The app was wrapped in a `docker` container with mongodb dependency.  
To depoy the app run de commande below:  

```bash
docker-compose up
```

and access http://localhost:12312/partners  

> To change any configurations you can access the `docker-compose.yaml` file on the repository root folder or access the application configuration file `src/Platform/TM.PartnerStores.Platform/appsettings.json` (maybe you need to configure a different mongodb address or port)

### Debugging

Requirements:

- [VSCode](https://code.visualstudio.com/download)
- [dotnet SDK (v3.1)](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.101-windows-x64-installer)
- mongodb (or an already running container image)

In the folder `src/Platform/TM.PartnerStores.Platform/`, run:

```bash
dotnet run
```

Or open the repository root folder with VSCode and press F5!

Access http://localhost:5000/partners  

> To change any configurations you can access the application configuration file `src/Platform/TM.PartnerStores.Platform/appsettings.json` (you'll need to configure a different mongodb address or port)

### Documentation

This project includes OpenAPI docs available on http://localhost:12312/swagger

Endpoints:

POST `/partners/{partner object}` - create a partner  
GET  `/partners` - list partners  
GET  `/partners/{id}` - retrieve one partner by id  
GET  `/partners/search?lng={lng}&lat={lat}` - search one partner by geo position  


### Testing

Run tests with  

```bash
dotnet test src/UnitTests/TM.PartnerStores.UnitTests/TM.PartnerStores.UnitTests.csproj
```

or 

```bash
dotnet tool install --global coverlet.console --version 1.7.0
dotnet test src/UnitTests/TM.PartnerStores.UnitTests/TM.PartnerStores.UnitTests.csproj  /p:CollectCoverage=true
```

this 2nd command generates a table with code coverage table.  
The tests was written for domain layer, that is the one that have most sensitive rules.

| Module                              | Line   | Branch | Method |
|-------------------------------------|--------|------- |--------|
| TM.PartnerStores.Domain             | 80%    | 92%    | 79,59% |
