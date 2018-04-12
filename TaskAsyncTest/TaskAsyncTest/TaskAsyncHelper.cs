using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaskAsyncTest
{
    /// <summary>
    /// 静态类：将普通的function方法执行异步调用
    /// </summary>
    public static class TaskAsyncHelper
    {
        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback  
        /// </summary>
        /// <param name="function"> 异步方法，该方法没有参数，返回类型必须是void </param>
        /// <param name="callback"> 异步方法执行完毕时执行的回调方法，该方法没有参数，返回类型必须是void </param>
        public static async void RunAsync(Action function, Action callback)
        {
            Func<Task> taskFunc = () =>
            {
                return Task.Run(() =>
                {
                    function();
                });
            };
            await taskFunc();

            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback
        /// </summary>
        /// <typeparam name="T1"> 异步方法参数类型 </typeparam>
        /// <typeparam name="T2"> 异步方法参数类型 </typeparam>
        /// <param name="function"> 异步方法，该方法有2个参数，返回类型必须是void </param>
        /// <param name="callback"> 异步方法执行完毕时执行的回调方法，该方法没有参数，返回类型必须是void  </param>
        /// <param name="par1"> 异步方法参数 </param>
        /// <param name="par2"> 异步方法参数 </param>
        public static async void RunAsync<T1, T2>(Action<T1, T2> function, Action callback, T1 par1, T2 par2)
        {
            Func<T1, T2, Task> taskFunc = (t1, t2) =>
            {
                return Task.Run(() =>
                {
                    function(t1, t2);
                });
            };
            await taskFunc(par1, par2);

            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback
        /// </summary>
        /// <typeparam name="T"> 异步方法的返回类型 </typeparam>
        /// <param name="function"> 异步方法，该方法没有参数，返回类型必须是TResult </param>
        /// <param name="callback"> 异步方法执行完毕时执行的回调方法，该方法参数为TResult，返回类型必须是void </param>
        public static async void RunAsync<T>(Func<T> function, Action<T> callback)
        {
            Func<Task<T>> taskFunc = () =>
            {
                return Task.Run(() =>
                {
                    return function();
                });
            };
            T rlt = await taskFunc();

            if (callback != null)
            {
                callback(rlt);
            }
        }
    }
}