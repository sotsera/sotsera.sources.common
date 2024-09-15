// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sotsera.Sources.Common.Extensions;

/// <summary>
/// Provides extension methods for array validation and manipulation.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the provided array is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="argument">The array to check.</param>
    /// <param name="paramName">The name of the parameter being checked. This is automatically provided by the compiler.</param>
    /// <returns>The original array if it is not null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the array is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the array is empty.</exception>
    public static T[] ThrowIfEmpty<T>([NotNull] this T[]? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        if (argument.Length == 0)
        {
            throw new ArgumentException($"The {typeof(T[])} cannot be empty.", paramName);
        }

        return argument;
    }

    /// <summary>
    /// Determines whether the specified array is null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="value">The array to check.</param>
    /// <returns><c>true</c> if the array is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsEmpty<T>([NotNullWhen(false)] this T[]? value) => value is null || value.Length == 0;

    /// <summary>
    /// Determines whether the specified array is not null and contains elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="value">The array to check.</param>
    /// <returns><c>true</c> if the array is not null and contains elements; otherwise, <c>false</c>.</returns>
    public static bool IsNonEmpty<T>([NotNullWhen(true)] this T[]? value) => value is not null && value.Length != 0;
}
