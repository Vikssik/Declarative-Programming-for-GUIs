using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private double? _firstNumber = null;
        private string _operation = "";
        private bool _isNewInput = true;

        public MainWindow()
        {
            InitializeComponent();
            Display.Text = "0";
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((Button)sender).Content;

            if (_isNewInput || Display.Text == "0")
            {
                Display.Text = number;
                _isNewInput = false;
            }
            else
            {
                Display.Text += number;
            }
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (_isNewInput)
            {
                Display.Text = "0.";
                _isNewInput = false;
            }
            else if (!Display.Text.Contains("."))
            {
                Display.Text += ".";
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            double number;
            if (double.TryParse(Display.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
            {
                _firstNumber = number;
                _operation = (string)((Button)sender).Content;
                _isNewInput = true;
            }
            else
            {
                Display.Text = "Помилка";
            }
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (_firstNumber == null || string.IsNullOrEmpty(_operation))
                return;

            double secondNumber;
            if (!double.TryParse(Display.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out secondNumber))
            {
                Display.Text = "Помилка";
                return;
            }

            double result = 0;
            try
            {
                switch (_operation)
                {
                    case "+":
                        result = _firstNumber.Value + secondNumber;
                        break;
                    case "-":
                        result = _firstNumber.Value - secondNumber;
                        break;
                    case "*":
                        result = _firstNumber.Value * secondNumber;
                        break;
                    case "/":
                        if (secondNumber == 0)
                        {
                            Display.Text = "Ділення на 0!";
                            return;
                        }
                        result = _firstNumber.Value / secondNumber;
                        break;
                    default:
                        Display.Text = "Невідома операція";
                        return;
                }

                Display.Text = result.ToString(CultureInfo.InvariantCulture);
                _firstNumber = null;
                _isNewInput = true;
                _operation = "";
            }
            catch
            {
                Display.Text = "Помилка";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            _firstNumber = null;
            _operation = "";
            _isNewInput = true;
        }
    }
}

