# PocketGraph - Neo4j Mobile Client

[![Deploy GitHub Pages](https://github.com/kl0070/xamarin-neo4j/actions/workflows/deploy-pages.yml/badge.svg)](https://github.com/kl0070/xamarin-neo4j/actions/workflows/deploy-pages.yml)

A powerful mobile Neo4j database client for iOS and iPad, built with Xamarin.Forms.

[![Download on the App Store](https://tools.applemediaservices.com/api/badges/download-on-the-app-store/black/en-us?size=250x83&amp;releaseDate=1280278400)](https://apps.apple.com/nl/app/pocketgraph/id1604368926)

🌐 **[Visit our website](https://kl0070.github.io/xamarin-neo4j/)** for more information and beautiful screenshots!

## Overview

PocketGraph is a professional Neo4j graph database client that brings the power of Neo4j to your mobile device. Connect to your Neo4j instances securely via the lightning-fast Bolt protocol with SSL encryption, and manage your graph data on the go.

## Features

### 🔐 Secure Connections
- Connect via the lightning-fast Bolt protocol
- SSL/TLS encrypted connections
- Multiple connection management
- Database switching support

### 📊 Data Visualization
- **Graph View**: Visualize nodes and relationships in an interactive graph
- **Table View**: View query results in a structured table format
- Interactive data exploration

### ✍️ Advanced Query Editor
- Syntax highlighting for Cypher queries
- Query history and management
- Save and organize your queries
- Execute Cypher queries directly

### 🎨 Modern Interface
- Light and Dark theme support
- Responsive design for iPhone and iPad
- Intuitive navigation
- FontAwesome icon integration

### 📱 Platform Support
- iPhone (iOS 14.2+)
- iPad (iPadOS 14.2+)
- Apple Silicon Mac (macOS 11.0+)
- Apple Vision Pro (visionOS 1.0+)

## Project Structure

```
Xamarin.Neo4j/
├── Xamarin.Neo4j/           # Shared Xamarin.Forms project
│   ├── Controls/            # Custom UI controls
│   ├── Converters/          # XAML value converters
│   ├── Managers/            # Connection and query managers
│   ├── Models/              # Data models
│   ├── Pages/               # Application pages
│   ├── Services/            # Business logic services
│   ├── ViewModels/          # MVVM view models
│   └── Visualization/       # Graph visualization components
├── Xamarin.Neo4j.Android/   # Android-specific implementation
├── Xamarin.Neo4j.iOS/       # iOS-specific implementation
└── Xamarin.Neo4j.Tests/     # Unit tests
```

## Key Components

### Services
- **Neo4jService**: Handles all Neo4j database operations via Bolt protocol
- **ConnectionStringManager**: Manages saved connections
- **SavedQueryManager**: Organizes and stores queries

### Pages
- **ConnectionsPage**: Manage database connections
- **SessionPage**: Execute queries and view results
- **GraphPage**: Interactive graph visualization
- **TablePage**: Tabular data view
- **QueriesPage**: Saved queries management
- **SettingsPage**: Application settings

## Technology Stack

- **Framework**: Xamarin.Forms
- **Database**: Neo4j (Bolt Protocol)
- **Languages**: C#, XAML
- **Visualization**: Custom WebView integration with Neovis.js
- **Architecture**: MVVM (Model-View-ViewModel)

## Development

### Prerequisites
- Visual Studio 2019/2022 or Visual Studio for Mac
- Xamarin workload installed
- iOS development: macOS with Xcode
- Android development: Android SDK

### Building the Project

1. Clone the repository:
```bash
git clone https://github.com/kl0070/xamarin-neo4j.git
cd xamarin-neo4j
```

2. Open the solution:
```bash
open Xamarin.Neo4j/Xamarin.Neo4j.sln
```

3. Restore NuGet packages and build the solution

### Running Tests
```bash
dotnet test Xamarin.Neo4j/Xamarin.Neo4j.Tests/Xamarin.Neo4j.Tests.csproj
```

## Privacy

PocketGraph does not collect any user data. All connections and queries are stored locally on your device.

## About

PocketGraph is created by [Re: Software B.V.](https://resoftware.nl)

**Note**: The Neo4j Graph Database is a product created by Neo4j, Inc. Neo4j® and Cypher® are registered trademarks of Neo4j, Inc.

## License

See [LICENSE](LICENSE) file for details.

## Support

For support, please contact: info@resoftware.nl

---

**Available on the App Store** - [Download PocketGraph](https://apps.apple.com/nl/app/pocketgraph/id1604368926)
