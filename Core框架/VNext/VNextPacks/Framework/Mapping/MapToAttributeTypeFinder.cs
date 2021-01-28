using VNext.Finders;

namespace VNext.Mapping
{
    /// <summary>
    /// 标注了<see cref="MapToAttribute"/>标签的类型查找器
    /// </summary>
    public class MapToAttributeTypeFinder : AttributeTypeFinderBase<MapToAttribute>, IMapToAttributeTypeFinder
    {
        /// <summary>
        /// 初始化一个<see cref="MapToAttributeTypeFinder"/>类型的新实例
        /// </summary>
        public MapToAttributeTypeFinder(IAppAssemblyFinder allAssemblyFinder)
            : base(allAssemblyFinder)
        { }
    }
}