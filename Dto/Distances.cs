using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWPF.Dto
{
    public class Distances
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int ElapsedTime { get; set; }
        public double Speed { get; set; }
        public double SNR1 { get; set; }
        public double Distance { get; set; }
    }
}
