namespace BornToMove.Business;

using BornToMove.DAL;

public class BuMove
{

    private static readonly MoveCrud DbWrapper = new MoveCrud();
    
    public static Move GetRandomMove()
    {
        var moves = DbWrapper.GetAll();
        var rd = new Random();
        return moves[rd.Next(0, moves.Count - 1)];
    }

    public static List<Move> GetAllMoves()
    {
        return DbWrapper.GetAll();
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

    public static bool UpdateMove(Move move)
    {
        return DbWrapper.Update(move);
    }
}