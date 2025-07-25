# Yearcakes Clock - Xbox Game Bar Widget

A customizable clock widget for Xbox Game Bar that displays the current time with configurable formatting, styling, and refresh rate.

## Features

- **Customizable Time Format**: Support for various time formats (12-hour, 24-hour, with/without seconds)
- **Adjustable Font Size**: Configurable font size for optimal readability
- **Background Opacity**: Adjustable background transparency (0–100%)
- **Refresh Rate Control**: Configurable update interval for performance optimization
- **Persistent Settings**: Configuration is saved and restored automatically

## Installation

### Prerequisites

- Windows 10 version 1903 or later
- Xbox Game Bar enabled (Settings > Gaming > Xbox Game Bar)

### From Microsoft Store

Go to Microsoft Store and install the latest version of [Yearcakes Clock](https://apps.microsoft.com/detail/9P7FJWPVJF84)

## Usage

1. Press `Win + G` to open Xbox Game Bar
2. Click the widget menu and select "Yearcakes Clock"
3. The clock widget will appear and can be moved and resized

## Configuration

The widget can be configured by editing the `config.json` file located in the app’s local data folder:

```
C:\Users\%USERNAME%\AppData\Local\Packages\7363Zhaozr.YearcakesClock_960x246vj7j58\LocalState\config.json
```

Example:

```json
{
  "TimeFormat": "HH:mm:ss",
  "Refresh": 100,
  "FontSize": 16,
  "Opacity": 0
}
```

Please ensure the JSON is valid. After making changes, restart the widget to apply the new settings.

### Configuration Options

| Option       | Description                     | Default      | Valid Values              |
| ------------ | ------------------------------- | ------------ | ------------------------- |
| `TimeFormat` | .NET DateTime format string     | `"HH:mm:ss"` | Any valid DateTime format |
| `Refresh`    | Update interval in milliseconds | `100`        | Positive integer          |
| `FontSize`   | Font size for time display      | `16`         | Positive integer          |
| `Opacity`    | Background opacity percentage   | `0`          | 0–100                     |

### Common Time Formats

- `HH:mm:ss` – 24-hour format with seconds (14:30:45)
- `HH:mm` – 24-hour format without seconds (14:30)
- `h:mm:ss tt` – 12-hour format with seconds (2:30:45 PM)
- `h:mm tt` – 12-hour format without seconds (2:30 PM)

## Development

### Key Components

- **App.xaml.cs**: Handles Xbox Game Bar widget activation and lifecycle
- **Widget1.xaml.cs**: Main widget logic, configuration loading, and time display
- **JsonObject.cs**: Configuration data model
- **Package.appxmanifest**: Defines the Xbox Game Bar widget extension

### Building

1. Ensure you have the Windows 10 SDK installed
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Build the solution

### Dependencies

- Microsoft.Gaming.XboxGameBar
- Microsoft.NETCore.UniversalWindowsPlatform
- Newtonsoft.Json

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Reporting Issues

Please report bugs and feature requests via the [GitHub Issues](https://github.com/Yearcakes-Studios/Clock/issues) page.
Include your Windows version, Xbox Game Bar version, and a clear description of the issue.

## License

This project is licensed under the Apache License – see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to the Xbox Game Bar team for providing the widget platform
- Built with love for the gaming community ❤️

## Support & Contact

If you have any issues or questions, feel free to:

1. Check the [Issues](https://github.com/Yearcakes-Studios/Clock/issues) page
2. Open a new issue if needed
3. Or email us at: **[yearcakes@gmail.com](mailto:yearcakes@gmail.com)**

---

**Note**: This widget is not affiliated with Microsoft or Xbox. It is a community-created tool for Xbox Game Bar users.

---

✨ Made by Yearcakes Studios, for gamers like you.
