using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.Application.Services
{
   public interface IGeneralTypes
    {
        List<GeneralTypes> GetGeneralTypes();

        public int CreateGeneralTypes(GeneralTypes generalTypes);

        void DeleteGeneralTypeById(int Id);


    }
}
