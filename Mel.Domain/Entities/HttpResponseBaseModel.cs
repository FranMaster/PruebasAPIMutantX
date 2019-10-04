using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mel.Domain.Entities
{
    public class HttpResponseBaseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Exception Exception { get; set; }
    }
}
