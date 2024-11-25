namespace PDM___Projeto
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        // Método chamado quando o aplicativo é inicializado
        protected override void OnStart()
        {
            base.OnStart();
            Console.WriteLine("Aplicativo inicializado!");
        }

        // Método chamado quando o aplicativo entra em segundo plano
        protected override void OnSleep()
        {
            base.OnSleep();
            Console.WriteLine("Aplicativo pausado!");
            // Adicione aqui ações para salvar estado ou pausar tarefas
        }

        // Método chamado quando o aplicativo volta do segundo plano
        protected override void OnResume()
        {
            base.OnResume();
            Console.WriteLine("Aplicativo retomado!");
            // Adicione aqui ações para retomar tarefas ou atualizar a interface
        }
    }
}
