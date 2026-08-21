# LeoClinic

## Overview

LeoClinic is a .NET 9 clinic management API built with Clean Architecture.

## Features

- User registration, login, email verification, and password reset
- JWT authentication with refresh tokens
- Patient management and patient profiles
- Doctor management, doctor profiles, availability, slots, and locations
- Appointment scheduling and management
- Rating management
- Payment processing with Stripe and webhook handling
- Notifications
- Admin dashboard and reporting
- Swagger/OpenAPI documentation
- SQL Server persistence with Entity Framework Core
- FluentValidation for request validation
- AutoMapper for object mapping

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- SQL Server
- Entity Framework Core
- Repository Pattern
- Swagger / OpenAPI
- FluentValidation
- AutoMapper
- JWT Authentication
- Stripe payments
- SMTP email delivery
- DotNetEnv for local environment configuration

## Architecture

- Clean Architecture

## Project Structure

- `LeoClinic.API` - API host, controllers, middleware, Swagger, and authentication
- `LeoClinic.Application` - application services, DTOs, validation, and mappings
- `LeoClinic.Domain` - entities, enums, and core business rules
- `LeoClinic.Infrastructure` - EF Core data access, repositories, and external integrations
- `LeoClinic.Shared` - shared contracts and common utilities

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server

### Installation

```bash
git clone https://github.com/techmasterycompany-star/LeoClinic_.net.git
cd TechMasteryProjectOne
dotnet restore
```

### Configuration

Set the required values in `LeoClinic.API/appsettings.json` or environment variables before running the application:

- `ConnectionStrings__DefaultConnection`
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__Key`
- `EmailSettings__SmtpServer`
- `EmailSettings__SenderEmail`
- `EmailSettings__Username`
- `EmailSettings__Password`
- `Stripe__SecretKey`
- `Stripe__PublishableKey`
- `Stripe__WebhookSecret`

### Database Setup

Apply the Entity Framework Core migrations before running the API:

```bash
dotnet ef database update --project LeoClinic.Infrastructure --startup-project LeoClinic.API
```

### Run

```bash
dotnet run --project LeoClinic.API
```

## API Documentation

- Swagger: available when running the API in development mode
- Postman documentation: https://documenter.getpostman.com/view/46257067/2sBYApxsdV

## Deployment

Live application: http://leoclinic.runasp.net/


