# PROJETO KNEWIN


FERRAMENTAS, LINGUAGENS E FRAMEWORKS UTILIZADOS:
- Visual Studio Code
- ASP .NET CORE 3.1
- EF Core 3.1 
- Swagger
- Postman
- WEB API
- AspNetCore.Authentication


RODANDO O PROJETO
- Faça um CLONE ou DOWNLOAD do repositório rudvlima/Teste
- Abra no VSCODE (visual studio code) a pasta /knewinAPI 
- Digite o comando no terminal: dotnet run
- Verifique em qual porta o projeto está rodando no host "Now listening on"

- OBS: Como o banco de dados é criado em memória, ao compilar o banco já é povoado programaticamente. Não sendo necessário rodar scripts para realizar os teste


REALIZANDO OS TESTES:
Pode utilizar o POSTMAN ou o SWAGGER( exemplo ---> https://localhost:5001/swagger)


LOGIN
- Fazer Login com a rota .../api/login
- IMPORTANTE: Realizado o login será criado um TOKEN para autenticar outras rotas, guarde esse TOKEN

- Username: adm
- Password: adm
- JSON 
{
  "username": "admin",
  "password": "admin",
}

<img src="/img/login.gif"></img>


- Autenticação:
Para realizar a autenticação é necessário inserir o token gerado no login após a palavra "Baerer" (Baerer [TOKEN AQUI])

<img src="/img/autorização.gif"></img>

CIDADES

- Cadastrar nova cidade: .../api/Cidade/CadastrarNovaCidade
- JSON: 
{
  "id": 0,
  "nome": "string",
  "habitantes": 0,
  "fronteira": [
    "string"
  ]
}


<img src="/img/novaCidade.gif"></img>

- Cidades cadastradas: .../api/Cidade/RetornarTodasCidades

<img src="/img/todasCidades.gif"></img>

- Busca cidade por ID e por Nome: .../api/Cidade/RetornarCidadePorNome/{nome} E /api/Cidade/RetornarCidadePorId/{id}

<img src="/img/buscaCidadeId.gif"></img>

<img src="/img/buscaCidadeNome.gif"></img>

- Soma de habitantes: .../api/Cidade/RetornarSomaHabitantesPorCidades
- JSON: 
[
  "string", "string"
]

<img src="/img/habitantes.gif"></img>

- Atualizar cidade: .../api/Cidade/AlterarCidade/{id}
- Informar o ID da cidade no parâmetro {id}

<img src="/img/alterarCidade.gif"></img>

- Fronteiras de uma cidade: .../api/Cidade/RetornarFronteirasPorCidade/{nome}
- Informar o nome da cidade no lugar do paramêtro {nome}

<img src="/img/fronteiras.gif"></img>

ALGORITIMOS
Os algoritimos são compilados na mesma API

- Primeiro índice duplicado: .../algoritimos/DuplicadosNaLista
- JSON:

[
 0
]

<img src="/img/duplicados.gif"></img>

- Palíndromo: .../algoritimos/Palindromo/{palavra}

<img src="/img/palindromo.gif"></img>


- Por opção, decidi não fazer o último exercíco que consiste em traçar uma rota entre duas cidades.