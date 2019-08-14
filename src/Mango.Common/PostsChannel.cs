using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Mango.Repository;
using Mango.Framework.Services;
using Mango.Framework.Services.Cache;
using Newtonsoft.Json;
namespace Mango.Common
{
    public class PostsChannel
    {
        /// <summary>
        /// 从缓存服务中获取帖子频道数据
        /// </summary>
        /// <returns></returns>
        public List<Models.PostsChannelModel> GetListByCache()
        {
            List<Models.PostsChannelModel> Result = null;
            ICacheService cacheService = ServiceContext.GetService<ICacheService>();
            string cacheData = cacheService.Get("PostsChannelCache");
            if (string.IsNullOrEmpty(cacheData))
            {
                PostsChannelRepository repository = new PostsChannelRepository();
                Result = repository.GetPostsChannels().Where(m => m.IsShow == true).ToList();
                cacheData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Result)));
                //写入缓存
                cacheService.Add("PostsChannelCache", cacheData);
            }
            else
            {
                cacheData = Encoding.UTF8.GetString(Convert.FromBase64String(cacheData.Replace("\"", "")));
                //从缓存中获取
                Result = JsonConvert.DeserializeObject<List<Models.PostsChannelModel>>(cacheData);
            }
            return Result;
        }
    }
}
