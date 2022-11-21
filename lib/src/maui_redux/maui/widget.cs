using System;
namespace Redux.Maui;

public abstract class Widget //: Microsoft.Maui.Controls.View
{
    public abstract Widget buildWidget();
}

public abstract class BuildContext
{
    protected abstract Widget widget { get; }
}

public abstract class State<T> where T : StatefulWidget
{
    public T widget => _widget!;
#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
    protected T? _widget;
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。

    protected virtual void initState()
    {
    }

    public abstract Widget build(BuildContext context);
}

public abstract class StatefulWidget : Widget
{
    //public abstract State<StatefulWidget> createState();
}
