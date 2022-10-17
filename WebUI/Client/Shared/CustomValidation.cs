using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

namespace WebUI.Client.Shared;

public class CustomValidation : ComponentBase
{
    private ValidationMessageStore _messageStore;

    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; }

    [Parameter]
    public IStringLocalizer Localizer { get; set; }

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException(
                $"{nameof(CustomValidation)} requires a cascading " +
                $"parameter of type {nameof(EditContext)}. " +
                $"For example, you can use {nameof(CustomValidation)} " +
                $"inside an {nameof(EditForm)}.");
        }

        // Create a validation message store for the given form
        _messageStore = new(CurrentEditContext);

        // Clear validation errors when validation requested.
        CurrentEditContext.OnValidationRequested += (s, e) =>
            _messageStore.Clear();

        // Clear validation error when field changes.
        CurrentEditContext.OnFieldChanged += (s, e) =>
            _messageStore.Clear(e.FieldIdentifier);
    }

    public void DisplayErrors(IDictionary<string, string[]> errors)
    {
        foreach (var err in errors)
        {
            var errorValue = err.Value[0];

            if(Localizer != null)
            {
                var localizedValue = Localizer[errorValue];

                if (!string.IsNullOrWhiteSpace(localizedValue))
                {
                    errorValue = localizedValue;
                }
            }

            _messageStore.Add(CurrentEditContext.Field(err.Key), errorValue);
        }

        CurrentEditContext.NotifyValidationStateChanged();
    }

    public void ClearErrors()
    {
        _messageStore.Clear();

        CurrentEditContext.NotifyValidationStateChanged();
    }
}
