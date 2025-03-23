# HairCutApp (API)
Esse projeto foi gerado visando o gerenciamento de agendamento de cortes de cabelo

## Pré-requisitos
Antes de rodar a aplicação, você precisará de algumas ferramentas instaladas:

- Visual Studio 2022 (recomendado)
- .NET 6 SDK
- SQL Server ou uma instância compatível de banco de dados (a aplicação usa SQL Server com a string de conexão definida abaixo)

## Passo a passo
### Clonar repositório
Primeiro, faça o clone do repositório para a sua máquina local. Abra o terminal ou o Git Bash e execute:

`git clone https://github.com/Glrodrigo/HairCutApp.git`

### Configuração do Banco de Dados
A aplicação usa o SQL Server para armazenar os dados. No arquivo `appsettings.json` e `appsettings.Development.json` há uma string de conexão com o banco de dados que precisa ser configurada.

Por padrão, a string está assim:

`"ConnectionStrings": {
  "DefaultConnection": "Server=xxxxx\\SQLEXPRESS;Database=HairCutApp;User Id=xxxxx;Password=xxxxx;TrustServerCertificate=True;"
}`

Você precisará alterar a string de conexão para o seu ambiente local. Por exemplo:

`"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-FGE9EJC\\SQLEXPRESS;Database=HairCutApp;User Id=user;Password=change_25;TrustServerCertificate=True;"
}`

- `Server`: Se você estiver utilizando o SQL Server Express localmente, será o nome da sua máquina seguido de `\\SQLEXPRESS`. Caso contrário, substitua pelo seu servidor.
- `Database`: Aplique o nome do banco de dados que você está utilizando.
- `User Id` e `Password`: Se a autenticação for do tipo SQL Server, defina os dados correspondentes.

### Restaurar Pacotes NuGet
Depois de clonar o repositório, abra o arquivo da solução HairCutApp.sln no Visual Studio.

Na barra de menu do Visual Studio, vá para Ferramentas > Gerenciador de Pacotes NuGet > Restaurar Pacotes para garantir que todas as dependências sejam baixadas.

### Executar as Migrações do Banco de Dados
Caso o banco de dados ainda não exista ou você precise aplicar as migrações, siga os passos:

- Abra o Console do Gerenciador de Pacotes no Visual Studio (`Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes`)
- Execute o comando para aplicar as migrações ao banco de dados: `Update-Database`

### Definindo o Projeto de Inicialização
- Selecione o projeto `HairCutApp` como o projeto de inicialização.
- Escolha a opção "ISS Express" como executor e, se solicitado, aceite o certificado para rodar a aplicação localmente.

### Rodar a Aplicação
Para rodar a aplicação, clique em Iniciar ou pressione F5. O Visual Studio irá construir e iniciar o servidor local.

### Rodar os Testes
- No Visual Studio, localize o projeto de testes `HairCutApp.Tests` no "Solution Explorer".
- Clique com o botão direito sobre o projeto de testes e selecione Executar Testes para rodar todos os testes.

## Aviso
A aplicação depende da string de conexão correta para funcionar corretamente. Sem uma configuração válida para o banco de dados, a aplicação não conseguirá se conectar ao SQL Server e não executará operações de leitura e gravação de dados.

### Nota: Se você estiver utilizando uma instância diferente do SQL Server, altere a string de conexão conforme necessário.

### Link: [https://haircutapp-frb3grgtfkg8cdhj.centralus-01.azurewebsites.net/swagger/index.html].
