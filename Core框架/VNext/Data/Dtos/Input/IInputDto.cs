namespace VNext.Data
{
    /// <summary>
    /// 定义输入DTO
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IInputDto<TKey> : IDto
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        TKey Id { get; set; }
    }
}