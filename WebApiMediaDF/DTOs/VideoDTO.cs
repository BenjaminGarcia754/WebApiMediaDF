public class VideoDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Ruta { get; set; }
    public string Imagen { get; set; }
    public DateTime FechaSubida { get; set; }
    public DateTime FechaModificacion { get; set; }
    public int Materia { get; set; }
    public int? Valoracion { get; set; }
}
