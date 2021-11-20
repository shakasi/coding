using System.ComponentModel;

namespace DoCare.Hosting.Dtos
{
    /// <summary>
    /// 卡号类型
    /// </summary>
    public enum CardType
    {
        [Description("身份证号")]
        IdNo = 1,
        [Description("医保卡号")]
        CardNo,
        [Description("就诊卡号")]
        VisitNo
    }

    public enum SexType
    {
        [Description("未知得性别")]
        UnKnow = 0,
        [Description("男")]
        Male,
        [Description("女")]
        Female
    }

    public enum SoapType
    {
        [Description("登记列表")]
        PatientRegist = 1,
        [Description("检验")]
        Lab,
        [Description("检查")]
        Pacs
    }
}
