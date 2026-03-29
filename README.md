# Beckamo – консольный бекап‑инструмент для PostgreSQL

Beckamo — это **консольное приложение на .NET 8**, которое делает **резервные копии** выбранных таблиц PostgreSQL‑баз данных в виде **SQL‑дампов**.

Опционально бекап‑файлы **шифруются через AES‑256** с паролем.  
Все настройки вынесены в `settings.json` 

---

## 🚀 Быстрый старт

### 1. Установка и запуск

Если у тебя установлен **.NET 8 Runtime**:

```bash
git clone https://github.com/<ваш-логин>/beckamo.git
cd beckamo
dotnet run
```

Если ты хочешь **одно exe‑приложение** (без зависимости от .NET на машине):

```bash
dotnet publish -r win-x64 --self-contained true -o ./publish
./publish/Beckamo.exe
```

---

### 2. Настройка `settings.json`

В корне проекта есть файл `settings.json`.  
Пример:

```json
{
  "SourceConnectionString": "Host=localhost;Port=5432;Database=Beckamo;Username=postgres;Password=123456;",
  "TargetFilePathTemplate": "backups/{db}/{table}_{timestamp}.sql.enc",
  "IncludedDatabases": [ "Beckamo" ],
  "IncludedTables": [ "users" ],
  "UseEncryption": true,
  "EncryptionPassword": "topsecret"
}
```

Поменяй:

- `SourceConnectionString` — на свою БД,  
- `IncludedDatabases` и `IncludedTables` — на нужные тебе,  
- `TargetFilePathTemplate` — путь сохранения бекапов,  
- `UseEncryption` и `EncryptionPassword` — настройки шифрования.



## 🔐 Как работает шифрование

- Используется **AES‑256**.  
- Пароль хешируется через **BCrypt**, из результата и salt‑а генерируется ключ.  
- Бекап сохраняется в файле с расширением `.sql.enc`.  
- Для восстановления нужно **тот же `EncryptionPassword`**.

