# Sistema de Hotéis em C# com ASP.NET

Este projeto consiste em uma API para um sistema de hotéis desenvolvido em C# com ASP.NET, conectado a um banco de dados MySQL. A API oferece várias funcionalidades para usuários comuns e administradores, incluindo autenticação, manipulação de hotéis, quartos, cidades e reservas.

## Como Funciona

O sistema de hotéis permite que usuários realizem reservas em quartos de hotéis, consultem informações sobre hotéis, quartos e cidades, além de realizar autenticação para acessar funcionalidades específicas. Os administradores têm acesso a funcionalidades adicionais, como a adição e remoção de quartos, hotéis e cidades.

Além disso, a API está integrada a um serviço externo de geolocalização. Isso permite que os usuários encontrem hotéis próximos com base no endereço fornecido. Ao enviar uma solicitação para a rota `/geo/address`, você pode obter uma lista de hotéis ordenados por proximidade.

## Teste Agora!

Você pode testar o sistema de hotéis acessando o [deploy](https://hrs.up.railway.app/) deste projeto. Experimente!

## Rotas e Funcionalidades

A tabela a seguir detalha as rotas disponíveis na API, juntamente com suas funcionalidades para usuários e administradores:

| Rota          | Método | Funcionalidade para Usuários | Funcionalidade para Administradores |
|---------------|--------|-------------------------------|-------------------------------------|
| `/`           | GET    | Retorna uma mensagem de status "Online" da API. | Retorna uma mensagem de status "Online" da API. |
| `/login`      | POST   | Faz login na API.            | Faz login na API.                  |
| `/user`       | GET    | -                             | Retorna todos os usuários.         |
| `/user`       | POST   | Cria um novo usuário.        | Cria um novo usuário.               |
| `/booking`    | POST   | Registra uma reserva em um quarto. | Registra uma reserva em um quarto. |
| `/city`       | GET    | Retorna todas as cidades.    | Retorna todas as cidades.          |
| `/hotel`      | GET    | Retorna todos os hotéis.     | Retorna todos os hotéis.           |
| `/room/{id}`  | GET    | Retorna informações de um quarto com o ID especificado. | Retorna informações de um quarto com o ID especificado. |
| `/room`       | POST   | -                             | Cria um novo quarto.               |
| `/room/{id}`  | DELETE | -                             | Deleta um quarto pelo ID.          |
| `/hotel`      | POST   | -                             | Cria um novo hotel.                |
| `/city`       | POST   | -                             | Cria uma nova cidade.              |
| `/city/{id}`  | PUT    | -                             | Edita uma cidade existente.        |
| `/geo/status` | GET    | Retorna o status da API externa responsável pela geolocalização. | Retorna o status da API externa responsável pela geolocalização. |
| `/geo/address`| GET    | Retorna hotéis ordenados por distância de um endereço. | Retorna hotéis ordenados por distância de um endereço. |


## Instalação e Uso

1. Clone este repositório.
2. Configure o banco de dados MySQL.
3. Execute o projeto.

## Contribuição

Sinta-se à vontade para contribuir com novas funcionalidades, correções de bugs ou melhorias na documentação. Basta seguir estes passos:

1. Faça um fork do projeto.
2. Crie uma nova branch (`git checkout -b feature/nova-feature`).
3. Faça commit das suas alterações (`git commit -am 'Adiciona nova feature'`).
4. Faça push para a branch (`git push origin feature/nova-feature`).
5. Crie um novo Pull Request.
