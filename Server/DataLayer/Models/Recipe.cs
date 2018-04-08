﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Deleted { get; set; }

        public ICollection<RecipeRevision> Revisions { get; set; }
    }
}
