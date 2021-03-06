﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RestaurantManager.Commands
{
    class RelayCommandWithParameter : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> executeAction;

        public RelayCommandWithParameter(Action<object> executeAction)
            : this(executeAction, null)
        {
        }

        public RelayCommandWithParameter(Action<object> executeAction, Func<object, bool> canExecute)
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
            Func<object, bool> canExecuteHandler = this.canExecute;
            if (canExecuteHandler != null)
            {
                result = canExecuteHandler(parameter);
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
            this.executeAction(parameter);
        }
    }
}
