using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public partial class GaugeChartViewModel : ViewModelBase
    {
        public IEnumerable<ISeries> Series { get; set; } =
    GaugeGenerator.BuildSolidGauge(
        new GaugeItem(30, series => SetStyle("Vanessa", series)),
        new GaugeItem(50, series => SetStyle("Charles", series)),
        new GaugeItem(70, series => SetStyle("Ana", series)),
        new GaugeItem(GaugeItem.Background, series =>
        {
            series.InnerRadius = 20;
        }));

        public static void SetStyle(string name, PieSeries<ObservableValue> series)
        {
            series.Name = name;
            series.DataLabelsPosition = PolarLabelsPosition.Start;
            series.DataLabelsFormatter =
                    point => $"{point.Coordinate.PrimaryValue} {point.Context.Series.Name}";
            series.InnerRadius = 20;
            series.RelativeOuterRadius = 8;
            series.RelativeInnerRadius = 8;
        }
    }
}
