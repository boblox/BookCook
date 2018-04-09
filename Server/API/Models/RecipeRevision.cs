﻿using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class RecipeRevision
    {
        public int Id { get; set; }

        public RecipeData Data { get; set; }

        //public Recipe Recipe { get; set; }

        public int Version { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
