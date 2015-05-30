using System;

namespace _1.GasEmission
{
    public class TimeUtil
    {
        /// <summary>
        ///     已重载.计算两个日期的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="DateTime1">第一个日期和时间</param>
        /// <param name="DateTime2">第二个日期和时间</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                var ts1 = new TimeSpan(DateTime1.Ticks);
                var ts2 = new TimeSpan(DateTime2.Ticks);
                var ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Days + "天"
                           + ts.Hours + "小时"
                           + ts.Minutes + "分钟"
                           + ts.Seconds + "秒";
            }
            catch
            {
            }
            return dateDiff;
        }

        /// <summary>
        ///     已重载.计算一个时间与当前本地日期和时间的时间间隔,返回的是时间间隔的日期差的绝对值.
        /// </summary>
        /// <param name="DateTime1">一个日期和时间</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1)
        {
            return DateDiff(DateTime1, DateTime.Now);
        }
    }
}