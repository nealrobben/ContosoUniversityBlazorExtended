using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Home.Queries.GetAboutInfo;

namespace WebUI.Client.Pages.Home
{
    public partial class About
    {
        private AboutInfoVM aboutInfo;

        [Inject]
        public HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            aboutInfo = await Http.GetFromJsonAsync<AboutInfoVM>("/api/about");
        }
    }
}
