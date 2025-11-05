using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProjetoMacacos.Models
{
    public class Registros
    {
        [Key]
        public Guid RegistroId { get; set; }
        public string? Nome { get; set; }
        public Guid VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? Usuario { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}
