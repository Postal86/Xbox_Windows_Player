using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Xbox_Windows_UWP.Behaviors
{
    public  sealed  class ItemClickBehavior : Behavior<ListViewBase>
    {
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty,  value);
        }

        /// <summary>
        ///   Identifies  the  <see cref="Command"/> property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(ItemClickBehavior),
            new(default(ICommand)));


        /// <summary>
        ///  Handles  a clicked  item  and invokes the associated  command
        /// </summary>
        /// <param name="sender">The current <see cref="ListViewBase"/> instance</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance  with the clicked item</param>
        private void HandleItemClick(object  sender, ItemClickEventArgs  e)
        {
            if (!(Command is ICommand command)) return;
            if (!command.CanExecute(e.ClickedItem)) return;

            command.Execute(e.ClickedItem);

        }


        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null) AssociatedObject.ItemClick += HandleItemClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null) AssociatedObject.ItemClick -= HandleItemClick;

        }
    }
}
