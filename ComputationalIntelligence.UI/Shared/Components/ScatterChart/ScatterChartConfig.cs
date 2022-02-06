namespace ComputationalIntelligence.UI.Shared.Components.ScatterChart
{
    public class ScatterChartConfig
    {
        public ScatterChartConfig()
        {
            Type = "scatter";
        }

        public string Type { get; }

        public ScatterChartData Data { get; set; }

        public ScatterChartOptions Options { get; set; }
    }
}
