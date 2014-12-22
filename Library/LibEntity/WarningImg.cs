using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_EARLY_WARNING_IMG")]
    public class WarningImg
    {
        [PrimaryKey(PrimaryKeyType.Identity, "IMG_ID")]
        public int Id { get; set; }
        [Property("IMG_FILENAME")]
        public String FileName { get; set; }
        [Property("WARNING_ID")]
        public String WarningId { get; set; }
        [Property("REMARKS")]
        public String Remarks { get; set; }
        [Property("IMG")]
        public byte[] Img { get; set; }
    }
}