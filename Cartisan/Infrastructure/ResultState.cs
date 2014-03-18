namespace Cartisan.Infrastructure {
    public enum ResultState {
        Success = 1,
        Unauthorized = 2,
        Exception = 3,
        RuntimeFailure = 4,
        ValidateFailure = 5
    }
}