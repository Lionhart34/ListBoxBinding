using ListBoxBinding.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ListBoxBinding.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private BaseViewModel _CurrentViewModel = null;
        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _CurrentViewModel;
            }
            set
            {
                _CurrentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }

        }

        #region Command

        private ICommand _GoTo = null;
        public ICommand GoTo
        {
            get
            {
                if (_GoTo == null)
                    _GoTo = new ProxyCommand<string>((t) => NavigateTo(t));
                return _GoTo;
            }
        }
        public void NavigateTo(string targetViewModel)
        {
            try
            {
                switch (targetViewModel)
                {
                    case "1":
                        CurrentViewModel = new ProjectViewModel();
                        break;
                    case "2":
                        CurrentViewModel = new ManageSkillsViewModel();
                        break;
                    default:
                        CurrentViewModel = null;
                        break;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }

        private ICommand _GoToProj = null;
        public ICommand GoToProj
        {
            get
            {
                if (_GoToProj == null)
                    _GoToProj = new ProxyCommand(() => { CurrentViewModel = new ProjectViewModel(); });
                return _GoToProj;
            }
        }

        private ICommand _GoToSkills = null;
        public ICommand GoToSkills
        {
            get
            {
                if (_GoToSkills == null)
                    _GoToSkills = new ProxyCommand(() => { CurrentViewModel = new ManageSkillsViewModel(); });
                return _GoToSkills;
            }
        }

        private ICommand _ContentOfDataSource = null;
        public ICommand ContentOfDataSource 
        {
            get
            {
                if (_ContentOfDataSource == null)
                    _ContentOfDataSource = new ProxyCommand(() => 
                    {
                        foreach(Person P in Global.Context.Persons)
                        {
                            if (P.MasterSkill == null)
                                MessageBox.Show("The MasterSkill of " + P.Name + " is null !");
                            else
                                MessageBox.Show(P.Name + " MasterSkills=> " + P.MasterSkill.ShortName + " : " + P.MasterSkill.LongName);
                        }
                    });
                return _ContentOfDataSource;
            }
        }


        #endregion

        #region INotifyPropertyChanged

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class ProxyCommand : ICommand
    {
        #region Fields

        readonly Action _execute = null;
        readonly Func<bool> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public ProxyCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public ProxyCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute()
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }
            return true;
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute()
        {
            if (_execute != null)
            {
                try
                {
                    _execute();
                }
                catch (Exception E)
                {
                    System.Windows.MessageBox.Show(E.ToDisplay(), "Exception non gérée !", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }


        #endregion
    }

    public class ProxyCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Func<T,bool> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public ProxyCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public ProxyCommand(Action<T> execute, Func<T,bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(T parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(T parameter)
        {
            if (_execute != null)
            {
                try
                {
                    _execute(parameter);
                }
                catch (Exception E)
                {
                    System.Windows.MessageBox.Show(E.ToDisplay(), "Exception non gérée !", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                }
            }
        }
        
        bool ICommand.CanExecute(object parameter)
        {
            // if T is of value type and the parameter is not
            // set yet, then return false if CanExecute delegate
            // exists, else return true
            if (parameter == null &&
                typeof(T).IsValueType)
            {
                return (_canExecute == null);
            }


            // l'object parameter est en cours de destruction
            if (parameter != null)
                if (parameter.GetType().Name == "NamedObject") //MS.Internal.NamedObject
                    return false;

            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }


        #endregion
    }
    
    public static class ExceptionExtension
    {
        public static string ToDisplay(this Exception E)
        {
            string Message = "";

            Message += "EXCEPTION : \n" + E.Message + "\n";

            if (E.InnerException != null)
            {
                Message += "INNEREXCEPTION : \n" + E.InnerException.Message + "\n";
            }

            Message += "STACKTRACE : \n" + E.StackTrace;

            return Message;
        }
    }
}



