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
            List<Video> videos = new List<Video>();
            var favoritos = _contex.VideoTypes.Where(x => x.Tipo == "Tarde" && x.IdUsuario == id);
            foreach (VideoType favorito in favoritos)
            {
                var video = await _contex.Videos.FirstOrDefaultAsync(x => x.Id == favorito.IdVideo);
                if (video != null)
                {
                    videos.Add(video);
                }

            }
            return mapper.Map<List<VideoDTO>>(videos);
        }
    }
}
