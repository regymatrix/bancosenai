using System;
using System.IO;

Console.WriteLine("--- SISTEMA BANCÁRIO - VALIDAÇÃO INICIAL (V3) ---");
string localDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string caminhoArquivo = Path.Combine(localDesktop, "Token", "TokenClientes.txt");
bool continuar = true;
int qtdTentativa = 0;
string[] logTentativas = new string[3];

if (File.Exists(caminhoArquivo))
{
    while (continuar && qtdTentativa <= 2)
    {
        Console.Write("Digite o número da conta: ");
        string conta = Console.ReadLine();
        Console.Write("Digite a agência: ");
        string agencia = Console.ReadLine();
        Console.Write("Digite o seu CPF (somente números): ");
        string cpfDigitado = Console.ReadLine();

        string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        logTentativas[qtdTentativa] = $"Tentativa: {qtdTentativa + 1} | CPF: {cpfDigitado} | Data/Hora: {dataHora}";

        string[] linhasArquivo = File.ReadAllLines(caminhoArquivo);
        bool cpfEncontrado = false;
        bool tokenValido = false;
        bool tokenExpirado = false;
        string tokenEsperado = $"bs_{cpfDigitado}";

        foreach (string linha in linhasArquivo)
        {
            string[] dados = line: linha.Split(';');
            if (dados.Length > 0 && dados[0] == cpfDigitado)
            {
                cpfEncontrado = true;

                if (dados.Length > 1 && dados[1].Contains(tokenEsperado))
                {
                    tokenValido = true;

                    if (dados.Length > 2 && DateTime.TryParse(dados[2], out DateTime dataToken))
                    {
                        if (DateTime.Now > dataToken)
                        {
                            tokenExpirado = true;
                        }
                    }
                }
                break;
            }
        }

        if (cpfEncontrado && tokenValido && !tokenExpirado)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SUCESSO] Credenciais e Token confirmados com sucesso.");
            Console.ResetColor();
            continuar = false;
            break;
        }
        else if (cpfEncontrado && tokenValido && tokenExpirado)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ERRO] Credenciais inválidas! Conta Bloqueada.");
            Console.ResetColor();
            continuar = false;
            qtdTentativa = 3;
            break;
        }
        else if (cpfEncontrado && !tokenValido)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ERRO] Token inválido! Conta Bloqueada.");
            Console.ResetColor();
            continuar = false;
            qtdTentativa = 3;
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

    Console.WriteLine("\n--- LOG DE TENTATIVAS (EXIBIÇÃO VIA VETOR) ---");
    for (int i = 0; i < logTentativas.Length; i++)
    {
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