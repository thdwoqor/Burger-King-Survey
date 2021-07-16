using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 버거킹_설문조사
{
    public partial class Form1 : Form
    {
        private ChromeDriverService _driverService = null;
        private ChromeOptions _options = null;
        private ChromeDriver _driver = null;
        public Form1()
        {
            InitializeComponent();

            panel1.Visible = true;

            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            _options.AddArgument("--headless");
            _options.AddArgument("disable-gpu");
            _options.AddArgument("user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6)" + "AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 3) // 이벤트 핸들러 설정된 컨트롤의 글자입력수가 3글자이면,
            {
                textBox2.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 3) // 이벤트 핸들러 설정된 컨트롤의 글자입력수가 3글자이면,
            {
                textBox3.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 3) // 이벤트 핸들러 설정된 컨트롤의 글자입력수가 3글자이면,
            {
                textBox4.Focus();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 3) // 이벤트 핸들러 설정된 컨트롤의 글자입력수가 3글자이면,
            {
                textBox5.Focus();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 3) // 이벤트 핸들러 설정된 컨트롤의 글자입력수가 3글자이면,
            {
                textBox6.Focus();
            }
        }

        public void start()
        {
            _driver = new ChromeDriver(@"chrom", _options);
            //_driver.Navigate().GoToUrl("https://kor.tellburgerking.com/");  // 웹 사이트에 접속합니다.
            _driver.Navigate().GoToUrl("https://kor.tellburgerking.com/?AspxAutoDetectCookieSupport=1");  // 웹 사이트에 접속합니다.
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            //_driver.Manage().Window.Position = new System.Drawing.Point(1, 1);

            var element = _driver.FindElement(By.XPath("//*[@id='NextButton']"));
            _driver.ExecuteScript("arguments[0].click();", element);

            element = _driver.FindElement(By.XPath("//*[@id='CN1']"));
            element.SendKeys(textBox1.Text);
            element = _driver.FindElement(By.XPath("//*[@id='CN2']"));
            element.SendKeys(textBox2.Text);
            element = _driver.FindElement(By.XPath("//*[@id='CN3']"));
            element.SendKeys(textBox3.Text);
            element = _driver.FindElement(By.XPath("//*[@id='CN4']"));
            element.SendKeys(textBox4.Text);
            element = _driver.FindElement(By.XPath("//*[@id='CN5']"));
            element.SendKeys(textBox5.Text);
            element = _driver.FindElement(By.XPath("//*[@id='CN6']"));
            element.SendKeys(textBox6.Text);
            element = _driver.FindElement(By.XPath("//*[@id='NextButton']"));
            _driver.ExecuteScript("arguments[0].click();", element);

            bool check = true;
            while (check)
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        try
                        {
                            element = _driver.FindElement(By.XPath("//*[@id='NextButton']"));
                            _driver.ExecuteScript("arguments[0].click();", element);
                        }
                        catch (Exception ex)
                        {
                            panel1.Visible = false;

                            //element = _driver.FindElement(By.XPath("//*[@id='finishIncentiveHolder']/p[3]"));
                            //label6.Text = element.Text;
                            string source = _driver.PageSource;
                            string data = Regex.Split(Regex.Split(source, @"ValCode"">")[1], @"</p>")[0];
                            label6.Text = data;
                            panel2.Visible = true;
                            check = false;
                        }

                    }));
                }
                else
                {
                    try
                    {
                        element = _driver.FindElement(By.XPath("//*[@id='NextButton']"));
                        _driver.ExecuteScript("arguments[0].click();", element);
                    }
                    catch (Exception ex)
                    {
                        panel1.Visible = false;

                        //element = _driver.FindElement(By.XPath("//*[@id='finishIncentiveHolder']/p[3]"));
                        //label6.Text = element.Text;

                        string source = _driver.PageSource;
                        string data = Regex.Split(Regex.Split(source, @"ValCode"">")[1], @"</p>")[0];
                        label6.Text = data;

                        panel2.Visible = true;
                        check = false;
                    }
                }
            }
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            Thread acceptThread = new Thread(() => start());
            acceptThread.IsBackground = true;   // 부모 종료시 스레드 종료
            acceptThread.Start();
        }

    }
}
