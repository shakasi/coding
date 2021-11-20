using DoCare.Hosting.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using VNext.Dependency;

namespace DoCare.Hosting.IContracts
{
    /// <summary>
    /// 检验相关接口
    /// </summary>
    [MultipleDependency]
    public interface ILis
    {
        Task<List<LisReportOutDto>> QueryLisReport(CommonInDto dto);
    }
}
