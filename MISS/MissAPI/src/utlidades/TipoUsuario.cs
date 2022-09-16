using System.Text.Json.Serialization;

namespace MissAPI.Src.Utlidades
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        MEDICO,
        ADMINISTRADOR
    }
}
