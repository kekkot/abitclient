using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturientClient.JsonModels
{
    public class Department
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float Order { get; set; }

        public Department(string ValueCode, string ValueName, float ValueOrder)
        {
            Code = ValueCode;
            Name = ValueName;
            Order = ValueOrder;
        }
    }
}
