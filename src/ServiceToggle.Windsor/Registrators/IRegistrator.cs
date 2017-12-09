// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

using Castle.Windsor;

namespace ServiceToggle.Windsor.Registrators
{
    public interface IRegistrator
    {
        void Execute(IWindsorContainer container);
    }
}
