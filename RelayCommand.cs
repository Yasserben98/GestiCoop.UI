using System;
using System.Windows.Input;

namespace GestiCoop.UI
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _exec;
        private readonly Func<object?, bool>? _can;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _exec = execute ?? throw new ArgumentNullException(nameof(execute));
            _can = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? p) => _can == null || _can(p);

        public void Execute(object? p) => _exec(p);
    }
}