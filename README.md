# 📦 Product Manager - Система управления продуктами

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=flat&logo=dotnet)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-9.0-512BD4?style=flat)
![SQLite](https://img.shields.io/badge/SQLite-Database-003B57?style=flat&logo=sqlite)

Современное веб-приложение на ASP.NET Core MVC для управления продуктами с использованием Entity Framework Core и SQLite.

## ✨ Основные возможности

- ✅ **CRUD операции** - Создание, чтение, обновление и удаление продуктов
- 🔍 **Поиск** - Быстрый поиск по названию и описанию продуктов
- 📊 **Сортировка** - Сортировка по названию, цене и дате создания
- 📈 **Статистика** - Автоматический расчет статистики по продуктам
- ⚡ **Асинхронность** - Все операции с БД выполняются асинхронно
- 🎨 **Современный UI** - Адаптивный дизайн с использованием Bootstrap

## 🚀 Технологический стек

- **Framework:** ASP.NET Core 8.0 MVC
- **ORM:** Entity Framework Core 9.0
- **База данных:** SQLite
- **Frontend:** Razor Views, Bootstrap 5
- **Язык:** C# 12 с поддержкой Nullable Reference Types
- **Архитектура:** MVC Pattern

## 📋 Предварительные требования

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE: Visual Studio 2022, Rider или VS Code

## 🛠️ Установка и запуск

### 1. Клонирование репозитория

```bash
git clone https://github.com/Omasy4S/ProductManager.git
cd TestEntityFrameworkProject
```

### 2. Восстановление пакетов

```bash
dotnet restore
```

### 3. Создание базы данных

База данных создается автоматически при первом запуске приложения.

Для использования миграций (рекомендуется):

```bash
# Установка EF Core Tools (если еще не установлены)
dotnet tool install --global dotnet-ef

# Создание миграции
dotnet ef migrations add InitialCreate --project TestEntityFrameworkProject

# Применение миграций
dotnet ef database update --project TestEntityFrameworkProject
```

### 4. Запуск приложения

```bash
cd TestEntityFrameworkProject
dotnet run
```

Приложение будет доступно по адресу: `https://localhost:5001` или `http://localhost:5000`

## 📁 Структура проекта

```
TestEntityFrameworkProject/
├── Controllers/           # Контроллеры MVC
│   └── HomeController.cs
├── Data/                 # Контекст базы данных
│   └── AppDbContext.cs
├── Models/               # Модели данных
│   ├── Product.cs
│   ├── ProductStatsViewModel.cs
│   └── ErrorViewModel.cs
├── Views/                # Razor представления
│   ├── Home/
│   └── Shared/
├── wwwroot/             # Статические файлы
├── appsettings.json     # Конфигурация приложения
└── Program.cs           # Точка входа
```

## 🗄️ Модель данных

### Product (Продукт)

| Поле | Тип | Описание |
|------|-----|----------|
| Id | int | Уникальный идентификатор (PK) |
| Name | string | Название продукта (2-200 символов) |
| Description | string | Описание продукта (5-1000 символов) |
| Price | decimal | Цена (0.01-999999.99) |
| CreatedAt | DateTime | Дата и время создания (UTC) |

### Индексы базы данных

- **IX_Product_Name** - для оптимизации поиска по названию
- **IX_Product_Price** - для оптимизации сортировки по цене
- **IX_Product_CreatedAt** - для оптимизации сортировки по дате

## 📝 Основные команды

### Entity Framework Core

```bash
# Добавить миграцию
dotnet ef migrations add MigrationName

# Применить миграции
dotnet ef database update

# Откатить миграцию
dotnet ef database update PreviousMigrationName

# Удалить последнюю миграцию
dotnet ef migrations remove

# Посмотреть список миграций
dotnet ef migrations list
```

### Сборка и публикация

```bash
# Сборка проекта
dotnet build

# Запуск в режиме разработки
dotnet run

# Публикация Release версии
dotnet publish -c Release -o ./publish
```

## 🎯 Лучшие практики

Проект реализован с использованием современных практик ASP.NET Core:

- ✅ **Async/Await** - Все операции с БД асинхронные
- ✅ **Dependency Injection** - Внедрение зависимостей через конструктор
- ✅ **Repository Pattern** - Использование DbContext как репозиторий
- ✅ **Data Annotations** - Валидация на уровне модели
- ✅ **IQueryable** - Отложенное выполнение запросов для оптимизации
- ✅ **XML Documentation** - Комментарии для всех публичных API
- ✅ **CSRF Protection** - ValidateAntiForgeryToken на всех POST запросах
- ✅ **Proper Error Handling** - Обработка исключений конкурентности

## 🔐 Безопасность

- HTTPS Redirection включен по умолчанию
- HSTS включен для production
- CSRF защита на всех формах
- Валидация входных данных через Data Annotations

## 🌐 API Endpoints

| Метод | Путь | Описание |
|-------|------|----------|
| GET | `/` | Список всех продуктов |
| GET | `/?searchString=query` | Поиск продуктов |
| GET | `/?sort=price_asc` | Сортировка продуктов |
| GET | `/Home/Create` | Форма создания продукта |
| POST | `/Home/Create` | Создание нового продукта |
| GET | `/Home/Edit/{id}` | Форма редактирования |
| POST | `/Home/Edit/{id}` | Обновление продукта |
| POST | `/Home/Delete/{id}` | Удаление продукта |

### Параметры сортировки

- `name_asc` - По названию (A-Z)
- `name_desc` - По названию (Z-A)
- `price_asc` - По цене (возрастание)
- `price_desc` - По цене (убывание)
- `date_asc` - По дате (старые первыми)
- `date_desc` - По дате (новые первыми)

## 📄 Лицензия

Этот проект распространяется под лицензией MIT. См. файл `LICENSE` для деталей.

---

⭐ Если проект был полезен, поставьте звезду!
