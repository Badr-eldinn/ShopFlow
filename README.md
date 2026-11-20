# ShopFlow

E-commerce cart API built with ASP.NET Core 10.

## Overview

ShopFlow is a backend API for shopping cart operations. The code is organized by feature (for example `Features/AddToCart`), so each use case keeps its endpoint, validation and logic together instead of spreading them across layers.

## Tech stack

- C# / .NET 10
- ASP.NET Core (controllers + feature endpoints)
- FluentValidation for request validation
- Swashbuckle (Swagger UI) + Microsoft.OpenApi

## Running locally

1. Clone the repo:
   ```
   git clone https://github.com/Badr-eldinn/ShopFlow.git
   ```
2. Run the API:
   ```
   cd ShopFlow
   dotnet run
   ```
3. Open Swagger UI at `/swagger` to explore and try the endpoints.

## API documentation

All endpoints are documented through Swagger/OpenAPI. With the app running in Development, the interactive UI is available at `/swagger`.
