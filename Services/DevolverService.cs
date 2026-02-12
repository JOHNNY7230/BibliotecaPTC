using BibliotecaPTC.Data;

namespace BibliotecaPTC.Service
{
    public class DevolverService
    {
        private readonly BibliotecaContext _context;

        public DevolverService(BibliotecaContext context)
        {
            _context = context;
        }

        public void DevolverLivro(int emprestimoId)
        {
            var emprestimo = _context.Emprestimos.Find(emprestimoId);
            if (emprestimo == null)
                throw new Exception("Empréstimo não encontrado.");

            var livro = _context.Livros.Find(emprestimo.LivroId);
            if (livro != null)
                livro.Disponivel = true;

            emprestimo.DataDevolucao = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
