using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Publications
{
    public static class GetAll
    {
        public sealed record Query : IRequest<IEnumerable<PublicationDto>?>;

        public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<PublicationDto>?>
        {
            private readonly IDbContextFactory<NovelistsDbContext> _factory;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContextFactory<NovelistsDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<IEnumerable<PublicationDto>?> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();
                var queryable = context.Publications.AsNoTracking();
                var entities = _mapper.ProjectTo<PublicationDto>(queryable);

                var dto = await entities.ToListAsync(cancellationToken);

                return dto;
            }
        }
    }
}
