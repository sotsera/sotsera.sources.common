// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Sotsera.Sources.Common.Extensions;
#pragma warning restore IDE0130 // Namespace does not match folder structure

internal static class StringValuesExtensions
{
    public static StringValues ThrowIfEmpty(this StringValues argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        if (argument.Count == 0 || !argument.ToArray().Any(x => x.IsNonEmpty()))
        {
            throw new ArgumentException($"The argument '{paramName}' cannot be empty or composed by only empty values", paramName);
        }

        return argument;
    }

    public static bool IsEmpty(this StringValues argument)
    {
        var data = argument.ToArray();

        return data.Length == 0 || data.All(x => x.IsEmpty());
    }

    public static bool IsNonEmpty(this StringValues argument)
    {
        var data = argument.ToArray();

        return data.Length > 0 && data.Any(x => x.IsNonEmpty());
    }
}
