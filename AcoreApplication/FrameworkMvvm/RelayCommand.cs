using System;
using System.Windows.Input;
using AcoreApplication.Model;
using GalaSoft.MvvmLight.Command;

namespace AcoreApplication.FrameworkMvvm
{
    public class RelayCommand : ICommand
    {
        private readonly Action<Object> actionAExecuter;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<Object> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            
            this.actionAExecuter = action;
        }

        public bool CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter)
        {
            this.actionAExecuter(parameter);
        }
    }
}
