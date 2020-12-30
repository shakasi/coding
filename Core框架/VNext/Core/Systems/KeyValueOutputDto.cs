using System;
using System.ComponentModel;
using VNext.Data;
using VNext.Entity;
using VNext.Mapping;

namespace VNext.Systems
{
    /// <summary>
    /// ���DTO:��ֵ����
    /// </summary>
    [MapFrom(typeof(KeyValue))]
    public class KeyValueOutputDto : IOutputDto
    {
        /// <summary>
        /// ��ȡ������ ���
        /// </summary>
        [DisplayName("���")]
        public Guid Id { get; set; }

        /// <summary>
        /// ��ȡ������ ����ֵJSON
        /// </summary>
        [DisplayName("����ֵJSON")]
        public string ValueJson { get; set; }

        /// <summary>
        /// ��ȡ������ ����ֵ������
        /// </summary>
        [DisplayName("����ֵ������")]
        public string ValueType { get; set; }

        /// <summary>
        /// ��ȡ������ ���ݼ���
        /// </summary>
        [DisplayName("���ݼ���")]
        public string Key { get; set; }

        /// <summary>
        /// ��ȡ������ ����ֵ
        /// </summary>
        [DisplayName("����ֵ")]
        public object Value { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�����
        /// </summary>
        [DisplayName("�Ƿ�����")]
        public bool IsLocked { get; set; }
    }
}