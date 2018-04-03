using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuscapi.Utils
{
    /// <summary>
    /// 非托管代码的父类
    /// </summary>
    public class DisposableBase : IDisposable
    {
        //保证不会重复释放资源
        protected bool _isDisposabled = false;

        ~DisposableBase()
        {
            //此处只需要释放非托管代码即可，因为GC调用时该对象资源可能还不需要释放
            Dispose(false);
        }

        public void Dispose()
        {
            //外部手动调用或者在using中自动调用，同时释放托管资源和非托管资源
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 虚方法，未实现，子类中需要重写
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposabled)
            {
                if (disposing)
                {
                    //释放托管资源(比如一些对象的Dispose())

                }
                //释放非托管资源(比如数据库连接的关闭)

                //释放大对象(参照MSDN的IDisposable.Dispose)


                _isDisposabled = true;
            }
        }
    }
}