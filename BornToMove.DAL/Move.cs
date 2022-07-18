using BornToMove.DAL;

namespace BornToMove;

public class Move
{
    public int Id
    { get; set; }

    public string Name
    {
        get;
        set;
    } = default!;

    public string Description
    {
        get;
        set;
    } = default!;
    
    public int SweatRate
    { get; set; }

    public ICollection<MoveRating> Ratings
    {
        get;
    } = default!;

    public double AverageRating
    {
        get
        {
            return Ratings.Count > 0 ? Ratings.Average(r => r.Rating) : 0;
        }
    }
}