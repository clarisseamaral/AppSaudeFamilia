using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ColetaApi.Helper
{
    public static class EntityHelper
    {
        public static async Task<List<TDto>> ToListAsync<TBase, TDto>(this IQueryable<TBase> consulta, Func<TBase,TDto> conversor, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await consulta.ToListAsync(cancellationToken)).Select(conversor).ToList();
        }
    }
}
