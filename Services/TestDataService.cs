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
        private CancellationTokenSource source;
        private CancellationToken token;


        public TestDataService()
        {
            ws = new ClientWebSocket();
            source = new CancellationTokenSource();
            token = source.Token;
        }

        public async Task getDataAsync()
        {

            using (ws)
            {
                await ws.ConnectAsync(uri,token);
                while (ws.State == WebSocketState.Open && !token.IsCancellationRequested)
                {
                    try
                    {
                        var result = await ws.ReceiveAsync(buffer, token);
                        string res = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var testData = JsonConvert.DeserializeObject<TestData>(res);
                        Messenger.Send(new GetData(testData, true));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                }
            }


        }

        public void stopDataAsync()
        {
            source.Cancel();
            source.Dispose();
        }




    }
}
