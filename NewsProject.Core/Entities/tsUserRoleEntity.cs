using BusinessAccount.Entities;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Core.Entities
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "ts_UserRole")]
    public class tsUserRoleEntity : Entity<int>
    {
        public tsUserRoleEntity() { }
        [JsonProperty, Column(Name = "UserId")]
        public int UserId { get; set; }
        [JsonProperty, Column(Name = "RoleId")]
        public int RoleId { get; set; }
        [JsonProperty, Column(Name = "IsDel")]
        public bool IsDel { get; set; }
    }
}
