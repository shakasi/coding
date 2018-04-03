using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Reflection;

namespace Transight.Tablet.REV.HQ.UploadData
{
    public class ServiceBase
    {
        private static readonly object _lockHelper = new object();

        //60*1000  5*60*1000  10*60*1000
        private static Dictionary<string, double> _errorTryAgainDic;

        static ServiceBase()
        {
            _errorTryAgainDic = new Dictionary<string, double>();
        }

        public static object ErrorTryAgain(Type t, string methodName, params object[] args)
        {
            methodName = methodName.ToLower();
            lock (_lockHelper)
            {
                if (_errorTryAgainDic.ContainsKey(methodName))
                {
                    _errorTryAgainDic.Remove(methodName);
                }
            }

            try
            {
                return DoWork(t, methodName, args);
            }
            catch (Exception ex)
            {
                //日志不需要返回
                if (methodName.Contains("log"))
                {
                    _errorTryAgainDic[methodName] = 1000;
                    TimerDoWork(t, methodName, args);
                    return null;
                }
                else
                {
                    throw ex;
                }
            }
        }

        private static void TimerDoWork(Type t, string methodName, params object[] args)
        {
            //老化执行
            System.Timers.Timer time = new System.Timers.Timer();
            lock (_lockHelper)
            {
                if (_errorTryAgainDic.ContainsKey(methodName))
                {
                    time.Interval = _errorTryAgainDic[methodName];
                }
                else
                {
                    return;
                }
                time.AutoReset = false;
                time.Elapsed += delegate (object sender, ElapsedEventArgs e)
                {
                    lock (_lockHelper)
                    {
                        try
                        {
                            if (!_errorTryAgainDic.ContainsKey(methodName))
                            {
                                return;
                            }
                            object obj = null;
                            obj = DoWork(t, methodName, args);
                            _errorTryAgainDic.Remove(methodName);
                            return;
                        }
                        catch (Exception ex)
                        {
                            if (_errorTryAgainDic.ContainsKey(methodName))
                            {
                                if (_errorTryAgainDic[methodName] == 1000)
                                {
                                    _errorTryAgainDic[methodName] = 5 * 1000;
                                    TimerDoWork(t, methodName, args);
                                }
                                else if (_errorTryAgainDic[methodName] == 5 * 1000)
                                {
                                    _errorTryAgainDic[methodName] = 10 * 1000;
                                    TimerDoWork(t, methodName, args);
                                }
                                else if (_errorTryAgainDic[methodName] == 10 * 1000)
                                {
                                    if (_errorTryAgainDic.ContainsKey(methodName))
                                    {
                                        _errorTryAgainDic.Remove(methodName);
                                    }

                                }
                            }
                        }
                    }
                };
                time.Start();
            }
        }

        private static object DoWork(Type t, string methodName, params object[] args)
        {
            return t.InvokeMember(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                , null, null, args);
        }
    }
}