using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoDejogos
{
    public class JogoRepository : IJogoRepository
    {


        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Jogo{Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Fifa 21", Produtora = "EA", Preco = 200} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Jogo{Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "God Of War", Produtora = "Santa Monica", Preco = 200} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Jogo{Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Sunset Over Driver", Produtora = "Insomniac", Preco = 150} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Jogo{Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Forza", Produtora = "Turn 10 Studios", Preco = 200} }
      
        };


      

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);

        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }


        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach(var jogo in jogos.Values)
            {
                if(jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                {
                    retorno.Add(jogo);
                }
            }

            return Task.FromResult(retorno);
        }


        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }


        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }


        public void Dispose()
        {
            // fecha a conexão
        }
    }
}
