# GoalTrackerMVVM

A modern goal tracking application built with .NET MAUI, demonstrating the MVVM (Model-View-ViewModel) pattern with SQLite data persistence.

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?logo=windows)

## ?? About

GoalTrackerMVVM is a personal goal management application that helps you track your goals, monitor progress, and stay motivated. The app showcases modern .NET MAUI development practices including:

- **MVVM Architecture** - Complete separation of concerns using ViewModels
- **SQLite Database** - Local data persistence with async operations
- **Dependency Injection** - Proper service registration and lifetime management
- **CommunityToolkit.Mvvm** - Leveraging source generators for boilerplate-free MVVM code
- **Responsive UI** - Beautiful, theme-aware interface with progress visualization

## ? Features

### Core Functionality
- ? **Create Goals** - Set up new goals with name, motivation, steps, and target date
- ? **View Goals** - Browse all your goals with progress indicators
- ? **Edit Goals** - Update goal details and adjust progress with a slider
- ? **Delete Goals** - Remove goals you no longer need
- ? **Progress Tracking** - Visual progress bars with 5% increment adjustments
- ? **Data Persistence** - All data saved to SQLite database

### UI/UX Features
- ?? **Dark/Light Theme Support** - Automatically adapts to system theme
- ?? **Progress Visualization** - ProgressBar components show completion status
- ?? **Pull-to-Refresh** - Swipe down to reload goals from database
- ?? **Smartphone Window Size** - Optimized 393x852 window for quick testing
- ?? **Clean Card-Based UI** - Modern, readable interface with rounded corners

## ??? Architecture

### Project Structure
```
GoalTrackerMVVM/
??? Models/
?   ??? Goal.cs                          # Data model with SQLite attributes
??? ViewModels/
?   ??? MainViewModel.cs                 # Main page logic and commands
?   ??? DetailViewModel.cs               # Goal detail and edit logic
?   ??? AddGoalViewModel.cs              # New goal creation logic
??? Views/
?   ??? MainPage.xaml                    # Goal list view
?   ??? DetailPage.xaml                  # Goal detail/edit view
?   ??? AddGoalPage.xaml                 # New goal form
??? Services/
?   ??? GoalDatabase.cs                  # SQLite database operations
??? Converters/
?   ??? StringNullOrEmptyBoolConverter.cs # XAML visibility converter
??? Resources/
?   ??? Styles/                          # Colors and style definitions
??? Constants.cs                         # Database configuration
??? MauiProgram.cs                       # DI and app configuration
??? AppShell.xaml                        # Shell navigation setup
```

### MVVM Pattern Implementation

**ViewModels** use `CommunityToolkit.Mvvm` attributes:
- `[ObservableProperty]` - Auto-generates properties with INotifyPropertyChanged
- `[RelayCommand]` - Auto-generates ICommand implementations
- `[QueryProperty]` - Receives navigation parameters

**Example:**
```csharp
public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isRefreshing;

    [RelayCommand]
    async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(AddGoalPage)}");
    }
}
```

## ??? Database

**SQLite** is used for local data persistence:
- **Location:** `FileSystem.AppDataDirectory/GoalTrackerSQLite.db3`
- **ORM:** `sqlite-net-pcl` with async/await support
- **Auto-initialization:** Database and tables created on first run
- **Sample data:** 5 pre-populated goals for demonstration

**Database Operations:**
```csharp
await _database.GetGoalsAsync();           // Read all goals
await _database.GetGoalAsync(int id);      // Read single goal
await _database.SaveGoalAsync(Goal goal);  // Create or update
await _database.DeleteGoalAsync(Goal goal);// Delete goal
```

## ?? Getting Started

### Prerequisites
- Visual Studio 2022 (17.8 or later)
- .NET 9 SDK
- Windows 10/11 (version 1809 or later)

### Running the Application

1. **Clone the repository:**
   ```bash
   git clone https://github.com/zawhtut/GoalTrackerMVVM.git
   cd GoalTrackerMVVM
   ```

2. **Open in Visual Studio:**
   - Open `GoalTrackerMVVM.sln`

