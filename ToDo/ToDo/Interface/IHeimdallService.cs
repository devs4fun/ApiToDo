using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Interface
{
    interface IHeimdallService
    {
        [Get("/api/usuario/ValidarChave")]
        Task<Usuario> GetUsuario(string chave);
    }
}
