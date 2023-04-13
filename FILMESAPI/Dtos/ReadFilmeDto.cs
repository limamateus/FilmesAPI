namespace FilmesAPI.Dtos
{
    public class ReadFilmeDto
    {

     
        public string Titulo { get; set; }
        
        public string Genero { get; set; }
        
        public int Duracao { get; set; }

        public DateTime DataHoraDeConsulta { get; set; } =  DateTime.Now;

    }
}
