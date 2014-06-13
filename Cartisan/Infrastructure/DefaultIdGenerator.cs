using System;
using System.Collections;

namespace Cartisan.Infrastructure {
    /// <summary>
    /// 以当前时间为基准的Id生成器
    /// </summary>
    public class DefaultIdGenerator: IdGenerator {
        // 批量生成Id的时间刻度
        private static readonly long _timescale = 1000000;
        // 生成Id的随机数长度
        private static readonly int _randomLength = 3;
        // 计算时间间隔的开始时间
        private static readonly DateTime _startDateTime = new DateTime(2014, 1, 1);
        // 随机数缓存
        private static Hashtable _hashtable = new Hashtable();
        // 时间戳缓存（上一次计算Id的系统时间按时间戳刻度取值)
        private long _lastEndDateTimeTicks;

        private static Random _random;

        protected override long NextLong() {
            // 取得时间戳（当前时间按刻度取值）
            long timestamp = this.GetTimestamp(_startDateTime, _timescale);

            // 新一轮时间戳更新后更新缓存
            if(timestamp!=_lastEndDateTimeTicks) {
                _hashtable.Clear();
            }

            // 幂
            long power = long.Parse(Math.Pow(10, _randomLength).ToString());

            long result = 0;
            long index = 0;
            bool resultIsRepeated = false;
            do {
               // 随机数
                long rand = this.GetRandom(_randomLength);
                // 生成结果（Id）
                result = timestamp * power + rand;

                resultIsRepeated = _hashtable.ContainsKey(result);
                index++;
                if(index==power) {
                    break;
                }

            } while (resultIsRepeated);

            if(resultIsRepeated) {
                throw new CartisanException(ErrorCode.Exception, "生成的Id重复。");
            }
            _hashtable.Add(result, result);

            // 记录当前一轮时间戳（当前时间按刻度取值）
            this._lastEndDateTimeTicks = timestamp;

            return result;
        }

        private long GetTimestamp(DateTime startDateTime, long timestampStyleTicks) {
            if(timestampStyleTicks<=0) {
                throw new ArgumentException("时间戳刻度样式精度值不符，不能为0或负数。");
            }
            DateTime endDateTime = DateTime.Now;
            long ticks = (endDateTime.Ticks - startDateTime.Ticks) / timestampStyleTicks;

            return ticks;
        }

        private long GetRandom(int length) {
            if(length<=0) {
                throw new ArgumentException("随机数长度设置错误，长度必须大于0。");
            }
            if(_random==null) {
                _random = new Random();
            }
            int minValue = 0;
            int maxValue = int.Parse(Math.Pow(10, length).ToString());
            return long.Parse(_random.Next(minValue, maxValue).ToString());
        }
    }
}