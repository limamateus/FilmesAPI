namespace FilmesAPI.Dtos
{
    public class ReadCinemaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ReadEnderecoDto Endereco { get; set; }

        public virtual ICollection<ReadSessaoDto> Sessao { get; set; }
    }
}
