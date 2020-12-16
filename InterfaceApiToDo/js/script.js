var Tarefa, Query;
function pegarNome ()
{
    Tarefa = document.getElementById('nometarefa').value;
    console.log(Tarefa);
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
            'Content-Type':'application/json'
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
            console.log(json);
            window.location.href = 'http://127.0.0.1:5500/';
        });
}

function get ()
{
    const url = 'https://localhost:44373/api/todo';
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
            for(var i = 0; i < json.length; i++)
            {
                var tarefaid = document.createElement('td');
                tarefaid.textContent = json[i].id;
                var tarefanome = document.createElement('td');
                tarefanome.textContent = json[i].nome;

                var buttonedt = document.createElement('button');
                buttonedt.textContent = "Editar";
                //buttonedt.setAttribute('onClick', "editar("+json[i].id+")")

                var a = document.createElement('a');
                a.appendChild(buttonedt);
                a.href = "editar.html?id="+json[i].id;

                var buttondel = document.createElement('button');
                buttondel.textContent = "Deletar";
                buttondel.setAttribute('onClick', "deletar("+json[i].id+")");

                var tr = document.createElement('tr');
                tr.appendChild(tarefaid);
                tr.appendChild(tarefanome);
                tr.appendChild(a);
                tr.appendChild(buttondel);

                var tbody = document.querySelector('tbody');
                tbody.appendChild(tr);
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


function deletar(id)
{
    const url = 'https://localhost:44373/api/todo/'+id;
    const params = 
    {
        method:'DELETE'
    };
    fetch(url, params);
    alert('Deletado');
    window.location.href = 'http://127.0.0.1:5500/';
}

function editar()
{
    pegarNome();
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
            'Content-Type':'application/json'
        },
        body:JSON.stringify
        (
            {
                "Id":parseInt(id),
                "Nome":Tarefa
            }
        )
    };
    fetch(url, params)
        window.location.replace("http://127.0.0.1:5500/");
}