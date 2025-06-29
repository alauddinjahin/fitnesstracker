using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Utils
{
    public class ActivityMETConfig
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public float BaseMET { get; set; }
        public float Metric1Factor { get; set; }
        public float Metric2Factor { get; set; }
        public float Metric3Factor { get; set; }
        public string Notes { get; set; }
    }

}


