
# Zoo Management System

A simple Animal Transfer System in .NET that facilitates the movement of animals to an empty zoo.

## Deployment

To deploy this project run

- Create database
```bash
  dotnet ef migrations add Initial
  dotnet ef database update
```
- Generate certificate
```bash
  dotnet dev-certs https --trust
```
- Building/running the project
```bash
  dotnet build
  dotnet run
```
Alternatively, run via Visual Studio in a Docker container for debugging.

## Features
The system considers specific guidelines for appropriate animal grouping during transfer process:

- The system takes into account the enclosures provided and animal count. All animals should fit.
- Animals are placed adhering to the rules:
    - Herbivore animals can be placed together in the same enclosures
    - Animals of the same species are not seperated
    - Maximum 2 groups of carnivore species in one enclosure


## REST API

### Animals

`GET /api/Animals` 

`GET /api/Animals/{id}`

`POST /api/Animals`

`POST /api/Animals/json` 

`PUT /api/Animals/{id}`

`DELETE /api/Animals/{id}`

### Enclosure

`GET /api/Enclosures` 

`GET /api/Enclosures/{id}`

`POST /api/Enclosures`

`POST /api/Enclosures/json` 

`PUT /api/Enclosures/{id}`

`DELETE /api/Enclosures/{id}`