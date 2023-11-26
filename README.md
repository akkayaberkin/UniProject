# CourseManagement

This project integrates RabbitMQ, PostgreSQL, Redis, Keycloak, and Docker for data processing and user management. It employs a layered architecture and a generic repository pattern.

### Core Components

- **RabbitMQ**: For message queue management.
- **PostgreSQL**: Database management system.
- **Redis**: Data storage and caching system.
- **Keycloak**: User authentication and authorization.
- **Docker**: Containerization and isolated environment execution of the application.

### Architecture and Design Patterns

- **Layered Architecture**: Modular and understandable structure.
- **Generic Repository Pattern**: Abstraction of data access codes for maintainability.

### Usage and Integrations

- **RateLimiter as Middleware**: Managing incoming requests by recording them in Redis based on IP address and then transferring to RabbitMQ.
- **RabbitMQ Integration**: Transferring requests written in Redis to RabbitMQ for processing.
- **Keycloak Integration**: Implementing user management and security protocols.
- **Docker Integration**: Running and managing the application within Docker containers.
