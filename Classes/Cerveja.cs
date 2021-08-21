using static System.Environment;

namespace Cervejas
{
    public class Cerveja : EntidadeBase
    {
        private Tipo Tipo { get; set; }
        private string Nome { get; set; }
        private string Descricao { get; set; }
        private bool Excluido { get; set; }

        public Cerveja(int id, Tipo tipo, string nome, string descricao, bool excluido = false)
        {
            Id = id;
            Tipo = tipo;
            Nome = nome;
            Descricao = descricao;
            Excluido = excluido;
        }

        public string RetornaNome() => Nome;
        public Tipo RetornaTipo() => Tipo;
        public string RetornaDescricao() => Descricao;
        public int RetornaId() => Id;
        public bool RetornaExcluido() => Excluido;
        public void Excluir() => Excluido = true;

        public override string ToString()
        {
            string retorno = "";
            retorno += "Tipo: " + Tipo + NewLine;
            retorno += "Nome: " + Nome + NewLine;
            retorno += "Descrição: " + Descricao + NewLine;
            retorno += "Excluido: " + Excluido;
            return retorno;
        }
    }
}