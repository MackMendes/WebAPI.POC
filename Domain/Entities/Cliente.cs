using System;

namespace Domain.Entities
{
    public sealed class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Apelido { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }
    }
}
