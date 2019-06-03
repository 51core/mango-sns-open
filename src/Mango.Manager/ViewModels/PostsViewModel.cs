﻿using System;
using System.Collections.Generic;
using System.Text;
using Mango.Models;
namespace Mango.Manager.ViewModels
{
    public class PostsViewModel
    {
        /// <summary>
        /// 分页返回总记录
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 分页数据返回集合
        /// </summary>
        public List<PostsModel> ListData { get; set; }
        /// <summary>
        /// 帖子属性标签
        /// </summary>
        public List<PostsTagsModel> TagsListData { get; set; }
    }
}
