using System.Collections.Generic;

namespace Mango.Framework.Core
{
    public class Configuration
    {
        /// <summary>
        /// 存储配置项信息的容器
        /// </summary>
        private static Dictionary<string, string> Items = new Dictionary<string, string>();
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        public static void AddItem(string key, string value)
        {
            Items.Add(key, value);
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetItem(string key)
        {
            if (Items.ContainsKey(key))
            {
                return Items[key];
            }
            return string.Empty;
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool DeleteItem(string key)
        {
            if (Items.ContainsKey(key))
            {
                Items.Remove(key);
                return true;
            }
            return false;
        }
    }
}
