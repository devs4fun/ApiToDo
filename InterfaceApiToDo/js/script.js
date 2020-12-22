
var Tarefa, Query, IdUsuario;

function verificarSeEstaLogado() {
    var chave = localStorage.getItem('chave');
    if(chave == null || chave == ""){
        window.location.replace("http://127.0.0.1:5500/login.html");
    }
}

function fazerLogout(){
    
    const url = 'https://localhost:44357/api/usuario/logout';
    const params = 
    {
        method:'POST',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json',
            chave: localStorage.getItem('chave')
        }
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            //console.log(json);
            localStorage.removeItem("chave");
            window.location.href = 'http://127.0.0.1:5500/login.html';
        });
}

function load(){
    verificarSeEstaLogado();
}
/*
function geUsuario (chave)
{
    var id = pegarQueryParam();

    const url = 'https://localhost:44357/api/validarChave/'+id;
    const params = 
    {
        method:'GET',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json'
        }
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            document.getElementById('idtarefa').value = json.id;
            document.getElementById('nometarefa').value = json.nome;
            console.log(json);
        }
    );
}
*/

function cadastrarUsuario()
{
    var nomeCompleto = document.getElementById('nome-completo').value;
    var UserName = document.getElementById('username').value;
    var email = document.getElementById('email').value;
    var senha = document.getElementById('senha').value;

    const url = 'https://localhost:44357/api/usuario/cadastro-usuario';
    const params = 
    {
        method:'POST',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json'
        },
        body:JSON.stringify
        (
            {
                "nomecompleto":nomeCompleto,
                "username":UserName,
                "email":email,
                "senha": senha
            }
        )
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            //console.log(json);
            window.location.href = 'http://127.0.0.1:5500/login.html';
        });
}

function fazerLogin()
{
    var emailOuUserName = document.getElementById('email-ou-username').value;
    var senha = document.getElementById('senha').value;

    const url = 'https://localhost:44357/api/usuario/login';
    const params = 
    {
        method:'POST',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json'
        },
        body:JSON.stringify
        (
            {
                "email":emailOuUserName,
                "username":emailOuUserName,
                "senha": senha
            }
        )
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            setLocalStorage(json.chave);
            //console.log(json);
            window.location.href = 'http://127.0.0.1:5500/';
        });
}

//função para gravar chave no LocalStorage
function setLocalStorage(string){
    localStorage.setItem('chave',string)
}


function pegarNome ()
{
    Tarefa = document.getElementById('nometarefa').value;
    console.log(Tarefa);
}

function pegarIdDoUsuarioQueCriouOTodo ()
{
    IdUsuario = document.getElementById('idusuariobd').value;
    console.log(IdUsuario);
}

function pegarQueryParam()
{
    Query = location.search.slice(1);
    var ChaveValor = Query.split('=');
    return ChaveValor[1]
}

function post ()
{
    pegarNome();
    const url = 'https://localhost:44373/api/todo';
    const params = 
    {
        method:'POST',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json',
            chave: localStorage.getItem('chave')
        },
        body:JSON.stringify
        (
            {
                "Nome":Tarefa
            }
        )
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            //setLocalStorage(json.chave);
            //console.log(json);
            window.location.href = 'http://127.0.0.1:5500/';
        });
}

function get()
{
    const url = 'https://localhost:44373/api/todo';
    const params = 
    {
        method:'GET',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json',
            chave: localStorage.getItem('chave')
        }
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            for(var i = 0; i < json.length; i++)
            {
                //var tarefaid = document.createElement('td');
                //tarefaid.textContent = json[i].id;

                var tarefanome = document.createElement('td');
                tarefanome.textContent = json[i].nome;

                var buttonedt = document.createElement('img');
                buttonedt.setAttribute('src', 'img/editar.png')

                //buttonedt.setAttribute('onClick', "editar("+json[i].id+")")

                var a = document.createElement('a');
                a.appendChild(buttonedt);
                a.href = "editar.html?id="+json[i].id;
                a.setAttribute('class', 'btn-editar-teste');


                var buttondel = document.createElement('img');
                buttondel.setAttribute('src', 'img/excluir.png')
                buttondel.setAttribute('onClick', "deletar("+json[i].id+")");
                buttondel.setAttribute('class', 'btn-excluir-teste');

                var tdAcoes = document.createElement('td');
                tdAcoes.appendChild(a);
                tdAcoes.appendChild(buttondel);
                tdAcoes.setAttribute('class', 'tdAcoes');

                var tr = document.createElement('tr');
                //tr.appendChild(tarefaid);
                tr.appendChild(tarefanome);
                //tr.appendChild(a);
                //tr.appendChild(buttondel);
                tr.appendChild(tdAcoes);

                var div = document.createElement('div');
                div.appendChild(tr);

                var tbody = document.querySelector('tbody');
                //tbody.appendChild(tr);
                tbody.appendChild(div);
            }
            console.log(json);
        }
    );
}

function getUnico ()
{
    var id = pegarQueryParam();

    const url = 'https://localhost:44373/api/todo/'+id;
    const params = 
    {
        method:'GET',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json',
            chave: localStorage.getItem('chave')
        }
    };
    fetch(url, params)
        .then((r)=>r.json())
        .then((json)=>{
            document.getElementById('idtarefa').value = json.id;
            document.getElementById('nometarefa').value = json.nome;
            document.getElementById('idusuariobd').value = json.idUsuario;
            console.log(json);
        }
    );
}


function deletar(id)
{
    const url = 'https://localhost:44373/api/todo/'+id;
    const params = 
    {
        method:'DELETE',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json',
            chave: localStorage.getItem('chave')
        }
    };
    fetch(url, params);
    alert('Deletado');
    window.location.href = 'http://127.0.0.1:5500/';
}

function editar()
{
    pegarNome();

    pegarIdDoUsuarioQueCriouOTodo();

    var id = pegarQueryParam();
    //var Id = ParseInt(id);
    //alert(typeOf(ParseInt(id)));
    const url = 'https://localhost:44373/api/todo';
    const params = 
    {
        method:'PATCH',
        headers:
        {
            Accept:'application/json',
            'Content-Type':'application/json',
            chave: localStorage.getItem('chave')
        },
        body:JSON.stringify
        (
            {
                "Id":parseInt(id),
                "IdUsuario": parseInt(IdUsuario),
                "Nome":Tarefa
            }
        )
    };
    fetch(url, params)
        window.location.href = "http://127.0.0.1:5500/";
}

function MostrarAddTarefa () {
    document.querySelector(".corpo").classList.add('displayShow');
}

function EsconderAddTarefa () {
    document.querySelector(".corpo").classList.remove('displayShow');
}