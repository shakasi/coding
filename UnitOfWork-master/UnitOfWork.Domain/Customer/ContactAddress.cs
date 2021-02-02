namespace UnitOfWork.Customer
{
    /// <summary>
    /// ��ϵ�˵�ַ
    /// </summary>
    public class ContactAddress : Entity
    {
        public ContactAddress(string contactRealName, string contactPhone, string province, string city, string county,
            string street, string zip = "")
        {
            Province = province;
            City = city;
            County = county;
            Street = street;
            ContactRealName = contactRealName;
            ContactPhone = contactPhone;
            Zip = zip;
        }

        /// <summary>
        /// ��ϵ������
        /// </summary>
        public string ContactRealName { get; private set; }

        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string ContactPhone { get; private set; }

        /// <summary>
        /// ʡ��
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// ����
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// ����
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// �ֵ�
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Zip { get; private set; }

        /// <summary>
        /// ʡ�����ֵ�
        /// </summary>
        public string SimpleAddress => $"{Province} {City} {County} {Street}";

        /// <summary>
        /// �����ֵ�����
        /// </summary>
        public string DetailAddress => $"{County}{Street}({Zip})";

        public bool IsDefault { get; set; }
    }
}