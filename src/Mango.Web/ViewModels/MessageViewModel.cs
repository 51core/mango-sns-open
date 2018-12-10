using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Web.ViewModels
{
    public class MessageViewModel
    {
        /// <summary>
        /// 分页返回总记录
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 分页数据返回集合
        /// </summary>
        public List<Models.MessageModel> ListData { get; set; }
    }
}