3. **Restore NuGet packages:**
   - Packages will restore automatically, or run:
   ```bash
   dotnet restore
   ```

4. **Build and Run:**
   - Press `F5` or click the **Run** button
   - The app window will open at 393x852 (smartphone size for testing)

## ?? NuGet Packages

| Package | Version | Purpose |
|---------|---------|---------|
| `CommunityToolkit.Mvvm` | 8.4.0 | MVVM helpers and source generators |
| `sqlite-net-pcl` | 1.9.172 | SQLite ORM for .NET |
| `SQLitePCLRaw.bundle_green` | 2.1.2 | SQLite native binaries |

## ?? Platform Support

### Currently Enabled
- ? **Windows** - Fully configured and ready to run

**Why Windows-only?** For faster development and testing cycles. Windows builds are significantly faster than mobile platform builds, making it ideal for rapid development iteration.

### Enabling Additional Platforms

The project is built with .NET MAUI and can easily target multiple platforms. To enable Android, iOS, and macOS:

1. **Open** `GoalTrackerMVVM.csproj`
2. **Uncomment** this line (around line 3):
   ```xml
   <!--<TargetFrameworks>net9.0-android;net9.0-ios;net9.0-maccatalyst</TargetFrameworks>-->
   ```
   Should become:
   ```xml
   <TargetFrameworks>net9.0-android;net9.0-ios;net9.0-maccatalyst</TargetFrameworks>
   ```
3. **Save** the file and reload the project
4. **Select** your target platform from the dropdown (Android/iOS/Mac Catalyst)

**That's it!** The app uses .NET MAUI standard APIs and will work cross-platform without code changes.

### Platform Requirements
- **Android** - Requires Android SDK (installable via Visual Studio)
- **iOS** - Requires macOS with Xcode and paired Mac
- **macOS** - Requires macOS to build and run

## ?? Usage

### Creating a Goal
1. Click the **"+ New Goal"** button
2. Fill in:
   - **Goal Name** (required)
   - **Motivation** (optional)
   - **Steps to Achieve** (optional)
   - **Target Date**
3. Click **"Save Goal"**

### Viewing Goal Details
1. Tap any goal card in the main list
2. View all goal information and current progress

### Editing a Goal
1. Open a goal's detail page
2. Click **"Edit Goal"**
3. Modify any fields or adjust progress with the slider (5% increments)
4. Click **"Save Changes"** or **"Cancel"**

### Deleting a Goal
1. In the main list, click the **???** button on any goal card
2. The goal is immediately deleted from both UI and database

### Refreshing the List
- Pull down on the goal list to refresh from the database

## ?? Customization

### Window Size
The app opens at smartphone dimensions for quick testing. To change:

**File:** `App.xaml.cs`
```csharp
window.Width = 393;   // Adjust width
window.Height = 852;  // Adjust height
```

### Theme Colors
Modify colors in `Resources/Styles/Colors.xaml`:
```xml
<Color x:Key="Primary">#512BD4</Color>
<Color x:Key="MidnightDepth">#0A1818</Color>
```

### Sample Data
Initial goals are defined in `MainViewModel.cs` in the `SeedDatabaseAsync()` method.

## ?? Testing

The app includes:
- ? Form validation (goal name required)
- ? Null safety checks throughout
- ? Error handling for database operations
- ? User-friendly error messages

## ?? Code Conventions

Following C# and .NET MAUI best practices:
- **PascalCase** for public members
- **camelCase** with `_` prefix for private fields
- **Async/await** for all I/O operations
- **ObservableCollection** for data-bound collections
- **Dependency Injection** for services

## ?? Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ?? License

This project is open source and available for educational purposes.

## ?? Acknowledgments

- Built with [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- MVVM helpers from [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
- Database powered by [SQLite](https://www.sqlite.org/)

## ?? Contact

**GitHub:** [@zawhtut](https://github.com/zawhtut)  
**Project Link:** [https://github.com/zawhtut/GoalTrackerMVVM](https://github.com/zawhtut/GoalTrackerMVVM)

---

? If you find this project helpful, please consider giving it a star!
