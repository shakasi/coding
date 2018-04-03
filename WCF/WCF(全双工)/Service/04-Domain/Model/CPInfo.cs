using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuscapi.Model
{
    /// <summary>
    /// 产品
    /// </summary>
    public class CPInfo
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public int LINTITEMNUMBER { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string STRPRODUCTDESCRIPTION { get; set; }

        /// <summary>
        /// 产品品牌
        /// </summary>
        public string STRDEPTCODE { get; set; }

        /// <summary>
        /// 产品分类（POS）
        /// </summary>
        public string STRCATGORY { get; set; }

        /// <summary>
        /// 产品是否激活
        /// </summary>
        public string STRYSNACTIVE { get; set; }

        /// <summary>
        /// 产品是否套餐
        /// </summary>
        public string STRYSNSETMEAL { get; set; }

        /// <summary>
        /// 产品是否可以更改口味
        /// </summary>
        public string STRYSNFLAVOR { get; set; }

        /// <summary>
        /// 产品发布类型（1-ALL 2-堂食 3-外卖 4-电商 5-堂食+外卖 6-堂食+电商 7-外卖+电商)
        /// </summary>
        public string STRITEMTYPE { get; set; }
    }

    /// <summary>
    /// 做法
    /// </summary>
    public class ZFInfo
    {
        /// <summary>
        /// 门店编码
        /// </summary>
        public string STRTRADECODE { get; set; }

        /// <summary>
        /// 口味类别
        /// </summary>
        public int LINTMODIFIERCODE { get; set; }

        /// <summary>
        /// 口味编码
        /// </summary>
        public int CTRCODE { get; set; }

        /// <summary>
        /// 口味名称
        /// </summary>
        public string STRDESCRIPTION { get; set; }

        /// <summary>
        /// 产品编码（此编码不为空表示当前口味为一个正式的可作为直接销售的产品（有价格））
        /// </summary>
        public int LINTITEMNUMBER { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public string YSNACTIVE { get; set; }
    }

    /// <summary>
    /// 套餐明细
    /// </summary>
    public class TCMXInfo
    {
        /// <summary>
        /// 组合套餐产品主编码
        /// </summary>
        public int LINTITEMNUMBER { get; set; }

        /// <summary>
        /// 自增长唯一标识号
        /// </summary>
        public int CTRCODE { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int INTSEQUENCE { get; set; }

        /// <summary>
        /// 组合套餐默认售卖产品编号
        /// </summary>
        public int LINTINGREDIENTITEM { get; set; }

        /// <summary>
        /// 组合套餐默认售卖产品的起售数量
        /// </summary>
        public int DBLQTY { get; set; }
    }

    /// <summary>
    /// 套餐替换明细
    /// </summary>
    public class TCTHMXInfo
    {
        /// <summary>
        /// 组合套餐主产品编码
        /// </summary>
        public int LINTITEMNUMBER { get; set; }

        /// <summary>
        /// 组合套餐默认售卖产品编码
        /// </summary>
        public int LINTINGREDIENTITEM { get; set; }

        /// <summary>
        /// 自增长唯一标识号
        /// </summary>
        public int CTRCODE { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int INTSEQUENCE { get; set; }

        /// <summary>
        /// 组合套餐默认售卖产品对应的可替换产品编码
        /// </summary>
        public int LINTREPLACEITEM { get; set; }
    }

    /// <summary>
    /// 价格
    /// </summary>
    public class PriceInfo
    {
        /// <summary>
        /// 门店编码
        /// </summary>
        public string STRSTORECODE { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public int LINTITEMNUMBER { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal CURSELL { get; set; }

        /// <summary>
        /// 是否可销售
        /// </summary>
        public string YSNACTIVE { get; set; }
    }

    /// <summary>
    /// 分类类
    /// </summary>
    public class ClassInfo
    {
        /// <summary>
        /// 分类编码
        /// </summary>
        public string STRCATCODE { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string STRCATDESCRIPTION { get; set; }

        /// <summary>
        /// 是否可用（F-不可用 T-可用）
        /// </summary>
        public string YSNACTIVE { get; set; }
    }

    /// <summary>
    /// 品牌类
    /// </summary>
    public class BrandInfo
    {
        /// <summary>
        /// 品牌代码
        /// </summary>
        public string STRDEPTCODE { get; set; }

        /// <summary>
        /// 品名名称
        /// </summary>
        public string STRDEPTDESCRIPTION { get; set; }

        /// <summary>
        /// 是否激活（F-不可用 T-可用）
        /// </summary>
        public string YSNACTIVE { get; set; }
    }

    /// <summary>
    /// 门店标准菜明细类
    /// </summary>
    public class CPDetailInfo
    {
        /// <summary>
        /// 味千菜单分组ID
        /// </summary>
        public string STRMENUGROUPID { get; set; }

        /// <summary>
        /// 味千门店分组ID
        /// </summary>
        public string STRSTOREGROUPID { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public string STRTRADECODE { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public int LINTITEMNUMBER { get; set; }
    }
}