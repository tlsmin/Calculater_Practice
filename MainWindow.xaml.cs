
using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculater_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double Saved;
        private double percentage; // 사칙연산 계산 결과값을 담음 ( 퍼센트함수에 사용 )
        private bool OpFlag; // default -> false
        private double memory;
        private string Op = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            btn_MR.IsEnabled = false;
            btn_MC.IsEnabled = false;
        }

        private void btn_MC_Click(object sender, RoutedEventArgs e)
        {
            memory = 0;
            btn_MR.IsEnabled = false;
            btn_MC.IsEnabled = false;
        }

        private void btn_MR_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = memory.ToString();
        }

        private void btn_MPlus_Click(object sender, RoutedEventArgs e)
        {
            memory += double.Parse(txtResult.Text);
            btn_MR.IsEnabled = true;
            btn_MC.IsEnabled = true;
            OpFlag = true;
        }

        private void btn_MMinus_Click(object sender, RoutedEventArgs e)
        {
            memory -= double.Parse(txtResult.Text);
            btn_MR.IsEnabled = true;
            btn_MC.IsEnabled = true;
            OpFlag = true;
        }

        private void btn_MS_Click(object sender, RoutedEventArgs e)
        {
            memory = double.Parse(txtResult.Text);
            OpFlag = true;
        }

        private void btnPercent_Click(object sender, RoutedEventArgs e)
        {
            // 퍼센트 계산 = 전체값 * 퍼센트 / 100
            // 전체값 = 사칙연산 결과값 ( Saved인 줄 알고 멘붕에 빠졌음 -> percentage 필드를 생성해서 담아줌 )
            txtResult.Text = ((percentage * double.Parse(txtResult.Text)) / 100 ).ToString();
            txtExp.Text = txtResult.Text;
        }

        // 현재값 모두 지우기
        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
            txtExp.Text = "";
            Saved = 0;
            OpFlag = false;
            Op = "";
        }

        // 현재값 한칸 지우기
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);

            if (txtResult.Text == string.Empty)
            {
                txtResult.Text = "0";
            }
        }

        // 숫자키 처리
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (txtResult.Text == "0" || OpFlag == true)
            {
                txtResult.Text = btn.Content.ToString();
                OpFlag = false;
            }
            else
            {
                txtResult.Text += btn.Content.ToString();
            }
        }

        private void btnRecip_Click(object sender, RoutedEventArgs e)
        {
            txtExp.Text = " 1 / ( " + txtResult.Text + " ) ";
            txtResult.Text = (1 / double.Parse(txtResult.Text)).ToString();
            OpFlag = true;
        }

        private void btnSqr_Click(object sender, RoutedEventArgs e)
        {
            txtExp.Text = " x² ( " + txtResult.Text + " ) ";
            txtResult.Text = (double.Parse(txtResult.Text) * double.Parse(txtResult.Text)).ToString();
            OpFlag = true;
        }

        private void btnSqrt_Click(object sender, RoutedEventArgs e)
        {
            txtExp.Text = " √ ( " + txtResult.Text + " ) ";
            txtResult.Text = (Math.Sqrt(double.Parse(txtResult.Text))).ToString();
            OpFlag = true;
        }

        // 사칙연산 처리
        private void Op_Click(object sender, RoutedEventArgs e)
        {
            Saved = double.Parse(txtResult.Text);
            OpFlag = true;

            Button btn = sender as Button;
            Op = btn.Content.ToString();

            if (txtExp.Text.Contains("="))
            {
                txtExp.Text = Saved + " " + Op + " ";
            }
            else
            {
                txtExp.Text += Saved + " " + Op + " ";
            }
        }

        private void btnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            double v = double.Parse(txtResult.Text);
            v = -v;
            txtResult.Text = v.ToString();
        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            if(txtResult.Text.Contains("."))
            {
                return;
            }
            else
            {
                txtResult.Text += ".";
            }
        }

        // 사칙연산 계산처리
        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            double v = double.Parse(txtResult.Text);    

            switch (Op)
            {
                case "+":
                    txtResult.Text = (Saved + v).ToString();
                    break;

                case "-":
                    txtResult.Text = (Saved - v).ToString();
                    break;

                 case "x":
                    txtResult.Text = (Saved * v).ToString();
                    break;

                case "÷":
                    txtResult.Text = (Saved / v).ToString();
                    break;
            }

            txtExp.Text = Saved + " " + Op + " " + v + " = ";
            percentage = double.Parse(txtResult.Text);
            OpFlag = true;
            
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}