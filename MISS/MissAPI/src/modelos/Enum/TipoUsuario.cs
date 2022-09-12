using System.Text.Json.Serialization;

namespace MissAPI.Src.modelos.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        MEDICO,
        ADMINISTRADOR
    }
}
