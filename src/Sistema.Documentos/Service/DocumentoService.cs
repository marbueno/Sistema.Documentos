using iText.Html2pdf;
using Sistema.Documentos.Interface;
using System;
using System.Collections.Specialized;
using System.IO;

namespace Sistema.Documentos.Service
{
    public class DocumentoService : IDocumento
    {
        #region Variables

        private string _caminhoArquivoHtml { get; set; }
        private string _caminhoArquivoImagens { get; set; }
        private NameValueCollection Parametros { get; set; }

        #endregion Variables

        #region Constructor

        public DocumentoService()
        {
            Parametros = new NameValueCollection();
        }

        #endregion Constructor

        #region Methods

        public string GerarDocumento<TClasse>(string CaminhoArquivoHtml, string CaminhoArquivoImagens, TClasse ClasseParametros)
        {
            _caminhoArquivoHtml = CaminhoArquivoHtml;
            _caminhoArquivoImagens = CaminhoArquivoImagens;
            Utils.Helper.ObterParametros<TClasse>(Parametros, ClasseParametros);

            return Gerar();
        }

        public string GerarDocumento<TClasse>(string CaminhoArquivoHtml, string CaminhoArquivoImagens, string JSonParametros)
        {
            _caminhoArquivoHtml = CaminhoArquivoHtml;
            _caminhoArquivoImagens = CaminhoArquivoImagens;
            Utils.Helper.ObterParametros<TClasse>(Parametros, JSonParametros);

            return Gerar();
        }

        public string GerarDocumento(string CaminhoArquivoHtml, string CaminhoArquivoImagens, NameValueCollection ColecaoParametros)
        {
            _caminhoArquivoHtml = CaminhoArquivoHtml;
            _caminhoArquivoImagens = CaminhoArquivoImagens;
            Parametros = ColecaoParametros;

            return Gerar();
        }

        private string Gerar()
        {
            MemoryStream pdfStream = new MemoryStream();
            byte[] bytes;

            try
            {
                if (string.IsNullOrEmpty(_caminhoArquivoHtml)) throw new ArgumentException("Caminho do Arquivo HTML não informado.");
                if (string.IsNullOrEmpty(_caminhoArquivoImagens)) throw new ArgumentException("Caminho Base do Documento não informado.");

                StreamReader sr = new StreamReader(_caminhoArquivoHtml);

                string htmlReplaced = Utils.Helper.ReplaceParameters(sr.ReadToEnd(), "${", "}", Parametros);

                ConverterProperties converterProperties = new ConverterProperties();
                converterProperties.SetBaseUri(_caminhoArquivoImagens);
                HtmlConverter.ConvertToPdf(htmlReplaced, pdfStream, converterProperties);

                if (pdfStream == null)
                    throw new ArgumentException("Documento não gerado.");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Erro ao gerar documento: [{ex.Message}]");
            }

            return Convert.ToBase64String(pdfStream.ToArray());
        }

        #endregion Methods
    }
}
