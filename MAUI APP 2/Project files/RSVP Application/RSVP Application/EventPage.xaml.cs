namespace RSVP_Application;

public partial class EventPage : ContentPage
{
	bool IsCreator = false;

	public EventPage(bool userIsCreator)
	{
		InitializeComponent();
		IsCreator = userIsCreator;

		//toggle visibility
		CreatorSection.IsVisible = IsCreator;
		GuestSection.IsVisible = !IsCreator;

		//setting fields to read only if not creator
		EventNameEntry.IsReadOnly = !IsCreator;
		EventLocationEntry.IsReadOnly = !IsCreator;
		EventDescriptionEditor.IsReadOnly = !IsCreator;

		//date picker settings
		EventDatePicker.IsEnabled = IsCreator;
		EventTimePicker.IsEnabled = IsCreator;
    }
}