#pragma warning disable IDE0073 // The file header does not match the required text

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// https://github.com/dotnet/aspnetcore/blob/ab50c4e79a11380447fe554682faba29d7b24f5d/src/Shared/Debugger/DebuggerHelpers.cs

#pragma warning restore IDE0073

using System.Collections;
using System.Text;

namespace Sotsera.Sources.Common.Helpers;

/// <summary>
/// Provides helper methods for generating debug text representations of key-value pairs.
/// </summary>
internal static class DebuggerHelpers
{
    /// <summary>
    /// Generates a debug text representation for a single key-value pair.
    /// </summary>
    /// <param name="key1">The key of the first key-value pair.</param>
    /// <param name="value1">The value of the first key-value pair.</param>
    /// <param name="includeNullValues">Indicates whether to include null values in the output.</param>
    /// <param name="prefix">An optional prefix to include at the beginning of the output.</param>
    /// <returns>A string representing the debug text for the key-value pair.</returns>
    public static string GetDebugText(string key1, object? value1, bool includeNullValues = true, string? prefix = null)
    {
        return GetDebugText([Create(key1, value1)], includeNullValues, prefix);
    }

    /// <summary>
    /// Generates a debug text representation for two key-value pairs.
    /// </summary>
    /// <param name="key1">The key of the first key-value pair.</param>
    /// <param name="value1">The value of the first key-value pair.</param>
    /// <param name="key2">The key of the second key-value pair.</param>
    /// <param name="value2">The value of the second key-value pair.</param>
    /// <param name="includeNullValues">Indicates whether to include null values in the output.</param>
    /// <param name="prefix">An optional prefix to include at the beginning of the output.</param>
    /// <returns>A string representing the debug text for the key-value pairs.</returns>
    public static string GetDebugText(string key1, object? value1, string key2, object? value2, bool includeNullValues = true, string? prefix = null)
    {
        return GetDebugText([Create(key1, value1), Create(key2, value2)], includeNullValues, prefix);
    }

    /// <summary>
    /// Generates a debug text representation for three key-value pairs.
    /// </summary>
    /// <param name="key1">The key of the first key-value pair.</param>
    /// <param name="value1">The value of the first key-value pair.</param>
    /// <param name="key2">The key of the second key-value pair.</param>
    /// <param name="value2">The value of the second key-value pair.</param>
    /// <param name="key3">The key of the third key-value pair.</param>
    /// <param name="value3">The value of the third key-value pair.</param>
    /// <param name="includeNullValues">Indicates whether to include null values in the output.</param>
    /// <param name="prefix">An optional prefix to include at the beginning of the output.</param>
    /// <returns>A string representing the debug text for the key-value pairs.</returns>
    public static string GetDebugText(string key1, object? value1, string key2, object? value2, string key3, object? value3, bool includeNullValues = true, string? prefix = null)
    {
        return GetDebugText([Create(key1, value1), Create(key2, value2), Create(key3, value3)], includeNullValues, prefix);
    }

    /// <summary>
    /// Generates a debug text representation for a span of key-value pairs.
    /// </summary>
    /// <param name="values">The span of key-value pairs to include in the debug text.</param>
    /// <param name="includeNullValues">Indicates whether to include null values in the output.</param>
    /// <param name="prefix">An optional prefix to include at the beginning of the output.</param>
    /// <returns>A string representing the debug text for the key-value pairs.</returns>
    private static string GetDebugText(ReadOnlySpan<KeyValuePair<string, object?>> values, bool includeNullValues = true, string? prefix = null)
    {
        if (values.Length == 0)
        {
            return prefix ?? string.Empty;
        }

        var sb = new StringBuilder();

        if (prefix != null)
        {
            sb.Append(prefix);
        }

        var first = true;

        foreach (var kvp in values)
        {
            switch (kvp.Value)
            {
                case null when !includeNullValues:
                    continue;
                case null:
                    Append(kvp);
                    sb.Append("(null)");
                    break;
                case string s:
                    Append(kvp);
                    sb.Append(s);
                    break;
                case IEnumerable enumerable:
                    {
                        var firstItem = true;
                        foreach (var item in enumerable)
                        {
                            if (item is null && !includeNullValues)
                            {
                                continue;
                            }

                            if (firstItem)
                            {
                                Append(kvp);
                                firstItem = false;
                            }
                            else
                            {
                                sb.Append(',');
                            }

                            sb.Append(item ?? "(null)");
                        }

                        break;
                    }
                default:
                    sb.Append(kvp.Value);
                    break;
            }
        }

        return sb.ToString();

        void Append(KeyValuePair<string, object?> value)
        {
            if (first)
            {
                if (prefix != null)
                {
                    sb.Append(' ');
                }

                first = false;
            }
            else
            {
                sb.Append(", ");
            }

            sb.Append(value.Key);
            sb.Append(": ");
        }
    }

    /// <summary>
    /// Creates a key-value pair with the specified key and value.
    /// </summary>
    /// <param name="key">The key of the key-value pair.</param>
    /// <param name="value">The value of the key-value pair.</param>
    /// <returns>A key-value pair with the specified key and value.</returns>
    private static KeyValuePair<string, object?> Create(string key, object? value) => new(key, value);
}
