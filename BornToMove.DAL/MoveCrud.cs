using Microsoft.EntityFrameworkCore.ChangeTracking;
using BornToMove;
using Microsoft.EntityFrameworkCore;
using Organizer;

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

<<<<<<< Updated upstream
    public List<Move> GetAll()
=======
    public async Task<List<Move>> GetAllAsync()
>>>>>>> Stashed changes
    {
        var context = new MoveContext();
        var movesList = context.Moves.Include("Ratings").ToList();

        var sorter = new RotateSort<Move>();
        movesList = sorter.Sort(movesList, new RatingConverter());
        return movesList;
    }

    public bool Update(Move move)
    {
        var context = new MoveContext();
        var m = context.Moves.Find(move.Id);
        if (m == null) return false;
        
        context.Moves.Update(m);
        var affected = context.SaveChanges();
        return affected > 1;
    }

    public bool AddRating(Move move, MoveRating rating)
    {
        var context = new MoveContext();
<<<<<<< Updated upstream
        var m = context.Moves.Include("Ratings").FirstOrDefault(m => m == move);
        if (m == null) return false;
        
        m.Ratings.Add(rating);
        var affected = context.SaveChanges();
=======
        // var moves = await GetAllAsync();
        var m = await context.Moves.Include("Ratings").FirstAsync(m => m.Id == move.Id);
        m?.Ratings.Add(rating);
        int affected = await context.SaveChangesAsync();
        Console.WriteLine("Aff: " + affected);
>>>>>>> Stashed changes
        return affected > 0;
    }
    
    public bool Delete(int id)
    {
        var context = new MoveContext();
        var m = context.Moves.Find(id);
        if (m == null) return false;
        
        context.Moves.Remove(m);
        var affected = context.SaveChanges();
        return affected > 0;
    }
}