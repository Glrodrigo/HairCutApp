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
            //{ "Registro de pessoas contatadas não enviado", "00015" },
            //{ "Registro de pessoas contatadas incorreto", "00016" },
            //{ "Território preenchido não encontrado", "00017" },
            //{ "O bloco já possui cadastro", "00018" },
            //{ "A key de território está vazia ou inválida", "00019" },
            //{ "O bloco está sem a quadra preenchida corretamente", "00021" },
            //{ "Os números não estão preenchidos corretamente", "00022" },
            //{ "O território está sem as quadras preenchidas corretamente", "00023" },
            //{ "O território já possui cadastro", "00024" },
            //{ "O território não possui cadastro", "00025" },
            //{ "Território desativado", "00026" },
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
