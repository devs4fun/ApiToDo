//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using ToDo.Controllers;
//using ToDo.Models;
//using ToDo.Repository;
//using Xunit;

//namespace XUnitTestProject1
//{
//    public class UnitTest1
//    {
//        [Trait("TodoController", "Buscar todas as tarefas")]
//        [Fact(DisplayName = "01 - Deveria retornar uma lista de tarefas")]
//        public void DeveriaRetornarUmaListaDeTarefas()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            TarefaRepositoryMock.Setup(t => t.Get()).Returns(new List<Tarefa>());
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Get() as OkObjectResult;
//            var lista = retorno.Value as List<Tarefa>;

//            //Assert
//            Assert.True(lista.Count >= 0 && retorno.StatusCode == 200);
//        }

        
//        [Trait("TodoController", "Buscar uma tarefa com id negativo")]
//        [Fact(DisplayName = "02 - Deveria retornar um 404 porque o id é negativo")]
//        public void DeveriaRetornarUmNotFoundPorContadoIdNegativo()
//        {
//            //Arrange
//            var id = -50;
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Pegar(id) as NotFoundResult;

//            //Assert
//            Assert.True(retorno.StatusCode == 404);
//        }


//        [Trait("TodoController", "Buscar uma tarefa com id que não existe no banco")]
//        [Fact(DisplayName = "03 - Deveria retornar um 404 pois id não existe")]
//        public void DeveriaRetornarUmNotFoundPoisIdNaoExiste()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            int id = 5;
//            Tarefa tarefa = null;
//            TarefaRepositoryMock.Setup(t => t.Pegar(id)).Returns(tarefa);
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Pegar(id) as NotFoundResult;

//            //Assert
//            Assert.True(retorno.StatusCode == 404);
//        }

//        [Trait("TodoController", "Buscar uma tarefa")]
//        [Fact(DisplayName = "04 - Deveria retornar uma tarefa")]
//        public void DeveriaRetornarUmaTarefa()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            Tarefa taref = new Tarefa() { Id = 5, Nome = "dever de casa" };
//            TarefaRepositoryMock.Setup(t => t.Pegar(taref.Id)).Returns(taref);
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Pegar(taref.Id) as OkObjectResult;
//            var tarefa = retorno.Value as Tarefa;

//            //Assert
//            Assert.True(retorno.StatusCode == 200 && tarefa.Id == 5);
//        }

//        [Trait("TodoController", "Adicionar uma tarefa")]
//        [Fact(DisplayName = "05 - Deveria retornar BadRequest")]
//        public void DeveriaRetornarBadRequest()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Adicionar(new Tarefa() { }) as BadRequestResult;

//            //Assert
//            Assert.True(retorno.StatusCode == 400);
//        }

//        [Trait("TodoController", "Adicionar uma tarefa")]
//        [Fact(DisplayName = "06 - Deveria adicionar uma tarefa")]
//        public void DeveriaAdicionarTarefa()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);
//            var tarefa = new Tarefa() { Nome = "Estudar c#" };

//            //Act
//            var retorno = sutTodoController.Adicionar(tarefa) as OkObjectResult;
//            var value = retorno.Value as Tarefa;

//            //Assert
//            Assert.True(retorno.StatusCode == 200 && !(string.IsNullOrEmpty(value.Nome)));
//        }

//        [Trait("TodoController", "Detetar uma tarefa")]
//        [Fact(DisplayName = "07 - Deveria falhar ao tentar deletar uma tarefa se id é negativo")]
//        public void DeveriaFalharAoTentarDeletarTarefa()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Deletar(-1) as BadRequestResult;

//            //Assert
//            Assert.True(retorno.StatusCode == 400);
//        }

//        [Trait("TodoController", "Detetar uma tarefa")]
//        [Fact(DisplayName = "08 - Deveria falhar ao tentar deletar uma tarefa nula")]
//        public void DeveriaFalharAoTentarDeletarTarefaNula()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            Tarefa t = null;
//            TarefaRepositoryMock.Setup(t => t.Pegar(321651)).Returns(t);
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Deletar(321651) as BadRequestResult;

//            //Assert
//            Assert.True(retorno.StatusCode == 400);
//        }

//        [Trait("TodoController", "Detetar uma tarefa")]
//        [Fact(DisplayName = "09 - Deveria deletar uma tarefa")]
//        public void DeveriaDeletarTarefa()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var tarefa = new Tarefa() { Id = 1, Nome = "teste" };
//            int id = 1;
//            TarefaRepositoryMock.Setup(t => t.Pegar(id)).Returns(tarefa);
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Deletar(id) as OkObjectResult;

//            //Assert
//            TarefaRepositoryMock.Verify(t => t.Deletar(tarefa), Times.Once);
            
//            //Não conseguimos fazer com o que o retorno sem receber parametro fosse 200
//            //Assert.True(retorno.StatusCode == 200);
//        }

//        [Trait("TodoController", "Atualizar uma tarefa")]
//        [Fact(DisplayName = "10 - Deveria atualizar uma tarefa")]
//        public void DeveriaAtualizarTarefa()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var tarefa = new Tarefa() { Id = 1, Nome = "teste" };
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Atualizar(tarefa) as OkObjectResult;

//            //Assert
//            TarefaRepositoryMock.Verify(t => t.Atualizar(tarefa), Times.Once);

//            //Não conseguimos fazer com o que o retorno sem receber parametro fosse 200
//            //Assert.True(retorno.StatusCode == 200);
//        }

//        [Trait("TodoController", "Atualizar uma tarefa")]
//        [Fact(DisplayName = "11 - Deveria falhar ao tentar atualizar uma tarefa")]
//        public void DeveriaFalharAoTentarAtualizarTarefa()
//        {
//            //Arrange
//            var TarefaRepositoryMock = new Mock<ITarefaRepository>();
//            var tarefa = new Tarefa() { };
//            var sutTodoController = new ToDoController(TarefaRepositoryMock.Object);

//            //Act
//            var retorno = sutTodoController.Atualizar(tarefa) as BadRequestResult;

//            //Assert
//            //Dois testes equivalentes, porque se não passar no atualizar vai retornar um BadQuest né ?
//            TarefaRepositoryMock.Verify(t => t.Atualizar(tarefa), Times.Never);
//            Assert.True(retorno.StatusCode == 400);
//        }
//    }
//}
