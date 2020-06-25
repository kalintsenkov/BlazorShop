namespace BlazorShop.Web.Client.Infrastructure
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    public class ServerValidator : ComponentBase, IDisposable
    {
        private ValidationMessageStore messageStore;

        private EventHandler<ValidationRequestedEventArgs> OnValidationRequested => (s, e) =>
        {
            this.messageStore.Clear();
        };

        private EventHandler<FieldChangedEventArgs> OnFieldChanged => (s, e) =>
        {
            this.messageStore.Clear(e.FieldIdentifier);
        };

        [CascadingParameter]
        public EditContext CurrentEditContext { get; set; }

        public async Task Validate(HttpResponseMessage response, object model)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var body = await response.Content.ReadAsStringAsync();
                var validationProblemDetails = JsonSerializer.Deserialize<ValidationProblemDetails[]>(body);

                if (validationProblemDetails != null)
                {
                    this.messageStore.Clear();

                    foreach (var error in validationProblemDetails)
                    {
                        var fieldIdentifier = new FieldIdentifier(model, error.Code);
                        this.messageStore.Add(fieldIdentifier, error.Description);
                    }
                }
            }

            this.CurrentEditContext.NotifyValidationStateChanged();
        }

        public void Dispose()
        {
            if (this.CurrentEditContext != null)
            {
                this.CurrentEditContext.OnFieldChanged -= OnFieldChanged;
                this.CurrentEditContext.OnValidationRequested -= OnValidationRequested;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (this.CurrentEditContext == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(ServerValidator)} requires a cascading parameter of type {nameof(EditContext)}. " +
                    $"For example, you can use {nameof(ServerValidator)} inside an EditForm.");
            }

            this.messageStore = new ValidationMessageStore(this.CurrentEditContext);
            this.CurrentEditContext.OnFieldChanged += OnFieldChanged;
            this.CurrentEditContext.OnValidationRequested += OnValidationRequested;
        }

        private class ValidationProblemDetails
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }
    }
}
