using System;
using System.Text.Json.Serialization;

namespace Planner.Models
{
    public class User
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        [JsonIgnore] public string Password { set; get; }
    }
}
