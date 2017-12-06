// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.Windsor.Service.Replacement.UnitTest.Helpers.Dependencies;

namespace Castle.Windsor.Service.Replacement.UnitTest.Helpers.Factories
{
    public interface IDependencyFactory
    {
        IDependency Create(string name);

        void Release(IDependency dependency);
    }
}