# Documentation: WarehouseSchedule Plugin

WarehouseSchedule is a nopCommerce 4.90 plugin that adds a weekly schedule panel to the admin warehouse edit screen.
It lets administrators define whether each day is active and configure opening and closing times for the warehouse.

# Compilation and installation

Build the plugin with the nopCommerce 4.90.4 solution or from this plugin folder:

```powershell
dotnet build Nop.Plugin.Widgets.WarehouseSchedule.csproj
```

The plugin output is copied to `Presentation/Nop.Web/Plugins/Widgets.WarehouseSchedule`.
Install it from the admin area under Configuration / Local plugins.

# Fields and values

1. Is Active: Indicates whether the day is active.
2. Day: Indicates the weekday being configured.
3. Begin time: Indicates when the warehouse opens on that day.
4. End time: Indicates when the warehouse closes on that day.
