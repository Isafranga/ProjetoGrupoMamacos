using System.ComponentModel.DataAnnotations;

namespace ProjetoMacacos.Models
{
    public class Veiculo
    {
        [Key]
        public Guid VeiculoId { get; set; }

        [Display(Name = "Foto do Veículo")]
        public string? UrlFoto { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public bool Disponivel { get; set; } = true;
        public ICollection<Registros>? Registros { get; set; }
    }
}

