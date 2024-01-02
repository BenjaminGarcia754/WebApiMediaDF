using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApiMediaDF.Controllers.Services
{
    public class VideoServices
    {
        private readonly WebApiMediaDbContex _contex;
        private readonly IMapper mapper;

        public VideoServices(WebApiMediaDbContex contex, IMapper mapper) {
            this._contex = contex;
            this.mapper = mapper;
        }

        public async Task<List<VideoDTO>> GetFavoritos(int id)
        {
            var videos = await (
                from favorito in _contex.VideoTypes
                join video in _contex.Videos on favorito.IdVideo equals video.Id
                where favorito.Tipo == "Favoritos" && favorito.IdUsuario == id
                select video
            ).ToListAsync();

            return mapper.Map<List<VideoDTO>>(videos);
        }


        public async Task<List<VideoDTO>> GetMasTarde(int id)
        {
            var videos = await (
                from tarde in _contex.VideoTypes
                join video in _contex.Videos on tarde.IdVideo equals video.Id
                where tarde.Tipo == "Tarde" && tarde.IdUsuario == id
                select video
            ).ToListAsync();

            return mapper.Map<List<VideoDTO>>(videos);
        }

        public async Task<List<VideoDTO>> GetPorMateria(int idMateria)
        {
            List<Video> videos = new List<Video>();
            var materias = _contex.Videos.Where(x => x.Materia == idMateria);
            return mapper.Map<List<VideoDTO>>(materias);
        }
    }
}
