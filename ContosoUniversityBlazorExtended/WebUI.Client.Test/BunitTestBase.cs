using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace WebUI.Client.Test
{
    public abstract class BunitTestBase
    {
        protected TestContext Context { get; private set; }

        public BunitTestBase()
        {
            Context = new TestContext();

            Context.Services.AddMudServices(options =>
            {
                options.SnackbarConfiguration.ShowTransitionDuration = 0;
                options.SnackbarConfiguration.HideTransitionDuration = 0;
            });

            Context.Services.AddLocalization(opts => { opts.ResourcesPath = "Localization"; });
        }
    }
}
