namespace Redux.Maui;

public delegate Widget IndexedWidgetBuilder(BuildContext context, int index);

/// Define a base ListAdapter which is used for ListView.builder.
/// Many small listAdapters could be merged to a bigger one.
public class ListAdapter
{
    private int itemCount;
    private IndexedWidgetBuilder itemBuilder;

    ListAdapter(int itemCount, IndexedWidgetBuilder itemBuilder)
    {
        this.itemBuilder = itemBuilder;
        this.itemCount = itemCount;
    }
}