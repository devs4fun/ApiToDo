using System.Collections.Generic;
using ToDo.Models;

namespace ToDo.Repository
{
    public interface ITarefaRepository
    {
        void Adicionar(Tarefa tarefa);
        Tarefa Pegar(int id);
        List<Tarefa> Get();
        void Atualizar(Tarefa tarefa);
        void Deletar(Tarefa t);
        
    }
}