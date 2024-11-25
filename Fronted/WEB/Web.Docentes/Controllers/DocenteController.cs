using Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
using Web.Docentes.Models;

namespace Web.Docentes.Controllers
{
    public class DocenteController : Controller
    {
        public IActionResult Index(string Buscar = "", int? pagenumber = 1)
        {
            if (string.IsNullOrWhiteSpace(Buscar)) Buscar = "";

            List<DocenteE> ListadoDocente = new List<DocenteE>();
            ResultadoTransaccionE<DocenteE> resultado = new ResultadoTransaccionE<DocenteE>();
            try
            {
                resultado = ConsultaListaDocente(Buscar);
                if (resultado.IdRegistro > -1)
                {
                    ListadoDocente = resultado.DataList;
                }
            }
            catch (Exception ex) { }
            int pageSize = 3;

            return View(PaginatedList<DocenteE>.CreateAsync(ListadoDocente, pagenumber ?? 1, pageSize));
        }

        ResultadoTransaccionE<DocenteE> ConsultaListaDocente(string buscar)
        {
            ResultadoTransaccionE<DocenteE> resultado = new ResultadoTransaccionE<DocenteE>();
            try
            {
                var ruta = MetaGlobal.RutaApiVenta + MetaGlobal.RutaApiVentaRol + "/Listar_All?buscar=" + buscar;
                using (RestClient _client = new RestClient(ruta))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Get;
                    var response = _client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var respuesta = JsonConvert.DeserializeObject<ResultadoTransaccionE<DocenteE>>(response.Content);
                        resultado.IdRegistro = respuesta.IdRegistro;
                        resultado.Mensaje = respuesta.Mensaje; ;
                        resultado.DataList = respuesta.DataList;
                    }
                    else
                    {
                        resultado.IdRegistro = -1;
                        resultado.Mensaje = response.Content;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.IdRegistro = -1;
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public IActionResult Create()
        {
            DocenteE objDocente = new DocenteE();
            return View(objDocente);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(DocenteE objDocente)
        {
            ViewBag.Mensaje = "";
            ViewBag.ErrorMensaje = "";

            if (ModelState.IsValid)
            {
                var resultado = RegistrarDocente(objDocente);
                if (resultado.IdRegistro == -1)
                {
                    ViewBag.ErrorMensaje = resultado.Mensaje;
                }
                else
                {
                    ViewBag.Mensaje = resultado.Mensaje;
                }
            }
            return View(objDocente);
        }

        ResultadoTransaccionE<string> RegistrarDocente(DocenteE obj)
        {
            ResultadoTransaccionE<string> resultado = new ResultadoTransaccionE<string>();
            try
            {
                var ruta = MetaGlobal.RutaApiVenta + MetaGlobal.RutaApiVentaRol + "/Registrar_Docente";
                var JsonData = JsonConvert.SerializeObject(obj);

                using (RestClient _client = new RestClient(ruta))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AddJsonBody(JsonData);
                    var response = _client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var respuesta = JsonConvert.DeserializeObject<ResultadoTransaccionE<string>>(response.Content);
                        resultado.IdRegistro = respuesta.IdRegistro;
                        resultado.Mensaje = respuesta.Mensaje;
                    }
                    else
                    {
                        resultado.IdRegistro = -1;
                        resultado.Mensaje = response.Content;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.IdRegistro = -1;
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
