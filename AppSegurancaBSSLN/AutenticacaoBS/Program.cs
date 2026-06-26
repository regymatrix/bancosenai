using System;
using System.IO;
Console.WriteLine("--- SISTEMA BANCÁRIO - VALIDAÇÃO INICIAL (V1) ---");
string localDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string caminhoArquivo = Path.Combine(localDesktop, "Token", "TokenClientes.txt");
bool continuar = true;
int qtdTentativa = 0;

if (File.Exists(caminhoArquivo))
{
    while (continuar & qtdTentativa<=2)    {
        Console.Write("Digite o número da conta: ");
        string conta = Console.ReadLine();
        Console.Write("Digite a agência: ");
        string agencia = Console.ReadLine();
        Console.Write("Digite o seu CPF (somente números): ");
        string cpfDigitado = Console.ReadLine();
        string conteudoArquivo = File.ReadAllText(caminhoArquivo);
        string buscaExata = cpfDigitado + ";";
        if (conteudoArquivo.Contains(buscaExata))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SUCESSO] Credenciais confirmadas com sucesso.");
            Console.ResetColor();
            continuar = false; //sair da aplicação
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
        Console.WriteLine("\n[ERRO] Tentou várias vezes!");
        Console.ResetColor();
    }
}
else
{
    Console.WriteLine($"\n[ERRO] O arquivo 'TokenClientes.txt' não foi encontrado em: {caminhoArquivo}");
}
Console.WriteLine("\n------------------------------------------------");