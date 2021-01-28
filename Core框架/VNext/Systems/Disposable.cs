using System;

namespace VNext.Systems
{
    /// <summary>
    /// <see cref="IDisposable"/>重要对象Disposable基类
    /// 一般来说资源释放分为:1.GC回收(托管资源);2.IDispose释放(托管资源以及非托管资源)
    /// 1.System.Object 具有一个Finalize()的虚空方法，C#中通过~Name(析构函数)来重写。
    ///   当内存中删除这个类型的对象/Net框架的自发回收/GC.Collect()强制垃圾回收  都会触发Finalize();
    /// 2.垃圾回收生效时，可以利用终结器来释放非托管资源。然而，很多非托管资源都非常宝贵（如数据库和文件句柄）
    ///   ，所以它们应该尽可能快的被清除，而不能依靠垃圾回收的发生。除了重写Finalize之外，类还可以实现IDisposable接口，
    ///   然后在代码中主动调用Dispose方法来释放资源。通过Using(){},Dispose()等方式触发。
    /// 
    /// 注意点:Finalize可以通过垃圾回收进行自动的调用，而Dispose需要被代码显式的调用，所以，为了保险起见，对于一些非托管资源，还是有必要实现终结器的(析构函数中实现)。
    /// 也就是说，如果我们忘记了显示的调用Dispose，那么垃圾回收也会调用Finalize，从而保证非托管资源的回收。
    /// Dispse()方法中因为释放了托管资源，后期终结器回收的时候需跳过此对象回收。
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        /// 是否被程序主动处理释放
        /// </summary>
        protected bool Disposed { get; private set; }

        /// <summary>IDispose接口方法实现</summary>
        public void Dispose()
        {
            //程序主动处理释放
            Dispose(true);

            //调用SuppressFinalize()方法就意味着,标识后期垃圾回收器跳过处理当前对象的析构函数
            GC.SuppressFinalize(this);
        }

        //GC回收触发，系统处理
        ~Disposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// disposing为true，表示手动释放，此时应释放所有资源;
        /// disposing为false，只需释放非托管资源;
        /// </summary>
        /// <param name="disposing">释放模式</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disposed = true;
            }
        }
    }
}