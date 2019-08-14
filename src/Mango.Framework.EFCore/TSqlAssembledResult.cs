using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace Mango.Framework.EFCore
{
    public class TSqlAssembledResult
    {
        /// <summary>
        /// 编译后的参数集合
        /// </summary>
        public SqlParameter[] SqlParameters { get; set; }
        /// <summary>
        /// 编译后的SQL字符串
        /// </summary>
        public string SqlStr { get; set; }
    }
}
