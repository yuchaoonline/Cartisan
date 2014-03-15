using System.Collections.Generic;
using Cartisan.Result;
using YouQiu.Framework.CommandProcessor.Command;

namespace Cartisan.CommandProcessor.Command {
    public class ValidationResult: DefaultResult, IValidationResult {
        private List<ValidationRule> _rules = new List<ValidationRule>(); 

        public ValidationResult() {}
        public ValidationResult(bool success) : base(success) { }

        public IEnumerable<ValidationRule> Rules { get { return this._rules; } }

        public void Add(ValidationRule rule) {
            this.Success = false;
            this._rules.Add(rule);
        }
    }
}