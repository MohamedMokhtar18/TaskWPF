using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWPF.Dto
{
    public class ApplicationData
    {
        public double SpeedThreshold { get; set; }
        public double SNRThreshold { get; set; }
        public IList<Distances> Distances { get; set; }
        // Other properties...
    }
}
