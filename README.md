
# Serviço windows para envio de email automatizado

Aplicação e serviço windows para envio de email automatizado, para Outlook (na aplicação Outlook e gmail) e gmail (no serviço). Utilizado Framework 4.6.1. 

## Pré requisitos
 
Para executar esses sistemas você precisa baixar e instalar os softwares listados.

1. [Visual Studio](https://www.visualstudio.com/pt-br/free-developer-offers/)
2. [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
3. [SQL Server Management Studio](https://docs.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)


## Como baixar o código

git clone https://github.com/JucelioAmaral/ServicoWindowsDeEnvioDeEmail.git

## Como configurar o sistema?

1. Abrir a Visual Studio;
2. Configura o arquivo "App.config" ou o "ServicoDeEnvioDeEmail.exe.config" para as configurações locais inclusive o BD;

## Em caso de erro

1. Erro "Faulting module path: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\clr.dll". Esse erro  pode ser visto no Event viewer.
Verifique a versão do .Net Framework. Tive 	que criar novo projeto, copiar e colar para solucionar pois, não foi encontrada a solução.

