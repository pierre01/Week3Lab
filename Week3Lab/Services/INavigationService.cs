namespace Week3Lab.Services;

public interface INavigationService
{
    Task GoToAsync(string state, bool animate, IDictionary<string, object> parameters);
    Task GoToAsync(string state, IDictionary<string, object> parameters);
    Task GoToAsync(string state, bool animate);
    Task GoToAsync(string state);
    Task<Page> GoBackAsync(bool animate);
}