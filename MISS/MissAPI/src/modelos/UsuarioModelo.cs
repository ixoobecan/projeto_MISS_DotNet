using MissAPI.Src.modelos.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MissAPI.Src.modelos
{    /// <summary>
     /// <para>Resumo: Classe responsavel por representar tb_usuarios no banco.</para>
     /// <para>Criado por: Samira Ixoobecan</para>
     /// <para>Versão: 1.0</para>
     /// <para>Data: 06/09/2022</para>
     /// </summary>
    [Table("tb_usuarios")]
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Foto { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string CPF { get; set; }

        public TipoUsuario Tipo { get; set; }

        [JsonIgnore, InverseProperty("Usuario")]
        public List<Consulta> ConsultasMarcadas { get; set; }
    }
}


        
   



