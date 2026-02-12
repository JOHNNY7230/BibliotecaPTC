    using BibliotecaPTC.Data;
    using BibliotecaPTC.Models;
    using global::BibliotecaPTC.Data;
    using global::BibliotecaPTC.Models;

    namespace BibliotecaPTC.Service
    {
        public class EmprestarService
        {
            private readonly BibliotecaContext _context;

            public EmprestarService(BibliotecaContext context)
            {
                _context = context;
            }

            public void EmprestarLivro(int usuarioId, int livroId)
            {
                var livro = _context.Livros.Find(livroId);
                if (livro == null)
                    throw new Exception("Livro não encontrado.");
                if (!livro.Disponivel)
                    throw new Exception("Livro indisponível.");

                livro.Disponivel = false;
                _context.Emprestimos.Add(new Emprestimo
                {
                    UsuarioId = usuarioId,
                    LivroId = livroId,
                    DataEmprestimo = DateTime.Now
                });
                _context.SaveChanges();
            }
        }
    }
