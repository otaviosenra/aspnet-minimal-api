# 🌐 Minimal API com Autenticação JWT

Projeto exemplo de API mínima com autenticação via JWT, Entity Framework Core e testes automatizados usando MSTest.

![.NET Version](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Tests](https://img.shields.io/badge/tests-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue)

---

## 📦 Tecnologias utilizadas

- ASP.NET Core 8 (Minimal API)
- Entity Framework Core
- SQL Server (ou outro banco configurável)
- Autenticação com JWT
- MSTest para testes automatizados
- Swagger para documentação

---

## 🚀 Como rodar este projeto localmente

### ✅ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- (Opcional) SQL Server
- Git instalado

---

### 🔧 Passos para execução

```bash
# 1. Clone o repositório
git clone https://github.com/otaviosenra/aspnet-minimal-api.git
cd aspnet-minimal-api

# 2. Restaure os pacotes
dotnet restore

# 3. Compile a solution
dotnet build

# 4. Rode a API (ajuste o nome da pasta se necessário)
dotnet run --project API
```

A API estará acessível em: `https://localhost:5001` ou `http://localhost:5000`

---

## 🧪 Rodando os testes

```bash
# Rode todos os testes da solution
dotnet test
```

---

## ⚙️ Configuração do appsettings.json

No projeto da API, edite o arquivo `appsettings.json` com suas configurações:

```json
"Jwt": {
  "Key": "sua-chave-secreta-aqui"
},
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SuaDb;Trusted_Connection=True;"
}
```

---

## 📄 Endpoints

Acesse a documentação interativa via Swagger:

```
https://localhost:5001/swagger
```

---

## 📁 Estrutura do Projeto

```
├── API/                 # Projeto principal da API
│   ├── Controllers/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
│
├── Test/                # Projeto de testes
│   └── LoginRequestTest.cs
│
├── aspnet-minimal-api.sln
```

---

## 🤝 Contribuições

Sinta-se à vontade para abrir issues ou enviar pull requests 🚀