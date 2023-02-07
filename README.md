# DevFreela
API REST de um sistema onde clientes possam contratar freelancers para seus projetos

# Tecnologias, arquiteturas e padrões utilizados
* Arquitetura limpa
* ASP.NET Core com .NET 6
* Entity Framework Core
* Padrão CQRS (Command and Query Responsibility Segregation)
* Padrão Repository
* Validação com FluentValidation
* Autenticação e Autorização com JWT (Json Web Token)
* Testes Unitários com xUnit utilizando os padrões Given-When-Then e AAA (Arrange, Act e Assert)
* Microsserviço de pagamento
  * https://github.com/muguii/DevFreela.Microservice.Payments
* Mensageria com RabbitMQ
* Paginação de dados
* Unit of Work

# Funcionalidades do DevFreela
* Cadastro, atualização, remoção e obtenção de projetos
* Cadastro, obtenção e login de usuários
* Cadastro de comentários nos projetos
* Início e conclusão de projetos
