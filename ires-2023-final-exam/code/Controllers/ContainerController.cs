using Ires2023Exam.DataTransferObjects;
using Ires2023Exam.DbContexts;
using Ires2023Exam.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ires2023Exam.Controllers;

[ApiController]
[Route("[controller]")]
public class ContainerController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContainerController(AppDbContext db) 
    {
        _db = db;
    }

    [HttpGet("add-container")]
    public async Task<AddContainerOutputDto> AddContainer(AddContainerInputDto dto) {

        if (dto.SpotLength < 1 || dto.SpotLength > 3)
        {
            return new AddContainerOutputDto { Error = "Il container deve essere lungo tra 1 e 3." };
        }

        if (string.IsNullOrEmpty(dto.ContentType) || !IsValidContainerType(dto.ContentType))
        {
            return new AddContainerOutputDto { Error = "Container non valido." };
        }

        var spots = await _db.Spots
            .OrderBy(s => s.X)
                .ThenBy(s => s.Y)
            .ToListAsync();

        var grid = spots
            .GroupBy(s => s.X)
            .Select(g => g
                .OrderBy(x => x.Y)
                .ToArray()
            )
            .ToArray();
       
        if (!IsCompatible(dto.ContentType, grid))
        {
            return new AddContainerOutputDto { Error = "Il container è incompatibile con gli altri container." };
        }


        // trying horizontally
        for (var i = 0; i < grid.Length; i++) {
            for (var j = 0; j < grid[i].Length - dto.SpotLength + 1; j++) {
                bool fit = true;
                for (int k = 0; k < dto.SpotLength; k++) {
                    if (grid[i][j+k].ContainerId != null) {
                        fit = false;
                        break;
                    }
                }
                if (fit) {
                    var container = new ContainerEntity {
                        Code = dto.Code,
                        Length = dto.SpotLength,
                        Type = dto.ContentType,
                    };
                    _db.Containers.Add(container);
                    await _db.SaveChangesAsync();
                    for (int k = 0; k < dto.SpotLength; k++) {
                        grid[i][j+k].ContainerId = container.Id;
                    }
                    await _db.SaveChangesAsync();
                    return new AddContainerOutputDto { InsertedId = container.Id };
                }
            }
        }

        // trying vertically - omitted for brevity

        return new AddContainerOutputDto { Error = "No space for the new container!" };

        private bool IsValidContainerType(string contentType)
        {
            return ContainerTypes.IsValidType(contentType);
        }
        private bool IsCompatible(string contentType, SpotEntity[][] grid)
        {
            if (contentType == "FOOD")
            {
                foreach (var row in grid)
                {
                    foreach (var spot in row)
                    {
                        if (spot.Container != null && spot.Container.Type == "POISONOUS")
                        {
                            return false;
                        }
                    }
                }
            }
            return true;

    [HttpGet("get-empty-spots")]
            public async Task<ActionResult<List<AvailableSpotDto>>> GetEmptySpots()
            {
                var emptySpots = await _db.Spots
                    .Where(s => s.ContainerId == null)
                    .OrderBy(s => s.X)
                    .ThenBy(s => s.Y)
                    .Select(s => new AvailableSpotDto
                    {
                        X = s.X,
                        Y = s.Y
                    })
                    .ToListAsync();

                return emptySpots;
            }





        }
    }
