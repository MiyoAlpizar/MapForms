using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapForms.Models.DTOS
{

    public enum StatusResponse
    {
        Ok, 
        NotFound, 
        Forbiden,
        BadRequest, 
        ModelError, 
        ServerError
    }
    public class ApiResponse<T> where T : class
    {
        public int StatusCode { get; set; } = 200;
        public T Result { get; set; }
        public string MessageError { get; set; } = "";

        public StatusResponse StatusResponse { get; set; }
    }
}
