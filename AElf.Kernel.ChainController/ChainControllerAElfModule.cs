﻿using AElf.Kernel;
using AElf.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace AElf.Kernel.ChainController
{
    [DependsOn(typeof(CoreKernelAElfModule))]
    public class ChainControllerAElfModule : AElfModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            services.AddAssemblyOf<ChainControllerAElfModule>();
        }
    }
}