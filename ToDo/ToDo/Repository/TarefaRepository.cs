using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        AppContext _appcontext;
        public TarefaRepository(AppContext appContext)
        {
            _appcontext = appContext;
        }
        public void Adicionar(Tarefa tarefa)
        {
            _appcontext.Tarefas.Add(tarefa);
            _appcontext.SaveChanges();
        }

        public void Atualizar(Tarefa tarefa)
        {
            _appcontext.Update(tarefa);
            _appcontext.SaveChanges();
        }

        public void Deletar(Tarefa t)
        {
            _appcontext.Remove(t);
            _appcontext.SaveChanges();
        }

        public List<Tarefa> Get()
        {
            return _appcontext.Tarefas.ToList();
        }

        public Tarefa Pegar(int id)
        {
            return _appcontext.Tarefas.FirstOrDefault(t => t.Id == id);
        }
    }
}