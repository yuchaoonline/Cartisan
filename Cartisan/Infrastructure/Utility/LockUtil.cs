using System;
using System.Collections;

namespace Cartisan.Infrastructure.Utility {
    public class LockUtil {
        class LockObject {
            public int Counter { get; set; } 
        }

        /// <summary>
        /// 锁对象池，引用计数大于0的锁对象都会在池中缓存起来
        /// </summary>
        private static readonly Hashtable _lockPool = new Hashtable();

        /// <summary>
        /// 释放锁对象，当锁的引用计数为0时，从锁对象池移除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="lockObj"></param>
        private static void ReleaseLock(object key, LockObject lockObj) {
            lockObj.Counter--;
            lock(_lockPool) {
                if(lockObj.Counter==0) {
                    _lockPool.Remove(key);
                }
            }
        }

        /// <summary>
        /// 从锁对象池中获取锁对象，并且锁对象的引用计数加1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static LockObject GetLock(object key) {
            lock(_lockPool) {
                var lockObj = _lockPool[key] as LockObject;
                if(lockObj==null) {
                    lockObj = new LockObject();
                    _lockPool[key] = lockObj;
                }
                lockObj.Counter++;
                return lockObj;
            }
        }

        /// <summary>
        /// 用法类似系统Lock，参数key为锁对象的键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public static void Lock(object key, Action action) {
            var lockObj = GetLock(key);
            try {
                lock(lockObj) {
                    action();
                }
            }
            finally {
                ReleaseLock(key, lockObj);
            }
        }
    }
}