using Mel.BussinesLogic;
using Mel.Domain.Entities;
using Mel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Mel.Controllers
{
    public class mutantController : ApiController
    {
        public const string HUMANO= "Humano";
        public const string MUTANTE = "Mutante";
        HumanRegistrerEntities1 db;
        public mutantController()
        {
            db = new HumanRegistrerEntities1();
        }
        /// <summary>
        /// Esta Api devolvera un resultado si El array contiene si encuentras más de una secuencia de cuatro letras
        /// iguales​, de forma oblicua, horizontal o vertical.
        /// </summary>
        /// <param name="Array"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage mutant(string[] Array)
        {
            try
            {
                MutanteLogic Modelo = new MutanteLogic();

                var result = new HttpResponseBaseModel();
                if (Array == null)
                {
                    result.Code = 400;
                    result.Message = "ERROR, No se recibe datos en Array base nitrogenada del ADN. ";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result, Configuration.Formatters.JsonFormatter);
                }
                if (!Modelo.ValidarMatrizNXN(Array))
                {
                    result.Code = 400;
                    result.Message = "ERROR, Array No es NxN";
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result, Configuration.Formatters.JsonFormatter);
                }
                if (Modelo.isMutant(Array))
                {
                    result.Code = 200;
                    result.Message = MUTANTE;
                    db.Consultas.Add(new Consulta { AdnAnalizado = JsonConvert.SerializeObject(Array), FechaConsulta = DateTime.Now, Resultado = result.Message });
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    result.Code = 403;
                    result.Message = HUMANO;
                    db.Consultas.Add(new Consulta { AdnAnalizado = JsonConvert.SerializeObject(Array), FechaConsulta = DateTime.Now, Resultado = result.Message });
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Forbidden, result, Configuration.Formatters.JsonFormatter);
                }
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, Configuration.Formatters.JsonFormatter);
            }

        }

        

    }
    public class statsController: ApiController
    {
        HumanRegistrerEntities1 db;
        public statsController()
        {
            db = new HumanRegistrerEntities1();
        }
        [HttpGet]
        public HttpResponseMessage stats()
        {
            try
            {

                statsModel Stadisticas = new statsModel();
                var ListaConsultas = db.Consultas.ToList();
                foreach (var item in ListaConsultas)
                {
                    if (item.Resultado.Equals(mutantController.HUMANO))
                        Stadisticas.count_human_dna++;
                    if (item.Resultado.Equals(mutantController.MUTANTE))
                        Stadisticas.count_human_dna++;
                }
                return Request.CreateResponse(HttpStatusCode.OK, Stadisticas, Configuration.Formatters.JsonFormatter);

            }
            catch (Exception E)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, E, Configuration.Formatters.JsonFormatter);
            }
           

           
        }
    }
}
