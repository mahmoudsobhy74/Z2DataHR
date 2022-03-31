using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.Application.Services
{
    public interface ITypes
    {
        List<Types> GetTypes();

        public int CreateTypes(Types types);
    }
}
