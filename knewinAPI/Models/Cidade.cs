
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace knewinAPI.Models
{
    public class Cidade
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Habitantes { get; set; }  
        
         public string[] Fronteira { get; set; }

    }

}