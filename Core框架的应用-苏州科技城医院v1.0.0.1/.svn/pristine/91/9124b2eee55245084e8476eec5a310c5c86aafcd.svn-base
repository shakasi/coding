using DoCare.Hosting.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using VNext.Dependency;

namespace DoCare.Hosting.IContracts
{
    /// <summary>
    /// 患者信息接口
    /// </summary>
    [MultipleDependency]
    public interface IPatient
    {
        /// <summary>
        /// 同步患者信息
        /// </summary>
        Task<PatientOutDto> GetPatient(PatientInDto dto);

        /// <summary>
        /// 查询对应条件的患者基本信息
        /// </summary>
        /// <param name="dto">患者登记查询入参信息</param>
        /// <returns></returns>
        Task<List<PatientRegistOutDto>> QueryRegist(PatientRegistInDto dto);

    }
}
