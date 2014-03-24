//namespace Cartisan.Web.Mvc {
//    public static class CommandHttpClient {
//        static readonly string CommandActionUrlTemplate = Configuration.GetAppConfig("CommandActionUrlTemplate");
//
//        public static Task<TResult> DoCommand<TResult>(this HttpClient apiClient, ICommand command, string commandUrl = null) {
//            return apiClient.PostAsJsonAsync(command, commandUrl)
//                            .Result.Content
//                            .ReadAsAsync<TResult>();
//        }
//
//
//        public static Task<TResult> DoCommand<TResult>(this HttpClient apiClient, ICommand command, TimeSpan timeout, string commandUrl = null) {
//            return apiClient.PostAsJsonAsync(command, commandUrl)
//                            .Result.Content
//                            .ReadAsAsync<TResult>()
//                            .Timeout(timeout);
//        }
//
//        public static Task<HttpResponseMessage> DoCommand(this HttpClient apiClient, ICommand command, string commandUrl = null) {
//            return apiClient.PostAsJsonAsync(command, commandUrl);
//        }
//
//        static string GetCommandUrl(ICommand command) {
//            return string.Format(CommandActionUrlTemplate, command.GetType().Name);
//        }
//
//        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, ICommand command, string commandUrl = null) {
//            if (string.IsNullOrWhiteSpace(commandUrl)) {
//                commandUrl = GetCommandUrl(command);
//            }
//            return client.PostAsJsonAsync(commandUrl, command);
//        }
//    }
//}