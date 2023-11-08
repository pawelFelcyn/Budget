using Budget.ViewModels;
using IeuanWalker.Maui.Switch.Events;
using IeuanWalker.Maui.Switch.Helpers;
using IeuanWalker.Maui.Switch;

namespace Budget.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    static void CustomSwitch_SwitchPanUpdate(CustomSwitch customSwitch, SwitchPanUpdatedEventArgs e)
    {
        //Color Animation
        Color fromColor = e.IsToggled ? Color.FromArgb("#33b68d") : Color.FromArgb("#e7640f");
        Color toColor = e.IsToggled ? Color.FromArgb("#e7640f") : Color.FromArgb("#33b68d");

        double t = e.Percentage * 0.01;

        customSwitch.BackgroundColor = ColorAnimationUtil.ColorAnimation(fromColor, toColor, t);
    }
}