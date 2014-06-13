using Cartisan.IoC;

namespace Cartisan.Infrastructure {
    /// <summary>
    /// Id生成器（用于替代数据库非自增主键）
    /// </summary>
    public abstract class IdGenerator {
        private static volatile IdGenerator _defaultInstance;
        private static readonly object LockObject = new object();

        private static IdGenerator Instance() {
            if(_defaultInstance==null) {
                lock(LockObject) {
                    if(_defaultInstance==null) {
                        _defaultInstance = ServiceLocator.GetService<IdGenerator>();
                        // ReSharper disable once ReadAccessInDoubleCheckLocking
                        if(_defaultInstance==null) {
                            throw new CartisanException(ErrorCode.Exception, "未在容器中注册IdGenerator的具体实现类");
                        }
                    }
                }
            }

            return _defaultInstance;
        }

        /// <summary>
        /// 获取下一个Id
        /// </summary>
        /// <returns>
        /// 返回生成的下一个Id
        /// </returns>
        public static long Next() {
            lock(LockObject) {
                return Instance().NextLong();
            }
        }

        /// <summary>
        /// 获取下一个long类型的Id
        /// </summary>
        /// <returns>
        /// 返回生成的下一个Id
        /// </returns>
        protected abstract long NextLong();
    }
}