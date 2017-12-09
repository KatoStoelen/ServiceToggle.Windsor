// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;
using NUnit.Framework;

namespace ServiceToggle.Windsor.UnitTest.UsingTypes
{
    [TestFixture]
    internal class WhenReplacementIsAmbiguous : GivenWhenThenTest
    {
        protected override void Given()
        {
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel));

            Container.Register(
                Component.For<IDependency>().ImplementedBy<DependencyImpl1>(),
                Component.For<IDependency>().ImplementedBy<DependencyImpl2>(),
                Component.For<ServiceWithCollectionDependency>());
        }

        protected override void When()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Container.Register(
                    ComponentReplacement.For(typeof(IDependency)).ReplacedBy(typeof(DependencyImpl3)));
            });
        }

        [Then]
        public void ShouldThrowInvalidOperationException()
        {
            // Will fail in setup if exception is not thrown
        }
    }
}