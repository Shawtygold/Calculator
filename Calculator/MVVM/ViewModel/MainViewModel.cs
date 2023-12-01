using Calculator.Core;
using Calculator.MVVM.Model;
using Calculator.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModel
{
    internal class MainViewModel : Core.ViewModel
    {
        #region [Constructor]

        public MainViewModel(INavigationService navigation)
        {
            AppTitle = "Numberbox";
            Navigation = navigation;

            CloseCommand = new RelayCommand(Close);
            MaximizeCommand = new RelayCommand(Maximize);
            MinimizeCommand = new RelayCommand(Minimize);

            NumCommand = new RelayCommand(NumClick);
            SymbolCommand = new RelayCommand(SymbolClick);
            CommaCommand = new RelayCommand(CommaClick);
            EqualsCommand = new RelayCommand(EqualsClick);
            PiCommand = new RelayCommand(PiClick);
            ClearCommand = new RelayCommand(ClearClick);
            BackSpaceCommand = new RelayCommand(BackSpaceClick);
            SquareCommand = new RelayCommand(SquareClick);
            FactorialCommand = new RelayCommand(FactorialClick);
            PercentCommand = new RelayCommand(PercentClick);
            OneDividedByXCommand = new RelayCommand(OneDividedByXClick);
            RootOfXCommand = new RelayCommand(RootOfXClick);
        }

        #endregion

        #region [Properties]

        private double FirstNum { get; set; }
        private double SecondNum { get; set; }
        private string? Symbol { get; set; }

        #region [Number properties]

        public string Num0 { get; set; } = "0";
        public string Num1 { get; set; } = "1";
        public string Num2 { get; set; } = "2";
        public string Num3 { get; set; } = "3";
        public string Num4 { get; set; } = "4";
        public string Num5 { get; set; } = "5";
        public string Num6 { get; set; } = "6";
        public string Num7 { get; set; } = "7";
        public string Num8 { get; set; } = "8";
        public string Num9 { get; set; } = "9";

        #endregion

        #region [Symbol properties]

        public string Plus { get; set; } = "+";
        public string Minus { get; set; } = "-";
        public string Multiply { get; set; } = "×";
        public string Devide { get; set; } = "÷";
        public string Equals{ get; set; } = "=";
        public string Comma { get; set; } = ",";
        public string Pi { get; set; } = "π";
        public string Clear { get; set; } = "C";
        public string XSquared { get; set; } = "x²";
        public string BackSpace { get; set; } = "⌫";
        public string Factorial { get; set; } = "!x";
        public string Percent { get; set; } = "%";
        public string OneDividedByX { get; set; } = "⅟ₓ";
        public string RootOfX { get; set; } = "√x";

        #endregion

        public string AppTitle { get; set; }

        private string _text = "0";
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        #endregion

        #region [Commands]

        #region [Title bar buttons]

        public ICommand CloseCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }

        private void Close(object obj) => Application.Current.Shutdown();
        private void Maximize(object obj)
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }
        private void Minimize(object obj) => Application.Current.MainWindow.WindowState = WindowState.Minimized;

        #endregion

        #region [Calculator buttons]

        public ICommand NumCommand {  get; set; }
        public ICommand SymbolCommand { get; set; }
        public ICommand CommaCommand { get; set; }
        public ICommand EqualsCommand { get; set; }
        public ICommand PiCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand BackSpaceCommand { get; set; }   
        public ICommand SquareCommand { get; set; }
        public ICommand FactorialCommand { get; set; }
        public ICommand PercentCommand { get; set; }
        public ICommand OneDividedByXCommand { get; set; }
        public ICommand RootOfXCommand { get; set; }


        private void NumClick(object obj)
        {
            if (obj is not string num)
                return;

            if (Text == "0")
                Text = "";

            Text += num;
        }
        private void SymbolClick(object obj)
        {
            if (obj is not string symb || Text.Trim() == "")
                return;

            FirstNum = Convert.ToDouble(Text);
            switch (symb)
            {
                case "×": Symbol = "*"; break;
                case "÷": Symbol = "/"; break;
                case "+": Symbol = "+"; break;
                case "-": Symbol = "-"; break;
                default: break;
            }

            Text = "";
        }
        private void CommaClick(object obj)
        {
            if (Text.Trim() == "")
                return;

            Text += Comma;
        }
        private void EqualsClick(object obj)
        {
            if(Text.Trim() == "" || Text == "0" || Symbol == null)
                return;

            SecondNum = Convert.ToDouble(Text);
            Text = "";

            double? result = CalculatorManager.Calculate(FirstNum, SecondNum, Symbol);
            if(result == null)
            {
                Text = "0";
                return;
            }

            Text = result.ToString();

            Symbol = null;
        }
        private void PiClick(object obj) => Text = "3,14";
        private void ClearClick(object obj)
        {
            Text = "0";
            FirstNum = 0;
            SecondNum = 0;
            Symbol = null;
        }
        private void BackSpaceClick(object obj)
        {
            if (Text.Trim() == "" || Text == "0" || Text.Length - 1 == 0)
                return;

            char[] chars = new char[Text.Length - 1];
            Text.CopyTo(0, chars, 0, chars.Length);
            Text = new string(chars);
        }
        private void SquareClick(object obj)
        {
            FirstNum = Convert.ToDouble(Text);
            Text = Math.Pow(Convert.ToDouble(FirstNum), 2).ToString();
        }
        private void FactorialClick(object obj)
        {
            int num;
            try
            {
                num = Convert.ToInt32(Text);
                if (num < 0)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Факториал не может быть отрицательным или дробным!");
                Text = "0";
                return;
            }

            long? result = CalculatorManager.Factorial(num);
            if(result == null)
                return;

            Text = result.ToString();
        }
        private void PercentClick(object obj) => Text = (Convert.ToDouble(Text) / 100).ToString();
        private void OneDividedByXClick(object obj) => Text = (1 / Convert.ToDouble(Text)).ToString();
        private void RootOfXClick(object obj) => Text = Math.Sqrt(Convert.ToDouble(Text)).ToString();

        #endregion

        #endregion
    }
}
