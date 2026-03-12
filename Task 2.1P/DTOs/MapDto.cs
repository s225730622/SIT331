namespace Robot.Api.Dtos;

public record MapDto(
    int MapId,
    int MapSize,
    int XVal,
    int YVal
)
{
    public string Coordinate => $"{XVal}-{YVal}";
    public string MapArea => $"{MapSize}x{MapSize}";
}

