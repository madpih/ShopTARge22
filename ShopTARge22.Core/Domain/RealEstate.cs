﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.Core.Domain
{
    public class RealEstate
    {
        [Key]
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public float SizeSqrM { get; set; }
        public int RoomCount { get; set; }
        public int Floor { get; set; }
        public string BuildingType { get; set; }
        public DateTime BuiltInYear{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}



    }
}
