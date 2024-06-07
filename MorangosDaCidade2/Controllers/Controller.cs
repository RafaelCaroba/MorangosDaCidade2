using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MorangosDaCidade2.Controllers
{
    internal class Controller
    {
        public void ExibirTituloDaOpcao(string titulo)
        {
            int quantidadeDeLetras = titulo.Length;
            string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '-');
            Console.WriteLine(asteriscos);
            Console.WriteLine(titulo);
            Console.WriteLine(asteriscos + "\n");
        }

        public virtual async Task ExecutarAsync()
        {
           Console.Clear();

        }

        public static implicit operator Dictionary<object, object>(Controller v)
        {
            throw new NotImplementedException();
        }
    }
}
