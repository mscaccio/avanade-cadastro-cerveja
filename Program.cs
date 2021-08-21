using System;
using static System.Console;

namespace Cervejas
{
    class Program
    {
        static CervejaRepositorio repositorio = new CervejaRepositorio();
        static void Main(string[] args)
        {
            repositorio.CarregaListaCervejaDoArquivo();

            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarCerveja();
                        break;
                    case "2":
                        InserirAtualizarCerveja(inserir: true);
                        break;
                    case "3":
                        InserirAtualizarCerveja(inserir: false);
                        break;
                    case "4":
                        ExcluirCerveja();
                        break;
                    case "5":
                        VisualizarCerveja();
                        break;
                    case "C":
                        Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static string ObterOpcaoUsuario()
        {
            var ehSexta = DateTime.Now.DayOfWeek == DayOfWeek.Friday ? "> SEXTOU :) " : "";

            WriteLine();
            WriteLine($"*** CERVEJAS - BEBA SEMPRE {ehSexta}***");
            WriteLine();
            WriteLine("Informe a opção desejada:");
            WriteLine("1- Listar cervejas");
            WriteLine("2- Inserir nova cerveja");
            WriteLine("3- Atualizar cerveja");
            WriteLine("4- Excluir cerveja");
            WriteLine("5- Visualizar cerveja");
            WriteLine("C- Limpar Tela");
            WriteLine("X- Sair");
            WriteLine("");

            string opcaoUsuario = ReadLine().ToUpper();
            WriteLine();
            return opcaoUsuario;
        }

        private static void ListarCerveja()
        {
            WriteLine("Listar cervejas");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                WriteLine("Nenhuma cerveja cadastrada.");
                return;
            }

            foreach (var cerveja in lista)
                WriteLine("#ID {0}: - {1} {2}", cerveja.RetornaId(), cerveja.RetornaNome(), cerveja.RetornaExcluido() ? "*Excluido*" : "");
        }

        private static void InserirAtualizarCerveja(bool inserir)
        {
            if (inserir)
            {
                WriteLine("Inserir nova cerveja");
                Cerveja cerveja = informarCerveja();
                repositorio.Insere(cerveja);
            }
            else
            {
                Write("Digite o id cerveja: ");
                int indiceCerveja = int.Parse(ReadLine());
                Cerveja cerveja = informarCerveja(indiceCerveja);
                repositorio.Atualiza(indiceCerveja, cerveja);
            }

            Cerveja informarCerveja(int? indice = null)
            {
                foreach (int i in Enum.GetValues(typeof(Tipo)))
                    WriteLine($"{i} - {Enum.GetName(typeof(Tipo), i)}");

                Write("Digite o tipo entre as opções acima: ");
                int entradaTipo = int.Parse(ReadLine());

                Write("Digite o nome da cerveja: ");
                string entradaNome = ReadLine();

                Write("Digite descrição da cerveja: ");
                string entradaDescricao = ReadLine();

                return new Cerveja(id: indice.HasValue ? (int)indice : repositorio.ProximoId(),
                                   tipo: (Tipo)entradaTipo,
                                   nome: entradaNome,
                                   descricao: entradaDescricao);
            }
        }

        private static void ExcluirCerveja()
        {
            Write("Digite o id da cerveja: ");
            int indiceCerveja = int.Parse(ReadLine());

            repositorio.Exclui(indiceCerveja);
        }

        private static void VisualizarCerveja()
        {
            Write("Digite o id da cerveja: ");
            int indiceCerveja = int.Parse(ReadLine());

            var cerveja = repositorio.RetornaPorId(indiceCerveja);

            WriteLine(cerveja);
        }
    }
}