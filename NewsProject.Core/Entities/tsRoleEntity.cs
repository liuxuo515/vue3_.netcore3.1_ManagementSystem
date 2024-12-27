using BusinessAccount.Entities;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Core.Entities
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "ts_Role")]
    public class tsRoleEntity : Entity<int>
    {
        public tsRoleEntity() { }

        [JsonProperty, Column(Name = "rolename")]
        public string rolename { get; set; }
        [JsonProperty, Column(Name = "account")]
        public string account { get; set; }
        [JsonProperty, Column(Name = "state")]
        public bool state { get; set; }
        [JsonProperty, Column(Name = "CreateUserId")]
        public int CreateUserId { get; set; }
        [JsonProperty, Column(Name = "CreateUserName")]
        public string CreateUserName { get; set; }
        [JsonProperty, Column(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty, Column(Name = "ModifyUserId")]
        public int ModifyUserId { get; set; }
        [JsonProperty, Column(Name = "ModifyUserName")]
        public string ModifyUserName { get; set; }
        [JsonProperty, Column(Name = "ModifyDate")]
        public DateTime? ModifyDate { get; set; }

        [JsonProperty, Column(Name = "IsDel")]
        public bool IsDel { get; set; }
    }
}
