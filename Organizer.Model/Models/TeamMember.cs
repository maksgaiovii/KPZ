using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPZ_Lab5.Models
{
    [Serializable]
    public class TeamMember
    {
        public int TeamMemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Editor,
        Illustrator,
        CoverDesigner
    }
}
