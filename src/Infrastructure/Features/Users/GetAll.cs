using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public static class GetAll
    {
        public sealed record Query : IRequest<IEnumerable<UserDto>?>;

        public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<UserDto>?>
        {
            private readonly IDbConnection _connection;
            private readonly IMapper _mapper;

            public QueryHandler(IDbConnection connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }

            public async Task<IEnumerable<UserDto>?> Handle(Query request, CancellationToken cancellationToken)
            {
                const string sql = @"
                    SELECT u.* FROM novelists.users AS u
                    ";

                var result = await _connection.QueryAsync<User>(sql);
                var entities = _mapper.Map<IEnumerable<UserDto>>(result);

                // await using var context = _factory.CreateDbContext();
                // var queryable = context.Users.AsNoTracking();
                // var entities = _mapper.ProjectTo<UserDto>(queryable);

                return entities;
            }
        }
    }
}
