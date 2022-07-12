using BornToMove.DAL;

namespace BornToMove;

public class Move
{
    public int Id
    { get; set; }
    
    public string Name
    { get; set; }
    
    public string Description
    { get; set; }
    
    public int SweatRate
    { get; set; }
    
    public ICollection<MoveRating> Ratings
    {
        get;
    }
}