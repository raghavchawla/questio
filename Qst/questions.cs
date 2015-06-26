using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qst
{
    public class questions
    {
        
        public string id { get; set; }
        public string question_id { get; set; }
        public string question_value { get; set; }
        public double radius { get; set; }
        public string userid { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public int no_of_responses { get; set; }
        public int answered1 { get; set; }
        public int answered2 { get; set; }
        public int answered3 { get; set; }
        public int answered4 { get; set; }
        public double location_longitude { get; set; }
        public double location_latitude { get; set; }

    }
}
