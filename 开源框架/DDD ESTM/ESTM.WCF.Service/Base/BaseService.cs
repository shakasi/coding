using AutoMapper;
using ESTM.Common.DtoModel;
using ESTM.Domain;
using ESTM.Infrastructure.MEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace ESTM.WCF.Service
{
    public class BaseService
    {
        #region Fields
        private bool bInitAutoMapper = false;
        #endregion

        #region Construct 
        public BaseService()
        {
            //注册MEF
            Regisgter.regisgter().ComposeParts(this);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 通用单表查询方法
        /// </summary>
        /// <typeparam name="DtoModel">DTOmodel</typeparam>
        /// <typeparam name="DomainModel">领域模型</typeparam>
        /// <param name="oRepository">需要传过来的仓储接口对象</param>
        /// <param name="selector">前端传过来的lamada表达式</param>
        /// <returns></returns>
        public List<DtoModel> GetDtoByLamada<DtoModel, DomainModel>(IRepository<DomainModel> oRepository, Expression<Func<DtoModel, bool>> selector = null)
            where DomainModel : AggregateRoot
            where DtoModel : Dto_BaseModel
        {
            InitAutoMapper<DtoModel, DomainModel>();
            if (selector == null)
            {
                var lstDomainModel = oRepository.Entities.ToList();
                return Mapper.Map<List<DomainModel>, List<DtoModel>>(lstDomainModel);
            }
            //得到从Web传过来和DTOModel相关的lamaba表达式的委托
            Func<DtoModel, bool> match = selector.Compile();
            //创建映射Expression的委托
            Func<DomainModel, DtoModel> mapper = AutoMapper.QueryableExtensions.Extensions.CreateMapExpression<DomainModel, DtoModel>(Mapper.Engine).Compile();
            //得到领域Model相关的lamada
            Expression<Func<DomainModel, bool>> lamada = ef_t => match(mapper(ef_t));
            List<DomainModel> list = oRepository.Find(lamada).ToList();
            return Mapper.Map<List<DomainModel>, List<DtoModel>>(list);
        } 
        #endregion

        #region 新增
        public DtoModel AddDto<DtoModel, DomainModel>(IRepository<DomainModel> oRepository, DtoModel oDtoModel)
            where DomainModel : AggregateRoot
            where DtoModel : Dto_BaseModel
        {
            InitAutoMapper<DtoModel, DomainModel>();
            var oDomain = Mapper.Map<DtoModel, DomainModel>(oDtoModel);
            oRepository.Insert(oDomain);
            return Mapper.Map<DomainModel, DtoModel>(oDomain);
        }
        #endregion

        #region 删除
        public int DeleteDto<DtoModel, DomainModel>(IRepository<DomainModel> oRepository, DtoModel oDtoModel)
            where DomainModel : AggregateRoot
            where DtoModel : Dto_BaseModel
        {
            InitAutoMapper<DtoModel, DomainModel>();
            var oDomain = Mapper.Map<DtoModel, DomainModel>(oDtoModel);
            return oRepository.Delete(oDomain);
        }

        public int DeleteDto<DtoModel, DomainModel>(IRepository<DomainModel> oRepository, Expression<Func<DtoModel, bool>> selector = null)
            where DomainModel : AggregateRoot
            where DtoModel : Dto_BaseModel
        {
            InitAutoMapper<DtoModel, DomainModel>();
            if (selector == null)
            {
                return 0;
            }
            //得到从Web传过来和DTOModel相关的lamaba表达式的委托
            Func<DtoModel, bool> match = selector.Compile();
            //创建映射Expression的委托
            Func<DomainModel, DtoModel> mapper = AutoMapper.QueryableExtensions.Extensions.CreateMapExpression<DomainModel, DtoModel>(Mapper.Engine).Compile();
            //得到领域Model相关的lamada
            Expression<Func<DomainModel, bool>> lamada = ef_t => match(mapper(ef_t));
            return oRepository.Delete(lamada);
        } 
        #endregion

        #region 更新
        public void UpdateDto<DtoModel, DomainModel>(IRepository<DomainModel> oRepository, DtoModel oDtoModel)
            where DomainModel : AggregateRoot
            where DtoModel : Dto_BaseModel
        {
            InitAutoMapper<DtoModel, DomainModel>();
            var oDomain = Mapper.Map<DtoModel, DomainModel>(oDtoModel);
            oRepository.Update(oDomain);
        }
        #endregion

        #region Private
        private void InitAutoMapper<DtoModel, DomainModel>()
        {
            var oType = Mapper.FindTypeMapFor<DtoModel, DomainModel>();
            if (oType==null)
            {
                Mapper.CreateMap<DtoModel, DomainModel>();
                Mapper.CreateMap<DomainModel, DtoModel>();
            }
        }
        #endregion
    }
}
