namespace fastfood_production.Domain.Contants
{
    public static class ErrorMessages
    {
        public static Dictionary<string, string> ErrorMessageList => _errorMessages;

        private static readonly Dictionary<string, string> _errorMessages = new()
        {
            { "PBE001", "Request inválido ou fora de especificação, vide documentação." },
            { "PBE002", "Pedido já se encontra em processo de produção." },
            { "PBE003", "Falha em atualização de status do pedido." },
            { "PBE004", "Pedido não se encontra em produção." },
            { "PBE005", "Novo status de produção inválido." },
            { "PBE006", "O id do pedido precisa estar preenchido." },
            { "PBE007", "O pedido precisa ter pelo menos um produto para ser produzido." },
            { "PBE008", "O nome do produto precisa estar preenchido." },
            { "PBE009", "A quantidade do produto precisa ser maior que zero." },
            { "PBE010", "O status de produção é inválido." },
            { "PIE999", "Internal server error" }
        };
    }
}
