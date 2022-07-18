using Microsoft.EntityFrameworkCore.ChangeTracking;
using BornToMove;
using Microsoft.EntityFrameworkCore;
using Organizer;

namespace BornToMove.DAL;

public class MoveCrud
{
    public async Task<int> Create(Move move)
    {
        var context = new MoveContext();
        var m = context.Moves.Add(move);
        await context.SaveChangesAsync();
        return m.Entity.Id;
    }

    public Move? Get(int id)
    {
        var context = new MoveContext();
        return context.Moves.Find(id);
    }

    public async Task<List<Move>> GetAll()
    {
        var context = new MoveContext();
        var movesList = await context.Moves.Include("Ratings").ToListAsync();

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

    public async Task<bool> AddRating(Move move, MoveRating rating)
    {
        var context = new MoveContext();
        var moves = await GetAll();
        var m = moves.FirstOrDefault(m => m.Id == move.Id);

        if (m == null)
        {
            Console.WriteLine("No");
            return false;
        }

        m.Ratings.Add(rating);
        
        int affected = await context.SaveChangesAsync();
        Console.WriteLine("Aff: " + affected);
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