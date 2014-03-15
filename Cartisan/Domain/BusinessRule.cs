namespace Cartisan.Domain {
    /// <summary>
    /// 业务规则信息
    /// </summary>
    public class BusinessRule {
        private readonly string _property;
        private readonly string _rule;

        public BusinessRule(string property, string rule) {
            this._property = property;
            this._rule = rule;
        }

        /// <summary>
        /// 规则信息
        /// </summary>
        public string Rule {
            get { return this._rule; }
        }

        /// <summary>
        /// 规则适用的属性
        /// </summary>
        public string Property {
            get { return this._property; }
        }
    }
}