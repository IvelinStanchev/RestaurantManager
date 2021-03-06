﻿using Parse;
using RestaurantManager.Commands;
using RestaurantManager.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.ViewModels
{
    public class MenuViewModel
    {
        private ICommand goToAddOrdersCommand;
        private ICommand goToFinishedOrdersCommand;
        private ICommand goToAllOrdersCommand;
        private ICommand logOutCommand;
        private ICommand exitApplicationCommand;

        public MenuViewModel()
        {
            this.goToAddOrdersCommand = new RelayCommandWithOneParameter(this.GoToAddOrdersAction);
            this.goToFinishedOrdersCommand = new RelayCommand(this.GoToFinishedOrdersAction);
            this.goToAllOrdersCommand = new RelayCommand(this.GoToAllOrdersAction);
            this.logOutCommand = new RelayCommand(this.LogOutAction);
            this.exitApplicationCommand = new RelayCommand(this.ExitApplicationAction);
        }

        public ICommand GoToAddOrdersCommand
        {
            get
            {
                return this.goToAddOrdersCommand;
            }
        }

        public ICommand GoToFinishedOrdersCommand
        {
            get
            {
                return this.goToFinishedOrdersCommand;
            }
        }

        public ICommand GoToAllOrdersCommand
        {
            get
            {
                return this.goToAllOrdersCommand;
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return this.logOutCommand;
            }
        }

        public ICommand ExitApplicationCommand
        {
            get
            {
                return this.exitApplicationCommand;
            }
        }

        private void GoToAddOrdersAction(object parameter)
        {
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(AddOrderPage), 0);
        }

        private void GoToFinishedOrdersAction()
        {
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(FinishedOrdersPage), 0);
        }

        private void GoToAllOrdersAction()
        {
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(AllOrdersPage), 0);
        }

        private void LogOutAction()
        {
            ParseUser.LogOut();
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(LoginPage), 0);
            frame.BackStack.Clear();
        }

        private void ExitApplicationAction()
        {
            App.Current.Exit();
        }
    }
}
