using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL.Extentions
{
    public static class MoveExtentions
    {

        public static async Task<T> FindAsync<T>(this IQueryable<T> moves, long id, CancellationToken token = default) where T : Base
        {
            return await moves.FirstAsync(m => m.Id == id, token);
        }
    }
}
