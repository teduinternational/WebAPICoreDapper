using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICoreDapper.Extensions
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app, RequestLocalizationOptions options)
        {
            app.UseRequestLocalization(options);
        }
    }
}
