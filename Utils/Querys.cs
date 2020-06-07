using System.Text;
using static monicaWebsocketClient.Utils.GlobalVariables;

namespace monicaWebsocketClient.Utils
{
    public class Querys
    {
        public static string IndividualClientStatusQuery(string codigo_clte)
        {
            var query = new StringBuilder();
            query.Append("  SELECT ");
            query.Append("      D.fecha_emision, ");
            query.Append("      D.fecha_vcmto, ");
            query.Append("      D.descripcion_dcmto, ");
            query.Append("      (D.monto_dcmto - D.balance) pagosAcumulados, ");
            query.Append("      D.ncf ");
            query.Append($" FROM {DbNames[0]}.dbo.clientes C ");
            query.Append("  JOIN docs_cc D ON C.cliente_id = D.cliente_id ");
            query.Append($" WHERE C.codigo_clte = {codigo_clte} ");

            return query.ToString();
        }
    }
}