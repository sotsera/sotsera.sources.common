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
    [ClassData(typeof(NullOrWhiteSpaceValueGenerator))]
    public void ThrowIfEmpty_ShouldThrowArgumentException_WhenValueIsEmpty(string value)
    {
        value.IsEmpty().Should().Be(true);
    }

    [Theory]
    [ClassData(typeof(NullOrWhiteSpaceValueGenerator))]
    public void IsEmpty_ShouldReturnTrue_WhenTheValueIsNullOrWhitespace(string? value)
    {
        value.IsEmpty().Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(NonWhitespaceValueGenerator))]
    public void IsEmpty_ShouldReturnFalse_WhenTheValueIsNonEmpty(string? value)
    {
        value.IsEmpty().Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(NullOrWhiteSpaceValueGenerator))]
    public void IsNonEmpty_ShouldReturnFalse_WhenTheValueIsNullOrWhitespace(string? value)
    {
        value.IsNonEmpty().Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(NonWhitespaceValueGenerator))]
    public void IsNonEmpty_ShouldReturnTrue_WhenTheValueIsNonEmpty(string? value)
    {
        value.IsNonEmpty().Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(StringEnumerableToBeJoinedValueGenerator))]
    public void JoinStrings_ShouldReturnTheExpectedValue(EnumerableStringList list, bool includeEmptyValues, string expected)
    {
        list.Values.JoinStrings("; ", includeEmptyValues).Should().Be(expected);
    }

    private class NullOrWhiteSpaceValueGenerator : TheoryData<string?>
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

    private class NonWhitespaceValueGenerator : TheoryData<string?>
    {
        public NonWhitespaceValueGenerator()
        {
            Add("ciao");
            Add(" ciao ");
            Add("\tciao\t");
        }
    }

    private class StringEnumerableToBeJoinedValueGenerator : TheoryData<EnumerableStringList, bool, string>
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
