using System;
using System.Windows.Forms;
using QuanLyTiemDaQuy.Forms;

namespace QuanLyTiemDaQuy
{
    internal static class Program
    {
        /// <summary>
        /// Điểm bắt đầu chính của ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Loop để handle logout -> quay lại login
            bool continueRunning = true;
            while (continueRunning)
            {
                var loginForm = new LoginForm();
                Application.Run(loginForm);
                
                // Nếu login thành công, mở MainForm
                if (loginForm.DialogResult == DialogResult.OK)
                {
                    var mainForm = new MainForm();
                    Application.Run(mainForm);
                    
                    // Nếu MainForm đóng vì logout, tiếp tục vòng lặp
                    // Nếu đóng vì exit, thoát vòng lặp
                    continueRunning = mainForm.ReturnToLogin;
                }
                else
                {
                    // Cancel login -> thoát hoàn toàn
                    continueRunning = false;
                }
            }
        }
    }
}
