using Cartisan.Infrastructure;

namespace Cartisan.Result {
    public interface IResult {
        bool Success { get; set; }
        ResultState State { get; set; }
        string Message { get; set; }
    }
}