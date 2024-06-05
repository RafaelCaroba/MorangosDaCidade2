using System;
using System.Collections.Generic;

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

        public virtual void Executar()
        {
            Console.Clear();

        }

        public static implicit operator Dictionary<object, object>(Controller v)
        {
            throw new NotImplementedException();
        }
    }
}
