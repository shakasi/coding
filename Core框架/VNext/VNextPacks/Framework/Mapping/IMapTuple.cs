using VNext.Dependency;


namespace VNext.Mapping
{
    /// <summary>
    /// 定义对象映射源与目标配对
    /// </summary>
    [MultipleDependencyAttribute]
    public interface IMapTuple
    {
        /// <summary>
        /// 执行对象映射构造
        /// </summary>
        void CreateMap();
    }
}