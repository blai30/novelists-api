using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using NovelistsApi.Domain.Models;

namespace NovelistsApi.Infrastructure.Features.Users
{
    public static class Create
    {
        public sealed record Command(string Email, string Password, string DisplayName) : IRequest<UserDto?>;

        public sealed class CommandHandler : IRequestHandler<Command, UserDto?>
        {
            private readonly IDbConnection _connection;
            private readonly IMapper _mapper;

            public CommandHandler(IDbConnection connection, IMapper mapper)
            {
                _connection = connection;
                _mapper = mapper;
            }

            public async Task<UserDto?> Handle(Command request, CancellationToken cancellationToken)
            {
                const string sql = @"
                    INSERT INTO novelists.users (email, password, display_name)
                    VALUES (@p0, @p1 @p2)
                    RETURNING id;
                    ";

                var id = await _connection.ExecuteScalarAsync<string>(sql, new { p0 = request.Email, p1 = request.Password, p2 = request.DisplayName });
                var entry = await _connection.QueryFirstOrDefaultAsync<User>(@"SELECT * FROM novelists.users WHERE id = @p0",
                    new { p0 = id });
                var dto = _mapper.Map<UserDto>(entry);

                // var entity = new User
                // {
                //     Email = request.Email,
                //     Password = request.Password,
                //     DisplayName = request.DisplayName
                // };
                //
                // await using var context = _factory.CreateDbContext();
                // var entry = await context.Users.AddAsync(entity, cancellationToken);
                // await context.SaveChangesAsync(cancellationToken);
                //
                // var dto = _mapper.Map<UserDto>(entry.Entity);

                return dto;
            }
        }
    }
}
