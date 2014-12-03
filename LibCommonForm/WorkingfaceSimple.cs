using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;

namespace LibCommonForm
{
    public class WorkingfaceSimple
    {
        public WorkingfaceSimple(int id, string name, WorkingfaceTypeEnum type)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
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

        public WorkingfaceTypeEnum Type { get; set; }

        //重写Tostring
        public override string ToString()
        {
            return this.Name;
        }
    }
}
