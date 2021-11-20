using DoCare.Hosting.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using VNext.Dependency;

namespace DoCare.Hosting.IContracts
{
    /// <summary>
    /// 检查相关接口
    /// </summary>
    [MultipleDependency]
    public interface IExam
    {
        /// <summary>
        /// 获取患者检查信息
        /// </summary>
       Task<List<ExamReportOutDto>> QueryExam(CommonInDto dto);
    }
}
