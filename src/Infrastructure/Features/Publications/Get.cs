using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Features.Publications;

public static class Get
{
    public sealed record Query(Guid Id) : IRequest<PublicationDto?>;

    public sealed class QueryHandler : IRequestHandler<Query, PublicationDto?>
    {
        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public QueryHandler(IDbConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task<PublicationDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            const string sql = @"
                    SELECT p.*, u.*
                    FROM novelists.publications AS p
                        LEFT JOIN novelists.users AS u
                            ON p.user_id = u.id
                    WHERE p.id = @p0
                    LIMIT 1
                    ";

            var result = await _connection.QueryAsync<Publication, User, Publication>(sql,
                (publication, user) =>
                {
                    publication.User = user;
                    return publication;
                },
                new { p0 = request.Id });

            var entity = _mapper.Map<PublicationDto>(result.FirstOrDefault());

            // await using var context = _factory.CreateDbContext();
            // var entity = await context.Publications
            //     .AsNoTracking()
            //     .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            //
            // var dto = _mapper.Map<PublicationDto>(entity);

            return entity;
        }
    }
}
