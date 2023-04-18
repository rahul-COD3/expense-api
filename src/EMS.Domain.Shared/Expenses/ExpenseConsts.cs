using System;
using Volo.Abp;

namespace EMS.Expenses
{
    public static class ExpenseConsts
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 2048;
        public const int MaxCurrencyLength = 16;

        public static void CheckTitle(string title)
        {
            Check.NotNullOrWhiteSpace(
                title,
                nameof(title),
                maxLength: MaxTitleLength
            );
        }

        public static void CheckDescription(string description)
        {
            Check.NotNullOrWhiteSpace(
                description,
                nameof(description),
                maxLength: MaxDescriptionLength
            );
        }

        public static void CheckCurrency(string currency)
        {
            Check.NotNullOrWhiteSpace(
                currency,
                nameof(currency),
                maxLength: MaxCurrencyLength
            );
        }
    }
}
