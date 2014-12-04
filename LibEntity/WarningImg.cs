using System;

namespace LibEntity
{
    public class WarningImg
    {
        public int Id { get; set; }
        public String FileName { get; set; }
        public String WarningId { get; set; }
        public String Remarks { get; set; }
        public byte[] Img { get; set; }
    }
}