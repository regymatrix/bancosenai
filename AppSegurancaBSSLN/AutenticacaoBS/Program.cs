using System;
using System.IO;

Console.WriteLine("--- SISTEMA BANCÁRIO - VALIDAÇÃO INICIAL (V1) ---");
string localDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string caminhoArquivo = Path.Combine(localDesktop, "Token", "TokenClientes.txt");
bool continuar = true;
int qtdTentativa = 0;

// R7D - Criação do vetor para armazenar até 3 logs na memória
string[] logTentativas = new string[3];

if (File.Exists(caminhoArquivo))
{
    // Corrigido para && (operador lógico correto)
    while (continuar && qtdTentativa <= 2)
    {
        Console.Write("Digite o número da conta: ");
        string conta = Console.ReadLine();
        Console.Write("Digite a agência: ");
        string agencia = Console.ReadLine();
        Console.Write("Digite o seu CPF (somente números): ");
        string cpfDigitado = Console.ReadLine();

        string conteudoArquivo = File.ReadAllText(caminhoArquivo);
        string buscaExata = cpfDigitado + ";";

        // R7D - Salva a tentativa atual no vetor ANTES de incrementar a quantidade
        string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        logTentativas[qtdTentativa] = $"Tentativa: {qtdTentativa + 1} | CPF: {cpfDigitado} | Data/Hora: {dataHora}";

        if (conteudoArquivo.Contains(buscaExata))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SUCESSO] Credenciais confirmadas com sucesso.");
            Console.ResetColor();
            continuar = false;
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ERRO] Credenciais inválidas!");
            Console.ResetColor();
            qtdTentativa++;
        }
    }

    if (qtdTentativa > 2)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n[ERRO] Tentou várias vezes! Conta Bloqueada.");
        Console.ResetColor();
    }

    // R7D - Exibição do Log guardado no Vetor ao finalizar a aplicação
    Console.WriteLine("\n--- LOG DE TENTATIVAS (EXIBIÇÃO VIA VETOR) ---");
    for (int i = 0; i < logTentativas.Length; i++)
    {
        // Só exibe a posição do vetor se ela não estiver vazia
        if (!string.IsNullOrEmpty(logTentativas[i]))
        {
            Console.WriteLine(logTentativas[i]);
        }
    }
}
else
{
    Console.WriteLine($"\n[ERRO] O arquivo 'TokenClientes.txt' não foi encontrado em: {caminhoArquivo}");
}
Console.WriteLine("\n------------------------------------------------");