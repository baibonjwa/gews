using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class WarningImgEnt
    {
        public int Id { get; set; }
        public String FileName { get; set; }
        public String WarningId { get; set; }
        public String Remarks { get; set; }
        public byte[] Img { get; set; }
    }
}