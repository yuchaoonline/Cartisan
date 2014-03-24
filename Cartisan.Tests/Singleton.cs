using System.Threading;

namespace Cartisan.Temp {
    public class Singleton {
        private static readonly  object LockObject = new object();
        private static Singleton _instance;

        private Singleton() { }

        public static Singleton Instance {
            get {
                if(_instance==null) {
                    lock(LockObject) {
                        if(_instance==null) {
                            // 阻止其它纯种在对象构建过程中使用此对象
                            Interlocked.Exchange(ref _instance, new Singleton());
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
