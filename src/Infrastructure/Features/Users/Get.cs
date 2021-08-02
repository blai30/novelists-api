using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovelistsApi.Infrastructure.Persistence;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public static class Get
    {
        public sealed record Query(Guid Id) : IRequest<UserDto?>;

        public sealed class QueryHandler : IRequestHandler<Query, UserDto?>
        {
            private readonly IDbContextFactory<NovelistsDbContext> _factory;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContextFactory<NovelistsDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<UserDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();
                var entity = await context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                var dto = _mapper.Map<UserDto>(entity);

                return dto;
            }
        }
    }
}
