using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Core.Entities
{
    /// <summary>
    /// 查询实体
    /// </summary>
    public class PageDtoModel
    {
        /// <summary>
        /// 分页值
        /// </summary>
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
