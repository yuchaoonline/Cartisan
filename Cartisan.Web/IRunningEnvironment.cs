namespace Cartisan.Web {
    /// <summary>
    /// 运行环境接口
    /// </summary>
    public interface IRunningEnvironment {
        /// <summary>
        /// 是否完全信任运行环境
        /// </summary>
        bool IsFullTrust { get; }

        /// <summary>
        /// 重新启动AppDomain
        /// </summary>
        void RestartAppDomain();
    }
}