using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate.Engine;

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
            var results = new List<WorkingFaceHc>();
            using (new SessionScope())
            {
                var workingFaces = WorkingFace.FindAll();
                results.AddRange(from workingFace in workingFaces
                                 let tunnelZy = workingFace.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY)
                                 let tunnelFy = workingFace.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_FY)
                                 let tunnelQy = workingFace.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_QY)
                                 let tunnelOther = workingFace.Tunnels.Where(u => u.TunnelType == TunnelTypeEnum.OTHER).ToList()
                                 where tunnelZy != null && tunnelFy != null && tunnelQy != null
                                 select new WorkingFaceHc
                                 {
                                     WorkingFace = workingFace,
                                     TunnelZy = tunnelZy,
                                     TunnelFy = tunnelFy,
                                     TunnelQy = tunnelQy,
                                     TunnelOther = tunnelOther
                                 });
            }
            return results;
        }

        public static WorkingFaceHc FindByWorkingFace(WorkingFace workingFace)
        {
            using (new SessionScope())
            {
                var workingFaceLazy = WorkingFace.Find(workingFace.WorkingFaceId);
                var tunnelZy = workingFaceLazy.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY);
                var tunnelFy = workingFaceLazy.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_FY);
                var tunnelQy = workingFaceLazy.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_QY);
                var tunnelOther = workingFaceLazy.Tunnels.Where(u => u.TunnelType == TunnelTypeEnum.OTHER).ToList();
                var result = new WorkingFaceHc
                {
                    WorkingFace = workingFace,
                    TunnelZy = tunnelZy,
                    TunnelFy = tunnelFy,
                    TunnelQy = tunnelQy,
                    TunnelOther = tunnelOther
                };
                return result;
            }

        }

        public static bool JudgeWorkingFaceIsAssociated(WorkingFace workingFace)
        {
            using (new SessionScope())
            {
                var workingFaceLazy = WorkingFace.Find(workingFace.WorkingFaceId);
                var tunnelZy = workingFace.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_ZY);
                var tunnelFy = workingFace.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_FY);
                var tunnelQy = workingFace.Tunnels.FirstOrDefault(u => u.TunnelType == TunnelTypeEnum.STOPING_QY);
                return tunnelZy != null && tunnelFy != null && tunnelQy != null;
            }

        }

        public void Delete()
        {
            //TODO:尚未实现
        }
    }
}
