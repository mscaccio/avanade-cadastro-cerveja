using System;
using System.Collections.Generic;
using Cervejas.Interfaces;
using System.IO;
using static System.Environment;
using static System.IO.Path;
using static System.Console;

namespace Cervejas
{
    public class CervejaRepositorio : IRepositorio<Cerveja>
    {
        private List<Cerveja> listaCerveja = new List<Cerveja>();

        public void Atualiza(int id, Cerveja objeto)
        {
            listaCerveja[id] = objeto;
            EscreveArquivo();
        }

        public void Exclui(int id)
        {
            listaCerveja[id].Excluir();
            EscreveArquivo();
        }

        public void Insere(Cerveja entidade)
        {
            listaCerveja.Add(entidade);
            EscreveArquivo();
        }

        public List<Cerveja> Lista() => listaCerveja;
        public int ProximoId() => listaCerveja.Count;
        public Cerveja RetornaPorId(int id) => listaCerveja[id];

        public void CarregaListaCervejaDoArquivo()
        {
            try
            {
                var filePath = Combine(CurrentDirectory, "Cervejas.txt");

                if (!File.Exists(filePath))
                    return;

                using (StreamReader reader = File.OpenText(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var c = reader.ReadLine().Split('`');
                        var cerveja = new Cerveja(id: int.Parse(c[0]),
                                                  tipo: Enum.Parse<Tipo>(c[1]),
                                                  nome: c[2],
                                                  descricao: c[3],
                                                  excluido: bool.Parse(c[4]));

                        listaCerveja.Add(cerveja);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Tipo: {ex.GetType()} > Mensagem: {ex.Message}");
            }
        }

        private void EscreveArquivo()
        {
            try
            {
                var filePath = Combine(CurrentDirectory, "Cervejas.txt");
                
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    foreach (var cerveja in listaCerveja)
                        writer.WriteLine($"{cerveja.Id}`" +
                                         $"{cerveja.RetornaTipo()}`" +
                                         $"{cerveja.RetornaNome()}`" +
                                         $"{cerveja.RetornaDescricao()}`" +
                                         $"{cerveja.RetornaExcluido()}");
                }
            }
            catch (Exception ex)
            {
                WriteLine($"Tipo: {ex.GetType()} > Mensagem: {ex.Message}");
            }
        }
    }
}