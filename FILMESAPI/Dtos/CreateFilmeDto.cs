using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Dtos
{
    public class CreateFilmeDto
    {
       

        [Required(ErrorMessage = "O titulo do filme é obrigatório")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O genero é obrigatório")]
        [StringLength(50, ErrorMessage = "O Tamanho do gênero não pode exceder 50 caracaters")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "A duração do filme é obrigatório")]
        [Range(70, 600, ErrorMessage = "A duração deve ser entre {1} e {2}")]
        public int Duracao { get; set; }

    }
}
