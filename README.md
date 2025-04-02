# Система управления "складом" техники

Управление каталогом электронных устройств с расширенными возможностями фильтрации, сортировки и администрирования

## 🚀 Основные возможности

- 📋 Полный CRUD для:
  - Типов продуктов (компьютеры, ноутбуки, планшеты и др.)
  - Характеристик устройств
  - Производителей
- 🔍 Расширенная система фильтрации:
  - Динамические фильтры в зависимости от типа устройства
  - Диапазонные фильтры для числовых характеристик
  - Множественный выбор параметров
- 🔄 Гибкая сортировка по:
  - Цене
  - Гарантийному сроку
  - Производителю
- 📊 Пагинация с возможностью выбора страницы
- 🔐 Система авторизации и ролевой доступ:
  - Администраторы
  - Менеджеры
  - Пользователи
- 📱 Адаптивный интерфейс на Bootstrap

## 🛠 Технологии

- **Backend**: 
  - ASP.NET Core 6
  - Entity Framework Core
  - Identity для аутентификации
- **Frontend**:
  - Razor Pages
  - Bootstrap 5
  - JavaScript
- **База данных**: SQL Server
- **Дополнительно**:
  - AutoMapper
  - Bogus для генерации тестовых данных
  - JSON сериализация

## 🔑 Особенности реализации

- **Ролевая модель доступа:**
  - Администраторы: полный доступ
  - Менеджеры: управление контентом
  - Пользователи: только просмотр
- **Динамические фильтры:**
  - Автоматическая генерация фильтров на основе типа устройства
  - Интеллектуальные диапазоны значений
- **Система валидации:**
  - Кастомные сообщения об ошибках
  - Проверка целостности данных
