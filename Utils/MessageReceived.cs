using Dapper;
using System;
using System.Text;
using monicaWebsocketClient.Models.DTO.Reportes;
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
                        var dd = Conn.Query<IndividualClientStatusDTO>(IndividualClientStatusQuery(dataSplited[1]));

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