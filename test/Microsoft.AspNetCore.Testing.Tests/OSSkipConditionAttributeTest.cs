// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Microsoft.AspNetCore.Testing.xunit
{
    public class OSSkipConditionAttributeTest
    {
        [Fact]
        public void Skips_WhenOnlyOperatingSystemIsSupplied()
        {
            // Act
            var osSkipAttribute = new OSSkipConditionAttribute(
                OperatingSystems.Windows,
                OperatingSystems.Windows,
                "2.5");

            // Assert
            Assert.False(osSkipAttribute.IsMet);
        }

        [Fact]
        public void DoesNotSkip_WhenOperatingSystemDoesNotMatch()
        {
            // Act
            var osSkipAttribute = new OSSkipConditionAttribute(
                OperatingSystems.Linux,
                OperatingSystems.Windows,
                "2.5");

            // Assert
            Assert.True(osSkipAttribute.IsMet);
        }

        [Fact]
        public void DoesNotSkip_WhenVersionsDoNotMatch()
        {
            // Act
            var osSkipAttribute = new OSSkipConditionAttribute(
                OperatingSystems.Windows,
                OperatingSystems.Windows,
                "2.5",
                "10.0");

            // Assert
            Assert.True(osSkipAttribute.IsMet);
        }

        [Fact]
        public void DoesNotSkip_WhenOnlyVersionsMatch()
        {
            // Act
            var osSkipAttribute = new OSSkipConditionAttribute(
                OperatingSystems.Linux,
                OperatingSystems.Windows,
                "2.5",
                "2.5");

            // Assert
            Assert.True(osSkipAttribute.IsMet);
        }

        [Theory]
        [InlineData("2.5", "2.5")]
        [InlineData("blue", "Blue")]
        public void Skips_WhenVersionsMatches(string currentOSVersion, string skipVersion)
        {
            // Act
            var osSkipAttribute = new OSSkipConditionAttribute(
                OperatingSystems.Windows,
                OperatingSystems.Windows,
                currentOSVersion,
                skipVersion);

            // Assert
            Assert.False(osSkipAttribute.IsMet);
        }

        [Fact]
        public void Skips_WhenVersionsMatchesOutOfMultiple()
        {
            // Act
            var osSkipAttribute = new OSSkipConditionAttribute(
                OperatingSystems.Windows,
                OperatingSystems.Windows,
                "2.5",
                "10.0", "3.4", "2.5");

            // Assert
            Assert.False(osSkipAttribute.IsMet);
        }
    }
}
