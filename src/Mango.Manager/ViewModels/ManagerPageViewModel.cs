using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Framework.EFCore;

namespace Mango.Manager.ViewModels
{
    public class ManagerPageViewModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 分页数据
        /// </summary>
        public List<MangoData> ListData { get; set; }
    }
}
