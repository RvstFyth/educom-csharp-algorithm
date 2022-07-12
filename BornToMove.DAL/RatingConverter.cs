namespace BornToMove.DAL;

public class RatingConverter : IComparer<Move>
{
    public int Compare(Move? m1, Move? m2)
    {
        if (m1 == null) return m2 == null ? 0 : -1;
        if (m2 == null) return 1;
        if (m2.AverageRating > m1.AverageRating) return 1;
        if (m2.AverageRating < m1.AverageRating) return -1;
        return 0;
    }
}