using Mel.Domain.Entities;
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
        [HttpGet]
        public HttpResponseMessage mutant(string[] Array)
        {
            var result = new HttpResponseBaseModel();
            if (Array==null)
            {
                result.Code = 400;
                result.Message = "Array null.";
                return Request.CreateResponse(HttpStatusCode.BadRequest,result,Configuration.Formatters.JsonFormatter);
            }
            //if (isMutant(Array))
            //{
            //    throw new HttpResponseException(HttpStatusCode.OK);
            //}
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
          
           
        }


    }
}
