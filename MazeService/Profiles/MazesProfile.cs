using AutoMapper;
using MazeService.Dtos;
using MazeService.Models;

namespace MazeService.Profiles
{
    public class MazesProfile : Profile
    {
        public MazesProfile()
        {
            CreateMap<Maze, MazeReadDto>();
            CreateMap<MazeCreateDto, Maze>();
        }
    }
}