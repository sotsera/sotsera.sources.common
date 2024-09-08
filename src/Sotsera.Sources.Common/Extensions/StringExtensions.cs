using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sotsera.Sources.Common.Extensions;

internal static class StringExtensions
{
    public static string ThrowIfEmpty([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(argument, paramName);
        return argument;
    }

    public static bool IsEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrWhiteSpace(value);

    public static bool IsNonEmpty([NotNullWhen(true)] this string? value) => !string.IsNullOrWhiteSpace(value);

    public static string JoinStrings(this IEnumerable<string?>? values, string? separator, bool includeEmptyValues = false)
    {
        return values is null or IList { Count: 0 }
            ? string.Empty
            : string.Join(separator, values.Where(x => includeEmptyValues || !string.IsNullOrWhiteSpace(x)));
    }
} 
