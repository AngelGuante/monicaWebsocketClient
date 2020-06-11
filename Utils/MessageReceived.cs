using Dapper;
using System;
using Newtonsoft.Json;
using static monicaWebsocketClient.Program;
using static monicaWebsocketClient.Utils.GlobalVariables;

namespace monicaWebsocketClient
{
    public class MessageReceived
    {
        public static void Message(string data)
        {
            Console.WriteLine("Recibiendo query...");
            Console.WriteLine(data);
            try
            {
                var resultset = Conn.Query(data);
                Console.WriteLine("Devolviendo resultset...");
                Console.WriteLine("------------------------");
                SendMessageAsync(JsonConvert.SerializeObject(resultset));
            }
            catch (Exception e)
            {
                SendMessageAsync($"Error: {e.Message}");
                Console.WriteLine(e);
            }
        }
    }
}