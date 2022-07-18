namespace BornToMove.Business;

using BornToMove.DAL;

public class BuMove
{

    private static readonly MoveCrud DbWrapper = new MoveCrud();
    
    public static Move GetRandomMove()
    {
<<<<<<< Updated upstream
        var moves = DbWrapper.GetAll();
=======
        var moves = await DbWrapper.GetAllAsync();
>>>>>>> Stashed changes
        var rd = new Random();
        return moves[rd.Next(0, moves.Count - 1)];
    }

    public static List<Move> GetAllMoves()
    {
<<<<<<< Updated upstream
        return DbWrapper.GetAll();
=======
        return await DbWrapper.GetAllAsync();
>>>>>>> Stashed changes
    }

    public static Move? SaveMove(string name, string description, int sweatRate)
    {
        if (String.IsNullOrEmpty(name)) return null;

        var move = new Move()
        {
            Name = name,
            Description = description,
            SweatRate = sweatRate
        };

        var id = DbWrapper.Create(move);
        move.Id = id;
        return move;
    }

    public static bool AddRating(Move move, MoveRating rating)
    {
        if (rating.Rating < 1) rating.Rating = 1;
        else if (rating.Rating > 5) rating.Rating = 5;

        if (rating.Intensity < 1) rating.Intensity = 1;
        else if (rating.Intensity > 5) rating.Intensity = 5;
        
        return DbWrapper.AddRating(move, rating);
    }
    
    public static bool UpdateMove(Move move)
    {
        return DbWrapper.Update(move);
    }
}