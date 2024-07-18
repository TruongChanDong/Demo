using Demo.Models;
using LiveChartsCore.Defaults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.Services
{
    internal class TestDataService
    {
        private Uri uri = new("ws://113.161.84.132:8800/wsadxl");
        private ClientWebSocket ws = new ClientWebSocket();
        private byte[] buffer = new byte[1024];

        public TestDataService()
        {
        }

        public Uri Uri { get => uri; set => uri = value; }
        public ClientWebSocket Ws { get => ws; set => ws = value; }
        public byte[] Buffer { get => buffer; set => buffer = value; }

        /*public async Task getDataAsync()
        {
            using (ws)
            {
                await ws.ConnectAsync(uri, CancellationToken.None);
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
                        testData = JsonConvert.DeserializeObject<TestData>(res);
                    }
                }
            }
        }*/
    }
}
