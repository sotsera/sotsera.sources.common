// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sotsera.Sources.Common.Extensions;

/// <summary>
/// Provides extension methods for list validation and manipulation.
/// </summary>
internal static class ListExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the provided list is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="argument">The list to check.</param>
    /// <param name="paramName">The name of the parameter being checked. This is automatically provided by the compiler.</param>
    /// <returns>The original list if it is not null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the list is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the list is empty.</exception>
    public static List<T> ThrowIfEmpty<T>([NotNull] this List<T>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        if (argument.Count == 0)
        {
            throw new ArgumentException($"The {typeof(List<T>)} cannot be empty.", paramName);
        }

        return argument;
    }

    /// <summary>
    /// Determines whether the specified list is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="value">The list to check.</param>
    /// <returns><c>true</c> if the list is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsEmpty<T>([NotNullWhen(false)] this List<T>? value) => value is null || value.Count == 0;

    /// <summary>
    /// Determines whether the specified list is not null and contains elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the list.</typeparam>
    /// <param name="value">The list to check.</param>
    /// <returns><c>true</c> if the list is not null and contains elements; otherwise, <c>false</c>.</returns>
    public static bool IsNonEmpty<T>([NotNullWhen(true)] this List<T>? value) => value is not null && value.Count != 0;
}
