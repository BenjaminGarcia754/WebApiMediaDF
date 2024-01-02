using AutoMapper;
using WebApiMediaDF.Modelos;
using WebApiMediaDF.Modelos.DTOs;

namespace WebApiMediaDF.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<ComentarioDTO, Comentarios>().ReverseMap();
            CreateMap<VideoDTO, Video>().ReverseMap();
            CreateMap<MateriaDTO, Materia>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<ReporteDTO, Reporte>().ReverseMap();
            CreateMap<TipoReporteDTO, TipoReporte>().ReverseMap();
            CreateMap<TipoUsuario, TipoUsuarioDTO>().ReverseMap();
            CreateMap<VideoTypeDTO,  VideoType>().ReverseMap();
            CreateMap<CalificacionVideoDTO, CalificacionVideo>().ReverseMap();
        }
    }
}
