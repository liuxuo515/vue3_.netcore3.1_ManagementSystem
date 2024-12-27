using BusinessAccount.Entities;
using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsProject.Core.Entities
{
    [JsonObject(MemberSerialization.OptIn), Table(Name = "ts_Permiss")]

    public class tsPermissEntity : Entity<int>
    {
        public tsPermissEntity() { }
        [JsonProperty, Column(Name = "roleId")]
        public int roleId { get; set; }
        [JsonProperty, Column(Name = "permiss")]
        public string permiss { get; set; }
        [JsonProperty, Column(Name = "IsDel")]
        public bool IsDel { get; set; }
    }
}
