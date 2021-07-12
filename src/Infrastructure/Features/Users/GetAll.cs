using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public static class GetAll
    {
        public sealed record Query : IRequest<IEnumerable<UserDto>?>;

        public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<UserDto>?>
        {
            private readonly IDbContextFactory<NovelistsDbContext> _factory;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContextFactory<NovelistsDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<IEnumerable<UserDto>?> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();
                var queryable = context.Users.AsNoTracking();
                var entities = _mapper.ProjectTo<UserDto>(queryable);

                var dto = await entities.ToListAsync(cancellationToken);

                return dto;
            }
        }
    }
}
