using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderTerminal
{
    class TunnelSimple
    {
        public TunnelSimple(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        //重写Tostring
        public override string ToString()
        {
            return this.Name;
        }
    }
}
