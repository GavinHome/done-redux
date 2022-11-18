namespace Redux.Routes;

/// Define a basic behavior of routes.
public abstract class AbstractRoutes
{
    public abstract dynamic buildPage(String path, dynamic arguments);
}

/// Each page has a unique store.
public class PageRoutes : AbstractRoutes
{
    private IDictionary<String, Page<Object, dynamic>> _pages;

    public PageRoutes(IDictionary<String, Page<Object, dynamic>> pages)
    {
        this._pages = pages;
    }

    public override dynamic buildPage(string path, dynamic arguments) => _pages[path]?.buildPage(arguments);
}
