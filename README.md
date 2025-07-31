# 🌐 Minimal API com Autenticação JWT

Projeto exemplo de API mínima com autenticação via JWT, Entity Framework Core e testes automatizados usando MSTest.

![.NET Version](https://img.shields.io/badge/.NET-8.0-blueviolet)

![Tests](https://img.shields.io/badge/tests-passing-brightgreen)

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
- SQL Server (Se quiser configurar outro banco, é só realizar os devidos ajustes)
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

Pode abrir direto pelo navegador, o Swagger está disponível direto na raiz. A API estará acessível em: `https://localhost:5230`

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
  "Key": "ajuste-sua-chave-secreta-aqui-como-voce-preferir"
},
"ConnectionStrings": {
  "DefaultConnection": "Server=O_SERVIDOR_QUE_FOR_USAR;Database=SUA_DATABASE;Trusted_Connection=True;"
}
```



## 🤝 Contribuições

Sinta-se à vontade para abrir issues ou enviar pull requests 🚀