# MauiBlazorKeyboard

A .NET MAUI Blazor application that demonstrates how to handle on-screen keyboard visibility on mobile devices to prevent input fields from being hidden by the keyboard.

## Problem Statement

In mobile applications with forms, the on-screen keyboard can often hide input fields, creating a poor user experience. This project demonstrates a native .NET MAUI solution to adjust the application layout when the keyboard appears.

## Implementation Details

This project implements platform-specific solutions to handle keyboard visibility:

### Android Implementation

- Modified `MainActivity.cs` to set `WindowSoftInputMode = SoftInput.AdjustResize` which instructs Android to resize the app window when the keyboard appears
- Added additional code in `OnCreate` method to ensure proper keyboard behavior:

```csharp
[Activity(WindowSoftInputMode = SoftInput.AdjustResize)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // Ensure the window adjusts when keyboard is shown
        Window?.SetSoftInputMode(SoftInput.AdjustResize);
    }
}
```
### iOS and MacCatalyst Implementation

- Modified `AppDelegate.cs` to observe keyboard show/hide events and adjust the page padding accordingly:

```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    // Enable keyboard adjustment
    UIKeyboard.Notifications.ObserveWillShow((sender, args) => {
        if (Microsoft.Maui.Controls.Application.Current?.Windows.Count > 0 && 
            Microsoft.Maui.Controls.Application.Current.Windows[0]?.Page is not null)
        {
            Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Padding = 
                new Microsoft.Maui.Thickness(0, 0, 0, args.FrameEnd.Height);
        }
    });
    
    UIKeyboard.Notifications.ObserveWillHide((sender, args) => {
        if (Microsoft.Maui.Controls.Application.Current?.Windows.Count > 0 && 
            Microsoft.Maui.Controls.Application.Current.Windows[0]?.Page is not null)
        {
            Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Padding = 
                new Microsoft.Maui.Thickness(0);
        }
    });
    
    return base.FinishedLaunching(app, options);
}
```

### Cross-Platform Service

- Created a `KeyboardHandler` service to provide a unified API for keyboard handling across platforms:

```csharp
public interface IKeyboardHandler
{
    void Initialize();
}

public class KeyboardHandler : IKeyboardHandler
{
    public void Initialize()
    {
        // Platform-specific implementations handle most of the keyboard behavior
    }
}
```

- Registered the service in `MauiProgram.cs`:
`builder.Services.AddSingleton<IKeyboardHandler, KeyboardHandler>();`
- Initialized the service in `App.xaml.cs`:

```csharp
public App(IKeyboardHandler keyboardHandler)
{
    InitializeComponent();
    
    _keyboardHandler = keyboardHandler;
    _keyboardHandler.Initialize();
}
```

## Testing

The solution can be tested by running the application on an Android or iOS device/emulator and focusing on input fields. The application should automatically adjust its layout to ensure the focused input field remains visible above the keyboard.

## Requirements

- .NET 9
- Compatible with Android, iOS, and MacCatalyst platforms