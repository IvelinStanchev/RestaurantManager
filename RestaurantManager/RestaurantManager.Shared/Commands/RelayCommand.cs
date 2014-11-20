using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RestaurantManager.Commands
{
    class RelayCommand : ICommand
    {
        Func<bool> canExecute;
        Action executeAction;

        public RelayCommand(Action executeAction)
            : this(executeAction, null)
        {
        }

        public RelayCommand(Action executeAction, Func<bool> canExecute)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException("executeAction");
            }

            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool result = true;
            Func<bool> canExecuteHandler = this.canExecute;
            if (canExecuteHandler != null)
            {
                result = canExecuteHandler();
            }

            return result;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            this.executeAction();
        }
    }
}
