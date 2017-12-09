// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.MicroKernel.Registration;
using NUnit.Framework;
using ServiceToggle.Windsor.Extensions;
using ServiceToggle.Windsor.UnitTest.Helpers;
using ServiceToggle.Windsor.UnitTest.Helpers.Dependencies;

namespace ServiceToggle.Windsor.UnitTest.UsingTypes
{
    internal class WhenTogglingReplacementOnAndOff : GivenWhenThenTest
    {
        private class Feature
        {
            public bool IsEnabled { get; set; }
        }

        private Feature _feature;

        protected override void Given()
        {
            Container.Register(
                Component.For<ServiceWithInterfaceDependency>(),
                Component.For<IDependency>().ImplementedBy<DependencyImpl1>());

            _feature = new Feature();
        }

        protected override void When()
        {
            Container.Register(
                ComponentReplacement
                    .For(typeof(IDependency))
                    .If(() => _feature.IsEnabled)
                    .ReplacedBy(typeof(DependencyImpl1Extension)));
        }

        [Then]
        public void ShouldReturnOriginalWhenFeatureIsOff()
        {
            lock (_feature)
            {
                _feature.IsEnabled = false;

                var implementation = Container.Resolve<IDependency>();

                Assert.AreEqual(typeof(DependencyImpl1), implementation.GetType());
            }
        }

        [Then]
        public void ShouldReturnReplacementWhenFeatureIsOn()
        {
            lock (_feature)
            {
                _feature.IsEnabled = true;

                var implementation = Container.Resolve<IDependency>();

                Assert.AreEqual(typeof(DependencyImpl1Extension), implementation.GetType());
            }
        }
    }
}
