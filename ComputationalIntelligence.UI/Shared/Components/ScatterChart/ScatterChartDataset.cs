namespace ComputationalIntelligence.UI.Shared.Components.ScatterChart
{
    public class ScatterChartDataset
    {
        public string Label { get; set; }

        public string BackgroundColour { get; set; }

        public string PointStyle { get; set; }

        public int PointRadius { get; set; }

        public List<ScatterChartPoint> Data { get; set; }
    }
}
