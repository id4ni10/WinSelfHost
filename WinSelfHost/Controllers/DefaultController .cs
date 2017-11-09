using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WinSelfHost.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var html = "<!DOCTYPE html><html><head><meta charset='utf-8'/><title>WinSelfHost</title><style>label{font-weight: bold;}body{background-color: #3e94ec; font-family: 'Roboto', helvetica, arial, sans-serif; font-size: 16px; font-weight: 400; text-rendering: optimizeLegibility;}div.table-title{display: block; margin: auto; max-width: 600px; padding:5px; width: 100%;}.table-title h3{color: #fafafa; font-size: 30px; font-weight: 400; font-style:normal; font-family: 'Roboto', helvetica, arial, sans-serif; text-shadow: -1px -1px 1px rgba(0, 0, 0, 0.1); text-transform:uppercase;}/*** Table Styles **/.table-fill{background: white; border-radius:3px; border-collapse: collapse; height: 320px; margin: auto; max-width: 600px; padding:5px; width: 100%; box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1); animation: float 5s infinite;}th{color:#D5DDE5;; background:#1b1e24; border-bottom:4px solid #9ea7af; border-right: 1px solid #343a45; font-size:23px; font-weight: 100; padding:24px; text-align:left; text-shadow: 0 1px 1px rgba(0, 0, 0, 0.1); vertical-align:middle;}th:first-child{border-top-left-radius:3px;}th:last-child{border-top-right-radius:3px; border-right:none;}tr{border-top: 1px solid #C1C3D1; border-bottom-: 1px solid #C1C3D1; color:#666B85; font-size:16px; font-weight:normal; text-shadow: 0 1px 1px rgba(256, 256, 256, 0.1);}tr:hover td{background:#4E5066; color:#FFFFFF; border-top: 1px solid #22262e;}tr:first-child{border-top:none;}tr:last-child{border-bottom:none;}tr:nth-child(odd) td{background:#EBEBEB;}tr:nth-child(odd):hover td{background:#4E5066;}tr:last-child td:first-child{border-bottom-left-radius:3px;}tr:last-child td:last-child{border-bottom-right-radius:3px;}td{background:#FFFFFF; padding:20px; text-align:left; vertical-align:middle; font-weight:300; font-size:18px; text-shadow: -1px -1px 1px rgba(0, 0, 0, 0.1); border-right: 1px solid #C1C3D1;}td:last-child{border-right: 0px;}th.text-left{text-align: left;}th.text-center{text-align: center;}th.text-right{text-align: right;}td.text-left{text-align: left;}td.text-center{text-align: center;}td.text-right{text-align: right;}</style></head><body><div><select id='date-search'><option value='1'>Janeiro</option><option value='2'>Fevereiro</option><option value='3'>Março</option><option value='4'>Abril</option><option value='5'>Maio</option><option value='6'>Junho</option><option value='7'>Julho</option><option value='8'>Agosto</option><option value='9'>Setembro</option><option value='10'>Outubro</option><option value='11'>Novembro</option><option value='12'>Dezembro</option></select></div><div class='table-title'><h3>Histórico</h3></div><table class='table-fill'><thead><tr><th class='text-left'>Dia</th><th class='text-left'>Power On</th><th class='text-left'>Power Off</th></tr></thead><tbody class='table-hover'></tbody></table> </body><script src='https://code.jquery.com/jquery-3.2.1.min.js'></script><script>$(document).ready(function(){$('#date-search').change(function(e){$.ajax({url: 'Search/' + e.target.value,method: 'get',success: function(data){var body=$('.table-hover');body.html('');data.forEach(function(e, i){body.append('<tr><td class=\"text-left\">'+ e.dia + '</td><td class=\"text-left\">' + e.chegada + '</td><td class=\"text-left\">' + e.saida + '</td></tr>')});},error: function(data){alert('Um erro ocorreu ao processar a sua solicitação!');}});});});</script></html>";
            
            var content = new StringContent(html, Encoding.UTF8, "text/html");

            return new HttpResponseMessage()
            {
                Content = content
            };
        }

        [HttpGet]
        public HttpResponseMessage Search(int id)
        {
            var logs = EventLog.GetEventLogs().Where(log => log.Log.Equals("System"));

            var result = from EventLogEntry entry in logs.First().Entries
                         where id == 0 ? true : entry.TimeGenerated.Date.Month == id
                         group entry by entry.TimeGenerated.Date into dias
                         orderby dias.Key descending
                         select new
                         {
                             dia = dias.First().TimeGenerated.Date.ToString("dd/MM/yyyy"),
                             chegada = dias.First().TimeGenerated.ToString("HH:mm:ss"),
                             saida = dias.Last().TimeGenerated.ToString("HH:mm:ss")
                         };

            var output = JsonConvert.SerializeObject(result, Formatting.Indented);

            return new HttpResponseMessage()
            {
                Content = new StringContent(output, Encoding.UTF8, "application/json")
            };
        }
    }
}
