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
using System.Threading.Tasks;
using Newtonsoft.Json;
using Demo.Services;
using Demo.Models;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using CommunityToolkit.Mvvm.Messaging;

namespace Demo.ViewModels
{
    public partial class RealTimeChartViewModel : ViewModelBase, IRecipient<GetData>
    {
        private readonly List<DateTimePoint> _values = new();
        private readonly List<DateTimePoint> _values2 = new();
        private readonly List<DateTimePoint> _values3 = new();
        private readonly TestDataService service = new TestDataService();


        private readonly DateTimeAxis _customAxis;

        public RealTimeChartViewModel() { }

        public RealTimeChartViewModel(int series)
        {
            var blue = new SKColor(25, 118, 210);
            var red = new SKColor(229, 57, 53);
            var yellow = new SKColor(198, 167, 0);
            var barWidth = 5;
            var strokeWidth = 5;
            var padding = 2;

            if (series == 0)
            {
                Series = new ObservableCollection<ISeries>
                {
                    new LineSeries<DateTimePoint>
                    {
                        Name = "X value",
                        Values = _values,
                        Fill = null,
                        GeometryFill =  null,
                        GeometryStroke = null,
                        Stroke = new SolidColorPaint(red,strokeWidth),
                    },
                    new LineSeries<DateTimePoint>
                    {
                        Name = "Y value",
                        Values = _values2,
                        Fill = null,
                        GeometryFill =  null,
                        GeometryStroke = null,
                        Stroke = new SolidColorPaint(blue,strokeWidth),
                    },
                    new LineSeries<DateTimePoint>
                    {
                        Name = "Z value",
                        Values = _values3,
                        Fill = null,
                        GeometryFill =  null,
                        GeometryStroke = null,
                        Stroke = new SolidColorPaint(yellow,strokeWidth),
                    },
                };
            }
            else 
            {
                Series = new ObservableCollection<ISeries>
                {
                    new ColumnSeries<DateTimePoint>
                    {
                        Name = "X value",
                        Values = _values,
                        Fill = new SolidColorPaint(blue),
                        Stroke = new SolidColorPaint(blue),
                        MaxBarWidth = barWidth,
                        Padding = padding,
                    },
                    new ColumnSeries<DateTimePoint>
                    {
                        Name = "Y value",
                        Values = _values2,
                        Fill = new SolidColorPaint(red),
                        Stroke = new SolidColorPaint(red),
                        MaxBarWidth = barWidth,
                        Padding = padding,
                    },
                    new ColumnSeries<DateTimePoint>
                    {
                        Name = "Z value",
                        Values = _values3,
                        Fill = new SolidColorPaint(yellow),
                        Stroke = new SolidColorPaint(yellow),
                        MaxBarWidth = barWidth,
                        Padding = padding,
                    },
                };
            };
            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new Axis[] { _customAxis };
            //_ = ReadData(data);
            Messenger.RegisterAll(this);
        }

        public ObservableCollection<ISeries> Series { get; set; }

        public Axis[] XAxes { get; set; }
        public bool IsReading { get; set; } = true;

        private  void ReadData(TestData data)
        {
            /*var ws = service.Ws;
            var buffer = service.Buffer;
            using (ws)
            {
                await ws.ConnectAsync(service.Uri, CancellationToken.None);
                while (ws.State == WebSocketState.Open)
                {
                    var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    }
                    else
                    {
                        string res = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var data = JsonConvert.DeserializeObject<TestData>(res);

                        _values.Add(new DateTimePoint(DateTime.Now, data.x));
                        _values2.Add(new DateTimePoint(DateTime.Now, data.y));
                        _values3.Add(new DateTimePoint(DateTime.Now, data.z));

                        if (_values.Count > 50)
                        {
                            _values.RemoveAt(0);
                            _values2.RemoveAt(0);
                            _values3.RemoveAt(0);
                        }

                        _customAxis.CustomSeparators = GetSeparators();

                        if (IsReading == false)
                        {
                            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                            Debug.WriteLine("Client Closed");
                        }
                    }
                }
            }*/
        }

        private double[] GetSeparators()
        {
            var now = DateTime.Now;

            return new double[]
            {
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

        public void Receive(GetData message)
        {
            IsReading = message.IsReading;
            if (message.data != null)
                {
                    _values.Add(new DateTimePoint(DateTime.Now, message.data.x));
                    _values2.Add(new DateTimePoint(DateTime.Now, message.data.y));
                    _values3.Add(new DateTimePoint(DateTime.Now, message.data.z));

                    if (_values.Count > 50)
                    {
                        _values.RemoveAt(0);
                        _values2.RemoveAt(0);
                        _values3.RemoveAt(0);
                    }

                    _customAxis.CustomSeparators = GetSeparators();
                }
        }
    }
}
