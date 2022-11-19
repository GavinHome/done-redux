namespace example.Pages.Todos.ReportComponent;

internal class ReportState
{
    public int Total { get; set; }
    public int Done { get; set; }

    public ReportState(int total = 0, int done = 0)
    {
        this.Total = total;
        this.Done = done;
    }

    public String toString()
    {
        return $"ReportState total: {Total}, done: {Done}";
    }
}
