using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Features.Users;

public static class Get
{
    public sealed record Query(Guid Id) : IRequest<UserDto?>;

    public sealed class QueryHandler : IRequestHandler<Query, UserDto?>
    {
        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public QueryHandler(IDbConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            const string sql = @"
                    SELECT u.*
                    FROM novelists.users AS u
                    WHERE u.id = @p0
                    LIMIT 1
                    ";

            var result = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { p0 = request.Id });
            var entity = _mapper.Map<UserDto>(result);

            // await using var context = _factory.CreateDbContext();
            // var entity = await context.Users
            //     .AsNoTracking()
            //     .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            //
            // var dto = _mapper.Map<UserDto>(entity);

            return entity;
        }
    }
}
