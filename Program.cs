
using BibliotecaPTC.Models;

List<Livro> livros = new List<Livro>();
List<Usuario> usuarios = new List<Usuario>();
List<Emprestimo> emprestimos = new List<Emprestimo>();

while (true)
{
    Console.WriteLine("\n=== SISTEMA DE BIBLIOTECA ===");
    Console.WriteLine("1 - Cadastrar Livro");
    Console.WriteLine("2 - Listar Livros");
    Console.WriteLine("3 - Cadastrar Usuário");
    Console.WriteLine("4 - Listar Usuários");
    Console.WriteLine("0 - Sair");

    Console.Write("Escolha: ");
    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarLivro();
            break;
        case "2":
            ListarLivros();
            break;
        case "3":
            CadastrarUsuario();
            break;
        case "4":
            ListarUsuarios();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}

void CadastrarLivro()
{
    Console.Write("\nTítulo: ");
    string titulo = Console.ReadLine();

    Console.Write("Autor: ");
    string autor = Console.ReadLine();

    Livro novo = new Livro { Id = livros.Count + 1, Titulo = titulo, Autor = autor };
    livros.Add(novo);

    Console.WriteLine("Livro cadastrado com sucesso!");
}

void ListarLivros()
{
    Console.WriteLine("\n--- Lista de Livros ---");
    foreach (var livro in livros)
    {
        Console.WriteLine($"ID: {livro.Id} | {livro.Titulo} - {livro.Autor} | {(livro.Disponivel ? "Disponível" : "Emprestado")}");
    }
}
void CadastrarUsuario()
{
    Console.Write("\nNome do usuário: ");
    string nome = Console.ReadLine();

    Console.Write("Email: ");
    string email = Console.ReadLine();

    Usuario novo = new Usuario
    {
        Id = usuarios.Count + 1,
        Nome = nome,
        Email = email
    };

    usuarios.Add(novo);

    Console.WriteLine("Usuário cadastrado com sucesso!");
}
void ListarUsuarios()
{
    Console.WriteLine("\n--- Lista de Usuários ---");
    foreach (var usuario in usuarios)
    {
        Console.WriteLine($"ID: {usuario.Id} | Nome: {usuario.Nome} | Email: {usuario.Email}");
    }
}
