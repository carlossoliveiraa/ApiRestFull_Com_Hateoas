namespace SIICO.Dominio.Entidades
{
    public class Correspondente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public Correspondente()
        {
            DataCriacao = DateTime.UtcNow;
            Ativo = true;
        }

        public Correspondente(string nome, string cnpj, string email, string telefone) : this()
        {
            Nome = nome;
            Cnpj = cnpj;
            Email = email;
            Telefone = telefone;
        }
    }

    public class CorrespondenteConvenio
    {
        public int Id { get; set; }
        public int CorrespondenteId { get; set; }
        public int NumConvenio { get; set; }
        public string NumCnpj { get; set; } = string.Empty;
        public string NoEmpresa { get; set; } = string.Empty;
        public string NoFantasia { get; set; } = string.Empty;
        public int TipoConvenio { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        
        public Correspondente? Correspondente { get; set; }

        public CorrespondenteConvenio()
        {
            DataCriacao = DateTime.UtcNow;
            Ativo = true;
        }

        public CorrespondenteConvenio(int correspondenteId, int numConvenio, string numCnpj, 
            string noEmpresa, string noFantasia, int tipoConvenio) : this()
        {
            CorrespondenteId = correspondenteId;
            NumConvenio = numConvenio;
            NumCnpj = numCnpj;
            NoEmpresa = noEmpresa;
            NoFantasia = noFantasia;
            TipoConvenio = tipoConvenio;
        }
    }
}
