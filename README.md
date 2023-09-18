# Task Management API | TaskSync


A Task Management System developed using ASP.NET Core to efficiently organize, track, and manage tasks. Seamlessly handle assignments, deadlines, and progress monitoring. Enhance productivity and streamline workflows effortlessly.

## Features
+ Task Management: Create, update, delete, and retrieve.
+ Project Management: Organize tasks into projects, enabling better organization and tracking.
+ Notifications: Receive notifications for task assignments, deadlines, and other important events.
+ Background services
+ Global exception handling
+ Authentication and Authorization: Secure access to API endpoints with user authentication and role-based authorization.
+ Validation: Input data validation to ensure data integrity.
+ Documentation: API documentation for easy integration using Swagger.

## Prerequisites
Before you begin, ensure you have met the following requirements:

+  .NET 6 SDK
+  Visual Studio
 
## Getting Started
Follow these steps to set up and run the API locally:

1. Clone this repository:

```
git clone https://github.com/Charles-04/TaskManagementApi.git
cd TaskSync.API

```
2. Provide database connection string in appsettings.json or Leave as is to user in-app database
3. Apply database migrations to create the database:

```
Packager Manager : Update-Database
```
4. Build and run the API:

```
dotnet run
```
The API should be accessible at http://localhost:5000 (or https://localhost:5001 for HTTPS).

## API Endpoints
For detailed information about available API endpoints and their usage, refer to the API Documentation section or navigate to /swagger when the API is running locally.

## Authentication
To access protected endpoints, you must obtain an authentication token. Refer to the API documentation for authentication details.


## Documentation
API documentation can be found at /swagger when the API is running locally. For additional documentation and usage examples, please refer to API Documentation.

## Security
This project follows security best practices to protect against common vulnerabilities. Regularly update dependencies to address security concerns.

## Authors

👤 **Joshua Eze**

- GitHub: [@Allenkeys](https://github.com/Allenkeys)
- Twitter: [@jdgraay](https://twitter.com/jdgraay)
- LinkedIn: [Eze Joshua](https://linkedin.com/in/eze-joshua)

## Contributing
Contributions are welcome! 

## License
This project is licensed under the MIT License.

