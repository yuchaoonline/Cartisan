using System;

namespace Cartisan.BusinessComponents.OperationLog {
    public class OperationLogEntry {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; } 

        /// <summary>
        /// 应用Id
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// 日志来源，一般为应用模块名称
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 操作类型标识
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 操作对象名称
        /// </summary>
        public string OperationObjectName { get; set; }

        /// <summary>
        /// 操作对象Id
        /// </summary>
        public long OperationObjectId { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 操作者Id
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 操作者名称
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperatorIP { get; set; }

        /// <summary>
        /// 操作访问Url
        /// </summary>
        public string AccessUrl { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}