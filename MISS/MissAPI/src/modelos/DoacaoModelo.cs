using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissAPI.Src.modelos
{  /// <summary>
   /// <para>Resumo: Classe responsavel por representar tb_doacao no banco.</para>
   /// <para>Criado por: Samira Ixoobecan</para>
   /// <para>Versão: 1.0</para>
   /// <para>Data: 06/09/2022</para>
   /// </summary>

    [Table("tb_Doacao")]
    public class DoacaoModelo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Contato { get; set; }

        public int  Dias { get; set; }

        public int Hora { get; set; }

        public string Especialidade { get; set; }

        public string CNPJDoador { get; set; }

        public string Localizacao { get; set; }

        public StatusDoacao Status { get; set; }
    }
}

