// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sotsera.Sources.Common.Extensions;

internal static class ObjectExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    /// <param name="argument">The argument to check for null.</param>
    /// <param name="paramName">The name of the parameter that caused the exception. This is optional and will be automatically set to the name of the argument if not provided.</param>
    /// <returns>The original argument if it is not null.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the argument is null.</exception>
    public static T ThrowIfNull<T>([NotNull] this T? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
        return argument;
    }
}
