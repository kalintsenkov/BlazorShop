﻿namespace BlazorShop.Web.Client.Infrastructure
{
    using Microsoft.AspNetCore.Components.Forms;

    public class InputSelectNumber<TValue> : InputSelect<TValue>
    {
        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (typeof(TValue) != typeof(int))
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);

            if (int.TryParse(value, out var resultInt))
            {
                result = (TValue)(object)resultInt;
                validationErrorMessage = null;
                return true;
            }

            result = default;
            validationErrorMessage = "The chosen value is not a valid number.";
            return false;
        }
    }
}
