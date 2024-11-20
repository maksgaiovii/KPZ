using KPZ_lab5.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KPZ_lab5.Models
{
    [Serializable]
    public class TeamMember
    {
        [Key]
        public int TeamMemberId { get; set; }

        [Required, MaxLength(40)]
        public string Name { get; set; }

        [Required, MaxLength(40)]
        public string Surname { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }

        // Navigation property for the relationship with ContributorHistory
        public ICollection<ContributorHistory>? ContributorHistories { get; set; }
    }

    [JsonConverter(typeof(RoleConverter))]
    public enum Role
    {
        Editor,
        Illustrator,
        CoverDesigner
    }
}
