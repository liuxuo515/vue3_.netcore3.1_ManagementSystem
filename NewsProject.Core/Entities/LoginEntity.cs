using BusinessAccount.Entities;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Core.Entities
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "ts_LawWitLawyers")]
    public class LoginEntity : Entity<int>
    {
        public LoginEntity() { }

        [JsonProperty, Column(Name = "LawWitUserID")]
        public int LawWitUserID { get; set; }
        [JsonProperty, Column(Name = "OfficeID")]
        public int OfficeID { get; set; }
        [JsonProperty, Column(Name = "UserName")]
        public string UserName { get; set; }
        [JsonProperty, Column(Name = "RealName")]
        public string RealName { get; set; }
        [JsonProperty, Column(Name = "Sex")]
        public string Sex { get; set; }
        [JsonProperty, Column(Name = "Address")]
        public string Address { get; set; }
        [JsonProperty, Column(Name = "Phone")]
        public string Phone { get; set; }
        [JsonProperty, Column(Name = "PersonType")]
        public string PersonType { get; set; }
        [JsonProperty, Column(Name = "Deparment")]
        public string Deparment { get; set; }
        [JsonProperty, Column(Name = "IDType")]
        public string IDType { get; set; }
        [JsonProperty, Column(Name = "IDCard")]
        public string IDCard { get; set; }
        [JsonProperty, Column(Name = "LicenceNumber")]
        public string LicenceNumber { get; set; }
        [JsonProperty, Column(Name = "Mail")]
        public string Mail { get; set; }
        [JsonProperty, Column(Name = "Political")]
        public string Political { get; set; }
        [JsonProperty, Column(Name = "Graduation")]
        public string Graduation { get; set; }
        [JsonProperty, Column(Name = "Qualification")]
        public string Qualification { get; set; }
        [JsonProperty, Column(Name = "PersonnelStatus")]
        public int PersonnelStatus { get; set; }
        [JsonProperty, Column(Name = "Jobs")]
        public string Jobs { get; set; }
        [JsonProperty, Column(Name = "IsForgein")]
        public bool IsForgein { get; set; }
        [JsonProperty, Column(Name = "IsPublic")]
        public bool IsPublic { get; set; }
        [JsonProperty, Column(Name = "IsDel")]
        public bool IsDel { get; set; }
        [JsonProperty, Column(Name = "InputTime")]
        public DateTime? InputTime { get; set; }
        [JsonProperty, Column(Name = "UpdateTime")]
        public DateTime? UpdateTime { get; set; }
        [JsonProperty, Column(Name = "UpdateCnt")]
        public int UpdateCnt { get; set; }
    }
}
