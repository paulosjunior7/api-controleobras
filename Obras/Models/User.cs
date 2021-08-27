using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Project.Enums;

namespace Base.Project.Models
{
    public class User
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Password { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public bool Active { get; set; }
        public ePerfil Perfil { get; set; }

        public DateTime? ChangeDate { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
