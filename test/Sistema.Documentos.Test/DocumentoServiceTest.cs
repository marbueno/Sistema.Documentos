using Xunit;
using Sistema.Documentos.Interface;
using Sistema.Documentos.Service;
using Newtonsoft.Json;
using System.IO;
using System;

namespace Sistema.Documentos.Test
{
    public class DocumentoResumo
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string EstadoCivil { get; set; }
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string PoliticamenteExposta { get; set; }
        public string RendaMensalAuferida { get; set; }
        public string CodigoBeneficioINSS { get; set; }
        public string CodigoBeneficioINSSDV { get; set; }
        public string ClassBeneficiarios { get; set; }
        public string NomeProduto { get; set; }
        public string Valor { get; set; }
        public string Cobertura { get; set; }
        public string FranquiaDias { get; set; }
        public string PrincipalRS { get; set; }
        public string ConjugeRS { get; set; }
        public string FilhosRS { get; set; }
        public string Assistencias1 { get; set; }
        public string Assistencias2 { get; set; }
        public string Pagamento { get; set; }
    }

    public class DocumentoServiceTest
    {
        private readonly IDocumento _certificado;
        private readonly DocumentoResumo docResumo;

        public DocumentoServiceTest()
        {
            _certificado = new DocumentoService();

            string
                beneficiarios = "<tr><td><b>Beneficiários 1:</b>Juliana Bueno, <b>Parentesco:</b> Cônjuge , <b>Porcentagem: </b> 50%</td></tr>";
                beneficiarios += "<tr><td><b>Beneficiários 2:</b>Manuela Bueno, <b>Parentesco:</b> Filha , <b>Porcentagem: </b> 50%</td></tr>";

            string
                pagamentos = "<tr><td colspan='2'><b>TipoPagamento:</b><span >Cartão de Crédito</span></td></tr>";
                pagamentos += "<tr><td><b>Bandeira do Cartão:</b>VISA</td><td></td></tr>";
                pagamentos += "<tr><td><b>Número do Cartão:</b> XXXX9999999XXXX</td> <td><b>Validade:</b> 12/2019 </td></tr>";

            docResumo = new DocumentoResumo()
            {
                Cpf = "999.999.888-77",
                Nome = "Marcelo Moreira Bueno",
                Telefone1 = "(11) 9.9906-4656",
                Telefone2 = "(11) 2387-1656",
                EstadoCivil = "Casado",
                DataNascimento = "11/05/1984",
                Sexo = "Masculino",
                PoliticamenteExposta = "Não",
                RendaMensalAuferida = "R$ 1.000,00",
                CodigoBeneficioINSS = "0001",
                CodigoBeneficioINSSDV = "002200",
                ClassBeneficiarios = beneficiarios,
                NomeProduto = "Vida",
                Valor = "R$ 5.000,00",
                Cobertura = "Morte Acidental",
                FranquiaDias = "365",
                PrincipalRS = "R$ 10.000,00",
                ConjugeRS = "R$ 5.000,00",
                FilhosRS = "R$ 3.000,00",
                Assistencias1 = "Assistência Teste 1",
                Assistencias2 = "Assistência Teste 2",
                Pagamento = pagamentos
            };
        }

        [Fact]
        public void Deve_Criar_Documento_por_Classe()
        {
            var base64 = _certificado.GerarDocumento<DocumentoResumo>(@"Templates/certificadoResumoTemplate.html", @"/Templates", docResumo);

            Assert.False(string.IsNullOrEmpty(base64));
        }

        [Fact]
        public void Deve_Criar_Documento_por_Json()
        {
            string json = JsonConvert.SerializeObject(docResumo);

            var base64 = _certificado.GerarDocumento<DocumentoResumo>(@"Templates/certificadoResumoTemplate.html", @"/Templates", json);

            Assert.False(string.IsNullOrEmpty(base64));
        }

        [Fact]
        public void Deve_Criar_Documento_Fisicamente()
        {
            var base64 = _certificado.GerarDocumento<DocumentoResumo>(@"Templates/certificadoResumoTemplate.html", @"/Templates", docResumo);

            File.WriteAllBytes(@"certificadoResumoTemplate.pdf", Convert.FromBase64String(base64));

            Assert.False(string.IsNullOrEmpty(base64));
        }

        //[Fact]
        //public void Deve_Criar_Com_Cabecalho_e_Rodape()
        //{
        //    var individualcotacaoEmail = new IndividualCotacaoEmail()
        //    {
        //        SeguradoNome = "Marcelo Moreira Bueno",
        //        DataInclusao = DateTime.Now.ToString("dd/MM/yyyy"),
        //        HorarioInclusao = DateTime.Now.ToString("HH:mm:sss"),
        //        CorretoraNome = "THE FAMILY CORR DE SEGUROS LTDA",
        //        CorretoraNomeFuncionario = string.Empty,
        //        CorretoraTelefone = "11 3287-2222",
        //        CorretoraEmail = "rose@thefamily.com.br",
        //        ProdutoNome = "MQC",

        //        SeguradoDataNascimento = "11/05/1984",
        //        SeguradoCPF = "325.187.998-77",
        //        SeguradoSexo = "MASCULINO",
        //        SeguradoEstadoCivil = "CASADO",
        //        SeguradoProfissao = "DESENVOLVEDOR DE SISTEMAS",
        //        SeguradoRenda = "R$ 1.000,00",
        //        SeguradoPPE = "NÃO",
        //        SeguradoTelCelular = "11 9.9999-9999",
        //        SeguradoTelResidencial = "",
        //        SeguradoEmail = "marcelo.bueno@previsul.com.br",
        //        SeguradoEndereco = "Av Engenheiro Luis Carlos Berrini",
        //        SeguradoEnderecoNumero = "105",
        //        SeguradoEnderecoComplemento = "15° Andar",
        //        SeguradoEnderecoCEP = "04571-010",
        //        SeguradoEnderecoBairro = "Itaim Bibi",
        //        SeguradoEnderecoCidade = "São Paulo",
        //        SeguradoEnderecoEstado = "SP",
        //        Planos = MontarPlanos(),
        //        ValidadeCotacao = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy"),
        //        NumeroProposta = "1001300819000001171",
        //        DataInclusaoPorExtenso = "02 de Setembro de 2019"
        //    };


        //    var base64 = _certificado.GerarDocumento<IndividualCotacaoEmail>(@"D:\Marcelo\Projetos\Previsul\Previsul.Cotador\src\Previsul.Cotador.Api\wwwroot\Template\Individual\CotacaoEmkt\pdf.html", @"D:\Marcelo\Projetos\Previsul\Previsul.Cotador\src\Previsul.Cotador.Api\wwwroot\Template\Individual\CotacaoEmkt", individualcotacaoEmail);

        //    File.WriteAllBytes(@"emailIndividual.pdf", Convert.FromBase64String(base64));

        //    Assert.False(string.IsNullOrEmpty(base64));
        //}
    }
}
