// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using System.Text;
using Microsoft.Extensions.Primitives;
using Sotsera.Sources.Common.Extensions;
using Xunit.Abstractions;

namespace Sotsera.Sources.Common.Tests.Unit.Extensions;

public class StringValuesExtensionsTests
{

    [Theory]
    [ClassData(typeof(EmptyStringValuesValueGenerator))]
    public void ThrowIfEmpty_ShouldThrowArgumentException_WhenValueIsMeptyOrIsComposedByAllEmptyValues(SerializableStringValues values)
    {
        var value = values.Value;
        const string message = $"The argument '{nameof(value)}' cannot be empty or composed by only empty values (Parameter '{nameof(value)}')";

        Action act = () => value.ThrowIfEmpty();

        act.Should().ThrowExactly<ArgumentException>()
            .WithMessage(message)
            .WithParameterName(nameof(value));
    }

    [Theory]
    [ClassData(typeof(NonEmptyStringValuesValueGenerator))]
    public void ThrowIfEmpty_ShouldNotThrow_WhenValueContainsAtLeastANonEmptyValue(SerializableStringValues values)
    {
        var value = values.Value;

        Action act = () => value.ThrowIfEmpty();

        act.Should().NotThrow();
    }

    [Theory]
    [ClassData(typeof(EmptyStringValuesValueGenerator))]
    public void IsEmpty_ShouldReturnTrue_WhenValueIsEmpty(SerializableStringValues values)
    {
        var value = values.Value;

        value.IsEmpty().Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NonEmptyStringValuesValueGenerator))]
    public void IsEmpty_ShouldReturnFalse_WhenValueIsNonEmpty(SerializableStringValues values)
    {
        var value = values.Value;

        value.IsEmpty().Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(EmptyStringValuesValueGenerator))]
    public void IsNonEmpty_ShouldReturnFalse_WhenValueIsEmpty(SerializableStringValues values)
    {
        var value = values.Value;

        value.IsNonEmpty().Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(NonEmptyStringValuesValueGenerator))]
    public void IsNonEmpty_ShouldReturnTrue_WhenValueIsNonEmpty(SerializableStringValues values)
    {
        var value = values.Value;

        value.IsNonEmpty().Should().BeTrue();
    }

    private sealed class EmptyStringValuesValueGenerator : TheoryData<SerializableStringValues>
    {
        public EmptyStringValuesValueGenerator()
        {
            Add(new SerializableStringValues([]));
            Add(new SerializableStringValues([null]));
            Add(new SerializableStringValues([""]));
            Add(new SerializableStringValues(["", null]));
        }
    }

    private sealed class NonEmptyStringValuesValueGenerator : TheoryData<SerializableStringValues>
    {
        public NonEmptyStringValuesValueGenerator()
        {
            Add(new SerializableStringValues(["ciao!"]));
            Add(new SerializableStringValues([null, "ciao!"]));
            Add(new SerializableStringValues(["", "ciao!"]));
            Add(new SerializableStringValues(["", null, "ciao!"]));
        }
    }

    public class SerializableStringValues : IXunitSerializable
    {
        public StringValues Value { get; private set; }

        public SerializableStringValues(string?[] values)
        {
            Value = new StringValues(values);
        }

        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public SerializableStringValues()
        {
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Value = new StringValues(info.GetValue<string?[]>(nameof(Value)));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Value), Value.ToArray());
        }

        public override string ToString()
        {
            var values = Value.ToArray();

            if (values.Length == 0)
            {
                return "[]";
            }

            var builder = new StringBuilder();

            builder.Append('[');

            for (var i = 0; i < values.Length; i++)
            {
                builder.AddToBuilder(values[i], i);
            }

            builder.Append(']');

            return builder.ToString();
        }

    }
}

static file class StringBuilderExtension
{
    public static void AddToBuilder(this StringBuilder builder, string? value, int index)
    {
        if (index > 0)
        {
            builder.Append(", ");
        }

        switch (value)
        {
            case null:
                builder.Append("null");
                break;
            default:
                builder.Append('"');
                builder.Append(value);
                builder.Append('"');
                break;
        }
    }
}
