using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Publications
{
    public static class Get
    {
        public sealed record Query(Guid Id) : IRequest<PublicationDto?>;

        public sealed class QueryHandler : IRequestHandler<Query, PublicationDto?>
        {
            private readonly IDbContextFactory<NovelistsDbContext> _factory;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContextFactory<NovelistsDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<PublicationDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();
                var entity = await context.Publications
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                var dto = _mapper.Map<PublicationDto>(entity);

                return dto;
            }
        }
    }
}
