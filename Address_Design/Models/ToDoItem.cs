﻿using System.ComponentModel.DataAnnotations;

namespace Address_Design.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsDone { get; set; }
    }
}