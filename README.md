#  Backend Developer Code Case

## Overview

The Configuration Management System is a dynamic configuration library, creating and updating configuration settings without requiring application restarts. The settings are stored in a database and are refreshed periodically.

## Features

- Dynamic configuration retrieval without application restarts
- Secure and efficient configuration management
- User interface for viewing, creating, searching and updating configuration settings
- MongoDB is used as the database.
- The ConfigurationReaderTask structure checks for changes in the database within a specified timeframe.

## Requirements

- The library must be written in .NET 8.
- The library should be able to work with the last successful configuration records when unable to access storage.
- The library should handle the conversion of return values for each type internally.
- The system should periodically check for new records and record changes based on a parameterized time interval.
- Each service should only be able to access its own configuration records and should not be able to view others' records.

## Project Structure

- **CQRS Architechture**

- **Application**: Manages database interactions using the repository pattern.
- **Domain**: Contains entities,DTOs and Enums.
- **Infrastructure**: The foundational components and resources (hardware, software, networks) that support the operation of a software application
- **Persistence**: The mechanism by which data is stored and retrieved from a storage system, typically a database, to maintain state across sessions or requests.
- **WebAPI**: ASP.NET Core application for managing configurations via a web interface.

- **UserInterface**: ASP.NET Core MVC application for managing configurations via a web interface.


