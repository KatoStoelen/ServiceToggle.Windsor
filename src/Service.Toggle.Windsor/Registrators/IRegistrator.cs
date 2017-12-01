// Copyright (c) Kato Stoelen. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the repository root for license information.

namespace Castle.Windsor.Service.Replacement.Registrators
{
    public interface IRegistrator
    {
        void Execute(IWindsorContainer container);
    }
}
