using Calc20WpfApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Calc20WpfApp.Properties;


namespace Calc20WpfApp.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private double number1;
        public double Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged();
            }
        }

        private double number2;
        public double Number2
        {
            get => number2;
            set
            {
                number2 = value;
                OnPropertyChanged();
            }
        }

        private double number3;
        public double Number3
        {
            get => number3;
            set
            {
                number3 = value;
                OnPropertyChanged();
            }
        }

        public string textBlockText;





        public ICommand AddCommand { get; }

        

        private void OnAddCommandExecute(object p) // тот метод который все вычисляет ссылается на Ariph.Add
        {
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }




        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текст кнопки
            string s = (string)((Button)e.OriginalSource).Content;
            // ДОбавляем его в текстовое поле
            textBlockText += s;
            double num;
            // Пытаемся преобразовать его в число
            bool result = double.TryParse(s, out num);
            // Если текст - это число
            if (result == true)
            {
                // Если операция не задана
                if (operation == "")
                {
                    // Добавляем к левому операнду
                    leftop += s;
                }
                else
                {
                    // Иначе к правому операнду
                    rightop += s;
                }
            }
            // Если было введено не число
            else
            {
                // Если равно, то выводим результат операции
                if (s == "=")
                {
                    Update_RightOp();
                    textBlockText += rightop;
                    operation = "";
                }
                // Очищаем поле и переменные
                else if (s == "C")
                {
                    leftop = "";
                    rightop = "";
                    operation = "";
                    textBlockText = "";
                }
                // Получаем операцию
                else
                {
                    // Если правый операнд уже имеется, то присваиваем его значение левому
                    // операнду, а правый операнд очищаем
                    if (rightop != "")
                    {
                        Update_RightOp();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }
        private bool CanAddCommandExecuted(object p)
        { //проверка числа
              
                return true;
            
        }

        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, CanAddCommandExecuted);
        }





        public string leftop = ""; // Левая часть
        public string operation = ""; // Знак операции
        public string rightop = ""; // Правая часть




        // Обновляем значение правого операнда
        private void Update_RightOp()
        {
            double num1 = double.Parse(leftop);
            double num2 = double.Parse(rightop);
            // И выполняем операцию
            switch (operation)
            {
                case "+":
                    rightop = Ariph.Add1(num1, num2).ToString();
                    break;
                case "-":
                    rightop = Ariph.Add2(num1, num2).ToString();
                    break;
                case "x":
                    rightop = Ariph.Add3(num1, num2).ToString();
                    break;
                case "/":
                    rightop = Ariph.Add4(num1, num2).ToString();
                    break;
            }
        }

    }
}
