// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Sotsera.Sources.Common.Extensions;
#pragma warning restore IDE0130 // Namespace does not match folder structure

internal static class StringValuesExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the provided <see cref="StringValues"/> is empty or composed by only empty values.
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static StringValues ThrowIfEmpty(this StringValues argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument.Count == 0 || !argument.ToArray().Any(x => x.IsNonEmpty()))
        {
            throw new ArgumentException($"The argument '{paramName}' cannot be empty or composed by only empty values", paramName);
        }

        return argument;
    }

    /// <summary>
    /// Determines whether the specified <see cref="StringValues"/> is empty or composed by only empty values.
    /// </summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    public static bool IsEmpty(this StringValues argument)
    {
        var data = argument.ToArray();

        return data.Length == 0 || data.All(x => x.IsEmpty());
    }

    /// <summary>
    /// Determines whether the specified <see cref="StringValues"/> is not empty and contains non-empty values.
    /// </summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    public static bool IsNonEmpty(this StringValues argument)
    {
        var data = argument.ToArray();

        return data.Length > 0 && data.Any(x => x.IsNonEmpty());
    }
}
