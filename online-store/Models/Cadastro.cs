using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace online_store.Models
{
    [DataContract]
    public class Cadastro : BaseModel
    {
        public Cadastro()
        {

        }

        public virtual Order Pedido { get; set; }
        [MinLength(5, ErrorMessage ="nome deve ter no mínimo 5 caracteres")]
        [Required(ErrorMessage = "nome é obrigatório")]
        public string Nome { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string? Telefone { get; set; } = "";
        //[Required]
        public string? Endereco { get; set; } = "";
        public string? Complemento { get; set; } = "";
        //[Required]
        public string? Bairro { get; set; } = "";
        //[Required]
        public string? Municipio { get; set; } = "";
        //[Required]
        public string? UF { get; set; } = "";
       // [Required]
        public string? CEP { get; set; } = "";

        internal void Update(Cadastro novoCadastro)
        {
            this.Nome = novoCadastro.Nome;
            this.Email = novoCadastro.Email;
            this.Telefone = novoCadastro.Telefone;
            this.Endereco = novoCadastro.Endereco;
            this.Complemento = novoCadastro.Complemento;
            this.Bairro = novoCadastro.Bairro;
            this.Municipio = novoCadastro.Municipio;
            this.UF = novoCadastro.UF;
            this.CEP = novoCadastro.CEP;
        }
    }
}