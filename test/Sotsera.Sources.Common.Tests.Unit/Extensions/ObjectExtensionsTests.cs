// Copyright (c) Alessandro Ghidini. All rights reserved.
// SPDX-License-Identifier: MIT.

using Sotsera.Sources.Common.Extensions;

namespace Sotsera.Sources.Common.Tests.Unit.Extensions;

public class ObjectExtensionsTests
{
    [Fact]
    public void ThrowIfNull_ShouldThrowArgumentNullException_WhenValueIsNull()
    {
        object? value = null;

        Action act = () => value.ThrowIfNull();

        act.Should().Throw<ArgumentNullException>().WithParameterName(nameof(value));
    }

    [Theory]
    [InlineData("ciao")]
    [InlineData(true)]
    public void ThrowIfNull_ShouldNotThrow_WhenValueIsNotNull(object? value)
    {
        Action act = () => value.ThrowIfNull();

        act.Should().NotThrow();
    }
}
