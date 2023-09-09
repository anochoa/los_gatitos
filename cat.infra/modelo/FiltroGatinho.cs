using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.infra.modelo
{
    public class FiltroGatinho
    {
        public int FiltroLimit { get; set; }
        public FiltroGatinho() { FiltroLimit = 10; }

        public FiltroGatinho(int limit)
        {
            if (limit < 0) throw new ArgumentOutOfRangeException(nameof(limit));

            FiltroLimit = limit;
        }
    }
}
