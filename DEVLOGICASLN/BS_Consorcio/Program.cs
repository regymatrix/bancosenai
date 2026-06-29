using System;
Console.WriteLine("--- BANCO SENAI: SISTEMA DE CONSÓRCIO E CONTEMPLAÇÃO ---");
Console.Write("Nome do Cliente: ");
string nome = Console.ReadLine();
Console.Write("Valor da Carta de Consórcio (R$): ");
decimal valorCarta = Convert.ToDecimal(Console.ReadLine());
Console.Write("Taxa Administrativa Mensal (%): ");
decimal taxaJuros = Convert.ToDecimal(Console.ReadLine()) / 100;
Console.Write("Quantidade de Parcelas: ");
int qtdParcelas = int.Parse(Console.ReadLine());
Console.Write("Data da 1ª Parcela (DD/MM/YYYY): ");
DateTime dataInicial = DateTime.Parse(Console.ReadLine());
decimal jurosTotais = valorCarta * taxaJuros * qtdParcelas;
decimal montanteTotal = valorCarta + jurosTotais;
decimal valorParcela = montanteTotal / qtdParcelas;
Console.WriteLine($"\nRESUMO DO CONSÓRCIO PARA: {nome.ToUpper()}");
Console.WriteLine($"VALOR TOTAL A PAGAR: R$ {montanteTotal:F2}");
Console.WriteLine("--------------------------------------------");

for (int i = 0; i < qtdParcelas; i++)
{
    DateTime dataParcela = dataInicial.AddMonths(i);
    Console.WriteLine($"Parcela {i + 1:00} | Vencimento: {dataParcela:dd/MM/yyyy} | Valor: R$ {valorParcela:F2}");
}
Console.WriteLine("\n--------------------------------------------");
Console.Write("Informe o valor do seu LANCE (R$): ");
decimal valorLance = Convert.ToDecimal(Console.ReadLine());
decimal metaContemplacao = valorCarta * 0.50m;

if (valorLance > metaContemplacao)
{
    Console.WriteLine("\nPARABÉNS! Seu lance foi superior a 50% do valor da carta.");
    Console.WriteLine("STATUS: CLIENTE CONTEMPLADO!");
}
else
{
    Console.WriteLine("\nLANCE REGISTRADO. O valor não atingiu a meta de 50%.");
    Console.WriteLine("STATUS: AGUARDANDO PRÓXIMO SORTEIO.");
}

Console.WriteLine("--- Simulação de Consórcio Finalizada ---");