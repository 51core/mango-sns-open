using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Web.Controllers
{
    public class ManagerController : Controller
    {
        // GET: /<controller>/
        /// <summary>
        /// 设置帖子属性标签
        /// </summary>
        /// <param name="PostsId"></param>
        /// <param name="Property"></param>
        /// <returns></returns>
        public bool SetProperty(int postsId, string tags)
        {
            bool Result = true;
            Entity.m_Posts model = new Entity.m_Posts();
            model.PostsId = postsId;
            model.Tags = tags != null ? tags : "";
            CommonRepository repository = new CommonRepository();
            Result = repository.Update(model);
            return Result;
        }
        /// <summary>
        /// 加载属性列表
        /// </summary>
        /// <returns></returns>
        public string LoadProperty()
        {
            //加载属性数据
            Common.PostsChannel common = new Common.PostsChannel();
            return Newtonsoft.Json.JsonConvert.SerializeObject(common.GetListByCache());
        }
    }
}
