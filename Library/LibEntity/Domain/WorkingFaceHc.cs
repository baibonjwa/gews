using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity.Domain
{
    public class WorkingFaceHc
    {
        public WorkingFace WorkingFace { get; set; }

        public Tunnel TunnelZy { get; set; }

        public Tunnel TunnelFy { get; set; }

        public Tunnel TunnelQy { get; set; }

        public List<Tunnel> TunnelOther { get; set; }

        public static List<WorkingFaceHc> FindAll()
        {
            var workingFaces = WorkingFace.FindAll();
            var tunnels = Tunnel.FindAll();
            var tunnelByGroup = tunnels.GroupBy(u => u.WorkingFace.WorkingFaceId);
            var result = new List<WorkingFaceHc>();
            //TODO：还未实现
            foreach (var t in tunnelByGroup)
            {
                foreach (var i in t)
                {

                }
                var item = new WorkingFace()
                {

                };
            }
            return result;
        }

        public void Delete()
        {
            //TODO:尚未实现
        }
    }
}
