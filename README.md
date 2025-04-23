# Beer API

## Overview
The Beer API is a simple web API built using ASP.NET Core. It provides endpoints to fetch and filter beer products based on various criteria, such as price and sorting order. The project is designed to be easy to set up and run.

## Features
- Fetch a list of beer products.
- Filter beers by price per unit.
- Sort beers by price in ascending or descending order.
- Swagger UI for API documentation and testing.

## Prerequisites
- .NET 8.0 SDK or later installed on your system.
- A modern web browser to access the Swagger UI.

## Getting Started

### 1. Clone the Repository
Clone this repository to your local machine:
```bash
git clone <repository-url>
```

### 2. Navigate to the Project Directory
```bash
cd BeerApi
```

### 3. Build the Project
Run the following command to build the project:
```bash
dotnet build
```

### 4. Run the Project
Run the project using the following command:
```bash
dotnet run
```
Alternatively, you can use the provided VS Code task:
- Open the Command Palette in VS Code (`Ctrl+Shift+P` or `Cmd+Shift+P` on macOS).
- Select `Tasks: Run Task`.
- Choose `watch` to start the project in watch mode.

### 5. Access the API
Once the project is running, you can access the API at:
- Swagger UI: [https://localhost:5001/swagger](https://localhost:5001/swagger) (or the port specified in your launch settings).

## Configuration
The project uses a hardcoded URL for the beer data source. You can find and update this URL in the `ProductService` class if needed.

## Project Structure
- **Controllers/**: Contains API controllers.
- **Models/**: Contains data models for the application.
- **Services/**: Contains business logic and services.
- **Properties/**: Contains launch settings for the application.

## Notes
- The project is configured to run without requiring any changes.

## License
This project is licensed under the MIT License.