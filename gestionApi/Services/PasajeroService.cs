using System.Net.Http.Headers;
using System.Text;
using gestionApi.Models.ModelViewDTO;
using gestionApi.Services.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gestionApi.Services;

public class PasajeroService : IPasajeroService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public PasajeroService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<PasajeroDto?> BuscarPorDni(string dni)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["ReniecApi:Token"]);

        var content = new StringContent(JsonConvert.SerializeObject(new { dni }), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://apiperu.dev/api/dni", content);

        if (!response.IsSuccessStatusCode) return null;

        string json = await response.Content.ReadAsStringAsync();
        var data = JObject.Parse(json)["data"];
        if (data == null)
        {
            return null;
        }

        return new PasajeroDto
        {
            Numero = (string?)data["numero"],
            NombreCompleto = (string?)data["nombre_completo"],
            Nombres = (string?)data["nombres"],
            ApellidoPaterno = (string?)data["apellido_paterno"],
            ApellidoMaterno = (string?)data["apellido_materno"],
            CodigoVerificacion = (int?)data["codigo_verificacion"] ?? 0,
            UbigeoSunat = (string?)data["ubigeo_sunat"],
            Ubigeo = data["ubigeo"]?.ToObject<string[]>(),
            Direccion = (string?)data["direccion"]
        };
    }

}