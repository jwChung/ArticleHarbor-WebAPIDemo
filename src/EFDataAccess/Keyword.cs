﻿namespace ArticleHarbor.EFDataAccess
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Keyword
    {
        [Key]
        [Column(Order = 0)]
        public int ArticleId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Word { get; set; }

        public virtual Article Article { get; set; }
    }
}