using CommunityToolkit.Mvvm.Messaging.Messages;
using ContriesDatabase.Models;

namespace InformationDirectoryOfСountries.Models;

public sealed class UpdateCountryMessage : ValueChangedMessage<Country>
{
    public UpdateCountryMessage(Country value) : base(value)
    {
    }
}