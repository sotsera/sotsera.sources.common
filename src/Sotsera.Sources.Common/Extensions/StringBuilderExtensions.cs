// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Text;

namespace Sotsera.Sources.Common.Extensions;

/// <summary>
/// Provides extension methods for <see cref="StringBuilder"/> validation and manipulation.
/// </summary>
internal static class StringBuilderExtensions
{
    /// <summary>
    /// Appends the specified value to the <see cref="StringBuilder"/> if the given condition is true.
    /// </summary>
    /// <typeparam name="T">The type of the value to append.</typeparam>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <param name="condition">The condition that determines whether the value should be appended.</param>
    /// <param name="value">The value to append if the condition is true.</param>
    /// <returns>The original <see cref="StringBuilder"/> with the value appended if the condition is true.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="StringBuilder"/> is null.</exception>
    public static StringBuilder AppendIf<T>(this StringBuilder builder, bool condition, T? value)
    {
        builder.ThrowIfNull();

        if (condition)
        {
            builder.Append(value);
        }

        return builder;
    }
}
