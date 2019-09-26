using System.Collections.Specialized;

namespace Sistema.Documentos.Interface
{
    public interface IDocumento
    {
        string GerarDocumento<TClasse>(string CaminhoArquivoHtml, string CaminhoArquivoImagens, TClasse ClasseParametros);
        string GerarDocumento<TClasse>(string CaminhoArquivoHtml, string CaminhoArquivoImagens, string JSonParametros);
        string GerarDocumento(string CaminhoArquivoHtml, string CaminhoArquivoImagens, NameValueCollection ColecaoParametros);
    }
}
