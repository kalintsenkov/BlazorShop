namespace BlazorShop.Web.Client.Shared.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    public partial class Pagination
    {
        private List<LinkModel> links;

        [Parameter]
        public int Page { get; set; } = 1;

        [Parameter]
        public int TotalPages { get; set; }

        [Parameter]
        public int Radius { get; set; } = 3;

        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        protected override void OnParametersSet() => this.LoadPages();

        private async Task SelectedPageInternal(LinkModel link)
        {
            if (link.Page == this.Page)
            {
                return;
            }

            if (!link.Enabled)
            {
                return;
            }

            this.Page = link.Page;

            await this.SelectedPage.InvokeAsync(link.Page);
        }

        private void LoadPages()
        {
            const string previous = "Previous";
            const string next = "Next";

            var isPreviousPageLinkEnabled = this.Page != 1;
            var previousPage = this.Page - 1;

            this.links = new List<LinkModel>
            {
                new LinkModel(previousPage, isPreviousPageLinkEnabled, previous)
            };

            for (int i = 1; i <= this.TotalPages; i++)
            {
                if (i >= this.Page - this.Radius && i <= this.Page + this.Radius)
                {
                    this.links.Add(new LinkModel(i)
                    {
                        Active = this.Page == i
                    });
                }
            }

            var isNextPageLinkEnabled = this.Page != this.TotalPages;
            var nextPage = this.Page + 1;

            this.links.Add(new LinkModel(nextPage, isNextPageLinkEnabled, next));
        }

        private class LinkModel
        {
            public LinkModel(int page)
                : this(page, true)
            {
            }

            private LinkModel(int page, bool enabled)
                : this(page, enabled, page.ToString())
            {
            }

            public LinkModel(int page, bool enabled, string text)
            {
                this.Page = page;
                this.Enabled = enabled;
                this.Text = text;
            }

            public string Text { get; }

            public int Page { get; }

            public bool Enabled { get; }

            public bool Active { get; set; }
        }
    }
}
