namespace BornToMove.Business;

using BornToMove.DAL;

public class BuMove
{

    private static readonly MoveCrud DbWrapper = new MoveCrud();
    
    public static async Task<Move> GetRandomMove()
    {
        var moves = await DbWrapper.GetAll();
        var rd = new Random();
        return moves[rd.Next(0, moves.Count - 1)];
    }

    public static async Task<List<Move>> GetAllMoves()
    {
        return await DbWrapper.GetAll();
    }

    public static async Task<Move?> SaveMove(string name, string description, int sweatRate)
    {
        if (String.IsNullOrEmpty(name)) return null;

        var move = new Move()
        {
            Name = name,
            Description = description,
            SweatRate = sweatRate
        };

        int id = await DbWrapper.Create(move);
        move.Id = id;
        return move;
    }

    public static async Task<bool> AddRating(Move move, MoveRating rating)
    {
        if (rating.Rating < 1) rating.Rating = 1;
        else if (rating.Rating > 5) rating.Rating = 5;

        if (rating.Intensity < 1) rating.Intensity = 1;
        else if (rating.Intensity > 5) rating.Intensity = 5;
        
        return await DbWrapper.AddRating(move, rating);
    }
    
    public static bool UpdateMove(Move move)
    {
        return DbWrapper.Update(move);
    }
}