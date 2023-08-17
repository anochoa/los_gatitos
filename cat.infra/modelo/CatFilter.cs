using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.infra.modelo
{
    public class CatFilter
    {
        public CatFilter() { filtroLimit = 10; }

        public CatFilter(float _limit)
        {
            int result  = 0;
            if(_limit < 0 && int.TryParse(_limit.ToString(), out result))
                filtroLimit = int.Parse(_limit.ToString());
        }
        public int filtroLimit { get; set; }
    }
}
