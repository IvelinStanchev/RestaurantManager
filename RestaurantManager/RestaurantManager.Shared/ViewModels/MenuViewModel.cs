﻿using RestaurantManager.Commands;
using RestaurantManager.Models.Interfaces;
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
        private ICommand exitApplicationCommand;

        public MenuViewModel()
        {
            this.goToAddOrdersCommand = new RelayCommandWithOneParameter(this.GoToAddOrdersAction);
            this.goToFinishedOrdersCommand = new RelayCommand(this.GoToFinishedOrdersAction);
            this.goToAllOrdersCommand = new RelayCommand(this.GoToAllOrdersAction);
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
            string pesho = "just testing";
            frame.Navigate(typeof(AddOrderPage), pesho);
        }

        private void GoToFinishedOrdersAction()
        {
        }

        private void GoToAllOrdersAction()
        {
        }

        private void ExitApplicationAction()
        {
            App.Current.Exit();
        }
    }
}
