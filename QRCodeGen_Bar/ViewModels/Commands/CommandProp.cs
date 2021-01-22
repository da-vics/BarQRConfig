using System;
using System.Windows.Input;

namespace QRCodeGen_Bar.ViewModels.Commands
{
    public class CommandProp : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, o) => { };

        private Action _action { get; set; }

        public CommandProp(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
