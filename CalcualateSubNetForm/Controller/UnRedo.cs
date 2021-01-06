using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcualateSubNetForm.Model;

namespace CalcualateSubNetForm
{
    class UnRedo
    {
        public enum Kind{
            add,
            delete
        }

        Kind kind;
        SubnetClass subnet;
        public UnRedo(Kind kindOfUnRedo, SubnetClass Subnet)
        {
            kind = kindOfUnRedo;
            subnet = Subnet;
        }
    }
}
