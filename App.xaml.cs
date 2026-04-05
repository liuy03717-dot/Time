namespace TimeApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        //Make sure the package is within the NavigationPage.
        return new Window(new NavigationPage(new MainPage()));
    }
}
