namespace PDM___Projeto
{
    using System.Net.Http;
    using Newtonsoft.Json;           // Para deserializar o JSON retornado pela API
    using Microsoft.Maui.Devices.Sensors; // Para acessar funcionalidades como geolocalização
    using System.Threading.Tasks;   // Para usar async/await
    using System;
    public partial class MainPage : ContentPage
    {
        public static string apiKey = "7ddd45fcbfc190d9c40b9e755be86b47";
        private static HttpClient client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSelectCityClicked(object sender, EventArgs e)
        {
            try
            {
                // Solicita o nome da cidade
                string cidade = await DisplayPromptAsync(
                    "Selecionar Cidade",
                    "Digite o nome da cidade:",
                    "OK",
                    "Cancelar",
                    "Ex.: São Paulo",
                    maxLength: 50);

                if (!string.IsNullOrEmpty(cidade))
                {
                    await GetTemperature(cidade);
                }
                else
                {
                    await DisplayAlert("Erro", "Você precisa digitar uma cidade válida.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao processar: {ex.Message}", "OK");
            }
        }
        private async Task GetTemperature(string cidade)
        {
            var weather = await GetWeatherAsync(cidade);

            if (weather != null && weather.main != null && weather.weather != null && weather.weather.Length > 0)
            {
                // Exibe a temperatura atual
                TemperatureLabel.Text = $"{weather.main.temp}°C";

                CidadeLabel.Text = $"{cidade}°C";

                // Exibe a condição climática (ex: "Ensolarado", "Chuvoso")
                ConditionLabel.Text = $"{weather.weather[0].description}";

                // Exibe a temperatura máxima e mínima
                MaxTempLabel.Text = $"Máxima: {weather.main.temp_max}°C";
                MinTempLabel.Text = $"Mínima: {weather.main.temp_min}°C";
            }
            else
            {
                // Caso haja erro na resposta da API
                TemperatureLabel.Text = "Erro ao obter clima.";
                ConditionLabel.Text = string.Empty;
                MaxTempLabel.Text = string.Empty;
                MinTempLabel.Text = string.Empty;
            }
        }

        private async Task<WeatherData> GetWeatherAsync(string cidade)
        {
            try
            {
                // URL da API com a cidade fixada como Brasília
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={apiKey}&units=metric&lang=pt";
                var response = await client.GetStringAsync(url); // Faz a requisição GET
                var weatherData = JsonConvert.DeserializeObject<WeatherData>(response); // Deserializa a resposta
                return weatherData;
            }
            catch (Exception)
            {
                return null; // Retorna null em caso de erro
            }
        }
    }

    public class WeatherData
    {
        public Main main { get; set; }
        public Weather[] weather { get; set; }  // Para acessar a condição climática
    }

    public class Main
    {
        public double temp { get; set; }
        public double temp_max { get; set; }
        public double temp_min { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }  // Descrição da condição climática (ex: "Ensolarado", "Nublado")
    }

}
