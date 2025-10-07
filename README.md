Restaurant Hotel Ads Platform – Architecture & GitHub Setup Guide
1. Overview

The Restaurant Hotel Ads Platform is a modular system designed to connect restaurants with hotels through integrated digital portals.
Each hotel can display restaurant offers, discounts, and promotions on room TVs or internal dashboards.
The solution uses a Clean Architecture structure on the backend and modular Angular applications on the frontend to keep the project scalable and maintainable.

2. Project Folder Structure
RestaurantHotelAds/
├── Backend/
│   ├── RestaurantHotelAds.Core/
│   ├── RestaurantHotelAds.Infrastructure/
│   ├── RestaurantHotelAds.Application/
│   └── RestaurantHotelAds.API/
└── Frontend/
    ├── hotel-portal/
    ├── restaurant-portal/
    └── room-display/

🔹 Backend Layer (ASP.NET Core)
RestaurantHotelAds.Core/

This is the domain layer, the foundation of the entire backend system.
It defines what the system does, not how it does it.

Contents:

Domain entities (e.g., Hotel, Restaurant, Offer, RoomDisplay)

Interfaces (contracts) for repositories or services

Core business rules and constants

Purpose:
This layer contains pure business logic with no dependency on external frameworks. It can be reused in any other project.

RestaurantHotelAds.Infrastructure/

This layer handles data access and persistence.

Contents:

Entity Framework DbContext class

Database configuration (SQL Server provider setup)

Repository implementations

Migration files

Purpose:
Implements the interfaces from the Core project to communicate with the database.

Dependencies:

Depends on RestaurantHotelAds.Core

RestaurantHotelAds.Application/

This is the business logic layer, where most of the processing happens.

Contents:

Service classes implementing business rules

Data Transfer Objects (DTOs)

Application-specific logic (validation, mapping, etc.)

Purpose:
Acts as a bridge between the API and the data layer.
Contains logic such as “get all offers for a specific hotel,” “calculate active discounts,” etc.

Dependencies:

Depends on both Core and Infrastructure

RestaurantHotelAds.API/

This is the presentation layer (backend) — the actual Web API that clients connect to.

Contents:

API Controllers (e.g., HotelController, RestaurantController)

Authentication (JWT Bearer setup)

Swagger documentation

Application configuration files (Program.cs, appsettings.json)

Purpose:
Handles HTTP requests, routes them to application services, and returns JSON responses to frontend apps.

Dependencies:

Depends on all backend layers (Core, Application, Infrastructure)

🔹 Frontend Layer (Angular)
hotel-portal/

Angular app for hotel owners and administrators.

Use case:

Manage hotel information

View incoming restaurant advertisements

Schedule ad displays for room TVs

Structure inside:

src/ (Angular components, services, and modules)

environments/ (API URL configuration)

angular.json, package.json (project setup files)

restaurant-portal/

Angular app for restaurant owners.

Use case:

Create and manage ads/offers

Track performance and statistics

Configure timing and hotel collaboration

room-display/

Angular app for room TV screens or kiosks.

Use case:

Displays live restaurant offers to hotel guests

Runs in kiosk mode or as a web app on smart TVs

Fetches offers dynamically from the API

3. Layered Dependency Flow
Core  ←  Infrastructure  ←  Application  ←  API  ←  Frontend


Each arrow points to the layer it depends on.
This structure ensures separation of concerns — each layer knows only what it must, keeping the architecture clean, testable, and scalable.

4. GitHub Setup and Repository Management
🧭 The Problem

When creating the Angular apps using ng new, each app automatically initialized its own .git repository inside Frontend/.
So when you tried to run:

git add .


Git detected nested repositories and treated them as submodules instead of normal folders.
That’s why they appeared on GitHub as empty directories.

🧩 The Solution

Removed the submodules from Git’s index:

git rm --cached Frontend/hotel-portal
git rm --cached Frontend/restaurant-portal
git rm --cached Frontend/room-display


Deleted leftover internal .git folders:

rm -rf Frontend/hotel-portal/.git
rm -rf Frontend/restaurant-portal/.git
rm -rf Frontend/room-display/.git


Re-added the projects as normal directories:

git add Frontend/hotel-portal
git add Frontend/restaurant-portal
git add Frontend/room-display
git commit -m "Fix: re-added Angular projects as regular folders instead of submodules"
git push -u origin main --force


Verified on GitHub that all Angular project files were visible and accessible.

🧠 Future Tip

When creating new Angular apps inside a larger repository, always disable Git initialization:

ng new <project-name> --skip-git


This prevents nested repositories from being created inside your main project.

5. Recommended .gitignore for This Project

Here’s a clean and efficient .gitignore to keep your repo tidy:

# Ignore Visual Studio and build files
.vs/
bin/
obj/
*.user
*.suo

# Ignore logs and temp files
*.log
*.tmp
*.dbmdl

# Ignore compiled Angular output
Frontend/**/dist/
Frontend/**/build/
Frontend/**/.angular/
Frontend/**/.env
Frontend/**/.cache/
Frontend/**/coverage/

# Ignore node_modules
Frontend/**/node_modules/

# Ignore environment configuration
**/appsettings.Development.json
**/appsettings.Local.json

# Ignore system files
.DS_Store
Thumbs.db

# Ignore IDE folders
.idea/
.vscode/


This setup ensures only source code and configuration are committed — no compiled or unnecessary files.

✅ Summary

Backend: Clean layered architecture using .NET 8, Entity Framework, and JWT authentication.

Frontend: Three separate Angular apps for hotels, restaurants, and room displays.

GitHub: Unified into one clean repository, with submodule issues resolved and .gitignore optimized.

This document serves as both a technical reference for developers and a portfolio explanation of your architectural decisions.