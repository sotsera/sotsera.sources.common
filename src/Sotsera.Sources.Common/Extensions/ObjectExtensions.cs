using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sotsera.Sources.Common.Extensions;

internal static class ObjectExtensions
{
    public static T ThrowIfNull<T>([NotNull] this T? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
        return argument;
    }
}
