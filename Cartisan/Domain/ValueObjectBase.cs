using System;
using System.Collections.Generic;
using System.Text;

namespace Cartisan.Domain {
    public abstract class ValueObjectBase {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        protected abstract void Validate();

        public void ThrowExceptionIfInvalid() {
            this.ThrowExceptionIfInvalid(issues => { throw new ValueObjectIsInvalidException(issues); });
        }

        public void ThrowExceptionIfInvalid(Action<string> exceptionToThrow) {
            _brokenRules.Clear();
            this.Validate();
            if (_brokenRules.Count > 0) {
                var issues = new StringBuilder();
                foreach (var businessRule in _brokenRules) {
                    issues.AppendLine(businessRule.Rule);
                }

                exceptionToThrow(issues.ToString());
            }
        }

        protected void AddBrokenRule(BusinessRule businessRule) {
            this._brokenRules.Add(businessRule);
        }

        protected void AddBrokenRule(string propertyName, string rule) {
            this.AddBrokenRule(new BusinessRule(propertyName, rule));
        }
    }
}