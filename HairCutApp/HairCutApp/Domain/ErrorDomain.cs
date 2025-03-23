namespace HairCutApp.Domain
{
    public class ErrorDomain
    {
        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }

        public ErrorDomain(string? errorCode, string? errorType)
        {
            ErrorCode = errorCode;
            ErrorType = errorType;
        }

        private static readonly Dictionary<string, string> _errorMappings = new()
        {
            { "Erro interno, tente novamente mais tarde", "00001" },
            { "A key não foi localizada em nossa base", "00002" },
            { "Acesso não autorizado", "00003" },
            { "O e-mail já possui cadastro", "00004" },
            { "O nome está vazio", "00005" },
            { "O nome está em um formato inválido", "00006" },
            { "O e-mail está vazio", "00007" },
            { "O e-mail está em um formato inválido", "00008" },
            { "A senha está vazia ou inválida", "00009" },
            { "A Senha precisa conter uma letra maíuscula, um caracter especial e no mínimo cinco caracteres", "00010" },
            { "A key está vazia ou inválida", "00011" },
            { "O usuário não possui login ativo", "00012" },
            { "Usuário desativado", "00013" },
            { "Bloco preenchido não encontrado", "00014" },
            { "A descrição está em um formato inválido", "00015" },
            { "A key de usuário não foi localizada em nossa base", "00016" },
            { "Usuário não localizado em nossa base", "00017" },
            { "Não foi possível realizar alterações", "00018" },
            { "O envio de notificação não foi localizada em nossa base", "00019" },
            { "Perfil de usuário existente", "00020" },
            { "Perfil de usuário inexistente", "00021" },
            { "Campo Nome inválido ou vazio", "00022" },
            { "Perfil desativado", "00023" },
            { "Nome de conta inválido ou vazio", "00024" },
            { "Nome de perfil inválido ou vazio", "00025" },
            { "Código inválido", "00026" },
            { "Código de cor inválido", "00027" },
            { "Nome de conta ou perfil inválido", "00028" },
            { "O link está vazio ou inválido", "00029" },
            { "Dados não preenchidos", "00030" },
            { "Usuário ou senha inválidos", "00031" },
            { "Erro ao realizar o login de usuário", "00032" },
            { "O e-mail não foi localizada em nossa base", "00033" },
            { "Informações de envio estão incompletas", "00034" },
            { "Caminho de arquivo não encontrado", "00035" },
            { "Não foi possível adicionar a imagem no momento", "00036" },
            { "Ocorreu um erro ao tentar obter o usuário do banco de dados", "00037" },
            { "Erro ao tentar atualizar usuário", "00038" },
            { "A ordem não foi localizada em nossa base", "00039" },
            { "Sem itens adicionados ao carrinho", "00040" },
            { "A ordem não pode ser utilizada", "00041" },
            { "O invoice não foi localizado em nossa base", "00042" },
            { "Aguarde ser efetuado o pagamento", "00043" },
            { "A página está vazia ou inválida", "00044" },
            { "O pagamento está vazio ou inválido", "00045" },
            { "O status está vazio ou inválido", "00046" },
            { "A página ou a chave status está vazia ou inválida", "00047" },
            { "Produto não localizado em nossa base", "00048" },
            { "Quantidade não disponível", "00049" },
            { "O telefone está vazio ou inválido", "00050" },
            { "A descrição está muito longa", "00051" },
            { "O data está vazia ou inválida", "00052" }
        };

        public static ErrorDomain GetError(string error)
        {
            var errorCode = _errorMappings.TryGetValue(error, out var code) ? code : "00001";

            if (errorCode == "00001")
                error = "Erro interno, tente novamente mais tarde";

            return new ErrorDomain(errorCode, error);
        }
    }
}
