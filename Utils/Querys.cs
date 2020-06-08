using System;
using System.Text;
using System.Linq;
using static monicaWebsocketClient.Utils.GlobalVariables;

namespace monicaWebsocketClient.Utils
{
    public class Querys
    {
        public static string IndividualClientStatusQuery(string data)
        {
            var query = new StringBuilder();
            var queryParams = data.Split(';');
            var codigo_clte = queryParams.FirstOrDefault(reg => reg.StartsWith("codigo_clte="))?.Replace("codigo_clte=", "");
            var SoloDocsVencidos = queryParams.FirstOrDefault(reg => reg.StartsWith("SoloDocsVencidos="));

            query.Append("  SELECT ");
            query.Append("      D.fecha_emision, ");
            query.Append("      D.fecha_vcmto, ");
            query.Append("      D.descripcion_dcmto, ");
            query.Append("      CAST((D.monto_dcmto - D.balance) AS VARCHAR) pagosAcumulados, ");
            query.Append("      D.ncf ");
            query.Append($" FROM {DbNames[0]}.dbo.clientes C ");
            query.Append("  JOIN docs_cc D ON C.cliente_id = D.cliente_id ");
            query.Append($" WHERE C.codigo_clte = {codigo_clte} ");

            if (SoloDocsVencidos != default)
            {
                var dateNow = DateTime.Now;
                query.Append($" AND fecha_vcmto < '{dateNow.Year}-{dateNow.Month}-{dateNow.Day}'  ");
            }

            return query.ToString();
        }
    }
}