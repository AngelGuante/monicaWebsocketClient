using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static monicaWebsocketClient.MessageReceived;

namespace monicaWebsocketClient
{
    class Program
    {
        private static ClientWebSocket _client = new ClientWebSocket();

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await ConnectToServerAsync();
            }).GetAwaiter().GetResult();

            Console.ReadLine();
        }

        private static async Task ConnectToServerAsync()
        {
            try
            {
                // await _client.ConnectAsync(new Uri($"ws://localhost:5000/ws"), CancellationToken.None);
                await _client.ConnectAsync(new Uri($"ws://monicawebsocketserver.azurewebsites.net/ws"), CancellationToken.None);

                await Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        WebSocketReceiveResult result;
                        ArraySegment<byte> message = new ArraySegment<byte>(new byte[4096]);

                        do
                        {
                            result = await _client.ReceiveAsync(message, CancellationToken.None);
                            byte[] messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                            string serialisedMessage = Encoding.UTF8.GetString(messageBytes);

                           Message(serialisedMessage);


                        } while (!result.EndOfMessage);
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async void SendMessageAsync(string message)
        {
            try
            {
                string serialisedMessage = message;
                var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
                var segmnet = new ArraySegment<byte>(byteMessage, 0, byteMessage.Length);

                await _client.SendAsync(segmnet, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine($"-MENSAGE: ERROR: {e.Message}");
            }
        }
    }
}
