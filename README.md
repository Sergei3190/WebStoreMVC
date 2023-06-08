WebStoreMVC

[![tests](https://github.com/Sergei3190/WebStoreMVC/actions/workflows/tests.yml/badge.svg)](https://github.com/Sergei3190/WebStoreMVC/actions/workflows/tests.yml)

Test web store.

Includes:

- administration area
- user account

Allows:

- sign in
- create orders from the cart
- view/create/edit/delete items, employees depending on the role assigned during user registration
- different search bots can retrieve an XML document with endpoints.
- move around the site using breadcrumbs

An SOA pattern was used in the implementation.

The client and backend have Docker support, the web store itself can be run in Docker Compose with a self-certified certificate.

This web store demonstrates the following skills:

- SOA pattern-based development 
- ASP.NET MVC (Razor)
- ASP.NET Web API using REST style
- ORM EF Core (Code First)
- MSSQL
- SQLite
- jQuery
- Ajax
- SignalR (chat alike)
- working with Identity
- Using cookies to store shopping carts,
- token substitution
- recreating system identifier
- customizing site navigation using breadcrumbs
- page dump
- creating TagHelpers and components
- Limiting access rights and hiding elements based on roles granted
- validation on the browser side, server side, hybrid
- Functional, system and unit testing (MsTest, xUnit using Moq)
- sitemap development
- log4net and serilog with SEQ logging
- application deployment in docker and docker compose
- CI/CD

----------------------------------------------------------------------------------------------------------------------------------

Тестовый веб магазин.

Включает в себя:

- область администрирования
- профиль пользователя

Позволяет:

- авторизовываться
- формировать заказы из корзины
- просматривать/создавать/редактировать/удалять товары, сотрудников в зависимости от присовенной при регистрации пользователя роли
- обращаться различным поисковым ботам к карте сайта для получения xml документа c конечными точками
- перемещаться по сайту с помощью хлебных крошек

При реализации использовался паттерн SOA.

Клиент и бэканд имеют поддержку Docker, сам веб магазин можно запустить в Docker Compose с самозаверенным сертификатом.

Данный веб магазин демонстрирует следующие навыки:

- разработка на основании паттерна SOA 
- ASP.NET MVC (Razor)
- ASP.NET Web Api с применением стиля REST
- ORM EF Core (Code First)
- MSSQL
- Sqlite
- jQuery
- ajax
- SignalR (подобие чата)
- работа с системой Identity
- использование куков для хранения корзины товаров,
- подмена токена
- пересоздание идентификатора системы
- настройка навигации сайта с помощью хлебных крошек
- постраничная выгрузка данных
- создание TagHelpers и компонентов
- ограничение прав доступа и скрытие элементов на основании выданных ролей
- валидация на стороне браузера, сервера, гибридная
- функциональное, системное и модульное тестирование (MsTest, xUnit с использованием Moq)
- создание карты сайта
- работа с log4net и serilog с записью логов на сервере SEQ
- развертывание приложения в docker и docker compose
- CI/CD
