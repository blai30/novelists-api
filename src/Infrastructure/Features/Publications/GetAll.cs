using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Features.Publications
{
    public static class GetAll
    {
        public sealed record Query : IRequest<IEnumerable<PublicationDto>?>;

        public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<PublicationDto>?>
        {
            private readonly IDbConnection _connection;
            private readonly IMapper _mapper;

            public QueryHandler(IDbConnection connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }

            public async Task<IEnumerable<PublicationDto>?> Handle(Query request, CancellationToken cancellationToken)
            {
                const string sql = @"
                    SELECT p.*, u.*
                    FROM novelists.publications AS p
                        LEFT JOIN novelists.users AS u
                            ON p.user_id = u.id
                    ";

                var result = await _connection.QueryAsync<Publication, User, Publication>(sql,
                    (publication, user) =>
                    {
                        publication.User = user;
                        return publication;
                    });

                var entities = _mapper.Map<IEnumerable<PublicationDto>>(result);

                // await using var context = _factory.CreateDbContext();
                // var queryable = context.Publications.AsNoTracking();
                // var entities = _mapper.ProjectTo<PublicationDto>(queryable);
                //
                // var dto = await entities.ToListAsync(cancellationToken);

                return entities;
            }
        }
    }
}
