using System.Collections.Generic;
using Cartisan.Result;
using YouQiu.Framework.CommandProcessor.Command;

namespace Cartisan.CommandProcessor.Command {
    public interface IValidationResult: IResult {
        IEnumerable<ValidationRule> Rules { get; }

        void Add(ValidationRule rule);
    }
}