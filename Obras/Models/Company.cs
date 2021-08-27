using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Base.Project.Enums;
using Base.Project.Models;

namespace Base.Project.Models
{
    public class Company
    {
        public Company()
        {
            Users = new List<User>();
        }
         
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracteres")]
        public string CorporateName { get; set; }
        public string FantasyName { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Telephone { get; set; }
        public string CellPhone { get; set; }
        public string EMail { get; set; }
        public bool Active { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public virtual List<User> Users { get; set; }

    }
}
