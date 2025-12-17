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
            
            // Khởi động với LoginForm
            Application.Run(new LoginForm());
        }
    }
}
