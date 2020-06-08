using Dapper;
using System;
using Newtonsoft.Json;
using static monicaWebsocketClient.Program;
using static monicaWebsocketClient.Utils.Querys;
using static monicaWebsocketClient.Utils.GlobalVariables;

namespace monicaWebsocketClient
{
    public class MessageReceived
    {
        private static string SEPARATOR = "<";

        public static void Message(string data)
        {
            var dataSplited = data.Split(SEPARATOR);

            switch (int.Parse(dataSplited[0]))
            {
                case (int)ClientMessageStatusEnum.DuplicatedIP:
                    Console.WriteLine(dataSplited[1]);
                    break;
                case (int)ClientMessageStatusEnum.IndividualClientStatusReport:
                    try
                    {
                        var resultset = Conn.Query(IndividualClientStatusQuery(dataSplited[1]));
                        SendMessageAsync(JsonConvert.SerializeObject(resultset));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
            }
        }
    }
}