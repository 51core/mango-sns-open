using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Framework.EFCore
{
    public class MangoData
    {
        private Dictionary<string, object> _row = null;
        public MangoData()
        {
            _row = new Dictionary<string, object>();
        }
        public Dictionary<string, object> Data
        {
            get
            {
                 return _row;
            }
        }
        /// <summary>
        /// 添加现有列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal void Add(string key, object value)
        {
            if (!_row.ContainsKey(key))
            {
                _row.Add(key, value);
            }
        }
        /// <summary>
        /// 获取(设置)指定值索引
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (_row.ContainsKey(key))
                {
                    return _row[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_row.ContainsKey(key))
                {
                    _row[key] = value;
                }
                else
                {
                    _row.Add(key, value);
                }
            }
        }
    }
}
