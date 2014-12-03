using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibCommonControl
{
    class RuleSimple
    {
        public RuleSimple(int id, string description)
        {
            this.Id = id;
            this.Description = description;
        }

        public int Id
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        //重写Tostring
        public override string ToString()
        {
            return this.Description;
        }
    }
}
