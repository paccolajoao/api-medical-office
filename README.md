# 🏥 ApiMedicalOffice

API RESTful desenvolvida em **.NET 9** com **Entity Framework Core** e **MySQL** para gerenciamento de pacientes.  
O projeto foi construído seguindo os princípios da **Clean Architecture**, usando boas práticas, DTOs e validações robustas.

---
## ⚙️ Como rodar o projeto localmente

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)

### Passo a passo

1. **Clone o repositório**
   ```bash
   git clone https://github.com/paccolajoao/api-medical-office.git
   cd seu-repo
2. **Crie o banco de dados**
   ```
   CREATE DATABASE medical_office;
3. **Configure o arquivo appsettings.json e ajuste**
	```
    "ConnectionStrings": {
	  "DefaultConnection": "server=localhost;port=3306;database=medical_office;user=seu_usuario;password=sua_senha"
	}
4. **Aplique as migrations e execute**
	```
    dotnet ef database update
	dotnet run

5. **Documentação Swagger**
    ```
    http://localhost:{porta}/swagger
---

## 🧱 Arquitetura

O projeto utiliza **Clean Architecture**, separando as responsabilidades em diferentes camadas:

- **Domain**: Entidades do negócio (ex: Paciente), com validações e regras principais.
- **Application**: Serviços, DTOs, interfaces e lógica de aplicação.
- **Infrastructure**: Acesso ao banco de dados (MySQL) via Entity Framework Core.
- **Presentation**: Controllers e endpoints expostos na API.

**Vantagem:** fácil manutenção, testes, evolução do sistema e possibilidade de trocar banco/framework sem mexer nas regras de negócio.

---
## 🗃️ Entidade: Paciente

A tabela de pacientes contém os seguintes campos:

| Campo          | Tipo           | Obrigatório | Tamanho/Limite | Descrição                       |
|----------------|----------------|-------------|----------------|---------------------------------|
| Id             | bigint unsigned| Sim         | -              | Identificador único             |
| Nome           | string         | Sim         | 100            | Nome do paciente                |
| NomePai        | string?        | Não         | 100            | Nome do pai                     |
| NomeMae        | string?        | Não         | 100            | Nome da mãe                     |
| DataNascimento | Date           | Sim         | -              | Data de nascimento              |
| Celular        | string         | Sim         | 20             | Celular para contato            |
| Email          | string?        | Não         | 120            | E-mail                          |
| DataCriacao    | DateTime       | Automático  | -              | Data de criação do registro     |
| DataAtualizacao| DateTime?      | Automático  | -              | Última atualização              |

> Os campos obrigatórios são validados tanto na API (DTOs) quanto no banco.

---
## 📦 Funcionalidades atuais

- CRUD de pacientes:
	- POST /api/pacientes
	- GET /api/pacientes?pageNumber={n}&pageSize={m} (listagem paginada)
	- GET /api/pacientes/{id}
	- PUT /api/pacientes/{id}
	- DELETE /api/pacientes/{id}
- Paginação de resultados com parâmetros pageNumber e pageSize
- Validações de campos obrigatórios e tamanho máximo ([Required], [MaxLength])
- Timezone “America/Sao_Paulo” na gravação de datas
- Documentação interativa via Swagger

---
## 💾 Exemplo de requisição JSON para criar paciente

```json
{
  "nome": "João da Silva",
  "nomePai": "Pedro da Silva",
  "nomeMae": "Maria Oliveira",
  "dataNascimento": "1984-06-07",
  "celular": "+55 11 91234-5678",
  "email": "joao.silva@email.com"
}
