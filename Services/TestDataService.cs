using CommunityToolkit.Mvvm.Messaging;
using Demo.Models;
using LiveChartsCore.Defaults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Services
{
    internal class TestDataService : ObservableRecipient
    {
        private Uri uri = new("ws://113.161.84.132:8800/wsadxl");
        private ClientWebSocket ws;
        private byte[] buffer = new byte[1024];
        public bool IsReading {  get; set; } = true;

        public TestDataService()
        {

        }

        public Uri Uri { get => uri; set => uri = value; }
        public ClientWebSocket Ws { get => ws; set => ws = value; }
        public byte[] Buffer { get => buffer; set => buffer = value; }

        public async Task getDataAsync()
        {
            if (IsReading)
            {
                using (ws = new ClientWebSocket())
                {
                    await ws.ConnectAsync(uri, CancellationToken.None);
                    while (ws.State == WebSocketState.Open)
                    {
                        var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
                        string res = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var testData = JsonConvert.DeserializeObject<TestData>(res);
                        Messenger.Send(new GetData(testData, true));

                    }
                }
            }
        }

        public async Task stopDataAsync()
        {
            IsReading = false;
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
        }


    }
}
