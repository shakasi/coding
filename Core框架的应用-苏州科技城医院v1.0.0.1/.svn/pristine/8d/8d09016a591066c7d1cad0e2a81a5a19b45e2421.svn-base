using DoCare.Hosting.Dtos;
using DoCare.Hosting.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VNext.Data;

namespace DoCare.Hosting
{
    public abstract partial class DoCareServiceBase
    {
        /// <summary>
        /// 获取 站内信信息查询数据集
        /// </summary>
        public IQueryable<LisReport> LabReports
        {
            get { return LabReportRepository.QueryAsNoTracking(); }
        }

        /// <summary>
        /// 检查站内信信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的站内信信息编号</param>
        /// <returns>站内信信息是否存在</returns>
        public virtual Task<bool> CheckLabReportExists(Expression<Func<LisReport, bool>> predicate, Guid id = default(Guid))
        {
            return LabReportRepository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 添加站内信信息
        /// </summary>
        /// <param name="dtos">要添加的患者登记信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> CreateLabReports(params SoapDto[] dtos)
        {
            Check.Validate<SoapDto, Guid>(dtos, nameof(dtos));
            return LabReportRepository.InsertAsync(dtos);
        }

        /// <summary>
        /// 更新站内信信息
        /// </summary>
        /// <param name="dtos">包含更新信息的患者登记DTO信息</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> UpdateLabReports(params SoapDto[] dtos)
        {
            Check.Validate<SoapDto, Guid>(dtos, nameof(dtos));
            return LabReportRepository.UpdateAsync(dtos);
        }

        /// <summary>
        /// 删除站内信信息
        /// </summary>
        /// <param name="ids">要删除的患者登记信息编号</param>
        /// <returns>业务操作结果</returns>
        public virtual Task<OperationResult> DeleteLabReports(params Guid[] ids)
        {
            Check.NotNull(ids, nameof(ids));
            return LabReportRepository.DeleteAsync(ids);
        }
    }
}
