// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using Castle.Windsor.Service.Replacement.Extensions;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers;
using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;
using NUnit.Framework;
using System;

namespace Castle.Windsor.Service.Replacement.UnitTest.UsingTypes
{
    [TestFixture]
    internal class WhenReplacementIsNotImplementationOfServiceType : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Register(
                Component
                    .For<IDependency>()
                    .ImplementedBy<DependencyImpl1>()
                    .LifestylePerThread(),
                Component
                    .For<ServiceWithInterfaceDependency>());
        }

        protected override void When()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Container.Register(
                    ComponentReplacement.For(typeof(IDependency)).ReplacedBy(typeof(DateTime)));
            });

            Assert.Throws<ArgumentException>(() =>
            {
                Container.Register(
                    ComponentReplacement
                        .For(typeof(IDependency), typeof(DependencyImpl1).FullName)
                        .ReplacedBy(typeof(DateTime)));
            });
        }

        [Then]
        public void ShouldThrowaArgumentException()
        {
            // Will fail in setup if exception is not thrown
        }
    }
}