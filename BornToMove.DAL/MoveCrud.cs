using Microsoft.EntityFrameworkCore.ChangeTracking;
using BornToMove;
using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL;

public class MoveCrud
{
    public int Create(Move move)
    {
        var context = new MoveContext();
        var m = context.Moves.Add(move);
        context.SaveChanges();
        return m.Entity.Id;
    }

    public Move? Get(int id)
    {
        var context = new MoveContext();
        return context.Moves.Find(id);
    }

    public List<Move> GetAll()
    {
        var context = new MoveContext();
        return context.Moves.Include("Ratings").ToList();
    }

    public bool Update(Move move)
    {
        var context = new MoveContext();
        var m = context.Moves.Find(move.Id);
        context.Moves.Update(m);
        var affected = context.SaveChanges();
        return affected > 1;
    }

    public bool Delete(int id)
    {
        var context = new MoveContext();
        var m = context.Moves.Find(id);
        context.Moves.Remove(m);
        var affected = context.SaveChanges();

        return affected > 0;
    }
}