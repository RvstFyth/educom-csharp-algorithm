namespace BornToMove.Business;

using BornToMove.DAL;

public class BuMove
{

    private static readonly MoveCrud DbWrapper = new MoveCrud();
    
    public static async Task<Move> GetRandomMoveAsync()
    {
        var moves = await DbWrapper.GetAllAsync();
        var rd = new Random();
        return moves[rd.Next(0, moves.Count - 1)];
    }

    public static async Task<List<Move>> GetAllMovesAsync()
    {
        return await DbWrapper.GetAllAsync();
    }

    public static async Task<Move?> SaveMoveAsync(string name, string description, int sweatRate)
    {
        if (String.IsNullOrEmpty(name)) return null;

        var move = new Move()
        {
            Name = name,
            Description = description,
            SweatRate = sweatRate
        };

        var id = await DbWrapper.CreateAsync(move);
        move.Id = id;
        return move;
    }

    public static async Task<bool> AddRatingAsync(Move move, MoveRating rating)
    {
        if (rating.Rating < 1) rating.Rating = 1;
        else if (rating.Rating > 5) rating.Rating = 5;

        if (rating.Intensity < 1) rating.Intensity = 1;
        else if (rating.Intensity > 5) rating.Intensity = 5;
        
        return await DbWrapper.AddRatingAsync(move, rating);
    }
    
    public static bool UpdateMove(Move move)
    {
        return DbWrapper.Update(move);
    }
}