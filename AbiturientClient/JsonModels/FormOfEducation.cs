using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.JsonModels
{
    public class FormOfEducation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float? Order { get; set; }
    }
}
