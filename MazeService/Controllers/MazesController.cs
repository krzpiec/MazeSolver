using Microsoft.AspNetCore.Mvc;
using MazeService.Data;
using AutoMapper;
using MazeService.Dtos;
using MazeService.Models;
using MazeService.SyncDataServices;
using System.Text.Json;

namespace MazeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MazesController : ControllerBase
    {
        private readonly IMazeRepo _repository;
        private readonly IMapper _mapper;
        private readonly IBlobStorageClient _blobStorageClient;
        private readonly IMazeGenDataClient _mazeGenClient;
        private readonly IMazeSolveDataClient _mazeSolveDataClient;

        public MazesController(
            IMazeRepo repository,
            IMapper mapper,
            IBlobStorageClient blobStorageClient,
            IMazeGenDataClient mazeGenClient,
            IMazeSolveDataClient mazeSolveDataClient
            )
        {
            _repository = repository;
            _mapper = mapper;
            _blobStorageClient = blobStorageClient;
            _mazeGenClient = mazeGenClient;
            _mazeSolveDataClient = mazeSolveDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MazeReadDto>> GetMazes()
        {
            var mazeItem = _repository.GetAllMazes();

            return Ok(_mapper.Map<IEnumerable<MazeReadDto>>(mazeItem));
        }

        [HttpGet("{id}", Name = "GetMazeById")]
        public ActionResult<MazeReadDto> GetMazeById(long id)
        {
            var mazeItem = _repository.GetMazeById(id);
            if (mazeItem != null)
            {
                return Ok(_mapper.Map<MazeReadDto>(mazeItem));
            }

            return NotFound();
        }

        [HttpPost("create", Name = "CreateMaze")]
        public async Task<ActionResult<MazeReadDto>> CreateMaze(MazeCreateDto mazeCreateDto)
        {
            var mazeModel = _mapper.Map<Maze>(mazeCreateDto);
            var entityId = _repository.CreateMaze(mazeModel);
            _repository.saveChanges();
            var status  = await _mazeGenClient.GenerateMaze(new MazeGenerateRequest(){Id = entityId, SizeX = mazeCreateDto.SizeX, SizeY = mazeCreateDto.SizeY });
            if(status == 0)
            {
                Console.WriteLine("Creation failed");
                return NotFound();//TODO tutaj
            }
            var mazeModelDto = _mapper.Map<MazeReadDto>(mazeModel);
            return CreatedAtRoute(nameof(CreateMaze), new { Id = mazeModelDto.Id }, mazeModelDto);
        }


        [HttpPost("solve", Name = "solveMaze")]
        public async Task<ActionResult<MazePath>> solveMaze(MazeSolveRequest mazeSolveRequest)
        {
           return await _mazeSolveDataClient.SolveMaze(mazeSolveRequest);
        }

        [HttpGet("blob/{id}", Name = "getMazeBlob")]
        public async Task<ActionResult<MazeDto>> getMazeBlob(long id)
        {
            var response = await _blobStorageClient.GetMazeBlob(id);
            var jsonResponse = JsonSerializer.Deserialize<List<List<CellDto>>>(response);
            if(jsonResponse == null)
            {
                return new MazeDto(){};
            }
            return new MazeDto(){cells = jsonResponse};
        }
    }
}