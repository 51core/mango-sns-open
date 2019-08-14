using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
namespace Mango.Repository
{
    public class PostsTagsRepository
    {
        private EFDbContext _dbContext = null;
        public PostsTagsRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 获取分类频道数据
        /// </summary>
        /// <returns></returns>
        public List<Models.PostsTagsModel> GetList()
        {
            var queryResult = _dbContext.m_PostsTags
                .Select(m => new Models.PostsTagsModel()
                {
                    TagsId = m.TagsId.Value,
                    TagsName = m.TagsName
                })
                .ToList();
            return queryResult;
        }
    }
}
