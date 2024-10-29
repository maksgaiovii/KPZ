﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_lab5.Models
{
    [Serializable]
    public class ContributorHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        [ForeignKey("TeamMember")]
        public int ContributorId { get; set; }
        public TeamMember Contributor { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        [Required]
        public ContributorStatus ContributorStatus { get; set; }
    }

    public enum ContributorStatus
    {
        Active,
        Inactive,
        Completed
    }
}