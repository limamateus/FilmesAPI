using System.ComponentModel.DataAnnotations;

namespace FILMESAPI.Models
{
    public class Filme
    {
        [Key] // Chave Primaria 
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O titulo do filme é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O genero é obrigatório")]
        [MaxLength(50, ErrorMessage = "O Tamanho do gênero não pode exceder 50 caracaters")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "A duração do filme é obrigatório")]
        [Range(70, 600, ErrorMessage = "A duração deve ser entre {1} e {2}")]
        public int Duracao { get; set; }

    }
}
