using System.ComponentModel.DataAnnotations;

namespace ProjetoMacacos.Models
{
    public class Clientes
    {
        [Key]
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public string Celular { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNacimento { get; set; }
        public string Endereço { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
    }
}
