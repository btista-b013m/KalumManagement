using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KalumManagement.Dtos;
using KalumManagement.Entities;

namespace KalumManagement.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        
        public AutoMapperProfiles()
        {
       CreateMap<Aspirante, CarreraTecnicaAspiranteListDTO>();
            CreateMap<CarreraTecnica, CarreraTecnicaListDTO>();
            CreateMap<Aspirante, JornadaAspiranteListDTO>();
            CreateMap<Jornada, JornadaListDTO>();
            CreateMap<CarreraTecnicaCreateDTO, CarreraTecnica>();
            CreateMap<AspiranteCreateDTO, Aspirante>();
            CreateMap<Aspirante, AspiranteDTO>().ConstructUsing(e => new AspiranteDTO{NombreCompleto = $"{e.Apellido} {e.Nombre}"});
            
            CreateMap<CarreraTecnica, CarreraTecnicaDTO>();
            CreateMap<CarreraTecnicaDTO, CarreraTecnica>();

            CreateMap<Jornada, JornadaDTO>();
            CreateMap<JornadaDTO, Jornada>();
           // CreateMap<Jornada, JornadaCreateDTO>();
            //CreateMap<JornadaCreateDTO, Jornada>();

            CreateMap<ExamenAdmision, ExamenAdmisionDTO>();
            CreateMap<ExamenAdmisionDTO, ExamenAdmision>();
            //CreateMap<ExamenAdmision, ExamenAdmisionCreateDTO>();
            //CreateMap<ExamenAdmisionCreateDTO, ExamenAdmision>();

            CreateMap<InversionCarreraTecnica, InversionCarreraTecnicaListDTO>();
            CreateMap<InversionCarreraTecnicaListDTO, InversionCarreraTecnica>();

            //CreateMap<Alumno, AlumnoCreateDTO>();
            //CreateMap<AlumnoCreateDTO, Alumno>();      
            //CreateMap<Alumno, AlumnoDTO>();
            //CreateMap<AlumnoDTO, Alumno>(); 
        }
    }
}