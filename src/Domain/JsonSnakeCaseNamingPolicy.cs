using System.Text.Json;
using Humanizer;

namespace NovelistsApi.Domain;

public sealed class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.Underscore();
}
