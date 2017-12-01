// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using NUnit.Framework;

namespace Castle.Windsor.Service.Replacement.UnitTest
{
    internal abstract class GivenWhenThenTest
    {
        public IWindsorContainer Container { get; private set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Container = new WindsorContainer();

            Given();
            When();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Container?.Dispose();
        }

        protected abstract void Given();
        protected abstract void When();
    }

    internal class ThenAttribute : TestAttribute { }
}