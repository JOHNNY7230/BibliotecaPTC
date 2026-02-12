using BibliotecaPTC.Data;
using BibliotecaPTC.Models;
using Microsoft.EntityFrameworkCore;

// Aplica migrações automaticamente (opcional)
using (var initialContext = new BibliotecaContext())
{
    initialContext.Database.Migrate();
}

while (true)
{
    Console.WriteLine("\n=== SISTEMA DE BIBLIOTECA ===");
    Console.WriteLine("1 - Cadastrar Livro");
    Console.WriteLine("2 - Listar Livros");
    Console.WriteLine("3 - Cadastrar Usuário");
    Console.WriteLine("4 - Listar Usuários");
    Console.WriteLine("5 - Emprestar Livro");
    Console.WriteLine("6 - Devolver Livro");
    Console.WriteLine("0 - Sair");

    Console.Write("Escolha: ");
    string opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1": CadastrarLivro(); break;
            case "2": ListarLivros(); break;
            case "3": CadastrarUsuario(); break;
            case "4": ListarUsuarios(); break;
            case "5": EmprestarLivro(); break;
            case "6": DevolverLivro(); break;
            case "0": return;
            default: Console.WriteLine("Opção inválida!"); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro: {ex.Message}");
    }
}

void CadastrarLivro()
{
    using var context = new BibliotecaContext();

    Console.Write("\nTítulo: ");
    string titulo = Console.ReadLine();

    Console.Write("Autor: ");
    string autor = Console.ReadLine();

    var livro = new Livro { Titulo = titulo, Autor = autor, Disponivel = true };
    context.Livros.Add(livro);
    context.SaveChanges();

    Console.WriteLine("✅ Livro cadastrado!");
}

void ListarLivros()
{
    using var context = new BibliotecaContext();

    var livros = context.Livros.ToList();
    Console.WriteLine("\n--- Lista de Livros ---");

    foreach (var livro in livros)
        Console.WriteLine($"ID: {livro.Id} | {livro.Titulo} - {livro.Autor} | {(livro.Disponivel ? "Disponível" : "Emprestado")}");
}

void CadastrarUsuario()
{
    using var context = new BibliotecaContext();

    Console.Write("\nNome: ");
    string nome = Console.ReadLine();
    Console.Write("Email: ");
    string email = Console.ReadLine();

    var usuario = new Usuario { Nome = nome, Email = email };
    context.Usuarios.Add(usuario);
    context.SaveChanges();

    Console.WriteLine("✅ Usuário cadastrado!");
}

void ListarUsuarios()
{
    using var context = new BibliotecaContext();

    var usuarios = context.Usuarios.ToList();
    Console.WriteLine("\n--- Lista de Usuários ---");

    foreach (var usuario in usuarios)
        Console.WriteLine($"ID: {usuario.Id} | {usuario.Nome} | {usuario.Email}");
}

void EmprestarLivro()
{
    using var context = new BibliotecaContext();

    Console.WriteLine("\n--- Empréstimo de Livro ---");

    ListarUsuarios();
    Console.Write("ID do usuário: ");
    if (!int.TryParse(Console.ReadLine(), out int usuarioId))
        return;

    ListarLivros();
    Console.Write("ID do livro: ");
    if (!int.TryParse(Console.ReadLine(), out int livroId))
        return;

    var livro = context.Livros.Find(livroId);
    if (livro == null)
    {
        Console.WriteLine("Livro não encontrado!");
        return;
    }
    if (!livro.Disponivel)
    {
        Console.WriteLine("⚠️ Livro já emprestado!");
        return;
    }

    var emprestimo = new Emprestimo
    {
        UsuarioId = usuarioId,
        LivroId = livroId,
        DataEmprestimo = DateTime.Now
    };

    livro.Disponivel = false;
    context.Emprestimos.Add(emprestimo);
    context.SaveChanges();

    Console.WriteLine("✅ Empréstimo realizado!");
}

void DevolverLivro()
{
    using var context = new BibliotecaContext();

    Console.WriteLine("\n--- Devolução de Livro ---");

    var emprestimosAbertos = context.Emprestimos
        .Include(e => e.Livro)
        .Include(e => e.Usuario)
        .Where(e => e.DataDevolucao == null)
        .ToList();

    if (emprestimosAbertos.Count == 0)
    {
        Console.WriteLine("⚠️ Nenhum empréstimo ativo!");
        return;
    }

    foreach (var e in emprestimosAbertos)
        Console.WriteLine($"ID: {e.Id} | Livro: {e.Livro?.Titulo} | Usuário: {e.Usuario?.Nome} | Desde: {e.DataEmprestimo:d}");

    Console.Write("Digite o ID do empréstimo a devolver: ");
    if (!int.TryParse(Console.ReadLine(), out int emprestimoId))
        return;

    var emprestimo = context.Emprestimos
        .Include(e => e.Livro)
        .FirstOrDefault(e => e.Id == emprestimoId);

    if (emprestimo == null)
    {
        Console.WriteLine("Empréstimo não encontrado!");
        return;
    }

    emprestimo.DataDevolucao = DateTime.Now;
    emprestimo.Livro.Disponivel = true;

    context.SaveChanges();
    Console.WriteLine("✅ Livro devolvido com sucesso!");
}
