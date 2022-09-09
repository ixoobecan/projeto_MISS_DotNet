using MissAPI.Src.modelos.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissAPI.Src.modelos
{  /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_solicitacao no banco.</para>
    /// <para>Criado por: Samira Ixoobecan</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 06/09/2022</para>
    /// </summary>

    [Table("tb_consultas")]
    public class Consulta
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("fk_usuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("fk_medico")]
        public  Medico Medico { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public StatusConsulta StatusConsulta { get; set; }
    }
}
