using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.ViewModels.Base
{
    public class DataGenericResponse<T> : GenericResponse
    {
        public T Data { get; set; }
    }
}
