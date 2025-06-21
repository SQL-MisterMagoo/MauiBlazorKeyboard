using MauiBlazorKeyboard.Services;

namespace MauiBlazorKeyboard
{
    public partial class App : Application
    {
        private readonly IKeyboardHandler _keyboardHandler;

        public App(IKeyboardHandler keyboardHandler)
        {
            InitializeComponent();
            
            _keyboardHandler = keyboardHandler;
            _keyboardHandler.Initialize();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new MainPage()) { Title = "MauiBlazorKeyboard" };
            
            // Set up window behavior
            window.Created += (s, e) => {
                // Window created
            };
            
            window.Activated += (s, e) => {
                // Window activated
            };
            
            return window;
        }
    }
}
