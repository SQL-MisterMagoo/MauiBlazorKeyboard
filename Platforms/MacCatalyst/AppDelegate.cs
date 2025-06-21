using Foundation;
using UIKit;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;

namespace MauiBlazorKeyboard
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // Enable keyboard adjustment for MacCatalyst
            UIKeyboard.Notifications.ObserveWillShow((sender, args) => {
                if (Microsoft.Maui.Controls.Application.Current?.Windows.Count > 0 && 
                    Microsoft.Maui.Controls.Application.Current.Windows[0]?.Page is not null)
                {
                    Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Padding = new Microsoft.Maui.Thickness(
                        0, 0, 0, args.FrameEnd.Height);
                }
            });
            
            UIKeyboard.Notifications.ObserveWillHide((sender, args) => {
                if (Microsoft.Maui.Controls.Application.Current?.Windows.Count > 0 && 
                    Microsoft.Maui.Controls.Application.Current.Windows[0]?.Page is not null)
                {
                    Microsoft.Maui.Controls.Application.Current.Windows[0].Page.Padding = new Microsoft.Maui.Thickness(0);
                }
            });
            
            return base.FinishedLaunching(app, options);
        }
    }
}
