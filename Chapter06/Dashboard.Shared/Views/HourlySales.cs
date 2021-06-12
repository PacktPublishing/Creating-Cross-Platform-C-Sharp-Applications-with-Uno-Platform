namespace Dashboard.Views
{
public class HourlySales
{
    public HourlySales(string hour, double totalSales)
    {
        Hour = hour;
        TotalSales = totalSales;
    }

    public string Hour { get; set; }

    public double TotalSales { get; set; }
}
}
