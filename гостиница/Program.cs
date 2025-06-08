using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace гостиница
{
    internal static class Program
    {
        public static IServiceProvider Services { get; private set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceCollection = new ServiceCollection();

            // Регистрируем все формы, которые будем использовать
            serviceCollection.AddTransient<Form1>();
            serviceCollection.AddTransient<Form6>();

            Services = serviceCollection.BuildServiceProvider();

            // Показываем форму логина
            var loginForm = Services.GetRequiredService<Form6>();
            var result = loginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Если логин успешен, запускаем главную форму
                Application.Run(Services.GetRequiredService<Form1>());
            }
            else
            {
                // Если отменили логин или неудача — завершаем приложение
                Application.Exit();
            }
        }
    }
}


