﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferServiceAPI.Models
{
    public class LocationCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
