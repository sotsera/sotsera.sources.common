// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using Sotsera.Sources.Common.Extensions;
using Xunit.Abstractions;

namespace Sotsera.Sources.Common.Tests.Unit.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void ThrowIfEmpty_ShouldThrowArgumentNullException_WhenValueIsNull()
    {
        string? value = null;

        Action act = () => value.ThrowIfEmpty();

        act.Should().ThrowExactly<ArgumentNullException>().WithParameterName(nameof(value));
    }

    [Theory]
    [ClassData(typeof(WhiteSpaceValueGenerator))]
    public void ThrowIfEmpty_ShouldThrowArgumentException_WhenValueIsEmpty(string value)
    {
        Action act = () => value.ThrowIfEmpty();

        act.Should().ThrowExactly<ArgumentException>().WithParameterName(nameof(value));
    }

    [Theory]
    [ClassData(typeof(NonWhitespaceValueGenerator))]
    public void ThrowIfEmpty_ShouldNotThrow_WhenValueIsEmpty(string value)
    {
        Action act = () => value.ThrowIfEmpty();

        act.Should().NotThrow();
    }

    [Theory]
    [ClassData(typeof(NullOrWhiteSpaceValueGenerator))]
    public void IsEmpty_ShouldReturnTrue_WhenValueIsNullOrWhitespace(string? value)
    {
        value.IsEmpty().Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NonWhitespaceValueGenerator))]
    public void IsEmpty_ShouldReturnFalse_WhenValueIsNonEmpty(string? value)
    {
        value.IsEmpty().Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(NullOrWhiteSpaceValueGenerator))]
    public void IsNonEmpty_ShouldReturnFalse_WhenValueIsNullOrWhitespace(string? value)
    {
        value.IsNonEmpty().Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(NonWhitespaceValueGenerator))]
    public void IsNonEmpty_ShouldReturnTrue_WhenValueIsNonEmpty(string? value)
    {
        value.IsNonEmpty().Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(StringEnumerableToBeJoinedValueGenerator))]
    public void JoinStrings_ShouldReturnTheExpectedValue(EnumerableStringList list, bool includeEmptyValues, string expected)
    {
        list.Values.JoinStrings("; ", includeEmptyValues).Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(NullOrWhiteSpaceValueGenerator))]
    public void Trimmed_ShouldReturnStringEmpty_WhenValueIsNullOrEmpty(string value)
    {
        value.Trimmed().Should().Be(string.Empty);
    }

    [Theory]
    [InlineData("ciao", "ciao")]
    [InlineData("ciao ", "ciao")]
    [InlineData(" ciao", "ciao")]
    [InlineData("\tciao\t", "ciao")]
    [InlineData(" ciao ciao ", "ciao ciao")]
    public void Trimmed_ShouldReturn_TheExpectedValue(string value, string expected)
    {
        value.Trimmed().Should().Be(expected);
    }

    private sealed class NullOrWhiteSpaceValueGenerator : TheoryData<string?>
    {
        public NullOrWhiteSpaceValueGenerator()
        {
            Add(null);
            Add("");
            Add(" ");
            Add("\t");
            Add(" \t ");
        }
    }

    private sealed class WhiteSpaceValueGenerator : TheoryData<string?>
    {
        public WhiteSpaceValueGenerator()
        {
            Add("");
            Add(" ");
            Add("\t");
            Add(" \t ");
        }
    }

    private sealed class NonWhitespaceValueGenerator : TheoryData<string?>
    {
        public NonWhitespaceValueGenerator()
        {
            Add("ciao");
            Add(" ciao ");
            Add("\tciao\t");
        }
    }

    private sealed class StringEnumerableToBeJoinedValueGenerator : TheoryData<EnumerableStringList, bool, string>
    {
        public StringEnumerableToBeJoinedValueGenerator()
        {
            Add(new EnumerableStringList(null), false, "");
            Add(new EnumerableStringList([]), false, "");
            Add(new EnumerableStringList([null]), false, "");
            Add(new EnumerableStringList([null, "ciao"]), false, "ciao");
            Add(new EnumerableStringList(["ciao"]), false, "ciao");
            Add(new EnumerableStringList(["a", "b"]), false, "a; b");
            Add(new EnumerableStringList(["a", null, "b"]), false, "a; b");

            Add(new EnumerableStringList(null), true, "");
            Add(new EnumerableStringList([]), true, "");
            Add(new EnumerableStringList([null]), true, "");
            Add(new EnumerableStringList([null, "ciao"]), true, "; ciao");
            Add(new EnumerableStringList(["ciao"]), true, "ciao");
            Add(new EnumerableStringList(["a", "b"]), true, "a; b");
            Add(new EnumerableStringList(["a", null, "b"]), true, "a; ; b");
        }
    }

    public class EnumerableStringList : IXunitSerializable
    {
        public IEnumerable<string?>? Values { get; private set; }

        public EnumerableStringList(IEnumerable<string?>? values)
        {
            Values = values;
        }

        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public EnumerableStringList()
        {
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Values = info.GetValue<IEnumerable<string?>?>(nameof(Values));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(Values), Values);
        }
    }
}
