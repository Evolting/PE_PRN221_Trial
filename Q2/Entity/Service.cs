using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Q2.Entity
{
    [Serializable]
    public partial class Service
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("RoomTitle")]
        public string? RoomTitle { get; set; }
        [XmlElement("Month")]
        public byte? Month { get; set; }
        [XmlElement("Year")]
        public int? Year { get; set; }
        [XmlElement("FeeType")]
        public string? FeeType { get; set; }
        [XmlElement("Amount")]
        public decimal? Amount { get; set; }
        [XmlElement("PaymentDate")]
        public DateTime? PaymentDate { get; set; }
        [XmlElement("Employee")]
        public int? Employee { get; set; }

        [XmlIgnoreAttribute]
        public virtual Employee? EmployeeNavigation { get; set; }
        [XmlIgnoreAttribute]
        public virtual Room? RoomTitleNavigation { get; set; }
    }
}
