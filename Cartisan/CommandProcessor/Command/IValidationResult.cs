using System.Collections.Generic;
using Cartisan.Result;

namespace Cartisan.CommandProcessor.Command {
    public interface IValidationResult: IResult {
        IEnumerable<ValidationRule> Rules { get; }

        void Add(ValidationRule rule);
    }
}