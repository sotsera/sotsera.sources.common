// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sotsera.Sources.Common.Extensions;

/// <summary>
/// Provides extension methods for string validation and manipulation.
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the provided string is null or consists only of white-space characters.
    /// </summary>
    /// <param name="argument">The string to check.</param>
    /// <param name="paramName">The name of the parameter being checked. This is automatically provided by the compiler.</param>
    /// <returns>The original string if it is not null or white-space.</returns>
    /// <exception cref="ArgumentException">Thrown if the string is null or white-space.</exception>
    public static string ThrowIfEmpty([NotNull] this string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(argument, paramName);
        return argument;
    }

    /// <summary>
    /// Determines whether the specified string is null or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is null or white-space; otherwise, <c>false</c>.</returns>
    public static bool IsEmpty([NotNullWhen(false)] this string? value) => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Determines whether the specified string is not null and contains non-white-space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns><c>true</c> if the string is not null and contains non-white-space characters; otherwise, <c>false</c>.</returns>
    public static bool IsNonEmpty([NotNullWhen(true)] this string? value) => !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Joins an enumeration of strings into a single string, with an optional separator and the option to include or exclude empty values.
    /// </summary>
    /// <param name="values">The enumeration of strings to join.</param>
    /// <param name="separator">The string to use as a separator between each joined string.</param>
    /// <param name="includeEmptyValues">If <c>true</c>, empty strings are included in the result; otherwise, they are excluded.</param>
    /// <returns>A single string that consists of the joined strings from the enumeration, separated by the specified separator.</returns>
    public static string JoinStrings(this IEnumerable<string?>? values, string? separator, bool includeEmptyValues = false)
    {
        return values is null or IList { Count: 0 }
            ? string.Empty
            : string.Join(separator, values.Where(x => includeEmptyValues || !string.IsNullOrWhiteSpace(x)));
    }
}
