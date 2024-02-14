namespace Week3Lab.Services;

public class NavigationService : INavigationService
{
    public Task<Page> GoBackAsync(bool animate)
    {
        return Shell.Current.Navigation.PopAsync(animate);
    }

    public Task GoToAsync(string state, bool animate, IDictionary<string, object> parameters)
    {
        return Shell.Current.GoToAsync(state, animate, parameters);
    }

    public Task GoToAsync(string state, IDictionary<string, object> parameters)
    {
        return Shell.Current.GoToAsync(state, parameters);
    }

    public Task GoToAsync(string state, bool animate)
    {
        return Shell.Current.GoToAsync(state, animate);
    }

    public Task GoToAsync(string state)
    {
        return Shell.Current.GoToAsync(state);
    }
}