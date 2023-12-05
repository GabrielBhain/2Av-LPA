using System;

class Program
{
    static int[] pedidos = new int[10];
    static int inicioFila = 0;
    static int fimFila = -1;
    static int quantidadePedidos = 0;

    const string MSG_PEDIDO_INCLUIDO = "Pedido incluído com sucesso!";
    const string MSG_NUMERO_PEDIDO_INVALIDO = "Número do pedido inválido.";
    const string MSG_FILA_CHEIA = "Fila Cheia – Não Pode Mais Incluir Pedidos";
    const string MSG_LISTA_VAZIA = "Lista Vazia – Não Existem Pedidos";
    const string MSG_PEDIDO_ATENDIDO = "Pedido {0} atendido.";
    const string MSG_PEDIDOS_PENDENTES = "Pedidos pendentes:";
    const string MSG_PEDIDO_ENCONTRADO = "Pedido {0} encontrado na posição {1}.";
    const string MSG_PEDIDO_NAO_ENCONTRADO = "Pedido {0} não encontrado.";
    const string MSG_PROGRAMA_ENCERRADO = "Programa encerrado.";

    static void Main()
    {
        int opcao;

        do
        {
            ExibirMenu();
            opcao = ObterOpcao();

            switch (opcao)
            {
                case 1:
                    IncluirPedido();
                    break;
                case 2:
                    AtenderPedido();
                    break;
                case 3:
                    ListarPedidos();
                    break;
                case 4:
                    PesquisarPedido();
                    break;
                case 5:
                    Encerrar();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

        } while (opcao != 5 || quantidadePedidos > 0);
    }

    static void ExibirMenu()
    {
        Console.WriteLine("###### LANCHONETE #######");
        Console.WriteLine("1 – INCLUIR PEDIDO");
        Console.WriteLine("2 - ATENDER PEDIDO");
        Console.WriteLine("3 - LISTAR PEDIDOS");
        Console.WriteLine("4 – PESQUISAR PEDIDO");
        Console.WriteLine("5 – ENCERRAR");
        Console.WriteLine("#########################");
        Console.Write("Escolha a opção: ");
    }

    static int ObterOpcao()
    {
        int opcao;
        while (!int.TryParse(Console.ReadLine(), out opcao))
        {
            Console.WriteLine("Opção inválida. Digite um número válido.");
            Console.Write("Escolha a opção: ");
        }
        return opcao;
    }

    static void IncluirPedido()
    {
        if (quantidadePedidos < pedidos.Length)
        {
            Console.Write("Digite o número do pedido: ");
            string? inputNumeroPedido = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputNumeroPedido) && int.TryParse(inputNumeroPedido, out int numeroPedido))
            {
                fimFila = (fimFila + 1) % pedidos.Length;
                pedidos[fimFila] = numeroPedido;
                quantidadePedidos++;

                Console.WriteLine(MSG_PEDIDO_INCLUIDO);
            }
            else
            {
                Console.WriteLine(MSG_NUMERO_PEDIDO_INVALIDO);
            }
        }
        else
        {
            Console.WriteLine(MSG_FILA_CHEIA);
        }
    }

    static void AtenderPedido()
    {
        if (quantidadePedidos > 0)
        {
            Console.WriteLine(MSG_PEDIDO_ATENDIDO, pedidos[inicioFila]);
            inicioFila = (inicioFila + 1) % pedidos.Length;
            quantidadePedidos--;
        }
        else
        {
            Console.WriteLine(MSG_LISTA_VAZIA);
        }
    }

    static void ListarPedidos()
    {
        if (quantidadePedidos > 0)
        {
            Console.WriteLine(MSG_PEDIDOS_PENDENTES);

            int i = inicioFila;
            int contador = 0;

            do
            {
                Console.WriteLine($"Pedido {pedidos[i]}");
                i = (i + 1) % pedidos.Length;
                contador++;
            } while (contador < quantidadePedidos);
        }
        else
        {
            Console.WriteLine(MSG_LISTA_VAZIA);
        }
    }

    static void PesquisarPedido()
    {
        if (quantidadePedidos > 0)
        {
            Console.Write("Digite o número do pedido a ser pesquisado: ");
            string? inputNumeroPesquisa = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputNumeroPesquisa) && int.TryParse(inputNumeroPesquisa, out int numeroPesquisa))
            {
                int i = inicioFila;
                int contador = 0;

                do
                {
                    if (pedidos[i] == numeroPesquisa)
                    {
                        Console.WriteLine(MSG_PEDIDO_ENCONTRADO, numeroPesquisa, i + 1);
                        return;
                    }

                    i = (i + 1) % pedidos.Length;
                    contador++;
                } while (contador < quantidadePedidos);

                Console.WriteLine(MSG_PEDIDO_NAO_ENCONTRADO, numeroPesquisa);
            }
            else
            {
                Console.WriteLine(MSG_NUMERO_PEDIDO_INVALIDO);
            }
        }
        else
        {
            Console.WriteLine(MSG_LISTA_VAZIA);
        }
    }

    static void Encerrar()
    {
        if (quantidadePedidos == 0)
        {
            Console.WriteLine(MSG_PROGRAMA_ENCERRADO);
        }
        else
        {
            Console.WriteLine("Ainda existem pedidos pendentes. Não é possível encerrar.");
        }
    }
}
