using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private double value1 = 0;
        private double value2 = 0;
        private double result = 0;
        private string operation = "";
        private bool operationPerformed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculator";
            this.Width = 300;
            this.Height = 450;
            this.StartPosition = FormStartPosition.CenterScreen;

            // إنشاء صندوق النص لعرض النتائج
            TextBox display = new TextBox();
            display.ReadOnly = true;
            display.Font = new System.Drawing.Font("Arial", 18);
            display.Width = 260;
            display.Height = 50;
            display.Location = new System.Drawing.Point(10, 20);
            display.TextAlign = HorizontalAlignment.Right;
            display.Text = "0";
            this.Controls.Add(display);

            // إنشاء الأزرار الرقمية والعمليات
            string[] buttonTexts = {
                "7", "8", "9", "/",
                "4", "5", "6", "*",
                "1", "2", "3", "-",
                "0", ".", "=", "+"
            };

            int buttonSize = 60;
            int xOffset = 10;
            int yOffset = 80;
            int padding = 5;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string buttonText = buttonTexts[i * 4 + j];
                    Button button = new Button();
                    button.Text = buttonText;
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    int x = xOffset + j * (buttonSize + padding);
                    int y = yOffset + i * (buttonSize + padding);
                    button.Location = new System.Drawing.Point(x, y);
                    button.Font = new System.Drawing.Font("Arial", 14);

                    // إضافة معالج الحدث للأزرار
                    button.Click += (sender, e) =>
                    {
                        Button btn = (Button)sender;

                        // الأرقام والنقطة العشرية
                        if (btn.Text == "." || (btn.Text[0] >= '0' && btn.Text[0] <= '9'))
                        {
                            if (display.Text == "0" || operationPerformed)
                            {
                                display.Clear();
                                operationPerformed = false;
                            }

                            // منع إضافة أكثر من نقطة عشرية واحدة
                            if (btn.Text == "." && display.Text.Contains("."))
                                return;

                            display.Text += btn.Text;
                        }
                        // العمليات الحسابية
                        else if (btn.Text == "+" || btn.Text == "-" || btn.Text == "*" || btn.Text == "/")
                        {
                            if (value1 != 0)
                            {
                                // إذا تم بالفعل إدخال عملية، قم بحساب النتيجة أولاً
                                Button equalsButton = new Button();
                                equalsButton.Text = "=";
                                equalsButton.PerformClick();
                            }

                            value1 = double.Parse(display.Text);
                            operation = btn.Text;
                            operationPerformed = true;
                        }
                        // زر المساواة
                        else if (btn.Text == "=")
                        {
                            value2 = double.Parse(display.Text);
                            
                            switch (operation)
                            {
                                case "+":
                                    result = value1 + value2;
                                    break;
                                case "-":
                                    result = value1 - value2;
                                    break;
                                case "*":
                                    result = value1 * value2;
                                    break;
                                case "/":
                                    if (value2 != 0)
                                        result = value1 / value2;
                                    else
                                    {
                                        MessageBox.Show("لا يمكن القسمة على صفر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    break;
                            }

                            display.Text = result.ToString();
                            value1 = result;
                            operation = "";
                        }
                    };

                    this.Controls.Add(button);
                }
            }

            // إضافة زر المسح C
            Button clearButton = new Button();
            clearButton.Text = "C";
            clearButton.Width = 260;
            clearButton.Height = 40;
            clearButton.Location = new System.Drawing.Point(10, 340);
            clearButton.Font = new System.Drawing.Font("Arial", 14);
            clearButton.Click += (sender, e) =>
            {
                display.Text = "0";
                value1 = 0;
                value2 = 0;
                operation = "";
            };
            this.Controls.Add(clearButton);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}