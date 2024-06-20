using FI.WebAtividadeEntrevista.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [ValidarCPF(ErrorMessage = "CPF inválido. Verifique as informações, por gentileza.")]
        [Required]
        public string CPF { get; set; }

        public string CpfCliente { get; set; }

        [ForeignKey("Cliente")]
        public long IdCliente { get; set; }

        public virtual ClienteModel Cliente { get; set; }
    }
}