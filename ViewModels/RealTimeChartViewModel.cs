using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Demo.Models;
using Avalonia.Animation;

namespace Demo.ViewModels
{
    public partial class RealTimeChartViewModel : ViewModelBase
    {
        private readonly Random _random = new();
        private readonly List<DateTimePoint> _values = new();
        private readonly DateTimeAxis _customAxis;

        public RealTimeChartViewModel()
        {
            Series = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _values,
                Fill = new SolidColorPaint(new SKColor(255, 205, 210, 100)),
                GeometryFill = null,
                GeometryStroke = null,
            }
        };

            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new Axis[] { _customAxis };

            _ = ReadData();
        }

        public ObservableCollection<ISeries> Series { get; set; }

        public object Sync { get; } = new object();


        public Axis[] XAxes { get; set; }

        public bool IsReading { get; set; } = true;

        private async Task ReadData()
        {
            // to keep this sample simple, we run the next infinite loop 
            // in a real application you should stop the loop/task when the view is disposed 

            while (IsReading)
            {
                await getWebSocket();
            }
        }

        private double[] GetSeparators()
        {
            var now = DateTime.Now;

            return new double[]
            {
                now.AddSeconds(-30).Ticks,
                now.AddSeconds(-25).Ticks,
                now.AddSeconds(-20).Ticks,
                now.AddSeconds(-15).Ticks,
                now.AddSeconds(-10).Ticks,
                now.AddSeconds(-5).Ticks,
                now.Ticks
            };
        }

        private static string Formatter(DateTime date)
        {
            var secsAgo = (DateTime.Now - date).TotalSeconds;

            return secsAgo < 1
                ? "now"
                : $"{secsAgo:N0}s ago";
        }

        private async Task getWebSocket()
        {
            Uri uri = new("ws://113.161.84.132:8800/temp");

            using ClientWebSocket ws = new();
            await ws.ConnectAsync(uri, default);

            var bytes = new byte[1024];
            var result = await ws.ReceiveAsync(bytes, default);
            string res = Encoding.UTF8.GetString(bytes, 0, result.Count);
            var data = JsonConvert.DeserializeObject<Temperature>(res);

            _values.Add(new DateTimePoint(DateTime.Now, data.temperature));

            if (_values.Count > 30) _values.RemoveAt(0);
            _customAxis.CustomSeparators = GetSeparators();

            Debug.WriteLine(data);

            //await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closed", default);
        }
    }
}
