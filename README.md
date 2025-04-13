# DigitalGarden

A comprehensive plant care management application with scheduling features, community sharing, and growth tracking.

![Screenshot 2025-04-04 170949](https://github.com/user-attachments/assets/3c7db56d-2c8d-478e-ab2f-3444b6ea1700)

## Overview

DigitalGarden is a web application designed to help plant enthusiasts manage and track the growth of their plants. Built with ASP.NET MVC and C#, the application provides a complete solution for maintaining plant care schedules, monitoring growth progress, and connecting with a community of fellow plant lovers.

## Features

### Plant Management
- Create, edit, and delete plant profiles
- Upload photos of your plants
- Track plant species, age, and health information
- Organize plants by location or type

### Care Scheduling
- Set watering schedules tailored to each plant
- Create sunlight exposure reminders
- Schedule fertilizing and repotting tasks
- Receive notifications for upcoming care tasks

### Growth Tracking
- Maintain a care log for each plant
- Document growth milestones
- Record treatments and interventions
- Track changes over time with photo history

### Community Features
- Share plant care tips with other users
- Ask questions about plant problems
- Browse community posts by plant type
- Save helpful tips to your profile

### User Profiles
- Personalized dashboard displaying your plant collection
- Achievement system for plant care milestones
- Customizable notification preferences
- Plant care statistics and insights

## Technical Details

- **Framework**: ASP.NET MVC
- **Language**: C#
- **Frontend**: HTML5, CSS3, JavaScript (SPA architecture)
- **Styling**: Bootstrap for responsive design
- **Authentication**: Google Auth integration
- **Database**: SQL Server
- **Hosting**: Microsoft Azure
- **Architecture**: Single Page Application (SPA)

## Installation and Setup

### Prerequisites
- Visual Studio 2019 or newer
- .NET Core 5.0 or newer
- SQL Server (local or Azure)
- Google Developer Account (for Auth setup)

### Local Development Setup
1. Clone the repository:
   ```
   git clone https://github.com/JustArmaan/Digital-Garden.git
   ```
2. Open the solution file in Visual Studio
3. Restore NuGet packages
4. Update the connection string in `appsettings.json` to point to your local SQL Server
5. Set up Google Authentication:
   - Add the credentials to your `appsettings.json`
6. Run the database migrations
7. Build and run the application

### Live Link
https://digitalgarden-e8a0h9g0gtepc9av.canadacentral-01.azurewebsites.net/?page=%2F


### Daily Use
1. Check your dashboard for upcoming care tasks
2. Log any care activities performed
3. Update plant growth information as needed
4. Interact with the community by sharing tips or asking questions

## Future Improvements

- Mobile application (iOS/Android)
- Plant recognition using AI/ML
- Integration with smart home systems for automated care
- Weather integration for more accurate care recommendations
- Expanded community features with plant trading marketplace

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Contact

For questions or feedback, please reach out through GitHub or open an issue in the repository.
